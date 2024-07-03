using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class CharacterView : MonoBehaviour
{
    [Header("ModelView")]
    [SerializeField] private GameObject[] _mechaModels;
    [SerializeField] private MechaViewWrapper[] _mechaViewWrapper;


    [Header("WorkView")]
    [SerializeField] private CharacterWorkType _currentCharacterWorkType;
    [SerializeField] private CharacterWorkWrapper[] _characterWorkWrapper;

    [Header("Ik")]
    [SerializeField] private ArmIK _armIK;
    [SerializeField] private FullBodyBipedIK _fullBodyBipedIK;


    private void Start()
    {
        SetWorkView();
        SetRandomModel();
    }

    private void SetRandomModel()
    {
        var rndModel = Random.Range(0, _mechaModels.Length);
        var rndSkinColor = Random.Range(0, _mechaViewWrapper[rndModel].ViewMaterials.Length);
        _mechaModels[rndModel].SetActive(true);
        _mechaViewWrapper[rndModel].SkinnedMesh.materials = _mechaViewWrapper[rndModel].ViewMaterials[rndSkinColor].Materials;
    }

    private void SetWorkView()
    {
        var number = (int)_currentCharacterWorkType - 1;
        _characterWorkWrapper[number].ActiveObject.SetActive(true);
        _armIK.solver.arm.target = _characterWorkWrapper[number].LeftHandTarget;

        _fullBodyBipedIK.solver.leftFootEffector.target = _characterWorkWrapper[number].LeftLegTarget;
        _fullBodyBipedIK.solver.leftFootEffector.positionWeight = _characterWorkWrapper[number].LeftLegTarget == null ? 0 : 1;
        _fullBodyBipedIK.solver.leftFootEffector.rotationWeight = _characterWorkWrapper[number].LeftLegTarget == null ? 0 : 1;

        _fullBodyBipedIK.solver.rightFootEffector.target = _characterWorkWrapper[number].RightLegTarget;
        _fullBodyBipedIK.solver.rightFootEffector.positionWeight = _characterWorkWrapper[number].RightLegTarget == null ? 0 : 1;
        _fullBodyBipedIK.solver.rightFootEffector.rotationWeight = _characterWorkWrapper[number].RightLegTarget == null ? 0 : 1;
    }
}

[System.Serializable]
public class CharacterWorkWrapper
{
    public CharacterWorkType CharacterWorkType;
    public GameObject ActiveObject;
    public Transform LeftHandTarget;
    public Transform LeftLegTarget;
    public Transform RightLegTarget;
}

public enum CharacterWorkType
{
    None = 0,
    PickaxeMining = 1,
    ShovelDig = 2,
    AxeChop = 3,
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
