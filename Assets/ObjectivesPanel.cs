using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectivesPanel : MonoBehaviour
{
    [SerializeField] private GameObject _objectiveItemPrefab;
    [SerializeField] private Transform _parentTransform;
    private List<ObjectiveForListData> _objectivesForList = new();

    private void Start()
    {
        CustomEvents.OnObjectiveAmountChange += UpdateAmount;
    }

    public int[] GetAllObjectivesAmount()
    {
        return _objectivesForList.Select(el => el.CurrentAmount).ToArray();
    }

    public void LoadObjectiveItems(int[] objectivesAmount, bool IsStartMission)
    {
        var objectives = CurrentMissionInfo.Instance.CurrentMission().Objectives;

        for (int i = 0; i < objectives.Length; i++)
        {
            var objectiveAmount = IsStartMission ? 0 : objectivesAmount[i];
            var item = Instantiate(_objectiveItemPrefab, _parentTransform.position, Quaternion.identity);
            item.transform.SetParent(_parentTransform);
            var objectiveItem = item.GetComponent<ObjectiveItem>();
            var complete = objectiveAmount >= objectives[i].ObjectiveAmount;
            objectiveItem.SetupItem(objectives[i], objectiveAmount, complete);

            _objectivesForList.Add(new ObjectiveForListData
            {
                ObjectiveItem = objectiveItem,
                ObjectiveEnum = objectives[i].ObjectiveEnum,
                CurrentAmount = objectiveAmount,
                NeedAmount = objectives[i].ObjectiveAmount,
                Complete = complete,
            });
        }
    }

    private void UpdateAmount(ObjectiveEnum objectiveEnum, int value)
    {
        var objective = _objectivesForList.Find(el => el.ObjectiveEnum == objectiveEnum);

        if (objective != null)
        {
            if (objectiveEnum is ObjectiveEnum.RestoreEcology or ObjectiveEnum.SurviveDays) objective.CurrentAmount = value;
            else objective.CurrentAmount += value;
            objective.Complete = objective.CurrentAmount >= objective.NeedAmount;
            objective.ObjectiveItem.UpdateText(objective.CurrentAmount, objective.Complete);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnObjectiveAmountChange -= UpdateAmount;
    }
}

[System.Serializable]
public class ObjectiveForListData
{
    public ObjectiveItem ObjectiveItem;
    public ObjectiveEnum ObjectiveEnum;
    public int CurrentAmount;
    public int NeedAmount;
    public bool Complete;

}
