using UnityEngine;

public class GroundTileColor : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer.material.color = CurrentMissionInfo.Instance.GetCurrentMission().GroundColor;
    }
}
