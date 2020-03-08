using BrightLib.RPGDatabase.Runtime;
using BrightLib.RPGDatabase.ThirdParty.ReoderableList;
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

        enum CoreTabId { Main, Config }
        private CoreTabId _coreTabSelected;

        enum MainTabId {Actors, Classes, Skills, Items, Weapons };
        private MainTabId _mainTabSelected;

        enum ConfigTabId { WeaponsTypes, AttributeSpecs };
        private ConfigTabId _configTabSelected;

        private RPGDatabaseManager _database;


        [MenuItem("Tools/" + _kMenuName)]
        public static void ShowWindow()
        {
            var wind = GetWindow(typeof(DatabaseWindow), false, _kWindowTitle);
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

        public void ShowMainTab()
        {
            _clearFocusThisFrame = true;
            _coreTabSelected = CoreTabId.Main;
            DatabaseEditorPrefs.SetCoreTab((int)_coreTabSelected);
        }

        public void ShowConfigTab()
        {
            _clearFocusThisFrame = true;
            _coreTabSelected = CoreTabId.Config;
            DatabaseEditorPrefs.SetCoreTab((int)_coreTabSelected);
        }


        void OnGUI()
        {
            if (GUILayout.Button("File", GUILayout.Width(60f)))
            {
                var menu = new GenericMenu();
                menu.AddItem(new GUIContent("Save"), false, Save);
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("View/Main"), _coreTabSelected == CoreTabId.Main, ShowMainTab);
                menu.AddItem(new GUIContent("View/Config"), _coreTabSelected == CoreTabId.Config, ShowConfigTab);
                menu.ShowAsContext();
            }


            if (_coreTabSelected == CoreTabId.Main)
            {
                EditorGUILayout.BeginHorizontal();
                DrawTab(MainTabId.Actors);
                DrawTab(MainTabId.Classes);
                DrawTab(MainTabId.Skills);
                DrawTab(MainTabId.Items);
                DrawTab(MainTabId.Weapons);
                EditorGUILayout.EndHorizontal();

                if (_mainTabSelected == MainTabId.Actors) DrawActorsContent();
                else if (_mainTabSelected == MainTabId.Classes) DrawClassesContent();
                else if (_mainTabSelected == MainTabId.Skills) DrawSkillsContent();
                else if (_mainTabSelected == MainTabId.Items) DrawItemsContent();
                else if (_mainTabSelected == MainTabId.Weapons) DrawWeaponsContent();
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                DrawTab(ConfigTabId.WeaponsTypes);
                DrawTab(ConfigTabId.AttributeSpecs);
                EditorGUILayout.EndHorizontal();

                if (_configTabSelected == ConfigTabId.WeaponsTypes) DrawWeaponTypesContent();
                else if (_configTabSelected == ConfigTabId.AttributeSpecs) DrawAttributeSpecsContent();
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

        private bool _clearFocusThisFrame;

        private void DrawActorsContent()
        {
            _actorListSection.PrepareList(_actorDataList);

            EditorGUILayout.BeginHorizontal();

            _actorListSection.Draw();
            EditorGUILayout.BeginVertical();
            DrawInfo(_actorListSection);
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            UpdateDataList(_actorDataList, _actorListSection);
        }

        private void DrawClassesContent()
        {
            _classListSection.PrepareList(_classDataList);

            EditorGUILayout.BeginHorizontal();

            _classListSection.Draw();

            EditorGUILayout.BeginVertical();
            DrawInfo(_classListSection);
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            UpdateDataList(_classDataList, _classListSection);
        }

        private void DrawSkillsContent()
        {
            _skillListSection.PrepareList(_skillDataList);

            EditorGUILayout.BeginHorizontal();

            _skillListSection.Draw();

            EditorGUILayout.BeginVertical();
            DrawInfo(_skillListSection);
            DrawEffects(_skillListSection);
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            UpdateDataList(_skillDataList, _skillListSection);
        }

        private void DrawItemsContent()
        {
            _itemListSection.PrepareList(_itemDataList);

            EditorGUILayout.BeginHorizontal();
            _itemListSection.Draw();

            EditorGUILayout.BeginVertical();

            DrawInfo(_itemListSection);
            DrawEffects(_itemListSection);

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            UpdateDataList(_itemDataList, _itemListSection);
        }

        private void DrawWeaponsContent()
        {
            _weaponListSection.PrepareList(_weaponDataList);

            EditorGUILayout.BeginHorizontal();

            _weaponListSection.Draw();
            DrawInfo(_weaponListSection);

            EditorGUILayout.EndHorizontal();

            UpdateDataList(_weaponDataList, _weaponListSection);
        }

        private void DrawWeaponTypesContent()
        {
            _weaponTypeListSection.PrepareList(_weaponTypeDataList);

            EditorGUILayout.BeginHorizontal();

            _weaponTypeListSection.Draw();
            DrawInfo(_weaponTypeListSection);

            EditorGUILayout.EndHorizontal();

            UpdateDataList(_weaponTypeDataList, _weaponTypeListSection);
        }

        private void DrawAttributeSpecsContent()
        {
            _attributeSpecListSection.PrepareList(_attributeSpectDataList);

            EditorGUILayout.BeginHorizontal();

            _attributeSpecListSection.Draw();
            DrawInfo(_attributeSpecListSection);

            EditorGUILayout.EndHorizontal();

            UpdateDataList(_attributeSpectDataList, _attributeSpecListSection);
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
            }
            GUI.enabled = true;

            DatabaseEditorPrefs.SetMainTab((int)_mainTabSelected);
        }

        private void DrawTab(ConfigTabId tabId)
        {
            GUI.enabled = _configTabSelected != tabId;
            if (GUILayout.Button(tabId.ToString(), GUILayout.Width(110f)))
            {
                GUI.FocusControl(null);
                _configTabSelected = tabId;
            }
            GUI.enabled = true;

            DatabaseEditorPrefs.SetConfigTab((int)_configTabSelected);
        }

    }
}