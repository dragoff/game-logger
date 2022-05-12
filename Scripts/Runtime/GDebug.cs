using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GameLogger
{
	public static class GDebug
	{
		public static event Action<string> OnError;
		public static event Action<string> OnWarning;

		private static readonly string LOG_SOURCE_NAME_COLOR = "#ffffff";
		private static readonly string LOG_LINE_COLOR = "#b5b180";
		private static readonly string LOG_PATH = Path.Combine(Application.persistentDataPath, "GameLog");

		public static void Log(object message, (string text, Color color) title = default,
			[CallerMemberName] string memberName = "",
			[CallerFilePath] string filePath = "",
			[CallerLineNumber] int lineNumber = 0) =>
			Log(message?.ToString(), title, memberName, filePath, lineNumber);

		public static void Log(string message = "", (string text, Color color) title = default,
			[CallerMemberName] string memberName = "",
			[CallerFilePath] string filePath = "",
			[CallerLineNumber] int lineNumber = 0)
		{
			var logMessage = PrepareLogMessage(message, title.text, title.color, filePath, memberName, lineNumber);
			LogToFile(message, "Log!    ", memberName, filePath, lineNumber);
			Debug.Log(logMessage);
		}

		public static void LogError(string message = "", (string text, Color color) title = default,
			[CallerMemberName] string memberName = "",
			[CallerFilePath] string filePath = "",
			[CallerLineNumber] int lineNumber = 0)
		{
			var logMessage = PrepareLogMessage(message, title.text, title.color, filePath, memberName, lineNumber);
			LogToFile(message, "Error!  ", memberName, filePath, lineNumber);

			Debug.LogError(logMessage);
			OnError?.Invoke(logMessage);
		}

		public static void LogError(Exception exception, (string text, Color color) title = default,
			string customMessage = "",
			[CallerMemberName] string memberName = "",
			[CallerFilePath] string filePath = "",
			[CallerLineNumber] int lineNumber = 0)
		{
			var message = string.IsNullOrEmpty(customMessage) ? exception.ToString() : (customMessage + $"\n-------------\n{exception}");
			var logMessage = PrepareLogMessage(message, title.text, title.color, filePath, memberName, lineNumber);
			LogToFile(message, "Error!  ", memberName, filePath, lineNumber);

			Debug.LogError(logMessage);
			OnError?.Invoke(logMessage);
		}

		public static void LogWarning(string message, (string text, Color color) title = default,
			[CallerMemberName] string memberName = "",
			[CallerFilePath] string filePath = "",
			[CallerLineNumber] int lineNumber = 0)
		{
			LogToFile(message, "Warning!", memberName, filePath, lineNumber);

			var logMessage = PrepareLogMessage(message, title.text, title.color != default ? title.color : Color.white, filePath,
				memberName,
				lineNumber);
			Debug.LogWarning(logMessage);
			OnWarning?.Invoke(logMessage);
		}

		public static void LogToFile(string message, string severity = "ToFile! ",
			[CallerMemberName] string memberName = "",
			[CallerFilePath] string filePath = "",
			[CallerLineNumber] int lineNumber = 0)
		{
			var logMessage = PrepareLogTiFile(message, severity, filePath, memberName, lineNumber);

			using (var stream = new StreamWriter(LOG_PATH, true))
			{
				stream.WriteLine(TimeWrapMessage(logMessage));
			}
		}

		public static void LogToFile(Exception exception, string severity = "ToFile! ",
			[CallerMemberName] string memberName = "",
			[CallerFilePath] string filePath = "",
			[CallerLineNumber] int lineNumber = 0)
		{
			var logMessage = PrepareLogTiFile(exception.Message, severity, filePath, memberName, lineNumber);

			using (var stream = new StreamWriter(LOG_PATH, true))
			{
				stream.WriteLine(TimeWrapMessage(logMessage));
				stream.WriteLine("Trace:");
				stream.WriteLine(exception.StackTrace);
				stream.WriteLine("EndOfTrace -------------------------------");
			}
		}

		public static void ResetLogFile()
		{
			if (!File.Exists(LOG_PATH))
				return;
			var oldFilePath = LOG_PATH + ".old";
			if (File.Exists(oldFilePath))
				File.Delete(oldFilePath);
			File.Move(LOG_PATH, oldFilePath);
		}

		private static string TimeWrapMessage(string message) => $"{DateTime.UtcNow.ToLongTimeString()}\t{message}";

		private static string PrepareLogTiFile(string message, string severity, string filePath, string memberName,
			int lineNumber)
		{
			message = Regex.Replace(message, "<(.*?)>", "");
			return $"{severity} (at {Path.GetFileName(filePath)}:{lineNumber}) {memberName}() : {message}";
		}

		private static string PrepareLogMessage(string message, string title, Color color, string filePath, string memberName,
			int lineNumber)
			=>
				$"{(string.IsNullOrEmpty(title) ? "" : $"<color=#{color.ToHexString()}>[{title}]</color>")} {message}\n<color={LOG_SOURCE_NAME_COLOR}><b>{Path.GetFileName(filePath)}:</b></color><color={LOG_LINE_COLOR}>{lineNumber}</color> {memberName}()";

		private static string ToHexString(this Color color)
		{
			return
				((byte)(color.r * 255)).ToString("X2") +
				((byte)(color.g * 255)).ToString("X2") +
				((byte)(color.b * 255)).ToString("X2") +
				((byte)(color.a * 255)).ToString("X2");
		}
#if UNITY_EDITOR

		static GDebug()
		{
			if (UnityEditor.EditorGUIUtility.isProSkin)
				return;

			LOG_SOURCE_NAME_COLOR = "#1c1c1c";
			LOG_LINE_COLOR = "#044b4b";
		}
#endif
	}
}