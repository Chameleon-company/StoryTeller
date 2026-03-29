using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class UnityServicesInit : MonoBehaviour
{
    public static bool IsInitialized = false;

    async void Start()
    {
        await InitializeUnityServices();
    }

    public async Task InitializeUnityServices()
    {
        if (IsInitialized) return;

        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services initialized successfully.");

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Signed in anonymously.");
                Debug.Log("Player ID: " + AuthenticationService.Instance.PlayerId);
            }

            IsInitialized = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Unity Services init failed: " + e.Message);
        }
    }
}