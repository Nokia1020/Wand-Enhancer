using System;
using System.Diagnostics;
using System.IO;
using WandEnhancer.Core.Patching.Shared;
using WandEnhancer.Core.Services;

namespace WandEnhancer.Core.Patching.Strategies.Static
{
    internal sealed class StaticStrategy : IPatchStrategy
    {
        public bool RequiresLauncherAlways => false;

        public void ApplyEnablement(PatchContext context)
        {
            if (!DiskFusePatch.Apply(context.Install.ExecutablePath, context.Log))
            {
                throw new Exception(LocalizationManager.Format("strat_fuse_clear_failed",
                    Path.GetFileName(context.Install.ExecutablePath)));
            }

            AuxiliaryCompatibility.Apply(context);
        }

        public bool Launch(PatchContext context, string args)
        {
            try
            {
                // dispose handle; process keeps running
                Process.Start(new ProcessStartInfo(context.Install.ExecutablePath, args ?? string.Empty)
                {
                    UseShellExecute = false,
                    WorkingDirectory = Path.GetDirectoryName(context.Install.ExecutablePath)
                })?.Dispose();
                return true;
            }
            catch (Exception e)
            {
                context.Log?.Invoke(LocalizationManager.Format("strat_start_failed", e.Message),
                    ELogType.Error);
                return false;
            }
        }

    }
}
