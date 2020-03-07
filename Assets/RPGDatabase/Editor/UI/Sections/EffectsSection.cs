using BrightLib.RPGDatabase.Runtime;
using UnityEditor;
using UnityEngine;

public class EffectsSection : Section
{
    public BaseData entrySelected;

    public EffectsSection()
    {
        _title = "Effect";
    }

    public override void Draw()
    {
        EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(400));
        DrawTitle();

        if (entrySelected is ItemData) DrawEffect(((ItemData)entrySelected).effect);
        else if (entrySelected is SkillData) DrawEffect(((SkillData)entrySelected).effect);

        if (entrySelected is ItemData) DrawEffectPreview(((ItemData)entrySelected).effect);
        else if (entrySelected is SkillData) DrawEffectPreview(((SkillData)entrySelected).effect);

        EditorGUILayout.EndVertical();
    }

    private void DrawEffect(EffectArgs effect)
    {
        effect.type = (EffectType)EditorGUILayout.EnumPopup("Type", effect.type);
        if(effect.type == EffectType.Recover)
        {
            DrawRecoverEffect(effect);
        }
        else if (effect.type == EffectType.Damage)
        {
            DrawDamageEffect(effect);
        }
        else if (effect.type == EffectType.ClearState)
        {
            effect.state = (ActorStatus)EditorGUILayout.EnumPopup("State", effect.state);
        }
    }

    private void DrawRecoverEffect(EffectArgs effect)
    {
        string[] validOptions = { ActorAttributeType.HP.ToString(), ActorAttributeType.MP.ToString() };
        int stringAttr = EditorGUILayout.Popup("Attribute", (int)effect.damage.attribute, validOptions);

        effect.recover.attribute = (ActorAttributeType)stringAttr;
        effect.recover.amountAsPerc = EditorGUILayout.Toggle("Use %", effect.recover.amountAsPerc);
        if (effect.recover.amountAsPerc)
        {
            effect.recover.amount = EditorGUILayout.Slider("Amount", effect.recover.amount, 0, 100f);
            effect.recover.amount = Mathf.Round(effect.recover.amount);
        }
        else
        {
            effect.recover.amount = EditorGUILayout.FloatField("Amount", effect.recover.amount);
        }
        effect.recover.varianceAsPerc = EditorGUILayout.Toggle("Use %", effect.recover.varianceAsPerc);
        if (effect.recover.varianceAsPerc)
        {
            effect.recover.variance = EditorGUILayout.Slider("Variance", effect.recover.variance, 0, 100f);
            effect.recover.variance = Mathf.Round(effect.recover.variance);
        }
        else
        {
            effect.recover.variance = EditorGUILayout.FloatField("Variance", effect.recover.variance);
        }
    }

    private void DrawDamageEffect(EffectArgs effect)
    {
        effect.damage.type = (DamageType)EditorGUILayout.EnumPopup("Damage", effect.damage.type); ;

        string[] validOptions = { ActorAttributeType.HP.ToString(), ActorAttributeType.MP.ToString() };
        int stringAttr = EditorGUILayout.Popup("Attribute", (int)effect.damage.attribute, validOptions);

        effect.damage.attribute = (ActorAttributeType)stringAttr;
        effect.damage.amountAsPerc = EditorGUILayout.Toggle("Use %", effect.damage.amountAsPerc);
        if (effect.damage.amountAsPerc)
        {
            effect.damage.amount = EditorGUILayout.Slider("Amount", effect.damage.amount, 0, 100f);
            effect.damage.amount = Mathf.Round(effect.damage.amount);
        }
        else
        {
            effect.damage.amount = EditorGUILayout.FloatField("Amount", effect.damage.amount);
        }
        effect.damage.varianceAsPerc = EditorGUILayout.Toggle("Use %", effect.damage.varianceAsPerc);
        if (effect.damage.varianceAsPerc)
        {
            effect.damage.variance = EditorGUILayout.Slider("Variance", effect.damage.variance, 0, 100f);
            effect.damage.variance = Mathf.Round(effect.damage.variance);
        }
        else
        {
            effect.damage.variance = EditorGUILayout.FloatField("Variance", effect.damage.variance);
        }


        effect.damage.canCritical = EditorGUILayout.Toggle("Critical", effect.damage.canCritical);
    }

    private void DrawEffectPreview(EffectArgs effect)
    {
        if(effect.type == EffectType.Recover)
        {
            EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
            var baseValue = " " + effect.recover.amount + "" + (effect.recover.amountAsPerc ? "%" : "");
            var variantValue = " " + effect.recover.variance + "" + (effect.recover.varianceAsPerc ? "%" : "");
            EditorGUILayout.LabelField($"{effect.type}=> [{baseValue} +- ({variantValue})]");
            EditorGUILayout.EndVertical();
        }
        else if (effect.type == EffectType.Damage)
        {
            EditorGUILayout.BeginVertical("GroupBox", GUILayout.Width(350));
            var baseValue = " " + effect.damage.amount + "" + (effect.damage.amountAsPerc ? "%" : "");
            var variantValue = " " + effect.damage.variance + "" + (effect.damage.varianceAsPerc ? "%" : "");
            EditorGUILayout.LabelField($"{effect.type}=> [{baseValue} +- ({variantValue})]");
            EditorGUILayout.EndVertical();
        }

    }
}
