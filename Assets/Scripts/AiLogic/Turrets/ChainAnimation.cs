using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChainAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _chain;
    [SerializeField] private Material _mat;

    private void Start()
    {
        _mat = _chain.GetComponent<MeshRenderer>().material;
    }

    public void AnimChain()
    {
        _mat.DOOffset(new Vector2(0, 0.4f), 0.7f).OnComplete(() => ShootAnim());
    }

    private void ShootAnim()
    {
        _mat.DOOffset(new Vector2(0, 0.27f), 0.1f).OnComplete(() => RechargeAnim());
    }

    private void RechargeAnim()
    {
        _mat.DOOffset(new Vector3(0, 0.373f), 2f);
    }
}
