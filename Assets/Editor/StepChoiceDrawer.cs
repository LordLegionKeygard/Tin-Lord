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
        // Сохраняем глобальные значения и обязательно восстанавливаем в finally
        int oldIndent = EditorGUI.indentLevel;
        float oldLabelWidth = EditorGUIUtility.labelWidth;

        try
        {
            // Заголовок (foldout)
            var line = new Rect(pos.x, pos.y, pos.width, EditorGUIUtility.singleLineHeight);
            prop.isExpanded = EditorGUI.Foldout(line, prop.isExpanded, label, true);
            if (!prop.isExpanded) return;

            float y = line.yMax + PadY;

            // Весь контент рисуем внутри scope — он сам вернёт indent обратно
            using (new EditorGUI.IndentLevelScope())
            {
                // Choice text number
                var choiceText = prop.FindPropertyRelative(nameof(StepChoice.ChoiseTextNumber));
                EditorGUI.PropertyField(new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
                                        choiceText, new GUIContent("Choice Text"));
                y += EditorGUIUtility.singleLineHeight + PadY;

                // Kind
                var kindProp = prop.FindPropertyRelative(nameof(StepChoice.Kind));
                EditorGUI.PropertyField(new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
                                        kindProp);
                y += EditorGUIUtility.singleLineHeight + PadY;

                // ссылки на вложенные блоки
                var stdProp = prop.FindPropertyRelative(nameof(StepChoice.Standard));
                var chanceProp = prop.FindPropertyRelative(nameof(StepChoice.Chance));
                var randProp = prop.FindPropertyRelative(nameof(StepChoice.Random));

                switch ((ChoiceKind)kindProp.enumValueIndex)
                {
                    case ChoiceKind.Standard:
                        {
                            // Next step
                            var nextProp = stdProp.FindPropertyRelative(nameof(StandardChoiceData.NextStepIndex));
                            EditorGUI.PropertyField(new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
                                                    nextProp, new GUIContent("Next Step Index"));
                            y += EditorGUIUtility.singleLineHeight + PadY;

                            // Rewards
                            var rewardsProp = stdProp.FindPropertyRelative(nameof(StandardChoiceData.Rewards));
                            float h = EditorGUI.GetPropertyHeight(rewardsProp, true);
                            EditorGUI.PropertyField(new Rect(pos.x, y, pos.width, h), rewardsProp, true);
                            y += h + PadY;

                            // Choice Required
                            var reqProp = stdProp.FindPropertyRelative(nameof(StandardChoiceData.ChoiceRequired));
                            float hReq = EditorGUI.GetPropertyHeight(reqProp, true);
                            EditorGUI.PropertyField(new Rect(pos.x, y, pos.width, hReq), reqProp, true);
                            y += hReq + PadY;
                            break;
                        }

                    case ChoiceKind.Chance:
                        {
                            var succText = chanceProp.FindPropertyRelative(nameof(ChanceChoiceData.SuccessTextNumber));
                            EditorGUI.PropertyField(new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
                                                    succText, new GUIContent("Success Text"));
                            y += EditorGUIUtility.singleLineHeight + PadY;

                            var failText = chanceProp.FindPropertyRelative(nameof(ChanceChoiceData.FailureTextNumber));
                            EditorGUI.PropertyField(new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
                                                    failText, new GUIContent("Failure Text"));
                            y += EditorGUIUtility.singleLineHeight + PadY;

                            var succRewards = chanceProp.FindPropertyRelative(nameof(ChanceChoiceData.SuccessRewards));
                            float h1 = EditorGUI.GetPropertyHeight(succRewards, true);
                            EditorGUI.PropertyField(new Rect(pos.x, y, pos.width, h1), succRewards, true);
                            y += h1 + PadY;

                            var failRewards = chanceProp.FindPropertyRelative(nameof(ChanceChoiceData.FailureRewards));
                            float h2 = EditorGUI.GetPropertyHeight(failRewards, true);
                            EditorGUI.PropertyField(new Rect(pos.x, y, pos.width, h2), failRewards, true);
                            y += h2 + PadY;
                            break;
                        }

                    case ChoiceKind.Random:
                        {
                            var listProp = randProp.FindPropertyRelative(nameof(RandomChoiceData.PossibleRewards));
                            float hList = EditorGUI.GetPropertyHeight(listProp, true);
                            EditorGUI.PropertyField(new Rect(pos.x, y, pos.width, hList), listProp, true);
                            y += hList + PadY;

                            var rcProp = randProp.FindPropertyRelative(nameof(RandomChoiceData.RewardCount));

                            // Фолдаут блока Reward Count
                            rcProp.isExpanded = EditorGUI.Foldout(
                                new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
                                rcProp.isExpanded, new GUIContent("Reward Count"), true);
                            y += EditorGUIUtility.singleLineHeight + PadY;

                            if (rcProp.isExpanded)
                            {
                                using (new EditorGUI.IndentLevelScope())
                                {
                                    var signProp = rcProp.FindPropertyRelative(nameof(RewardCount.PlusMinusEnum));

                                    EditorGUI.PropertyField(new Rect(pos.x, y, pos.width, EditorGUIUtility.singleLineHeight),
                                                            signProp, new GUIContent("Plus / Minus"));
                                    y += EditorGUIUtility.singleLineHeight + PadY;
                                }
                            }
                            break;
                        }
                }
            }
        }
        finally
        {
            EditorGUI.indentLevel = oldIndent;
            EditorGUIUtility.labelWidth = oldLabelWidth;
        }
    }


    public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
    {
        float h = EditorGUIUtility.singleLineHeight;
        if (!prop.isExpanded) return h;

        h += PadY + EditorGUIUtility.singleLineHeight;
        h += PadY + EditorGUIUtility.singleLineHeight;

        var kind = (ChoiceKind)prop.FindPropertyRelative(nameof(StepChoice.Kind)).enumValueIndex;
        var stdProp = prop.FindPropertyRelative(nameof(StepChoice.Standard));
        var chProp = prop.FindPropertyRelative(nameof(StepChoice.Chance));
        var rdProp = prop.FindPropertyRelative(nameof(StepChoice.Random));

        switch (kind)
        {
            case ChoiceKind.Standard:
                {
                    h += PadY + EditorGUIUtility.singleLineHeight;
                    var rewardsProp = stdProp.FindPropertyRelative(nameof(StandardChoiceData.Rewards));
                    h += PadY + EditorGUI.GetPropertyHeight(rewardsProp, true);

                    var reqProp = stdProp.FindPropertyRelative(nameof(StandardChoiceData.ChoiceRequired));
                    h += PadY + EditorGUI.GetPropertyHeight(reqProp, true);
                    break;
                }

            case ChoiceKind.Chance:
                {
                    h += PadY + EditorGUIUtility.singleLineHeight * 3; // succ/fail labels + spacer
                    var succ = chProp.FindPropertyRelative(nameof(ChanceChoiceData.SuccessRewards));
                    h += PadY + EditorGUI.GetPropertyHeight(succ, true);
                    var fail = chProp.FindPropertyRelative(nameof(ChanceChoiceData.FailureRewards));
                    h += PadY + EditorGUI.GetPropertyHeight(fail, true);
                    break;
                }
            case ChoiceKind.Random:
                {
                    h += PadY + EditorGUIUtility.singleLineHeight;  // next step
                    var listProp = rdProp.FindPropertyRelative(nameof(RandomChoiceData.PossibleRewards));
                    h += PadY + EditorGUI.GetPropertyHeight(listProp, true);

                    // RewardCount: фолдаут
                    h += PadY + EditorGUIUtility.singleLineHeight;

                    var rcProp = rdProp.FindPropertyRelative(nameof(RandomChoiceData.RewardCount));
                    if (rcProp.isExpanded)
                    {
                        // две строки (AmountEnum + Sign)
                        h += PadY + EditorGUIUtility.singleLineHeight;
                    }
                    break;
                }
        }
        return h + PadY;
    }
}
