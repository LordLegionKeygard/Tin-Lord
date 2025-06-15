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
        _mainText.text = step.Text[Language.LanguageNumber];

        foreach (Transform trans in _buttonsHolder) Destroy(trans.gameObject);

        int lang = Language.LanguageNumber;
        int visible = Mathf.Min(step.Choices.Count, 4);

        for (int i = 0; i < visible; i++)
        {
            var choice = step.Choices[i];
            string text = $"{i + 1}. {choice.ChoiseText[lang]}";

            var button = Instantiate(_buttonPrefab, _buttonsHolder);
            button.Setup(text, () => OnChoice(choice));
        }
    }

    private void OnChoice(EventChoice choice)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        // выдаём награды
        if (choice.Rewards != null)
        {
            foreach (var r in choice.Rewards) GrantReward(r);
        }

        if (choice.IsFinal || choice.NextStep == null)
        {
            Close();
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

    public void PlayerInputSelectNumber(int number)
    {
        if (!gameObject.activeInHierarchy) return;
        if (number < 1 || number > 4) return;

        var step = _stack.Peek();
        int idx = number - 1;

        if (idx < step.Choices.Count)
            OnChoice(step.Choices[idx]);
    }

    public void Close() => gameObject.SetActive(false);
}
