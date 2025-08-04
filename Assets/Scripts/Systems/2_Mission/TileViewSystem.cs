using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileViewSystem : MonoBehaviour
{
    [SerializeField] private GameObject _attackStructureRadiusView;

    public void ActivateRadius(Transform targetTransform, float attackRadius = 0f)
    {
        _attackStructureRadiusView.transform.position = targetTransform.position;

        if (attackRadius > 0f)
        {
            float d = attackRadius * 2f;
            _attackStructureRadiusView.transform.localScale = new Vector3(d, 0.05f, d);
        }

        _attackStructureRadiusView.SetActive(true);
    }

    public void UnactiveRadius()
    {
        _attackStructureRadiusView.SetActive(false);
    }
}
