using UnityEditor;
using UnityEngine;

namespace BrightLib.RPGDatabase.Editor
{
    public static class DatabaseFolderHandler
    {
        public static bool ValidateAllFolders()
        {
            bool valid = ValidateFolder("Assets/Resources", "Assets", "Resources");
            valid &= ValidateFolder("Assets/Resources/Database", "Assets/Resources", "Database");

            return valid;
        }

        private static bool ValidateFolder(string path, string parent, string child)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                Debug.Log($"creating {child} folder");
                AssetDatabase.CreateFolder(parent, child);
                Debug.Assert(AssetDatabase.IsValidFolder(path), $"Error creating {child} folder");
            }

            return AssetDatabase.IsValidFolder(path);
        }
    }
}