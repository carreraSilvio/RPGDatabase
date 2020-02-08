using UnityEditor;
using UnityEngine;

namespace BrightLib.Extensions
{
    public static class GUILayoutExtensions
	{
		public static void LabelBold(string text)
		{
			GUILayout.Label(text, EditorStyles.boldLabel);
		}
	}
}

