using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class DialogueValidator
{
    [Test]
    public void ValidateAllDialogues()
    {
        var guids = AssetDatabase.FindAssets("t:DialogueSequence");
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var dialogue = AssetDatabase.LoadAssetAtPath<DialogueSequence>(path);

            Assert.NotNull(dialogue, $"Dialogue at {path} is null");

            for (int i = 0; i < dialogue.Steps.Count; i++)
            {
                var step = dialogue.Steps[i];
                Assert.NotNull(step.Choices, $"{dialogue.name} step {i} has null Choices");

                foreach (var choice in step.Choices)
                {
                    if (choice.Kind == ChoiceKind.Standard && choice.Standard.NextStepIndex >= 0)
                    {
                        Assert.Less(choice.Standard.NextStepIndex, dialogue.Steps.Count,
                            $"{dialogue.name} step {i} choice points to invalid index {choice.Standard.NextStepIndex}");
                    }
                    if (choice.Kind == ChoiceKind.Random && choice.Random.NextStepIndex >= 0)
                    {
                        Assert.Less(choice.Random.NextStepIndex, dialogue.Steps.Count,
                            $"{dialogue.name} step {i} random choice points to invalid index {choice.Random.NextStepIndex}");
                    }
                }
            }
        }
    }
}
