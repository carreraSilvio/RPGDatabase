using System;
using UnityEditor;
using UnityEngine;

public class DatabaseFactory : MonoBehaviour
{
    public static void CreateDatabaseAsset <T> () where T  : DatabaseEntry
    {
        if (typeof(T) == typeof(ActorDataList))
        {
            CreateActors();
        }
        else if (typeof(T) == typeof(ActorClassDataList))
        {
            CreateClasses();
        }
        else if(typeof(T) == typeof(SkillDataList))
        {
            CreateSkills();
        }
        else if (typeof(T) == typeof(WeaponDataList))
        {
            
        }
        else if (typeof(T) == typeof(WeaponTypeDataList))
        {
            CreateWeaponTypes();
        }
    }

    private static void CreateActors()
    {
        Debug.Log("creating actors ");

        var list = ScriptableObject.CreateInstance<ActorDataList>();
        if (list == null)
        {
            Debug.Log("error creating");
        }
        else
        {
            list.entries = new System.Collections.Generic.List<ActorData>
            {
                new ActorData() { name = "Alex" }
            };
            AssetDatabase.CreateAsset(list, "Assets/Resources/Database/01-ActorDataTable.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private static void CreateClasses()
    {
        Debug.Log("creating classes");

        var list = ScriptableObject.CreateInstance<ActorClassDataList>();
        if (list == null)
        {
            Debug.Log("error creating");
        }
        else
        {
            list.entries = new System.Collections.Generic.List<ActorClassData>
            {
                new ActorClassData() { name = "Warrior" }
            };
            AssetDatabase.CreateAsset(list, "Assets/Resources/Database/02-ActorClassDataTable.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private static void CreateSkills()
    {
        Debug.Log("creating skills ");

        var skills = ScriptableObject.CreateInstance<SkillDataList>();
        if (skills == null)
        {
            Debug.Log("error creating");
        }
        else
        {
            skills.entries = new System.Collections.Generic.List<SkillData>
            {
                new SkillData() { name = "Fireball" }
            };
            AssetDatabase.CreateAsset(skills, "Assets/Resources/Database/03-SkillsDataTable.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private static void CreateWeaponTypes()
    {
        Debug.Log("creating weapon types");

        var list = ScriptableObject.CreateInstance<WeaponTypeDataList>();
        if (list == null)
        {
            Debug.Log("error creating");
        }
        else
        {
            list.entries = new System.Collections.Generic.List<WeaponTypeData>
            {
                new WeaponTypeData() { name = "Sword" }
            };
            AssetDatabase.CreateAsset(list, "Assets/Resources/Database/10-WeaponTypesDataList.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
