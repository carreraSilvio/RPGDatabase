using BrightLib.Utility;
using RPGDatabase.Runtime.Core;
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

    public override void Draw(RPGDatabaseManager database)
    {
        if (entrySelected is ActorData) DrawActor(database);
        else if (entrySelected is ActorClassData) DrawClass(database);
        else if (entrySelected is SkillData) DrawSkill();
        else if (entrySelected is ItemData) DrawItem();
        else if (entrySelected is WeaponData) DrawWeapon(database);
        else if (entrySelected is WeaponTypeData) DrawWeaponType();
        else if (entrySelected is AttributeSpecData) DrawAttributeSpec();
    }

    private void DrawActor(RPGDatabaseManager database)
    {
        var entry = (ActorData)entrySelected;

        var classNames = database.FetchEntry<ActorClassDataList>().entries.Select(l => l.name).ToArray();
        var classIds = database.FetchEntry<ActorClassDataList>().entries.Select(l => l.Id).ToArray();

        var actorClass = database.FetchEntry<ActorClassDataList>().entries.FirstOrDefault<ActorClassData>(l => l.Id == entry.classId);
        if (actorClass == null)
        {
            actorClass = new ActorClassData(entry.classId);
        }

        var weaponNames = database.FetchEntry<WeaponDataList>().entries.Where(l => l.typeId == actorClass.weaponTypeId).Select(l => l.name).ToArray();
        var weaponIds = database.FetchEntry<WeaponDataList>().entries.Where(l => l.typeId == actorClass.weaponTypeId).Select(l => l.Id).ToArray();

        var attrList = database.FetchEntry<AttributeSpecDataList>();
        var level = attrList.entries.First<AttributeSpecData>(x => x.name == "Level");

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(400));
        DrawTitle();

        entry.name = EditorGUILayout.TextField("Name", entry.name);
        entry.classId = EditorGUILayout.IntPopup("Class", entry.classId, classNames, classIds);
        entry.initialLevel = EditorGUILayout.IntSlider("Initial Level", entry.initialLevel, level.start, level.end);

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
        EditorGUILayoutUtility.LabelFieldBold("Initial Equipment");
        entry.initialWeapon = EditorGUILayout.IntPopup("Weapon", entry.initialWeapon, weaponNames, weaponIds);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
    }

    private int previewLv = 1;
    private Vector2 classAreaVect;
    private Vector2 skillUnlockVect;
    private void DrawClass(RPGDatabaseManager database)
    {
        var weaponTypeIds = database.FetchEntry<WeaponTypeDataList>().entries.Select(l => l.Id).ToArray();
        var weaponTypeNames = database.FetchEntry<WeaponTypeDataList>().entries.Select(l => l.name).ToArray();
        var skillIds = database.FetchEntry<SkillDataList>().entries.Select(l => l.Id).ToArray();
        var skillNames = database.FetchEntry<SkillDataList>().entries.Select(l => l.name).ToArray();

        var entry = (ActorClassData)entrySelected;

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(400));
        classAreaVect = EditorGUILayout.BeginScrollView(classAreaVect, GUILayout.Width(400));

        DrawTitle();
        entry.name = EditorGUILayout.TextField("Name", entry.name);


        #region Growth
        EditorGUILayoutUtility.LabelFieldBold("Growth");
        entry.expCurve = EditorGUILayout.CurveField("Exp", entry.expCurve, GUILayout.Height(25f));
        entry.hpCurve = EditorGUILayout.CurveField("HP", entry.hpCurve, GUILayout.Height(25f));
        entry.mpCurve = EditorGUILayout.CurveField("MP", entry.mpCurve, GUILayout.Height(25f));
        entry.strCurve = EditorGUILayout.CurveField("Strength", entry.strCurve, GUILayout.Height(25f));
        entry.magCurve = EditorGUILayout.CurveField("Magic", entry.magCurve, GUILayout.Height(25f));

        entry.dexCurve = EditorGUILayout.CurveField("Dextery", entry.dexCurve, GUILayout.Height(25f));
        entry.agiCurve = EditorGUILayout.CurveField("Agility", entry.agiCurve, GUILayout.Height(25f));
        entry.lckCurve = EditorGUILayout.CurveField("Luck", entry.lckCurve, GUILayout.Height(25f));

        entry.defCurve = EditorGUILayout.CurveField("Defense", entry.defCurve, GUILayout.Height(25f));
        entry.resCurve = EditorGUILayout.CurveField("Resistance", entry.resCurve, GUILayout.Height(25f));
        #endregion

        #region Preview
        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
        var list = database.FetchEntry<AttributeSpecDataList>();
        var level = list.entries.First<AttributeSpecData>(x => x.name == "Level");
        var exp = list.entries.First<AttributeSpecData>(x => x.name == "XP");

        var hp = list.entries.First<AttributeSpecData>(x => x.name == "HP");
        var mp = list.entries.First<AttributeSpecData>(x => x.name == "MP");
        var attr = list.entries.First<AttributeSpecData>(x => x.name == "Common");

        EditorGUILayoutUtility.LabelFieldBold("Preview");
        previewLv = EditorGUILayout.IntSlider("Lv", previewLv, level.start, level.end);

        float normalizedValue = Mathf.InverseLerp(level.start, level.end, previewLv);
        float targetCurveValue = 0f;

        targetCurveValue = entry.expCurve.Evaluate(normalizedValue);
        EditorGUILayout.LabelField($"Exp:\t{Mathf.Round(Mathf.Lerp(exp.start, exp.end, targetCurveValue))}");

        targetCurveValue = entry.hpCurve.Evaluate(normalizedValue);
        EditorGUILayout.LabelField($"HP:\t{Mathf.Round(Mathf.Lerp(hp.start, hp.end, targetCurveValue))}");
        targetCurveValue = entry.mpCurve.Evaluate(normalizedValue);
        EditorGUILayout.LabelField($"MP:\t{Mathf.Round(Mathf.Lerp(mp.end, mp.end, targetCurveValue))}");

        targetCurveValue = entry.strCurve.Evaluate(normalizedValue);
        EditorGUILayout.LabelField($"Str:\t{attr.FetchAtCurvePoint(targetCurveValue)}");
        targetCurveValue = entry.magCurve.Evaluate(normalizedValue);
        EditorGUILayout.LabelField($"Mag:\t{attr.FetchAtCurvePoint(targetCurveValue)}");

        targetCurveValue = entry.dexCurve.Evaluate(normalizedValue);
        EditorGUILayout.LabelField($"Dex:\t{attr.FetchAtCurvePoint(targetCurveValue)}");
        targetCurveValue = entry.agiCurve.Evaluate(normalizedValue);
        EditorGUILayout.LabelField($"Agi:\t{attr.FetchAtCurvePoint(targetCurveValue)}");
        targetCurveValue = entry.lckCurve.Evaluate(normalizedValue);
        EditorGUILayout.LabelField($"Lck:\t{attr.FetchAtCurvePoint(targetCurveValue)}");

        targetCurveValue = entry.defCurve.Evaluate(normalizedValue);
        EditorGUILayout.LabelField($"Def:\t{attr.FetchAtCurvePoint(targetCurveValue)}");
        targetCurveValue = entry.resCurve.Evaluate(normalizedValue);
        EditorGUILayout.LabelField($"Res:\t{attr.FetchAtCurvePoint(targetCurveValue)}");

        EditorGUILayout.EndVertical();
        #endregion

        #region Weapons
        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
        EditorGUILayoutUtility.LabelFieldBold("Weapon Type");
        entry.weaponTypeId = EditorGUILayout.IntPopup(entry.weaponTypeId, weaponTypeNames, weaponTypeIds);
        EditorGUILayout.EndVertical();
        #endregion

        #region Skills
        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
        skillUnlockVect = EditorGUILayout.BeginScrollView(skillUnlockVect, GUILayout.MinHeight(90));
        EditorGUILayoutUtility.LabelFieldBold("Skills");
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
            skill.skillId = EditorGUILayout.IntPopup(skill.skillId, skillNames, skillIds);
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        #endregion

        EditorGUILayout.EndScrollView();
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

    private void DrawWeapon(RPGDatabaseManager database)
    {
        var weaponTypeIds = database.FetchEntry<WeaponTypeDataList>().entries.Select(l => l.Id).ToArray();
        var weaponTypeNames = database.FetchEntry<WeaponTypeDataList>().entries.Select(l => l.name).ToArray();

        var entry = (WeaponData)entrySelected;

        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(400));
        DrawTitle();

        entry.name = EditorGUILayout.TextField("Name", entry.name);
        entry.description = EditorGUILayout.TextField("Description", entry.description);
        entry.typeId = EditorGUILayout.IntPopup("Type", entry.typeId, weaponTypeNames, weaponTypeIds);
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