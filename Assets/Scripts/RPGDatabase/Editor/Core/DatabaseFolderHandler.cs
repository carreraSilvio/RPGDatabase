using UnityEditor;
using UnityEngine;

public static class DatabaseFolderHandler
{
    public static bool ValidateAllFolders()
    {
        bool valid = ValidateFolder("Assets/Resources", "Assets", "Resource");
        valid &= ValidateFolder("Assets/Resources/Database", "Resources", "Database");

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
    
    //public static void CheckResourcesFolder()
    //{
    //    if (!AssetDatabase.IsValidFolder("Assets/Resources"))
    //    {
    //        Debug.Log("creating Resources folder");
    //        AssetDatabase.CreateFolder("Assets", "Resources");
    //        Debug.Assert(AssetDatabase.IsValidFolder("Assets/Resources"), "Error creating resources folder");
    //    }
    //}

    //public static void CheckDatabaseFolder()
    //{
    //    if (!AssetDatabase.IsValidFolder("Assets/Resources/Database"))
    //    {
    //        Debug.Log("creating Database folder");
    //        AssetDatabase.CreateFolder("Resources", "Database");
    //        Debug.Assert(AssetDatabase.IsValidFolder("Assets/Resources/Database"), "Error creating database folder");
    //    }
    //}


}
