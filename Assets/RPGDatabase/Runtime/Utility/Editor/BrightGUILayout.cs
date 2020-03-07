using UnityEditor;
using UnityEngine;

namespace Assets.Utility
{
    public static class BrightGUILayout
	{
		public static void LabelBold(string text)
		{
			GUILayout.Label(text, EditorStyles.boldLabel);
		}
	}
}

