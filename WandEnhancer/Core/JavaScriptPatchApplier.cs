using System;
using System.IO;
using System.Linq;
using WandEnhancer.Core.Js;
using WandEnhancer.Core.Services;
using WandEnhancer.Models;

namespace WandEnhancer.Core
{
    internal sealed class JavaScriptPatchApplier
    {
        private readonly Action<string, ELogType> _logger;

        public JavaScriptPatchApplier(Action<string, ELogType> logger)
        {
            _logger = logger;
        }

        public string Apply(string fileName, string source, EnhancerConfig.PatchEntry patch, EPatchType patchType, out bool patchApplied)
        {
            patchApplied = false;
            if (patch.Applied || !CanSearchFile(fileName, patch))
            {
                return source;
            }

            patch.CapabilityDetected |= ContainsAny(source, patch.CapabilityHints);
            if (!ContainsAny(source, patch.SearchHints))
            {
                return source;
            }

            string label = FormatLabel(patchType, patch);
            JsEdit[] edits;
            try
            {
                edits = patch.Locate(new JsCursor(source));
            }
            catch (Exception e)
            {
                throw new Exception(LocalizationManager.Format("js_patch_failed", label, e.Message), e);
            }

            if (edits == null || edits.Length == 0)
            {
                return source;
            }

            _logger(LocalizationManager.Format("js_target_found", label, Path.GetFileName(fileName)),
                ELogType.Info);
            foreach (var edit in edits.OrderByDescending(edit => edit.Start))
            {
                source = edit.ApplyTo(source);
            }

            _logger(LocalizationManager.Format("js_patch_applied", label), ELogType.Success);
            patch.Applied = true;
            patchApplied = true;
            return source;
        }

        public static string FormatLabel(EPatchType patchType, EnhancerConfig.PatchEntry patch)
        {
            return string.IsNullOrEmpty(patch.Name) ? patchType.ToString() : $"{patchType} -> {patch.Name}";
        }

        public static bool CanSearchFile(string filePath, EnhancerConfig.PatchEntry patch)
        {
            if (patch.CandidateFileNames == null || patch.CandidateFileNames.Length == 0)
            {
                return true;
            }

            string fileName = Path.GetFileName(filePath);
            return patch.CandidateFileNames.Any(candidate => fileName.Equals(candidate, StringComparison.OrdinalIgnoreCase));
        }

        private static bool ContainsAny(string source, string[] hints)
        {
            return hints != null && hints.Any(hint => source.IndexOf(hint, StringComparison.Ordinal) >= 0);
        }
    }
}
