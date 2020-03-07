using UnityEngine;
using System.Collections.Generic;
using System;
using BrightLib.RPGDatabase.Runtime;
using BrightLib.RPGDatabase.ThirdParty.ReoderableList;

namespace BrightLib.RPGDatabase.Editor
{
    public class ListSection<T> : Section where T : BaseData
    {
        private SelectableItemAdaptor _listAdapter;

        private DataList<T> _dataList;

        private ReorderableListFlags _flags;

        public ListSection(DataList<T> dataList, string title = "Title", ReorderableListFlags flags = ReorderableListFlags.ShowIndices)
        {
            _title = title;
            _dataList = dataList;
            _flags = flags;

            var entries = dataList.entries;
            var itemNames = new List<string>();

            for (var i = 0; i < entries.Count; i++)
            {
                itemNames.Add(entries[i].name);
            }

            _listAdapter = new SelectableItemAdaptor(itemNames, 20f);
            _listAdapter.onEntryMove += HandleEntryMove;
            _listAdapter.onEntryAdd += HandleEntryAdd;
            _listAdapter.onEntryRemove += HandleEntryRemove;
            _listAdapter.onEntryDuplicate += HandleEntryDuplicate;
            _listAdapter.onEntryInsert += HandleEntryInsert;


            if (_dataList.entries.Count > 0) _listAdapter.SetSelectedIndex(0);
        }

        public void PrepareList(DataList<T> dataList)
        {
            _listAdapter.Clear();
            _dataList = dataList;

            foreach (var entry in _dataList.entries)
            {
                _listAdapter.List.Add(entry.name);
            }
        }

        public override void Draw()
        {
            GUILayout.BeginHorizontal();

            // Draw list control on left side of the window.
            GUILayout.BeginVertical(GUILayout.Width(200));

            ReorderableListGUI.Title(_title);
            ReorderableListGUI.ListField(_listAdapter, _flags);
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
        }

        private void HandleEntryMove(int sourceIndex, int destIndex)
        {
            var item = _dataList.entries[sourceIndex];
            _dataList.entries.RemoveAt(sourceIndex);
            _dataList.entries.Insert(destIndex, item);
        }

        private void HandleEntryAdd()
        {
            var uniqueId = DatabaseUtils.Config.FetchUniqueId();
            var newEntry = (T)Activator.CreateInstance(typeof(T), uniqueId);
            _dataList.entries.Add(newEntry);
        }

        private void HandleEntryRemove(int index)
        {
            _dataList.entries.RemoveAt(index);

            PrepareList(_dataList);
            if (_dataList.entries.Count > 0)
                _listAdapter.SetSelectedIndex(index - 1);
        }

        private void HandleEntryDuplicate(int index)
        {
            var newEntry = Activator.CreateInstance<T>();
            _dataList.entries.Insert(index + 1, newEntry);
        }

        private void HandleEntryInsert(int index)
        {
            var newEntry = Activator.CreateInstance<T>();
            _dataList.entries.Insert(index, newEntry);
        }

        public T EntrySelected
        {
            get
            {
                if (_dataList.entries.Count == 0) return null;

                return _dataList.entries[_listAdapter.lastSelectedIndex];
            }
        }

        public int SelectedIndex
        {
            get
            {
                return _listAdapter.lastSelectedIndex;
            }
        }

        public int TotalEntries
        {
            get
            {
                return _dataList.entries.Count;
            }
        }

    }
}