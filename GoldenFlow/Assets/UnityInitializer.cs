using UnityEngine;
using Unity.Services.Core;


public class UnityInitializer : MonoBehaviour
{
    async void Start()
    {
        // Initialize Unity Services
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services initialized successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to initialize Unity Services: {e.Message}");
        }

    
    }
}
