using System;

namespace foriver4725.BetterLogging
{
    [Flags]
    public enum LogSettings : ulong
    {
        None = 0,

        // Log Type
        // Only one should be used
        Level_Normal = 1 << 0,
        Level_Warning = 1 << 1,
        Level_Error = 1 << 2,
        Level_Default = Level_Normal,

        // Color Hue (H)
        // Only one should be used
        Color_Hue_Red = 1 << 8,
        Color_Hue_Yellow = 1 << 9,
        Color_Hue_Green = 1 << 10,
        Color_Hue_Cyan = 1 << 11,
        Color_Hue_Blue = 1 << 12,
        Color_Hue_Magenta = 1 << 13,
        Color_Hue_Default = Color_Hue_Red,

        // Color Tone (S,V)
        // Only one should be used
        Color_Tone_Vivid = 1 << 16,
        Color_Tone_Light = 1 << 17,
        Color_Tone_VeryLight = 1 << 18,
        Color_Tone_Dark = 1 << 19,
        Color_Tone_VeryDark = 1 << 20,
        Color_Tone_Default = Color_Tone_Dark,

        // Special Colors (Black and White)
        // These are defined separately as special colors
        Color_Black = 1 << 24,
        Color_DarkGray = 1 << 25,
        Color_LightGray = 1 << 26,
        Color_White = 1 << 27,

        // Templates
        Normal = Level_Normal | Color_White,
        Warning = Level_Warning | Color_Hue_Yellow | Color_Tone_Vivid,
        Error = Level_Error | Color_Hue_Red | Color_Tone_Vivid,
    }
}
