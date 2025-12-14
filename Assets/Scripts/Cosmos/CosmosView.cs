using UnityEngine;

public class CosmosView : MonoBehaviour
{
    [SerializeField] private Light _directionalLight;
    [SerializeField] private Transform _parent;
    private GameObject _currentPlanetObject;
    private GameObject _currentEnvironmentObject;

    public int ChangeCosmos(CosmosVariations[] cosmosVariations, int forcedIndex = -1)
    {
        int id = forcedIndex < 0 ? Random.Range(0, cosmosVariations.Length) : Mathf.Clamp(forcedIndex, 0, cosmosVariations.Length - 1);

        var variations = cosmosVariations[id];

        if (_currentPlanetObject != null) Destroy(_currentPlanetObject);
        if(_currentEnvironmentObject != null) Destroy(_currentEnvironmentObject);


        if (variations.PlanetPrefab != null)
        {
            var planet = Instantiate(variations.PlanetPrefab, _parent);
            _currentPlanetObject = planet;
            _currentPlanetObject.transform.localPosition = variations.PlanetPosition;
            _currentPlanetObject.transform.localRotation = Quaternion.Euler(variations.PlanetRotation);
        }

        if (variations.EnvironmentPrefab != null)
        {
            var environment = Instantiate(variations.EnvironmentPrefab, _parent);
            _currentEnvironmentObject = environment;
            _currentEnvironmentObject.transform.localPosition = variations.EnvironmentPosition;
            _currentEnvironmentObject.transform.localRotation = Quaternion.Euler(variations.EnvironmentRotation);
        }

        _directionalLight.transform.rotation = Quaternion.Euler(variations.LightRotation);
        _directionalLight.colorTemperature = variations.Temperature;
        _directionalLight.useColorTemperature = true;

        RenderSettings.skybox = variations.CosmosSkybox;
        RenderSettings.skybox.SetFloat("_Rotation", variations.SkyboxRotation);
        DynamicGI.UpdateEnvironment();

        return id;
    }
}
