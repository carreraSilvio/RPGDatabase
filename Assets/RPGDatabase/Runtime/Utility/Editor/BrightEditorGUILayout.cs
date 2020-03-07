using UnityEditor;

namespace Assets.Utility
{
	public static class BrightEditorGUILayout
    {
		public static void LabelFieldBold(string text)
		{
			EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
		}
	}
}

