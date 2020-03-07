using System;
using System.Collections.Generic;
using System.Linq;
using Rotorz.ReorderableList;
using UnityEngine;

public class SelectableItemAdaptor : GenericListAdaptor<string>
{
	private HashSet<int> _selectedIndices = new HashSet<int>();
	private int _lastSelectedIndex;

	public Action<int, int> onEntryMove;
	public Action onEntryAdd;
    public Action<int> onEntryRemove;
    public Action<int> onEntryDuplicate;
    public Action<int> onEntryInsert;

	public SelectableItemAdaptor(List<string> list, float itemHeight)
		: base(list, null, itemHeight)
	{
	}
	
	public void SetSelectedIndex(int index)
	{
		_selectedIndices.Clear();
		_selectedIndices.Add(index);
		_lastSelectedIndex = index;
	}

	public override void Move(int sourceIndex, int destIndex)
	{
		base.Move(sourceIndex, destIndex);
		_selectedIndices.Clear();

		//For some reason when moving an item down it gives the index of the next position
		if (sourceIndex < destIndex) destIndex = destIndex - 1;
		_selectedIndices.Add(destIndex);
		_lastSelectedIndex = destIndex;

        onEntryMove?.Invoke(sourceIndex, destIndex);
    }

	public override void DrawItemBackground(Rect position, int index)
	{
		if (_selectedIndices.Contains(index)) 
		{
			ReorderableListStyles.SelectedItem.Draw(position, GUIContent.none, false, false, false, false);
		}
	}
	public override void Add()
	{
		base.Add();

        onEntryAdd?.Invoke();
    }

    public override void Remove(int index)
    {
        base.Remove(index);
        onEntryRemove?.Invoke(index);
    }

    public override void Duplicate(int index)
    {
        base.Duplicate(index);
        onEntryDuplicate?.Invoke(index);
    }


    public override void Insert(int index)
    {
        base.Insert(index);
        onEntryInsert?.Invoke(index);
    }

    public override void DrawItem(Rect position, int index)
	{
		var controlID = GUIUtility.GetControlID(FocusType.Passive);

		switch (Event.current.GetTypeForControl(controlID)) 
		{
			case EventType.MouseDown:
				if (Event.current.button == 0 && position.Contains(Event.current.mousePosition)) 
				{
					GUI.FocusControl(null);
					if (Event.current.control) 
					{
						// Toggle selection of this item if control key is held.
						if (_selectedIndices.Contains(index)) 
						{
							_selectedIndices.Remove(index);
							if (_selectedIndices.Count == 1)
								_lastSelectedIndex = _selectedIndices.ToList()[_selectedIndices.Count - 1];
						}
						else 
						{
							_selectedIndices.Add(index);
						}
					}
					else 
					{
						// Deselect all other items and select this one instead.
						_selectedIndices.Clear();
						_selectedIndices.Add(index);
						_lastSelectedIndex = index;
					}
					Event.current.Use();
				}
				break;

			case EventType.Repaint:
				GUI.skin.label.Draw(position, this[index], false, false, false, false);
				break;
		}
	}

	#region Properties

	public int lastSelectedIndex
	{
		get
		{
			return _lastSelectedIndex;
		}
	}

	#endregion
	
}