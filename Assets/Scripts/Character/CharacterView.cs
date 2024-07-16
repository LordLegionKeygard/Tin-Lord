using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

/// <summary>
/// Включает необходимый инструмент и сетит IK для рук и ног
/// </summary>
public class CharacterView : MonoBehaviour
{
    [Header("ModelView")]
    [SerializeField] private GameObject[] _mechaModels;
    [SerializeField] private MechaViewWrapper[] _mechaViewWrapper;


    [Header("WorkView")]
    [SerializeField] private CharacterWorkType _currentCharacterWorkType;
    [SerializeField] private CharacterWorkWrapper[] _characterWorkWrapper;

    [Header("Ik")]
    [SerializeField] private ArmIK _leftArmIK;
    [SerializeField] private ArmIK _rightArmIK;
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
        if (_characterWorkWrapper[number].ActiveObject != null) _characterWorkWrapper[number].ActiveObject.SetActive(true);

        _leftArmIK.solver.arm.target = _characterWorkWrapper[number].LeftHandTarget;
        _leftArmIK.solver.SetIKPositionWeight(_characterWorkWrapper[number].LeftHandTarget == null ? 0 : 1);
        _leftArmIK.solver.SetRotationWeight(_characterWorkWrapper[number].LeftHandTarget == null ? 0 : 1);
        _leftArmIK.solver.arm.shoulderRotationWeight = _characterWorkWrapper[number].LeftShoulderRotationWeight;
        _leftArmIK.solver.arm.wristToPalmAxis = _characterWorkWrapper[number].LeftWristToPalmAxis;

        _rightArmIK.solver.arm.target = _characterWorkWrapper[number].RightHandTarget;
        _rightArmIK.solver.SetIKPositionWeight(_characterWorkWrapper[number].RightHandTarget == null ? 0 : 1);
        _rightArmIK.solver.SetRotationWeight(_characterWorkWrapper[number].RightHandTarget == null ? 0 : 1);
        _rightArmIK.solver.arm.shoulderRotationWeight = _characterWorkWrapper[number].RightShoulderRotationWeight;
        _rightArmIK.solver.arm.wristToPalmAxis = _characterWorkWrapper[number].RightWristToPalmAxis;

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

    [Header("Left")]
    public Transform LeftHandTarget;
    public Transform LeftLegTarget;
    public float LeftShoulderRotationWeight;
    public Vector3 LeftWristToPalmAxis;

    [Header("Right")]
    public Transform RightHandTarget;
    public Transform RightLegTarget;
    public float RightShoulderRotationWeight;
    public Vector3 RightWristToPalmAxis;
}

public enum CharacterWorkType
{
    None = 0,
    PickaxeMining = 1,
    ShovelDig = 2,
    AxeChop = 3,
    HoldPlank = 4,
    OilHandPump = 5,
    WellHandleRotate = 6,
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
