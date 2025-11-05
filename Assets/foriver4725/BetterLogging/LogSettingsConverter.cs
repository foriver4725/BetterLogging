using System;
using UnityEngine;

namespace foriver4725.BetterLogging
{
    internal static class LogSettingsConverter
    {
        internal static Action<object> GetAction(this LogSettings settings) => settings switch
        {
            _ when settings.HasFlag(LogSettings.Level_Normal)  => UnityEngine.Debug.Log,
            _ when settings.HasFlag(LogSettings.Level_Warning) => UnityEngine.Debug.LogWarning,
            _ when settings.HasFlag(LogSettings.Level_Error)   => UnityEngine.Debug.LogError,
            _                                                  => LogSettings.Level_Default.GetAction(),
        };

        internal static string GetLevelName(this LogSettings settings) => settings switch
        {
            _ when settings.HasFlag(LogSettings.Level_Normal)  => "Log Normal",
            _ when settings.HasFlag(LogSettings.Level_Warning) => "Log Warning",
            _ when settings.HasFlag(LogSettings.Level_Error)   => "Log Error",
            _                                                  => LogSettings.Level_Default.GetLevelName(),
        };

        internal static Color GetColor(this LogSettings settings)
        {
            float h = settings.GetColorH();
            (float s, float v) = settings.GetColorSV();
            return Color.HSVToRGB(h, s, v);
        }

        private static float GetColorH(this LogSettings settings) => settings switch
        {
            _ when settings.HasFlag(LogSettings.Color_Black)       => 0.000f,
            _ when settings.HasFlag(LogSettings.Color_DarkGray)    => 0.000f,
            _ when settings.HasFlag(LogSettings.Color_LightGray)   => 0.000f,
            _ when settings.HasFlag(LogSettings.Color_White)       => 0.000f,
            _ when settings.HasFlag(LogSettings.Color_Hue_Red)     => 0.000f,
            _ when settings.HasFlag(LogSettings.Color_Hue_Yellow)  => 0.166f,
            _ when settings.HasFlag(LogSettings.Color_Hue_Green)   => 0.333f,
            _ when settings.HasFlag(LogSettings.Color_Hue_Cyan)    => 0.500f,
            _ when settings.HasFlag(LogSettings.Color_Hue_Blue)    => 0.666f,
            _ when settings.HasFlag(LogSettings.Color_Hue_Magenta) => 0.833f,
            _                                                      => LogSettings.Color_Hue_Default.GetColorH(),
        };

        private static (float s, float v) GetColorSV(this LogSettings settings) => settings switch
        {
            _ when settings.HasFlag(LogSettings.Color_Black)          => (0.000f, 0.000f),
            _ when settings.HasFlag(LogSettings.Color_DarkGray)       => (0.000f, 0.333f),
            _ when settings.HasFlag(LogSettings.Color_LightGray)      => (0.000f, 0.666f),
            _ when settings.HasFlag(LogSettings.Color_White)          => (0.000f, 1.000f),
            _ when settings.HasFlag(LogSettings.Color_Tone_Vivid)     => (1.000f, 1.000f),
            _ when settings.HasFlag(LogSettings.Color_Tone_Light)     => (0.666f, 1.000f),
            _ when settings.HasFlag(LogSettings.Color_Tone_VeryLight) => (0.333f, 1.000f),
            _ when settings.HasFlag(LogSettings.Color_Tone_Dark)      => (1.000f, 0.666f),
            _ when settings.HasFlag(LogSettings.Color_Tone_VeryDark)  => (1.000f, 0.333f),
            _                                                         => LogSettings.Color_Tone_Default.GetColorSV(),
        };
    }
}
