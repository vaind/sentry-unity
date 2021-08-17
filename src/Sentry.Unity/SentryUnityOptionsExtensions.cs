using Sentry.Extensibility;
using Sentry.Unity.Integrations;

namespace Sentry.Unity
{
    public static class SentryUnityOptionsExtensions
    {
        public static bool ShouldInitializeSdk(this SentryUnityOptions? options) => ShouldInitializeSdk(options, null);

        internal static bool ShouldInitializeSdk(this SentryUnityOptions? options, IApplication? application = null)
        {
            if (options is null)
            {
                new UnityLogger(new SentryOptions()).LogWarning(
                    "Sentry has not been configured. You can do that through the editor: Tools -> Sentry");
                return false;
            }

            if (!options.Enabled)
            {
                return false;
            }

            application ??= ApplicationAdapter.Instance;
            if (!options.CaptureInEditor && application.IsEditor)
            {
                options.DiagnosticLogger?.LogInfo("Disabled while in the Editor.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(options.Dsn))
            {
                options.DiagnosticLogger?.LogWarning("No Sentry DSN configured. Sentry will be disabled.");
                return false;
            }

            return true;
        }
    }
}