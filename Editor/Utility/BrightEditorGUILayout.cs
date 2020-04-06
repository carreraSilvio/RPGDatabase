using UnityEditor;

namespace BrightLib.RPGDatabase.Editor
{
	public static class BrightEditorGUILayout
	{
		public static void LabelFieldBold(string text)
		{
			EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
		}
	}
}

