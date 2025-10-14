using UnityEngine;
using Unity.Services.Core;

public class UnityGamingServiceInit : MonoBehaviour
{
    async void Awake()
    {
        try
        {
            var opts = new InitializationOptions();
            await UnityServices.InitializeAsync(opts);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"UGS init failed: {e}");
        }
    }
}
