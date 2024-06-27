using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [Header("ModelView")]
    [SerializeField] private GameObject[] _mechaModels;
    [SerializeField] private MechaViewWrapper[] _mechaViewWrapper;


    [Header("WorkView")]
    [SerializeField] private CharacterWorkType _currentCharacterWorkType;
    [SerializeField] private CharacterWorkWrapper[] _characterWorkWrapper;

    private void Start()
    {
        ActiveObjects();
        SetRandomModel();
    }

    private void SetRandomModel()
    {
        var rndModel = Random.Range(0, _mechaModels.Length);
        var rndSkinColor = Random.Range(0, _mechaViewWrapper[rndModel].ViewMaterials.Length);
        _mechaModels[rndModel].SetActive(true);
        _mechaViewWrapper[rndModel].SkinnedMesh.materials = _mechaViewWrapper[rndModel].ViewMaterials[rndSkinColor].Materials;
    }

    private void ActiveObjects()
    {
        _characterWorkWrapper[(int)_currentCharacterWorkType - 1].ActiveObject.SetActive(true);
    }
}

[System.Serializable]
public class CharacterWorkWrapper
{
    public CharacterWorkType CharacterWorkType;
    public GameObject ActiveObject;
}

public enum CharacterWorkType
{
    None = 0,
    PickaxeMining = 1,
}

[System.Serializable]
public class MechaViewWrapper
{
    public SkinnedMeshRenderer SkinnedMesh;
    public MechaViewMaterials[] ViewMaterials;
}

[System.Serializable]
public class MechaViewMaterials
{
    public Material[] Materials;
}
