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
        /// A convenient API for those who want quick logging without any setup.<br/>
        /// Simply call <see cref='"Message".Print()' /> on any string and it will be logged automatically.<br/>
        /// Uses <see cref="LogSettings.Normal" /> by default.<br/>
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
        /// A balanced API combining simplicity and flexibility.<br/>
        /// You can choose a preset from <see cref="LogSettings" /> such as<br/>
        /// <see cref="LogSettings.Normal" />, <see cref="LogSettings.Warning" />, or <see cref="LogSettings.Error" />.<br/>
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
        /// The core API providing full control over output.<br/>
        /// Allows manual color specification and detailed customization.<br/>
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

            LogMessageSb.AppendFormat("<color=#{0}>{1}</color> : {2}\n",
                                      ColorUtility.ToHtmlStringRGB(overrideColor), settings.GetLevelName(), message);
            LogMessageSb.AppendFormat("Link : <a href=\"{0}\" line=\"{1}\">{2}</a>\n",
                                      callerFilePath, callerLineNumber, callerMemberName);
            LogMessageSb.AppendFormat("Caller File Path : {0}\n", callerFilePath);
            LogMessageSb.AppendFormat("Caller Line Number : {0}\n", callerLineNumber);
            LogMessageSb.AppendFormat("Caller Member Name : {0}\n", callerMemberName);
            LogMessageSb.Append(
                "\n====================================================================================================\n\n");

            return LogMessageSb.ToString();
        }
    }
}
