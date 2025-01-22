using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PrologueText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _prologueText;

    // Индексы в массиве Language.TextStatic, откуда берём тексты
    private int[] _prologueTextIndices = new int[7] { 69, 70, 71, 72, 73, 74, 75 };

    private void Start()
    {
        StartCoroutine(PrologueRoutine());
    }

    private IEnumerator PrologueRoutine()
    {
        yield return new WaitForSeconds(20f);

        // Проходим по всем индексам
        for (int i = 0; i < _prologueTextIndices.Length; i++)
        {
            // Убедимся, что альфа выставлена в 0 перед показом очередного текста
            _prologueText.alpha = 0f;

            // Устанавливаем текст по текущему индексу
            _prologueText.text = Language.TextStatic[_prologueTextIndices[i]];

            // 1) Плавно поднимаем альфа канал с 0 до 1 за 2 секунды
            yield return _prologueText.DOFade(1f, 4f).WaitForCompletion();

            // 2) Ждём 5 секунд с показанным текстом
            yield return new WaitForSeconds(5f);

            // 3) Плавно убираем альфа канал с 1 до 0 за 2 секунды
            yield return _prologueText.DOFade(0f, 2f).WaitForCompletion();

            // 4) Ждём 1 секунду перед переходом к следующему тексту
            yield return new WaitForSeconds(1f);
        }
    }
}
