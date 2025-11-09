using UnityEngine;

public class ActiveBiomMaterialType : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private MeshRenderer[] _meshes;

    private void Start()
    {
        ActiveType();
    }

    private void ActiveType()
    {
        var landscape = CurrentMissionInfo.Instance.GetCurrentLandscape();
        if (landscape == null) return;
        var biomEnum = landscape.MissionView.BiomEnum;
        var matNamber = biomEnum == BiomEnum.Winter ? 1 : 0;

        foreach (var item in _meshes)
        {
            item.material = _materials[matNamber];
        }
    }
}
