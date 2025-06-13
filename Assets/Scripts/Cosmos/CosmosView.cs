using UnityEngine;

public class CosmosView : MonoBehaviour
{
    [SerializeField] private Light _directionalLight;
    [SerializeField] private GameObject _planetObject;
    [SerializeField] private MeshRenderer _planetRenderer;

    [SerializeField] private Material _prologueSkybox;

    public void ChangeCosmos(CosmosVariations[] cosmosVariations)
    {
        var randomIndex = Random.Range(0, cosmosVariations.Length);
        var randomCosmos = cosmosVariations[randomIndex];

        _planetRenderer.material.mainTexture = randomCosmos.PlanetTexture;

        _planetObject.transform.localPosition = randomCosmos.PlanetPosition;
        _planetObject.transform.localRotation = Quaternion.Euler(randomCosmos.PlanetRotation);

        _directionalLight.transform.rotation = Quaternion.Euler(randomCosmos.LightRotation);
        _directionalLight.colorTemperature = randomCosmos.Temperature;
        _directionalLight.useColorTemperature = true;

        RenderSettings.skybox = randomCosmos.CosmosSkybox;
        RenderSettings.skybox.SetFloat("_Rotation", randomCosmos.SkyboxRotation);
        DynamicGI.UpdateEnvironment();
    }

    public void SetDefaultCosmos()
    {
        RenderSettings.skybox = _prologueSkybox;
        DynamicGI.UpdateEnvironment();
    }
}
