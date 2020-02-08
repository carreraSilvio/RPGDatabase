using BrightLib.Extensions;
using Rotorz.ReorderableList;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class InfoSection : Section
{
	public BaseData entrySelected;
	
	public InfoSection()
	{
		_title = "Info";
	}
	
	public override void Draw(DatabaseManager database)
	{
        if (entrySelected is ActorData)              DrawActor(database);
        else if(entrySelected is ActorClassData)     DrawClass(database);
        else if (entrySelected is SkillData)         DrawSkill();
        else if (entrySelected is ItemData)          DrawItem();
        else if (entrySelected is WeaponData)        DrawWeapon(database);
        else if (entrySelected is WeaponTypeData)    DrawWeaponType();
        else if (entrySelected is AttributeSpecData) DrawAttributeSpec();
    }

    private void DrawActor(DatabaseManager database)
    {
        var weaponNames = database.FetchEntry<WeaponDataList>().entries.Select(l => l.name).ToArray();
        var list = database.FetchEntry<AttributeSpecDataList>();
        var level = list.entries.First<AttributeSpecData>(x => x.name == "Level");

        var entry = (ActorData)entrySelected;

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(400));
        DrawTitle();

        var classNames = database.FetchEntry<ActorClassDataList>().entries.Select(l => l.name).ToArray();

        entry.name = EditorGUILayout.TextField("Name", entry.name);
        entry.classId = EditorGUILayout.Popup("Class", entry.classId, classNames);
        entry.initialLevel = EditorGUILayout.IntSlider("Initial Level", entry.initialLevel, level.start, level.end);

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
        EditorGUILayoutExtensions.LabelFieldBold("Initial Equipment");
        entry.initialWeapon  = EditorGUILayout.Popup("Weapon", entry.initialWeapon, weaponNames);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
    }

    private int previewLv = 1;
    private Vector2 vect;
    private void DrawClass(DatabaseManager database)
    {
        var weaponTypeNames = database.FetchEntry<WeaponTypeDataList>().entries.Select(l => l.name).ToArray();
        var skillNames = database.FetchEntry<SkillDataList>().entries.Select(l => l.name).ToArray();

        var entry = (ActorClassData)entrySelected;

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(400));
        DrawTitle();

        entry.name = EditorGUILayout.TextField("Name", entry.name);
        

        EditorGUILayoutExtensions.LabelFieldBold("Growth");
        entry.expCurve = EditorGUILayout.CurveField("Exp", entry.expCurve, GUILayout.Height(25f));
        entry.hpCurve = EditorGUILayout.CurveField("HP", entry.hpCurve, GUILayout.Height(25f));
        entry.mpCurve = EditorGUILayout.CurveField("MP", entry.mpCurve, GUILayout.Height(25f));
        entry.strCurve = EditorGUILayout.CurveField("Strength", entry.strCurve, GUILayout.Height(25f));
        entry.magCurve = EditorGUILayout.CurveField("Magic", entry.magCurve, GUILayout.Height(25f));
        entry.dexCurve = EditorGUILayout.CurveField("Dextery", entry.dexCurve, GUILayout.Height(25f));
        entry.lckCurve = EditorGUILayout.CurveField("Luck", entry.lckCurve, GUILayout.Height(25f));
        entry.defCurve = EditorGUILayout.CurveField("Defense", entry.defCurve, GUILayout.Height(25f));
        entry.resCurve = EditorGUILayout.CurveField("Resistance", entry.resCurve, GUILayout.Height(25f));

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
        var list = database.FetchEntry<AttributeSpecDataList>();
        var level = list.entries.First<AttributeSpecData>(x => x.name == "Level");
        var exp = list.entries.First<AttributeSpecData>(x => x.name == "Exp");
        
        var hp = list.entries.First<AttributeSpecData>(x => x.name == "HP");
        var mp = list.entries.First<AttributeSpecData>(x => x.name == "MP");
        var attr = list.entries.First<AttributeSpecData>(x => x.name == "Common");

        EditorGUILayoutExtensions.LabelFieldBold("Preview");
        previewLv = EditorGUILayout.IntSlider("Lv", previewLv, level.start, level.end);

        float normalizedValue = Mathf.InverseLerp(level.start, level.end, previewLv);
        float baseT = Mathf.Lerp(0, 1f, normalizedValue);
        float curveT = 1f;

        curveT = entry.expCurve.Evaluate(baseT);
        EditorGUILayout.LabelField($"Exp:\t{Mathf.Round(Mathf.Lerp(exp.start, exp.end, curveT))}");

        curveT = entry.hpCurve.Evaluate(baseT);
        EditorGUILayout.LabelField($"HP:\t{Mathf.Round(Mathf.Lerp(hp.start, hp.end, curveT))}");
        curveT = entry.mpCurve.Evaluate(baseT);
        EditorGUILayout.LabelField($"MP:\t{Mathf.Round(Mathf.Lerp(mp.end, mp.end, curveT))}");

        curveT = entry.strCurve.Evaluate(baseT);
        EditorGUILayout.LabelField($"Str:\t{Mathf.Round(Mathf.Lerp(attr.start, attr.end, curveT))}");
        curveT = entry.magCurve.Evaluate(baseT);
        EditorGUILayout.LabelField($"Mag:\t{Mathf.Round(Mathf.Lerp(attr.start, attr.end, curveT))}");

        curveT = entry.dexCurve.Evaluate(baseT);
        EditorGUILayout.LabelField($"Dex:\t{Mathf.Round(Mathf.Lerp(attr.start, attr.end, curveT))}");
        curveT = entry.dexCurve.Evaluate(baseT);
        EditorGUILayout.LabelField($"Lck:\t{Mathf.Round(Mathf.Lerp(attr.start, attr.end, curveT))}");
        curveT = entry.lckCurve.Evaluate(baseT);
        EditorGUILayout.LabelField($"Mag:\t{Mathf.Round(Mathf.Lerp(attr.start, attr.end, curveT))}");
        curveT = entry.resCurve.Evaluate(baseT);
        EditorGUILayout.LabelField($"Res:\t{Mathf.Round(Mathf.Lerp(attr.start, attr.end, curveT))}");
        EditorGUILayout.EndVertical();

        #region Weapons
        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
        EditorGUILayoutExtensions.LabelFieldBold("Weapons");
        entry.weaponsMask = EditorGUILayout.MaskField(entry.weaponsMask, weaponTypeNames);
        EditorGUILayout.EndVertical();
        #endregion

        #region Skills
        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
        vect = EditorGUILayout.BeginScrollView(vect, GUILayout.MinHeight(90));
        EditorGUILayoutExtensions.LabelFieldBold("Skills");
        if (entry.skills == null) entry.skills = new SkillUnlockArgs[1];
        var total = EditorGUILayout.IntField("Total:", entry.skills.Length);

        System.Array.Resize<SkillUnlockArgs>(ref entry.skills, total);

        for (int i = 0; i < entry.skills.Length; i++)
        {
            var skill = entry.skills[i];
            if (skill == null) skill = new SkillUnlockArgs();

            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal();
            skill.level = EditorGUILayout.IntField("Lv", skill.level);
            skill.skillId = EditorGUILayout.Popup(skill.skillId, skillNames);
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        #endregion


        EditorGUILayout.EndVertical();
    }

    

    private void DrawSkill()
    {
        var skill = (SkillData)entrySelected;

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(400));
        DrawTitle();

        skill.name = EditorGUILayout.TextField("Name", skill.name);
        skill.description = EditorGUILayout.TextField("Description", skill.description);
        skill.usage = (Usage)EditorGUILayout.EnumPopup("Usage", skill.usage);
        skill.scope = (Scope)EditorGUILayout.EnumPopup("Scope", skill.scope);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Use Cost");
        skill.hpCost = EditorGUILayout.FloatField("HP", skill.hpCost);
        skill.mpCost = EditorGUILayout.FloatField("MP", skill.mpCost);

        EditorGUILayout.EndVertical();
    }

    private void DrawItem()
    {
        var item = (ItemData)entrySelected;

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(400));
        DrawTitle();

        item.name = EditorGUILayout.TextField("Name", item.name);
        item.description = EditorGUILayout.TextField("Description", item.description);
        item.type = (ItemType)EditorGUILayout.EnumPopup("Type", item.type);
        item.price = EditorGUILayout.IntField("Price", item.price);
        item.usage = (Usage)EditorGUILayout.EnumPopup("Usage", item.usage);
        item.scope = (Scope)EditorGUILayout.EnumPopup("Scope", item.scope);

        EditorGUILayout.EndVertical();
    }

    private void DrawWeapon(DatabaseManager database)
    {
        var weaponTypeNames = database.FetchEntry<WeaponTypeDataList>().entries.Select(l => l.name).ToArray();

        var entry = (WeaponData)entrySelected;

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(400));
        DrawTitle();

        entry.name = EditorGUILayout.TextField("Name", entry.name);
        entry.description = EditorGUILayout.TextField("Description", entry.description);
        entry.typeId = EditorGUILayout.Popup("Type", entry.typeId, weaponTypeNames);
        entry.price = EditorGUILayout.IntField("Price", entry.price);

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
        EditorGUILayout.LabelField("Attribute Effects");
        entry.strIncrease = EditorGUILayout.IntField("Strength", entry.strIncrease);
        entry.magIncrease = EditorGUILayout.IntField("Magic", entry.magIncrease);

        entry.dexIncrease = EditorGUILayout.IntField("Dextery", entry.dexIncrease);
        entry.agiIncrease = EditorGUILayout.IntField("Agility", entry.agiIncrease);

        entry.lckIncrease = EditorGUILayout.IntField("Luck", entry.lckIncrease);
        entry.defIncrease = EditorGUILayout.IntField("Defense", entry.defIncrease);
        entry.resIncrease = EditorGUILayout.IntField("Resistance", entry.resIncrease);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
    }

    private void DrawWeaponType()
    {
        var entry = (WeaponTypeData)entrySelected;

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(400));

        DrawTitle();
        entry.name = EditorGUILayout.TextField("Name", entry.name);

        EditorGUILayout.EndVertical();
    }

    private void DrawAttributeSpec()
    {
        var entry = (AttributeSpecData)entrySelected;

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(400));

        DrawTitle();
        entry.start = EditorGUILayout.IntField("Start", entry.start);
        entry.end = EditorGUILayout.IntField("End", entry.end);

        float start = entry.start;
        float end = entry.end;
        
        EditorGUILayout.MinMaxSlider(ref start, ref end, 0, 9999);

        entry.start = (int)start;
        entry.end = (int)end;

        EditorGUILayout.EndVertical();
    }

}