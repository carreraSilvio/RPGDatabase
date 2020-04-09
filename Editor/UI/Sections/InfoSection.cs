using BrightLib.RPGDatabase.Runtime;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BrightLib.RPGDatabase.Editor
{

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

            var actorClass = database.FetchEntry<ActorClassDataList>().entries.FirstOrDefault(l => l.Id == entry.classId);
            if (actorClass == null)
            {
                actorClass = new ActorClassData(entry.classId);
            }

            var weaponNames = database.FetchEntry<WeaponDataList>().entries.Where(l => l.typeId == actorClass.weaponTypeId).Select(l => l.name).ToArray();
            var weaponIds = database.FetchEntry<WeaponDataList>().entries.Where(l => l.typeId == actorClass.weaponTypeId).Select(l => l.Id).ToArray();

            var attrList = database.FetchEntry<AttributeSpecDataList>();
            var level = attrList.entries.First(x => x.name == "Level");

            EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(400));
            DrawTitle();

            entry.name = EditorGUILayout.TextField("Name", entry.name);
            entry.classId = EditorGUILayout.IntPopup("Class", entry.classId, classNames, classIds);
            entry.initialLevel = EditorGUILayout.IntSlider("Initial Level", entry.initialLevel, level.start, level.end);

            EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
            BrightEditorGUILayout.LabelFieldBold("Initial Equipment");
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
            BrightEditorGUILayout.LabelFieldBold("Growth");
            entry.expCurve = EditorGUILayout.CurveField("Exp", entry.expCurve, GUILayout.Height(25f));
            entry.hpCurve = EditorGUILayout.CurveField("HP", entry.hpCurve, GUILayout.Height(25f));
            entry.mpCurve = EditorGUILayout.CurveField("MP", entry.mpCurve, GUILayout.Height(25f));

            entry.strCurve = EditorGUILayout.CurveField(Constants.Attributes.STR, entry.strCurve, GUILayout.Height(25f));
            entry.intCurve = EditorGUILayout.CurveField(Constants.Attributes.INT, entry.intCurve, GUILayout.Height(25f));

            entry.dexCurve = EditorGUILayout.CurveField(Constants.Attributes.DEX, entry.dexCurve, GUILayout.Height(25f));
            entry.agiCurve = EditorGUILayout.CurveField(Constants.Attributes.AGI, entry.agiCurve, GUILayout.Height(25f));
            entry.lckCurve = EditorGUILayout.CurveField(Constants.Attributes.LCK, entry.lckCurve, GUILayout.Height(25f));

            entry.defCurve = EditorGUILayout.CurveField(Constants.Attributes.DEF, entry.defCurve, GUILayout.Height(25f));
            entry.resCurve = EditorGUILayout.CurveField(Constants.Attributes.RES, entry.resCurve, GUILayout.Height(25f));
            #endregion

            #region Preview
            EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
            var list = database.FetchEntry<AttributeSpecDataList>();
            var level = list.entries.First(x => x.name == "Level");
            var exp = list.entries.First(x => x.name == "XP");

            var hp = list.entries.First(x => x.name == "HP");
            var mp = list.entries.First(x => x.name == "MP");
            var attr = list.entries.First(x => x.name == "Common");

            BrightEditorGUILayout.LabelFieldBold("Preview");
            previewLv = EditorGUILayout.IntSlider("Lv", previewLv, level.start, level.end);

            float normalizedValue = Mathf.InverseLerp(level.start, level.end, previewLv);
            float targetCurveValue = 0f;

            targetCurveValue = entry.expCurve.Evaluate(normalizedValue);
            EditorGUILayout.LabelField($"Exp:\t{exp.FetchAtCurvePoint(targetCurveValue)}");

            targetCurveValue = entry.hpCurve.Evaluate(normalizedValue);
            EditorGUILayout.LabelField($"HP:\t{hp.FetchAtCurvePoint(targetCurveValue)}");
            targetCurveValue = entry.mpCurve.Evaluate(normalizedValue);
            EditorGUILayout.LabelField($"MP:\t{mp.FetchAtCurvePoint(targetCurveValue)}");

            targetCurveValue = entry.strCurve.Evaluate(normalizedValue);
            EditorGUILayout.LabelField($"{Constants.Attributes.STR_SHORT}:\t{attr.FetchAtCurvePoint(targetCurveValue)}");
            targetCurveValue = entry.intCurve.Evaluate(normalizedValue);
            EditorGUILayout.LabelField($"{Constants.Attributes.INT_SHORT}:\t{attr.FetchAtCurvePoint(targetCurveValue)}");

            targetCurveValue = entry.dexCurve.Evaluate(normalizedValue);
            EditorGUILayout.LabelField($"{Constants.Attributes.DEX_SHORT}:\t{attr.FetchAtCurvePoint(targetCurveValue)}");
            targetCurveValue = entry.agiCurve.Evaluate(normalizedValue);
            EditorGUILayout.LabelField($"{Constants.Attributes.AGI_SHORT}:\t{attr.FetchAtCurvePoint(targetCurveValue)}");

            targetCurveValue = entry.lckCurve.Evaluate(normalizedValue);
            EditorGUILayout.LabelField($"{Constants.Attributes.LCK_SHORT}:\t{attr.FetchAtCurvePoint(targetCurveValue)}");

            targetCurveValue = entry.defCurve.Evaluate(normalizedValue);
            EditorGUILayout.LabelField($"{Constants.Attributes.DEF_SHORT}:\t{attr.FetchAtCurvePoint(targetCurveValue)}");
            targetCurveValue = entry.resCurve.Evaluate(normalizedValue);
            EditorGUILayout.LabelField($"{Constants.Attributes.RES_SHORT}:\t{attr.FetchAtCurvePoint(targetCurveValue)}");

            EditorGUILayout.EndVertical();
            #endregion

            #region Weapons
            EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
            BrightEditorGUILayout.LabelFieldBold("Weapon Type");
            entry.weaponTypeId = EditorGUILayout.IntPopup(entry.weaponTypeId, weaponTypeNames, weaponTypeIds);
            EditorGUILayout.EndVertical();
            #endregion

            #region Skills
            EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
            skillUnlockVect = EditorGUILayout.BeginScrollView(skillUnlockVect, GUILayout.MinHeight(90));
            BrightEditorGUILayout.LabelFieldBold("Skills");
            if (entry.skills == null) entry.skills = new System.Collections.Generic.List<SkillUnlockArgs>();

            for (int i = 0; i < entry.skills.Count; i++)
            {
                var skillUnlockArgs = entry.skills[i];

                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                skillUnlockArgs.level = EditorGUILayout.IntField("Lv", skillUnlockArgs.level);
                skillUnlockArgs.skillId = EditorGUILayout.IntPopup(skillUnlockArgs.skillId, skillNames, skillIds);
                if (GUILayout.Button("-", GUILayout.Width(20f)))
                {
                    entry.skills.RemoveAt(i);
                    break;
                }
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+", GUILayout.Width(40f)))
            {
                entry.skills.Add(new SkillUnlockArgs());
            }
            EditorGUILayout.EndHorizontal();

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
            entry.strIncrease = EditorGUILayout.IntField(Constants.Attributes.STR, entry.strIncrease);
            entry.intIncrease = EditorGUILayout.IntField(Constants.Attributes.INT, entry.intIncrease);

            entry.dexIncrease = EditorGUILayout.IntField(Constants.Attributes.DEX, entry.dexIncrease);
            entry.agiIncrease = EditorGUILayout.IntField(Constants.Attributes.AGI, entry.agiIncrease);

            entry.lckIncrease = EditorGUILayout.IntField(Constants.Attributes.LCK, entry.lckIncrease);

            entry.defIncrease = EditorGUILayout.IntField(Constants.Attributes.DEF, entry.defIncrease);
            entry.resIncrease = EditorGUILayout.IntField(Constants.Attributes.RES, entry.resIncrease);
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
}