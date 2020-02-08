﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(StringDataList), menuName = "Database/" + nameof(StringDataList), order = 1)]
public class StringDataList : DatabaseEntry
{
    public List<string> entries = new List<string>();
}