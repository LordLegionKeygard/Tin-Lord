using UnityEngine;

public class CosmosView : MonoBehaviour
{
    [SerializeField] private Light _directionalLight;
    [SerializeField] private GameObject _planetObject;
    [SerializeField] private MeshRenderer _planetRenderer;

    public int ChangeCosmos(CosmosVariations[] cosmosVariations, int forcedIndex = -1)
    {
        int id = forcedIndex < 0 ? Random.Range(0, cosmosVariations.Length) : Mathf.Clamp(forcedIndex, 0, cosmosVariations.Length - 1);

        var variations = cosmosVariations[id];

        if (variations.PlanetTexture != null)
        {
            _planetRenderer.material.mainTexture = variations.PlanetTexture;
            _planetObject.transform.localPosition = variations.PlanetPosition;
            _planetObject.transform.localRotation = Quaternion.Euler(variations.PlanetRotation);
            _planetObject.SetActive(true);
        }
        else
        {
            _planetObject.SetActive(false);
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
