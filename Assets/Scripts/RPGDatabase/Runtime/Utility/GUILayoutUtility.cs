using UnityEditor;
using UnityEngine;

namespace BrightLib.Utility
{
    public static class GUILayoutUtility
	{
		public static void LabelBold(string text)
		{
			GUILayout.Label(text, EditorStyles.boldLabel);
		}
	}
}

