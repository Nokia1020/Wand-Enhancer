using System.Collections.Generic;



namespace WandEnhancer.Core.Services

{

    /// <summary>

    /// Source-language (English) copies of every localized string, generated from

    /// Locale/lang.en-US.xaml. Used when the WPF resource dictionaries are not loaded,

    /// e.g. in the headless launcher and update-watcher processes, so diagnostic logs

    /// never contain raw resource keys. Keep in sync with the locale files when adding

    /// or renaming keys.

    /// </summary>

    internal static class LocalizationFallback

    {

        public static readonly IReadOnlyDictionary<string, string> English =

            new Dictionary<string, string>

            {

                { "language_display_name", "English" },

                { "mw_folder_path", "Folder path" },

                { "mw_folder_not_found", "Folder not found" },

                { "mw_patch", "Enhance" },

                { "mw_restore", "Restore" },

                { "mw_footer_note", "Original author: k1tbyte. Chinese build released under the Apache License." },

                { "mw_copy_logs", "Copy logs to clipboard" },

                { "mw_export_logs", "Export logs to file" },

                { "mw_update_badge", "New version available" },

                { "settings_title", "Settings" },

                { "settings_language", "Language" },

                { "settings_check_updates", "Check for updates on startup" },

                { "settings_check_prereleases", "Also notify about pre-releases" },

                { "settings_save", "Save" },

                { "up_popup_title", "Update available" },

                { "up_warning", "WandEnhancer never updates itself. Build the new version in your fork, or open the release page." },

                { "up_current_version", "Current:" },

                { "up_latest_version", "Latest:" },

                { "up_release_notes", "Release notes" },

                { "up_show_more", "Show full changelog" },

                { "up_show_less", "Show release notes" },

                { "up_loading_changelog", "Loading..." },

                { "up_changelog_failed", "Could not load the full changelog." },

                { "up_release_notes_unavailable", "No release notes were published." },

                { "up_open_release", "Open release page" },

                { "pv_activate_pro", "Activate WeMod Pro" },

                { "pv_devtools", "DevTools on F12" },

                { "pv_disable_updates", "Disable updates" },

                { "pv_remote_web_panel_preview", "Enable remote access (beta)" },

                { "pv_custom_scripts", "Custom scripts" },

                { "pv_add_js_scripts", "Add .js" },

                { "pv_custom_scripts_hint", "Selected .js files are packed into Wand and loaded in the renderer." },

                { "pv_no_custom_scripts", "No scripts selected" },

                { "pv_start", "Start" },

                { "pv_auto_apply", "Auto-apply after updates" },

                { "pv_popup_title", "What are we gonna enhance?" },

                { "pv_patch_method", "Patch method" },

                { "pv_strategy_static", "Static (recommended)" },

                { "pv_strategy_supervised", "Supervised (runtime)" },

                { "pv_strategy_hint", "Static edits Wand on disk, which breaks its digital signature. Supervised patches in memory at every launch - it can trip antivirus and may not work in every setup." },

                { "log_type_info", "INFO" },

                { "log_type_warn", "WARN" },

                { "log_type_error", "ERROR" },

                { "log_type_success", "SUCCESS" },

                { "log_install_found", "WeMod(Wand) install directory: {0} ({1})" },

                { "log_already_patched", "WeMod(Wand) is already patched. To patch again, restore the original backup first." },

                { "log_ready", "Ready to patch." },

                { "log_install_not_found", "WeMod(Wand) install directory not found." },

                { "log_no_directory", "Cannot proceed. Please specify the directory first." },

                { "log_invalid_directory", "The selected folder {0} is not a valid WeMod(Wand) directory." },

                { "log_restore_failed", "Failed to restore the backup: {0}" },

                { "log_patch_failed", "Failed to patch: {0}" },

                { "log_copied", "Logs copied to clipboard." },

                { "log_copy_failed", "Failed to copy logs: {0}" },

                { "log_exported", "Logs exported to {0}." },

                { "log_export_failed", "Failed to export logs: {0}" },

                { "dialog_pick_install", "Select the WeMod(Wand) directory" },

                { "enh_backup_creating", "Creating backup..." },

                { "enh_backup_restoring_before_patch", "Backup found, restoring the pristine app.asar before patching..." },

                { "enh_unpacked_backup_creating", "Creating a backup of app.asar.unpacked..." },

                { "enh_unpacked_restoring_before_patch", "Restoring the pristine app.asar.unpacked before patching..." },

                { "enh_unpacked_missing", "app.asar.unpacked is missing and no backup exists. Restore the original WeMod(Wand) installation files or reinstall WeMod(Wand), then patch again." },

                { "enh_app_asar_missing", "app.asar not found" },

                { "enh_patch_done", "Done!" },

                { "enh_extracting_asar", "Extracting app.asar..." },

                { "enh_unpack_failed", "Failed to unpack app.asar: {0}" },

                { "enh_pack_failed", "Failed to pack app.asar: {0}" },

                { "enh_patch_failed_restored", "Patch failed - the original WeMod(Wand) files were restored." },

                { "enh_patch_failed_rollback_incomplete", "Patch failed and the rollback did not finish: {0}. Use Restore before launching WeMod(Wand)." },

                { "enh_backup_incomplete", "Backup is incomplete. Restore the original WeMod(Wand) installation files or reinstall WeMod(Wand)." },

                { "enh_backup_restored", "Backup restored successfully." },

                { "aux_not_managed", "{0} is not a managed image; leaving it alone." },

                { "aux_no_caller", "No {0} caller found in {1}; the auxiliary service may have changed." },

                { "aux_compat_applied", "Auxiliary compatibility applied ({0} signature, {1} ASAR, {2} fuse check(s), {3} changed)." },

                { "strat_read_failed", "Could not read {0}: {1}" },

                { "strat_start_failed", "Could not start WeMod(Wand): {0}" },

                { "strat_start_elevation", "Could not start WeMod(Wand) ({0}). WeMod(Wand) is marked \"Run as administrator\", which stops it from being started this way. Clear that box in the properties of Wand.exe and start it again." },

                { "strat_started_pid", "Started {0} as pid {1}." },

                { "strat_no_fuse_block", "No Electron fuse block in {0}. A patched WeMod(Wand) will exit with {1}; an unpatched one is unaffected." },

                { "strat_watch_failed", "Could not watch WeMod(Wand) for new processes ({0}). WeMod(Wand) will run, but the in-game overlay will not." },

                { "strat_exit_unreadable", "WeMod(Wand) exited, and its exit code could not be read." },

                { "strat_exited", "WeMod(Wand) exited with code {0}." },

                { "strat_quit_immediately", "WeMod(Wand) quit right after starting: another WeMod(Wand) was already running and took this launch over. Nothing is clearing the fuse in that instance, which is why its window stays black. End every WeMod(Wand) task in Task Manager, then start WeMod(Wand) again." },

                { "strat_fuse_cleared_all", "WeMod(Wand) closed: fuse cleared in {0} processes." },

                { "strat_fuse_cleared_missed", "WeMod(Wand) closed: fuse cleared in {0} processes, {1} missed." },

                { "strat_fuse_timeout", "Fuse not cleared in pid {0} after {1} ms: {2}. It may exit with {3}." },

                { "strat_pid_cleared", "pid {0} started - fuse cleared." },

                { "strat_fuse_missed_on_exit", "Fuse not cleared in pid {0} before it exited: {1}." },

                { "strat_pid_exited", "pid {0} exited with code {1}." },

                { "strat_fuse_clear_failed", "Could not clear the ASAR integrity fuse in {0}." },

                { "asar_no_bundle", "No app bundle found" },

                { "asar_capability_missing", "[{0}] capability not present, skipping" },

                { "asar_apply_failed", "Failed to apply patches: {0}. The version may not be supported." },

                { "js_patch_failed", "[{0}] {1}. The version may not be supported." },

                { "js_target_found", "[{0}] found the target in: {1}" },

                { "js_patch_applied", "[{0}] patch applied" },

                { "rpanel_bridge_missing", "Remote bridge artifact is missing. Run `cd web-panel &amp;&amp; pnpm run build` before patching." },

                { "rpanel_scripts_missing", "Remote renderer script artifacts are missing. Run `cd web-panel &amp;&amp; pnpm run build` before patching." },

                { "rpanel_injected", "Injected remote panel assets and renderer scripts into app.asar (default: {0}, selected: {1}, local: {2})" },

                { "ws_artifact_missing", "Required workspace artifact not found: {0}" },

                { "ws_resource_missing", "Embedded resource not found: {0}" },

                { "launch_icon_copy_failed", "Could not copy the WeMod(Wand) icon to the launcher; using the default." },

                { "launch_deployed", "Launcher deployed to the root directory" },

                { "launch_squirrel_root_unknown", "Cannot determine the Squirrel root directory" },

                { "watch_already_owned", "Another watcher already owns this install; exiting." },

                { "watch_watching", "Watching {0} for new WeMod(Wand) versions." },

                { "watch_disabled", "Auto-apply is no longer configured; exiting." },

                { "watch_stopped", "Stop requested; exiting." },

                { "watch_new_folder", "New version folder detected: {0}" },

                { "watch_settle_timeout", "Gave up waiting for {0} to finish installing." },

                { "watch_disabled_while_waiting", "Auto-apply was turned off while waiting; skipping." },

                { "watch_not_latest", "{0} is not (or no longer) the latest install; skipping." },

                { "watch_already_patched", "{0} is already patched; nothing to do." },

                { "watch_reapplying", "Re-applying patches to {0}..." },

                { "watch_succeeded", "Auto-patch after update succeeded." },

                { "watch_failed", "Auto-patch after update failed: {0}" },

                { "prog_no_install_under", "No WeMod(Wand) install found under {0}; opening the UI instead." },

                { "prog_auto_patch_failed", "Auto-patch failed: {0}" },

                { "prog_auto_patch_hint", "The new WeMod(Wand) version may need updated patches. Restore the backup and patch again." },

                { "prog_background_failed", "Background task failed: {0}" },

                { "prog_unknown_error", "Unknown error" },

                { "up_changelog_load_failed", "Could not load the full changelog: {0}" },

                { "cfg_remote_setvalue_call", "Remote setValue call" },

                { "cfg_trainer_info_param", "Trainer info parameter" },

                { "cfg_client_state_missing", "client-state payload object could not be located" },

                { "cfg_remote_listener_missing", "Remote value listener could not be located" },

                { "cfg_remote_handler_missing", "Remote value handler could not be located" },

                { "cfg_subscription_missing", "Trainer value subscription could not be located" },

                { "cfg_callback_missing", "Trainer value callback could not be located" },

                { "cfg_locate_failed", "{0} could not be located" },

                { "cfg_ambiguous", "{0} matched more than once; cannot tell which call site is the right one" },

                { "jsf_resolve_failed", "Could not resolve '{0}' inside {1}()" },

                { "jsf_pattern_missing", "Pattern '{0}' not found inside {1}()" },

                { "jsf_no_return", "No top-level return statement in {0}()" },

                { "fuse_no_block", "No Electron fuse block in {0}; nothing to flip." },

                { "fuse_cleared", "ASAR integrity fuse cleared on disk in {0}." },

                { "fuse_restore_missing", "Could not locate the fuse to restore in {0}; keeping {1}." },

                { "fuse_restored", "ASAR integrity fuse restored on disk in {0}." },

                { "fuse_problem_memory_unreadable", "its memory could not be read ({0})" },

                { "fuse_problem_block_mismatch", "the fuse block is not where the file on disk said it would be" },

                { "fuse_problem_page_not_writable", "the page could not be made writable ({0})" },

                { "fuse_problem_write_refused", "the write was refused ({0})" },

                { "fuse_problem_open_failed", "it could not be opened ({0})" },

                { "aux_not_found", "No auxiliary service found; skipping compatibility checks." },

                { "aux_unsupported", "Unsupported auxiliary service; it would reject the patched WeMod(Wand)." },

                { "aux_asar_signature_unknown", "Unrecognised auxiliary ASAR validation signature." },

                { "aux_asar_anchor_overlap", "Auxiliary ASAR anchors overlap the return stub." },

                { "aux_asar_ambiguous", "Ambiguous auxiliary ASAR validation methods." },

                { "aux_asar_unrecognised", "Auxiliary ASAR validation detected but not recognised." },

                { "aux_fuse_reader_ambiguous", "Ambiguous auxiliary Electron fuse readers." },

                { "aux_fuse_signature_unknown", "Unrecognised auxiliary fuse validation signature." },

                { "aux_fuse_reader_overlap", "Auxiliary fuse reader call overlaps the return stub." },

                { "aux_fuse_unrecognised", "Auxiliary fuse validation detected but not recognised." },

                { "watch_setup_failed", "Could not set up the background update watcher: {0}" },

                { "watch_already_running", "Update watcher is already running." },

                { "watch_installed", "Update watcher installed and running in the background." },

            };

    }

}