using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventNodePanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private EventNodeButton _buttonPrefab;
    [SerializeField] private Transform _buttonsHolder;
    private Stack<EventStep> _stack = new();   // стек пройденных шагов

    public void Open(EventNode node)
    {
        _stack.Clear();
        _stack.Push(node.RootStep);
        ShowStep(node.RootStep);
        gameObject.SetActive(true);
    }

    private void ShowStep(EventStep step)
    {
        _mainText.text = step.Text;

        foreach (Transform c in _buttonsHolder) Destroy(c.gameObject);

        for (int i = 0; i < step.Choices.Count && i < step.Choices.Count; i++)
        {
            var choice = step.Choices[i];
            var btn = Instantiate(_buttonPrefab, _buttonsHolder);
            btn.Setup(choice.Caption, () => OnChoice(choice));
        }
    }

    private void OnChoice(EventChoice choice)
    {
        foreach (var r in choice.Rewards)
            GrantReward(r);

        if (choice.NextStep == null)
        {
            Close();            // финал
        }
        else
        {
            _stack.Push(choice.NextStep);
            ShowStep(choice.NextStep);
        }
    }

    private void GrantReward(EventReward r)
    {
        switch (r.Type)
        {
            case RewardType.None:
                break;
        }

        // TODO: сохранение прогресса
    }

    public void Close() => gameObject.SetActive(false);
}
