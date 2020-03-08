using UnityEditor;

namespace BrightLib.RPGDatabase.Editor
{
    public static class DatabaseEditorPrefs
    {
        private const string _kCoreTabSelected = "BrightDBPref_CoreTabSelected";
        private const string _kMainTabSelected = "BrightDBPref_TabSelected";
        private const string _kConfigTabSelected = "BrightDBPref_ConfigTabSelected";
        private const string _kLastSaveDateTime = "BrightDBPref_LastSaveDateTime";

        internal static void SetCoreTab(int coreTabSelected)
        {
            EditorPrefs.SetInt(_kCoreTabSelected, coreTabSelected);
        }

        internal static void SetMainTab(int mainTabSelected)
        {
            EditorPrefs.SetInt(_kMainTabSelected, mainTabSelected); 
        }
        internal static void SetConfigTab(int configTabSelected)
        {
            EditorPrefs.SetInt(_kConfigTabSelected, configTabSelected);
        }

        internal static void SetLastSaveDateTime(string saveDateTime)
        {
            EditorPrefs.SetString(_kLastSaveDateTime, saveDateTime);
        }

        internal static int CoreTab
        {
            get
            {
                return EditorPrefs.GetInt(_kCoreTabSelected);
            }
        }

        internal static int MainTab
        {
            get
            {
                return EditorPrefs.GetInt(_kMainTabSelected);
            }
        }

        internal static int ConfigTab
        {
            get
            {
                return EditorPrefs.GetInt(_kConfigTabSelected);
            }
        }

        internal static string LastSaveDateTime
        {
            get
            {
                return EditorPrefs.GetString(_kLastSaveDateTime, "20-06-1990");
            }
        }
    }
}