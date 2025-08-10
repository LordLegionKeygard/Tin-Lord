using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

public class UnityGamingServiceInit : MonoBehaviour
{
    async void Awake()
    {
        try
        {
            var opts = new InitializationOptions()
                .SetEnvironmentName("production"); // или свой env, если используешь
            await UnityServices.InitializeAsync(opts);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"UGS init failed: {e}");
        }
    }
}
