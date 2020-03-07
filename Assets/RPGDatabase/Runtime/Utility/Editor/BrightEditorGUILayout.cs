using UnityEditor;

namespace BrightLib.Editor
{
	public static class BrightEditorGUILayout
	{
		public static void LabelFieldBold(string text)
		{
			EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
		}
	}
}

