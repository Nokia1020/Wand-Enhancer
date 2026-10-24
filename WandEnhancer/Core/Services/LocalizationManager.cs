using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

namespace WandEnhancer.Core.Services
{
    public static class LocalizationManager
    {
        public static readonly List<CultureInfo> SupportedLanguages = new List<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("zh-CN"),
            new CultureInfo("de-DE"),
            new CultureInfo("fr-FR"),
            new CultureInfo("es-ES"),
            new CultureInfo("it-IT"),
            new CultureInfo("pt-BR"),
            new CultureInfo("pl-PL"),
            new CultureInfo("ru-RU"),
            new CultureInfo("uk-UA"),
            new CultureInfo("ja-JP"),
            new CultureInfo("tr-TR"),
            new CultureInfo("bn-BD")
        };

        private static readonly object SyncRoot = new object();

        private static CultureInfo _currentLanguage;
        private static ResourceDictionary _englishBaseDictionary;
        private static ResourceDictionary _activeLocaleDictionary;
        private static Dictionary<string, string> _activeStrings = new Dictionary<string, string>();
        private static bool _loaded;

        /// <summary>
        /// Loads the locale dictionaries on first use. Startup log lines are emitted before
        /// the Application finishes starting, so resolving a string has to trigger the load.
        /// </summary>
        private static void EnsureLoaded()
        {
            if (_loaded || Application.Current == null)
            {
                return;
            }

            _loaded = true;
            Initialize();
        }

        /// <summary>
        /// Localized string for <paramref name="key"/>, falling back to the key itself.
        /// </summary>
        public static string Get(string key)
        {
            EnsureLoaded();

            // Resolved from a plain snapshot rather than the WPF dictionaries: callers log
            // from background threads, and those dictionaries belong to the UI thread.
            lock (SyncRoot)
            {
                string localized;
                if (_activeStrings.TryGetValue(key, out localized))
                {
                    return localized;
                }
            }

            // No Application exists in the headless launcher/watcher processes, so fall back
            // to the compiled-in source text instead of leaking a resource key into a log.
            string sourceText;
            if (LocalizationFallback.English.TryGetValue(key, out sourceText))
            {
                return sourceText;
            }

            var application = Application.Current;
            if (application != null && application.Dispatcher != null &&
                application.Dispatcher.CheckAccess())
            {
                return application.TryFindResource(key) as string ?? key;
            }

            return key;
        }

        /// <summary>Localized format string filled with <paramref name="args"/>.</summary>
        public static string Format(string key, params object[] args)
        {
            return string.Format(Get(key), args);
        }

        public static CultureInfo CurrentLanguage
        {
            get => _currentLanguage;
            set => SetLanguage(value);
        }

        public static void Initialize()
        {
            _loaded = true;

            // Load English as the base fallback dictionary
            _englishBaseDictionary = new ResourceDictionary
            {
                Source = new Uri("Locale/lang.en-US.xaml", UriKind.Relative)
            };

            // Try to load saved language preference
            var savedLanguage = SettingsManager.LoadSettings()?.Language;
            CultureInfo targetCulture = null;

            if (!string.IsNullOrEmpty(savedLanguage))
            {
                targetCulture = SupportedLanguages.FirstOrDefault(c => c.Name == savedLanguage);
            }

            if (targetCulture == null)
            {
                // Fall back to system culture detection
                var systemCulture = Thread.CurrentThread.CurrentUICulture;
                targetCulture = SupportedLanguages.FirstOrDefault(c => 
                    c.Name == systemCulture.Name || 
                    c.TwoLetterISOLanguageName == systemCulture.TwoLetterISOLanguageName);
            }
            
            // Force a re-apply: the locale dictionary is rebuilt from scratch here, and the
            // language may be unchanged since a previous call.
            _currentLanguage = null;
            SetLanguage(targetCulture ?? SupportedLanguages[0], saveSettings: false);
        }

        private static void SetLanguage(CultureInfo culture, bool saveSettings = true)
        {
            if (culture == null)
                throw new ArgumentNullException(nameof(culture));

            if (Equals(culture, _currentLanguage))
                return;

            var supportedCulture = SupportedLanguages.FirstOrDefault(c => c.Name == culture.Name);
            if (supportedCulture == null)
            {
                supportedCulture = SupportedLanguages[0]; // Default to English
            }

            _currentLanguage = supportedCulture;
            Thread.CurrentThread.CurrentUICulture = supportedCulture;

            // Create the locale dictionary with English as base for fallback
            var localeDict = new ResourceDictionary();
            
            // First, add English base dictionary for fallback
            if (_englishBaseDictionary != null && supportedCulture.Name != SupportedLanguages[0].Name)
            {
                foreach (var key in _englishBaseDictionary.Keys)
                {
                    localeDict[key] = _englishBaseDictionary[key];
                }
            }

            // Then overlay with the selected language (will override English keys)
            var targetDict = new ResourceDictionary
            {
                Source = new Uri($"Locale/lang.{supportedCulture.Name}.xaml", UriKind.Relative)
            };
            
            foreach (DictionaryEntry entry in targetDict)
            {
                localeDict[entry.Key] = targetDict[entry.Key];
            }

            // Keep a plain snapshot so background threads can resolve strings without
            // touching the UI-thread-owned resource dictionaries.
            var snapshot = new Dictionary<string, string>();
            foreach (DictionaryEntry entry in localeDict)
            {
                var entryKey = entry.Key as string;
                var entryValue = entry.Value as string;
                if (entryKey != null && entryValue != null)
                {
                    snapshot[entryKey] = entryValue;
                }
            }

            lock (SyncRoot)
            {
                _activeStrings = snapshot;
            }

            // Track injected dictionary to replace it on switch instead of appending.
            // Application.Current is null in the headless launcher/watcher processes.
            var application = Application.Current;
            if (application != null)
            {
                var merged = application.Resources.MergedDictionaries;
                if (_activeLocaleDictionary != null && merged.Contains(_activeLocaleDictionary))
                {
                    merged[merged.IndexOf(_activeLocaleDictionary)] = localeDict;
                }
                else
                {
                    merged.Add(localeDict);
                }

                _activeLocaleDictionary = localeDict;
            }
            
            if (saveSettings)
            {
                // Merge into the loaded settings so other properties survive the save.
                var settings = SettingsManager.LoadSettings() ?? new AppSettings();
                settings.Language = supportedCulture.Name;
                SettingsManager.SaveSettings(settings);
            }
        }

        public static string GetLanguageDisplayName(CultureInfo culture)
        {
            try
            {
                var dict = new ResourceDictionary
                {
                    Source = new Uri($"Locale/lang.{culture.Name}.xaml", UriKind.Relative)
                };
                
                if (dict.Contains("language_display_name"))
                {
                    return dict["language_display_name"] as string ?? culture.NativeName;
                }
            }
            catch
            {
                // Fallback to native name if loading fails
            }
            
            return culture.NativeName;
        }
    }
}
