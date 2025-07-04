using UnityEngine;
using RootMotion.FinalIK;
using Zenject;

/// <summary>
/// Включает необходимый инструмент и сетит IK для рук и ног
/// </summary>
public class RobotBuildingView : MonoBehaviour
{
    [Inject] private MissionHangarSystem _missionHangarSystem;
    [SerializeField] private GameObject[] _robots;
    [SerializeField] private CharacterWorkWrapper[] _characterWorkWrapper;
    private RobotBuildingAnimator _robotBuildingAnimator;

    [Header("Ik")]
    [SerializeField] private ArmIK _leftArmIK;
    [SerializeField] private ArmIK _rightArmIK;
    private FullBodyBipedIK _fullBodyBipedIK;

    private void Awake()
    {
        _robotBuildingAnimator = GetComponent<RobotBuildingAnimator>();
        _fullBodyBipedIK = GetComponent<FullBodyBipedIK>();
    }

    private void Start()
    {
        SetWorkView();
        SetModel();
    }

    private void SetModel()
    {
        _robots[_missionHangarSystem.GetCurrentRobot()].SetActive(true);
    }

    private void SetWorkView()
    {
        var number = _robotBuildingAnimator.GetRobotWorkTypeView();
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
    StoneCuttingTable = 7,
    StoneCuttingWorkbrench = 8,
    StickMix = 9,
    ComponentsCraft = 10,
}
