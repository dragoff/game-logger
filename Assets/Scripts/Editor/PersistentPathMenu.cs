using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace GameLogger
{
	public class PersistentPathMenu : MonoBehaviour
	{
		[MenuItem("Assets/Show Persistent Directory", false, 2)]
		static void DoMenu()
		{
			Process process = new Process();
			process.StartInfo.FileName = ((Application.platform == RuntimePlatform.WindowsEditor) ? "explorer.exe" : "open");
			process.StartInfo.Arguments = "file://" + Application.persistentDataPath;
			process.Start();
		}
	}
}