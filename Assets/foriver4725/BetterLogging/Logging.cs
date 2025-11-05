using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace foriver4725.BetterLogging
{
    public static class Logging
    {
        private const string ScriptingDefine = "BETTER_LOGGING";

        private static readonly StringBuilder LogMessageSb = new(0xffff);

        /// <summary>
        /// An API perfect for lazy people who find it troublesome to even set <see cref="LogSettings" />.<br/>
        /// Just do <see cref='"Message".Print()' /> on a string and you're good to go.<br/>
        /// <see cref="LogSettings.Normal" /> will be specified automatically.<br/>
        /// </summary>
        [Conditional(ScriptingDefine)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Print(
            this object message,
            [CallerFilePath] string callerFilePath = "",    // No need to set value
            [CallerLineNumber] int callerLineNumber = -1,   // No need to set value
            [CallerMemberName] string callerMemberName = "" // No need to set value
        )
            => message.Print(LogSettings.Normal, callerFilePath, callerLineNumber, callerMemberName);

        /// <summary>
        /// Hybrid API of ease and freedom<br/>
        /// It is recommended to set <see cref="LogSettings" /> from templates<br/>
        /// Such as <see cref="LogSettings.Normal" />, <see cref="LogSettings.Warning" />, <see cref="LogSettings.Error" /><br/>
        /// </summary>
        [Conditional(ScriptingDefine)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Print(
            this object message,
            LogSettings settings,
            [CallerFilePath] string callerFilePath = "",    // No need to set value
            [CallerLineNumber] int callerLineNumber = -1,   // No need to set value
            [CallerMemberName] string callerMemberName = "" // No need to set value
        )
            => message.Print(settings, settings.GetColor(), callerFilePath, callerLineNumber, callerMemberName);

        /// <summary>
        /// The most core API<br/>
        /// The most settings items<br/>
        /// You can specify the color freely<br/>
        /// </summary>
        [Conditional(ScriptingDefine)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Print(
            this object message,
            LogSettings settings,
            Color overrideColor,
            [CallerFilePath] string callerFilePath = "",    // No need to set value
            [CallerLineNumber] int callerLineNumber = -1,   // No need to set value
            [CallerMemberName] string callerMemberName = "" // No need to set value
        )
        {
            string logMessage = CreateLogMessage(
                message, settings, overrideColor, callerFilePath, callerLineNumber, callerMemberName);
            settings.GetAction()?.Invoke(logMessage);
        }

        private static string CreateLogMessage(
            object message,
            LogSettings settings,
            Color overrideColor,
            string callerFilePath,
            int callerLineNumber,
            string callerMemberName
        )
        {
            LogMessageSb.Clear();

            LogMessageSb.AppendFormat("<color=#{{2}}>{{1}}</color> : {{0}}\n",
                                      message, settings.GetLevelName(), ColorUtility.ToHtmlStringRGB(overrideColor));
            LogMessageSb.AppendFormat("Link : <a href=\"{{0}}\" line=\"{{1}}\">{{2}}</a>\n",
                                      callerFilePath, callerLineNumber, callerMemberName);
            LogMessageSb.AppendFormat("Caller File Path : {{0}}\n", callerFilePath);
            LogMessageSb.AppendFormat("Caller Line Number : {{0}}\n", callerLineNumber);
            LogMessageSb.AppendFormat("Caller Member Name : {{0}}\n", callerMemberName);
            LogMessageSb.Append(
                "\n====================================================================================================\n\n");

            return LogMessageSb.ToString();
        }
    }
}
