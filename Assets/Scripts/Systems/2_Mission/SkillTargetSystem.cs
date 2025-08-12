using UnityEngine;
using Zenject;

public class SkillTargetSystem : MonoBehaviour
{
    [Inject] private readonly MissionModeSystem _missionModeSystem;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;
    [SerializeField] private bool _isActive;
    public Transform GetTargetTransform() => _target.transform;
    public bool IsActive() => _isActive;

    private void OnEnable()
    {
        CustomEvents.OnActiveTargetSkill += ActiveSkillCircle;
    }

    private void Update()
    {
        if (!_isActive) return;
        UpdateTargetPosition();
    }

    public void CancelSkillCircle()
    {
        if (!_missionModeSystem.IsPlanetMode()) return;
        _target.gameObject.SetActive(false);
        _isActive = false;
    }

    private void ActiveSkillCircle()
    {
        _target.position = Input.mousePosition;
        _target.gameObject.SetActive(true);
        _isActive = true;
    }

    private void UpdateTargetPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _maxDistance, _mask))
        {
            _target.position = hit.point;
        }
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUISystem.IsPointerOverUI)
        {
            CustomEvents.FireUseTargetSkill();
            CancelSkillCircle();
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnActiveTargetSkill -= ActiveSkillCircle;
    }
}


