using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PrologueText : MonoBehaviour
{
    [SerializeField] private PrologueSystem _prologueSystem;
    [SerializeField] private TextMeshProUGUI _prologueText;
    private int[] _prologueTextIndices = new int[6] { 69, 70, 71, 72, 73, 74 };

    private void Start()
    {
        StartCoroutine(PrologueRoutine());
    }

    private IEnumerator PrologueRoutine()
    {
        yield return new WaitForSeconds(12);

        for (int i = 0; i < _prologueTextIndices.Length; i++)
        {
            _prologueText.text = Language.TextStatic[_prologueTextIndices[i]];

            yield return _prologueText.DOFade(1, 4).WaitForCompletion();

            yield return new WaitForSeconds(4);

            yield return _prologueText.DOFade(0, 2).WaitForCompletion();

            yield return new WaitForSeconds(1f);
        }

        _prologueSystem.ActiveCanvas();
    }
}
