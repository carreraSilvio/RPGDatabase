using BrightLib.RPGDatabase.Runtime;
using BrightLib.RPGDatabase.ThirdParty.ReoderableList;
using System;
using UnityEditor;
using UnityEngine;

namespace BrightLib.RPGDatabase.Editor
{
    public class DatabaseWindow : EditorWindow
    {
        private const string _kWindowTitle = "Database";
        private const string _kMenuName = "Database";

        private ActorDataList _actorDataList;
        private ActorClassDataList _classDataList;
        private SkillDataList _skillDataList;
        private ItemDataList _itemDataList;
        private WeaponDataList _weaponDataList;
        private WeaponTypeDataList _weaponTypeDataList;
        private AttributeSpecDataList _attributeSpectDataList;

        private ListSection<ActorData> _actorListSection;
        private ListSection<ActorClassData> _classListSection;
        private ListSection<SkillData> _skillListSection;
        private ListSection<ItemData> _itemListSection;
        private ListSection<WeaponData> _weaponListSection;
        private ListSection<WeaponTypeData> _weaponTypeListSection;
        private ListSection<AttributeSpecData> _attributeSpecListSection;

        private InfoSection _infoSection;
        private EffectsSection _effectsSection;

        private CoreTabId _coreTabSelected;
        private MainTabId _mainTabSelected;
        private ConfigTabId _configTabSelected;

        private RPGDatabaseManager _database;

        private bool _clearFocusThisFrame;

        [MenuItem("Tools/" + _kMenuName)]
        public static void ShowWindow()
        {
            GetWindow(typeof(DatabaseWindow), false, _kWindowTitle);
        }

        private void OnEnable()
        {
            _database = new RPGDatabaseManager();
            _database.Load();
            if (_database.TotalEntries == 0)
            {
                DatabaseFolderHandler.ValidateAllFolders();
                DatabaseFactory.CreateDatabase();
                _database.Load();
            }

            _actorDataList = _database.FetchEntry<ActorDataList>();
            _classDataList = _database.FetchEntry<ActorClassDataList>();
            _skillDataList = _database.FetchEntry<SkillDataList>();
            _itemDataList = _database.FetchEntry<ItemDataList>();
            _weaponDataList = _database.FetchEntry<WeaponDataList>();
            _weaponTypeDataList = _database.FetchEntry<WeaponTypeDataList>();
            _attributeSpectDataList = _database.FetchEntry<AttributeSpecDataList>();

            _actorListSection = new ListSection<ActorData>(_actorDataList, "Actors");
            _classListSection = new ListSection<ActorClassData>(_classDataList, "Classes");
            _skillListSection = new ListSection<SkillData>(_skillDataList, "Skills");
            _itemListSection = new ListSection<ItemData>(_itemDataList, "Items");
            _weaponListSection = new ListSection<WeaponData>(_weaponDataList, "Weapons");
            _weaponTypeListSection = new ListSection<WeaponTypeData>(_weaponTypeDataList, "Weapon Types");
            var flags = ReorderableListFlags.ShowIndices | ReorderableListFlags.HideAddButton | ReorderableListFlags.DisableReordering | ReorderableListFlags.HideRemoveButtons;
            _attributeSpecListSection = new ListSection<AttributeSpecData>(_attributeSpectDataList, "Attribute Specs", flags);

            _infoSection = new InfoSection();
            _effectsSection = new EffectsSection();

            _coreTabSelected = (CoreTabId)DatabaseEditorPrefs.CoreTab;
            _mainTabSelected = (MainTabId)DatabaseEditorPrefs.MainTab;
            _configTabSelected = (ConfigTabId)DatabaseEditorPrefs.ConfigTab;
        }

        private void OnDestroy()
        {
            Save();
        }

        private void Save()
        {
            var saveDateTime = System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString();

            Debug.Log($"Database saved {saveDateTime}");
            DatabaseEditorPrefs.SetLastSaveDateTime(saveDateTime);

            EditorUtility.SetDirty(_actorDataList);
            EditorUtility.SetDirty(_classDataList);
            EditorUtility.SetDirty(_skillDataList);
            EditorUtility.SetDirty(_itemDataList);
            EditorUtility.SetDirty(_weaponDataList);
            EditorUtility.SetDirty(_weaponTypeDataList);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void ChangeCoreTab(object coreTabId)
        {
            _clearFocusThisFrame = true;
            _coreTabSelected = (CoreTabId)coreTabId;
            DatabaseEditorPrefs.SetCoreTab((int)_coreTabSelected);
        }

        void OnGUI()
        {
            if (GUILayout.Button("File", GUILayout.Width(60f)))
            {
                var menu = new GenericMenu();
                menu.AddItem(new GUIContent("Save"), false, Save);
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("View/Main"), _coreTabSelected == CoreTabId.Main, ChangeCoreTab, CoreTabId.Main);
                menu.AddItem(new GUIContent("View/Config"), _coreTabSelected == CoreTabId.Config, ChangeCoreTab, CoreTabId.Config);
                menu.ShowAsContext();
            }


            if (_coreTabSelected == CoreTabId.Main)
            {
                EditorGUILayout.BeginHorizontal();
                var tab = MainTabId.Actors;
                for(; tab <= MainTabId.Weapons; tab++) DrawTab(tab);
                EditorGUILayout.EndHorizontal();

                DrawContent(_mainTabSelected, _mainTabSelected == MainTabId.Items || _mainTabSelected == MainTabId.Skills);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                var tab = ConfigTabId.WeaponsTypes;
                for (; tab <= ConfigTabId.AttributeSpecs; tab++) DrawTab(tab);
                EditorGUILayout.EndHorizontal();

                DrawContent(_configTabSelected);
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Last saved {DatabaseEditorPrefs.LastSaveDateTime}");
            EditorGUILayout.EndHorizontal();

            if (_clearFocusThisFrame)
            {
                GUI.FocusControl(null);
                _clearFocusThisFrame = false;
            }
        }

        private void DrawContent(MainTabId mainTabId, bool hasEffectsSection = false)
        {
            if (mainTabId == MainTabId.Actors)
                DrawContent<ActorData>(_actorListSection, _actorDataList, hasEffectsSection);
            else if (mainTabId == MainTabId.Classes)
                DrawContent<ActorClassData>(_classListSection, _classDataList, hasEffectsSection);
            else if (mainTabId == MainTabId.Items)
                DrawContent<ItemData>(_itemListSection, _itemDataList, hasEffectsSection);
            else if (mainTabId == MainTabId.Skills)
                DrawContent<SkillData>(_skillListSection, _skillDataList, hasEffectsSection);
            else if (mainTabId == MainTabId.Weapons)
                DrawContent<WeaponData>(_weaponListSection, _weaponDataList, hasEffectsSection);
        }

        private void DrawContent(ConfigTabId configTabId, bool hasEffectsSection = false)
        {
            if(configTabId == ConfigTabId.WeaponsTypes) 
                DrawContent<WeaponTypeData>(_weaponTypeListSection, _weaponTypeDataList, hasEffectsSection);
            else if(configTabId == ConfigTabId.AttributeSpecs)
                DrawContent<AttributeSpecData>(_attributeSpecListSection, _attributeSpectDataList, hasEffectsSection);
        }

        private void DrawContent<T>(ListSection<T> listSection, DataList<T> dataList, bool hasEffects = false) where T : BaseData
        {
            listSection.PrepareList(dataList);

            EditorGUILayout.BeginHorizontal();
            {
                listSection.Draw();

                EditorGUILayout.BeginVertical();
                {
                    DrawInfo(listSection);
                    if (hasEffects) DrawEffects(listSection);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();

            UpdateDataList(dataList, listSection);
        }

        private void DrawInfo<T>(ListSection<T> listSection) where T : BaseData
        {
            if (listSection.TotalEntries == 0) return;
            var currentSelected = listSection.EntrySelected;
            _infoSection.entrySelected = currentSelected;
            _infoSection.Draw(_database);
        }

        private void DrawEffects<T>(ListSection<T> listSection) where T : BaseData
        {
            if (listSection.TotalEntries == 0) return;
            var currentSelected = listSection.EntrySelected;
            _effectsSection.entrySelected = currentSelected;
            _effectsSection.Draw();
        }

        private void UpdateDataList<T>(DataList<T> dataList, ListSection<T> listSection) where T : BaseData
        {
            if (listSection.TotalEntries == 0) return;
            var currentSelected = listSection.SelectedIndex;
            dataList.entries[currentSelected] = (T)_infoSection.entrySelected;
        }

        private void DrawTab(MainTabId tabId)
        {
            GUI.enabled = _mainTabSelected != tabId;
            if (GUILayout.Button(tabId.ToString(), GUILayout.Width(110f)))
            {
                GUI.FocusControl(null);
                _mainTabSelected = tabId;
                DatabaseEditorPrefs.SetMainTab((int)_mainTabSelected);
            }
            GUI.enabled = true;
        }

        private void DrawTab(ConfigTabId tabId)
        {
            
            GUI.enabled = _configTabSelected != tabId;
            if (GUILayout.Button(tabId.ToString(), GUILayout.Width(110f)))
            {
                GUI.FocusControl(null);
                _configTabSelected = tabId;
                DatabaseEditorPrefs.SetConfigTab((int)_configTabSelected);
            }
            GUI.enabled = true;
        }

        private void TestTab(Enum tabId)
        {
            
        }

    }
}