using UnityEditor;
using UnityEngine;

namespace BrightLib.Extensions
{
    public static class EditorGUILayoutExtensions
	{
		public static void LabelFieldBold(string text)
		{
			EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
		}
	}
}

