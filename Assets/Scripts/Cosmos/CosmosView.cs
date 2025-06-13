using UnityEngine;

public class CosmosView : MonoBehaviour
{
    [SerializeField] private Light _directionalLight;
    [SerializeField] private GameObject _planetObject;
    [SerializeField] private MeshRenderer _planetRenderer;

    public void ChangeCosmos(CosmosVariations[] cosmosVariations)
    {
        var randomIndex = Random.Range(0, cosmosVariations.Length);
        var cosmos = cosmosVariations[randomIndex];

        _planetRenderer.material.mainTexture = cosmos.PlanetTexture;

        _planetObject.transform.localPosition = cosmos.PlanetPosition;
        _planetObject.transform.localRotation = Quaternion.Euler(cosmos.PlanetRotation);

        RenderSettings.skybox = cosmos.CosmosSkybox;
        DynamicGI.UpdateEnvironment(); // обновить GI

        _directionalLight.transform.rotation = Quaternion.Euler(cosmos.LightRotation);
        _directionalLight.colorTemperature = cosmos.Temperature;
        _directionalLight.useColorTemperature = true;

    }
}
