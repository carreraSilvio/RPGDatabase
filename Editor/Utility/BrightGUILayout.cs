using UnityEditor;
using UnityEngine;

namespace BrightLib.RPGDatabase.Editor
{
	public static class BrightGUILayout
	{
		public static void LabelBold(string text)
		{
			GUILayout.Label(text, EditorStyles.boldLabel);
		}
	}
}

