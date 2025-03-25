using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPManager : MonoBehaviour
{
    public CoinsManager coinsManager; // Reference to CoinsManager
    public GameManager gameManager;
    public AdsManager adsManager;

    private void Start()
    {
        // Ensure CoinsManager.Instance is assigned
        if (coinsManager == null)
        {
            coinsManager = CoinsManager.Instance;
        }
    }

    public void coin1Purchase()
    {
        AddCoins(50);
        Debug.Log("Purchased coin1");
    }

    public void coin2Purchase()
    {
        AddCoins(400);
        Debug.Log("Purchased coin2");
    }

    public void coin3Purchase()
    {
        AddCoins(900);
        Debug.Log("Purchased coin3");
    }

    public void coin4Purchase()
    {
        AddCoins(1500);
        Debug.Log("Purchased coin4");
    }

    public void coin5Purchase()
    {
        AddCoins(2800);
        Debug.Log("Purchased coin5");
    }

    public void coin6Purchase()
    {
        AddCoins(6000);
        Debug.Log("Purchased coin6");
    }

    private void AddCoins(float value)
    {
        // Increment CoinsManager's targetValue and trigger the coroutine to update
        if (coinsManager != null)
        {
            coinsManager.AddValue(value);
        }
    }

    private void Update()
    {
        // Ensure TMPro is updated with CoinsManager's currentValue
        if (coinsManager != null)
        {
            coinsManager.CoinsText.text = ((int)coinsManager.currentValue).ToString();
        }
    }

    public void RemoveAds()
    {
        Debug.Log("Removed Ads");
    }
}
