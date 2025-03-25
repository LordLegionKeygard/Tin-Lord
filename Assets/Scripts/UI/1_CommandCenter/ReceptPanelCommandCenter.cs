using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReceptPanelCommandCenter : MonoBehaviour
{
    [SerializeField] private GameObject[] _receptCells;
    [SerializeField] private Image[] _receptCellResourceIcon;
    [SerializeField] private TextMeshProUGUI[] _receptCellResourceText;
    public void UpdateReceptView(ResourceRecept[] resourceRecept)
    {
        if (resourceRecept.Length == 0)
        {
            return;
        }

        ResetCells();

        for (int i = 0; i < resourceRecept.Length; i++)
        {
            _receptCells[i].SetActive(true);
            _receptCellResourceIcon[i].sprite = resourceRecept[i].ResourceForRecept.Icon;        
            _receptCellResourceText[i].text = $"{resourceRecept[i].ResourcesForReceptAmount}";
        }
    }

    private void ResetCells()
    {
        foreach (var item in _receptCells)
        {
            item.SetActive(false);
        }
    }
}
