using System;
using System.IO;
using System.Linq;
using WandEnhancer.Core.Patching.Strategies;
using WandEnhancer.Core.Patching.Strategies.Static;

using WandEnhancer.Core.Services;
namespace WandEnhancer.Core.Patching.Shared
{
    internal static class AuxiliaryCompatibility
    {
        public static void Apply(PatchContext context)
        {
            string directory = string.IsNullOrEmpty(context.UnpackedPath) ? null
                : Path.Combine(context.UnpackedPath, @"static\unpacked\auxiliary");
            string auxiliary = directory != null && Directory.Exists(directory)
                ? Directory.EnumerateFiles(directory, "*AuxiliaryService.exe").SingleOrDefault() : null;
            if (auxiliary == null)
            {
                context.Log?.Invoke(LocalizationManager.Get("aux_not_found"), ELogType.Info);
                return;
            }
            if (AuxTrustNeutralizer.Neutralize(auxiliary, context.Log) < 0)
                throw new InvalidDataException(LocalizationManager.Get("aux_unsupported"));
        }
    }
}
