using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StepChoice))]
public class StepChoiceDrawer : PropertyDrawer
{
    const float PadY = 2f;

    //------------------------------------------------------------------
    // GUI
    //------------------------------------------------------------------
    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
    {
        // ── Foldout
        prop.isExpanded = EditorGUI.Foldout(
            new Rect(pos.x, pos.y, pos.width, EditorGUIUtility.singleLineHeight),
            prop.isExpanded, label, true);

        if (!prop.isExpanded) return;

        EditorGUI.indentLevel++;
        float y = pos.y + EditorGUIUtility.singleLineHeight + PadY;

        // ── Choice text number
        var choiceText = prop.FindPropertyRelative(nameof(StepChoice.ChoiseTextNumber));
        EditorGUI.PropertyField(
            new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
            choiceText, new GUIContent("Choice Text"));
        y += EditorGUIUtility.singleLineHeight + PadY;

        // ── Kind
        var kindProp = prop.FindPropertyRelative(nameof(StepChoice.Kind));
        EditorGUI.PropertyField(
            new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
            kindProp);
        y += EditorGUIUtility.singleLineHeight + PadY;

        var kind = (ChoiceKind)kindProp.enumValueIndex;
        var stdProp = prop.FindPropertyRelative(nameof(StepChoice.Standard));
        var chanceProp = prop.FindPropertyRelative(nameof(StepChoice.Chance));

        if (kind == ChoiceKind.Standard)
        {
            // ── Next step
            var nextProp = stdProp.FindPropertyRelative(nameof(StandardChoiceData.NextStepIndex));
            EditorGUI.PropertyField(
                new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
                nextProp, new GUIContent("Next Step Index"));
            y += EditorGUIUtility.singleLineHeight + PadY;

            // ── Rewards
            var rewardsProp = stdProp.FindPropertyRelative(nameof(StandardChoiceData.Rewards));
            float h = EditorGUI.GetPropertyHeight(rewardsProp, true);
            EditorGUI.PropertyField(
                new Rect(pos.x, y, pos.width, h),
                rewardsProp, true);
            y += h + PadY;

            // ── Choice Required (NEW)
            var reqProp = stdProp.FindPropertyRelative(nameof(StandardChoiceData.ChoiceRequired));
            float hReq = EditorGUI.GetPropertyHeight(reqProp, true);
            EditorGUI.PropertyField(
                new Rect(pos.x, y, pos.width, hReq),
                reqProp, true);
            y += hReq + PadY;
        }
        else    // ---------------- Chance branch ----------------
        {
            var succChance = chanceProp.FindPropertyRelative(nameof(ChanceChoiceData.SuccessChance));
            EditorGUI.PropertyField(
                new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
                succChance);
            y += EditorGUIUtility.singleLineHeight + PadY;

            var succText = chanceProp.FindPropertyRelative(nameof(ChanceChoiceData.SuccessTextNumber));
            EditorGUI.PropertyField(
                new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
                succText, new GUIContent("Success Text"));
            y += EditorGUIUtility.singleLineHeight + PadY;

            var failText = chanceProp.FindPropertyRelative(nameof(ChanceChoiceData.FailureTextNumber));
            EditorGUI.PropertyField(
                new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
                failText, new GUIContent("Failure Text"));
            y += EditorGUIUtility.singleLineHeight + PadY;

            var succRewards = chanceProp.FindPropertyRelative(nameof(ChanceChoiceData.SuccessRewards));
            float h1 = EditorGUI.GetPropertyHeight(succRewards, true);
            EditorGUI.PropertyField(
                new Rect(pos.x, y, pos.width, h1),
                succRewards, true);
            y += h1 + PadY;

            var failRewards = chanceProp.FindPropertyRelative(nameof(ChanceChoiceData.FailureRewards));
            float h2 = EditorGUI.GetPropertyHeight(failRewards, true);
            EditorGUI.PropertyField(
                new Rect(pos.x, y, pos.width, h2),
                failRewards, true);
            y += h2 + PadY;
        }

        EditorGUI.indentLevel--;
    }

    //------------------------------------------------------------------
    // Height
    //------------------------------------------------------------------
    public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
    {
        float h = EditorGUIUtility.singleLineHeight;             // foldout
        if (!prop.isExpanded) return h;

        h += PadY + EditorGUIUtility.singleLineHeight;           // choice text
        h += PadY + EditorGUIUtility.singleLineHeight;           // kind

        var kindProp = prop.FindPropertyRelative(nameof(StepChoice.Kind));
        var kind = (ChoiceKind)kindProp.enumValueIndex;
        var stdProp = prop.FindPropertyRelative(nameof(StepChoice.Standard));
        var chanceProp = prop.FindPropertyRelative(nameof(StepChoice.Chance));

        if (kind == ChoiceKind.Standard)
        {
            h += PadY + EditorGUIUtility.singleLineHeight;       // next step
            var rewardsProp = stdProp.FindPropertyRelative(nameof(StandardChoiceData.Rewards));
            h += PadY + EditorGUI.GetPropertyHeight(rewardsProp, true);

            var reqProp = stdProp.FindPropertyRelative(nameof(StandardChoiceData.ChoiceRequired));
            h += PadY + EditorGUI.GetPropertyHeight(reqProp, true);
        }
        else
        {
            h += PadY + EditorGUIUtility.singleLineHeight * 3;   // chance + 2 text fields
            var succRewards = chanceProp.FindPropertyRelative(nameof(ChanceChoiceData.SuccessRewards));
            h += PadY + EditorGUI.GetPropertyHeight(succRewards, true);
            var failRewards = chanceProp.FindPropertyRelative(nameof(ChanceChoiceData.FailureRewards));
            h += PadY + EditorGUI.GetPropertyHeight(failRewards, true);
        }

        return h + PadY;
    }
}
