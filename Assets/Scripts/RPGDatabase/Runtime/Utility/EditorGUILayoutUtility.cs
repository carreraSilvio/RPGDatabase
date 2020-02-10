using UnityEditor;
using UnityEngine;

namespace BrightLib.Utility
{
    public static class EditorGUILayoutUtility
    {
		public static void LabelFieldBold(string text)
		{
			EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
		}
	}
}

