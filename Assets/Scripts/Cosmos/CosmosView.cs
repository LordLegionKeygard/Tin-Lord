using UnityEngine;

public class CosmosView : MonoBehaviour
{
    [SerializeField] private Light _directionalLight;
    [SerializeField] private GameObject _planetObject;
    [SerializeField] private MeshRenderer _planetRenderer;
    [SerializeField] private Material _prologueSkybox;

    public int ChangeCosmos(CosmosVariations[] cosmosVariations, int forcedIndex = -1)
    {
        int id = forcedIndex < 0 ? Random.Range(0, cosmosVariations.Length) : Mathf.Clamp(forcedIndex, 0, cosmosVariations.Length - 1);

        var v = cosmosVariations[id];

        _planetRenderer.material.mainTexture = v.PlanetTexture;
        _planetObject.transform.localPosition = v.PlanetPosition;
        _planetObject.transform.localRotation = Quaternion.Euler(v.PlanetRotation);
        _planetObject.SetActive(true);

        _directionalLight.transform.rotation = Quaternion.Euler(v.LightRotation);
        _directionalLight.colorTemperature = v.Temperature;
        _directionalLight.useColorTemperature = true;

        RenderSettings.skybox = v.CosmosSkybox;
        RenderSettings.skybox.SetFloat("_Rotation", v.SkyboxRotation);
        DynamicGI.UpdateEnvironment();

        return id;
    }

    public void SetDefaultCosmos()
    {
        _planetObject.SetActive(false);
        RenderSettings.skybox = _prologueSkybox;
        DynamicGI.UpdateEnvironment();
    }
}
