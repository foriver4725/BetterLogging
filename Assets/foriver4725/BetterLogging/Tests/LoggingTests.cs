using UnityEngine;

namespace foriver4725.BetterLogging.Tests
{
    internal static class LoggingTests
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Init()
        {
            "Hello, World!".Print();
            "This is a warning message.".Print(LogSettings.Warning);
            "This is an error message.".Print(LogSettings.Error);
            "This is a custom colored message.".Print(
                LogSettings.Level_Warning | LogSettings.Color_Hue_Cyan | LogSettings.Color_Tone_Light);
            "This is a custom colored message with override color.".Print(LogSettings.Level_Normal, Color.magenta);
        }
    }
}
