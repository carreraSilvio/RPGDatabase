using BrightLib.RPGDatabase.Runtime;
using UnityEditor;
using UnityEngine;

namespace BrightLib.RPGDatabase.Editor
{
    public static class DatabaseFactory
    {
        private static void CreateDatabaseConfig()
        {
            var config = ScriptableObject.CreateInstance<DatabaseConfigData>();
            AssetDatabase.CreateAsset(config, "Assets/Resources/Database/00-DatabaseConfig.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void CreateDatabase()
        {
            CreateDatabaseConfig();

            CreateDatabaseAsset<ActorDataList>();
            CreateDatabaseAsset<ActorClassDataList>();
            CreateDatabaseAsset<SkillDataList>();
            CreateDatabaseAsset<ItemDataList>();
            CreateDatabaseAsset<WeaponDataList>();

            CreateDatabaseAsset<WeaponTypeDataList>();
            CreateDatabaseAsset<AttributeSpecDataList>();
        }

        public static void CreateDatabaseAsset<T>() where T : DatabaseEntry
        {
            if (typeof(T) == typeof(ActorDataList))
            {
                CreateActorDataList();
            }
            else if (typeof(T) == typeof(ActorClassDataList))
            {
                CreateActorClassDataList();
            }
            else if (typeof(T) == typeof(SkillDataList))
            {
                CreateSkillDataList();
            }
            else if (typeof(T) == typeof(ItemDataList))
            {
                CreateItemDataList();
            }
            else if (typeof(T) == typeof(WeaponDataList))
            {
                CreateWeaponDataList();
            }
            else if (typeof(T) == typeof(WeaponTypeDataList))
            {
                CreateWeaponTypeDataList();
            }
            else if (typeof(T) == typeof(AttributeSpecDataList))
            {
                CreateAttributeSpecDataList();
            }
        }

        private static void CreateActorDataList()
        {
            Debug.Log("Create ActorDataList");

            var list = ScriptableObject.CreateInstance<ActorDataList>();
            if (list == null)
            {
                Debug.Log("error creating");
            }
            else
            {
                list.entries = new System.Collections.Generic.List<ActorData>
                {
                    new ActorData(DatabaseUtils.Config.FetchUniqueId()) { name = "Alex" }
                };
                AssetDatabase.CreateAsset(list, "Assets/Resources/Database/01-ActorDataList.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private static void CreateActorClassDataList()
        {
            Debug.Log("Create ActorClassDataList");

            var list = ScriptableObject.CreateInstance<ActorClassDataList>();
            if (list == null)
            {
                Debug.Log("error creating");
            }
            else
            {
                list.entries = new System.Collections.Generic.List<ActorClassData>
                {
                    new ActorClassData(DatabaseUtils.Config.FetchUniqueId()) { name = "Warrior" }
                };
                AssetDatabase.CreateAsset(list, "Assets/Resources/Database/02-ActorClassDataList.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private static void CreateSkillDataList()
        {
            Debug.Log("Create SkillDataList");

            var skills = ScriptableObject.CreateInstance<SkillDataList>();
            if (skills == null)
            {
                Debug.Log("error creating");
            }
            else
            {
                skills.entries = new System.Collections.Generic.List<SkillData>
                {
                    new SkillData(DatabaseUtils.Config.FetchUniqueId()) { name = "Fireball" }
                };
                AssetDatabase.CreateAsset(skills, "Assets/Resources/Database/03-SkillDataList.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private static void CreateItemDataList()
        {
            Debug.Log("Create CreateItemDataList");

            var list = ScriptableObject.CreateInstance<ItemDataList>();
            if (list == null)
            {
                Debug.Log("error creating");
            }
            else
            {
                list.entries = new System.Collections.Generic.List<ItemData>
                {
                    new ItemData(DatabaseUtils.Config.FetchUniqueId()) { name = "Health Potion" }
                };
                AssetDatabase.CreateAsset(list, "Assets/Resources/Database/04-ItemDataList.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private static void CreateWeaponDataList()
        {
            Debug.Log("Create WeaponDataList");

            var list = ScriptableObject.CreateInstance<WeaponDataList>();
            if (list == null)
            {
                Debug.Log("error creating");
            }
            else
            {
                list.entries = new System.Collections.Generic.List<WeaponData>
                {
                    new WeaponData(DatabaseUtils.Config.FetchUniqueId()) { name = "Bronze Sword" }
                };
                AssetDatabase.CreateAsset(list, "Assets/Resources/Database/05-WeaponDataList.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }


        private static void CreateWeaponTypeDataList()
        {
            Debug.Log("Create WeaponTypeDataList");

            var list = ScriptableObject.CreateInstance<WeaponTypeDataList>();
            if (list == null)
            {
                Debug.Log("error creating");
            }
            else
            {
                list.entries = new System.Collections.Generic.List<WeaponTypeData>
                {
                    new WeaponTypeData(DatabaseUtils.Config.FetchUniqueId()) { name = "Sword" }
                };
                AssetDatabase.CreateAsset(list, "Assets/Resources/Database/10-WeaponTypesDataList.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private static void CreateAttributeSpecDataList()
        {
            Debug.Log("Create AttributeSpecDataList");

            var list = ScriptableObject.CreateInstance<AttributeSpecDataList>();
            if (list == null)
            {
                Debug.Log("error creating");
            }
            else
            {
                list.entries = new System.Collections.Generic.List<AttributeSpecData>
                {
                    new AttributeSpecData(DatabaseUtils.Config.FetchUniqueId()) { name = Constants.Attributes.LEVEL, start = 1, end = 100},
                    new AttributeSpecData(DatabaseUtils.Config.FetchUniqueId()) { name = Constants.Attributes.EXP, start = 0, end = 9999},
                    new AttributeSpecData(DatabaseUtils.Config.FetchUniqueId()) { name = Constants.Attributes.HP, start = 20, end = 999 },
                    new AttributeSpecData(DatabaseUtils.Config.FetchUniqueId()) { name = Constants.Attributes.MP, start = 20, end = 999 },
                    new AttributeSpecData(DatabaseUtils.Config.FetchUniqueId()) { name = Constants.Attributes.COMMON, start = 5, end = 99 }

                };
                AssetDatabase.CreateAsset(list, "Assets/Resources/Database/11-AttributeSpecDataList.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}