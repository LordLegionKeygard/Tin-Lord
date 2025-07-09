using UnityEngine;

public class SkillTargetSystem : MonoBehaviour
{
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

    private void CancelSkillCircle()
    {
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
            _target.position = Vector3.Lerp(_target.position, hit.point, 10f * Time.unscaledDeltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            CustomEvents.FireUseTargetSkill();
            CancelSkillCircle();
        }
        if (Input.GetMouseButtonDown(1))
        {
            CancelSkillCircle();
        }
    }

    private void OnDisable()
    {
        CustomEvents.OnActiveTargetSkill -= ActiveSkillCircle;
    }
}


