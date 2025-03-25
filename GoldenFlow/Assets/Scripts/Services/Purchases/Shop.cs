using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using System.Threading.Tasks;

[Serializable]
public class ConsumableItem
{
    // public string Name;
    public string Id;
    public string desc;
    public float price;
    public int amount;
}

public class Shop : MonoBehaviour, IDetailedStoreListener
{
    public static Shop Instance;
    IStoreController m_StoreController;
    Product currentItem;

    [Header("Coins")]
    public StoreItemButton CoinItem;
    public Transform CoinTransform;

    // Currency Products
    public List<ConsumableItem> ConsumableItems = new List<ConsumableItem>();
    public string removeAdsProcuct;
    public string environment = "production";

    public Data data;
    public Payload payload;
    public PayloadData payloadData;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    async void Start()
    {
        var options = new InitializationOptions()
               .SetEnvironmentName(environment);

        await UnityServices.InitializeAsync(options);

        // Call the async initialization method
        InitialzeBuilder();
    }

    private void InitialzeBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add all consumable products
        foreach (ConsumableItem item in ConsumableItems)
        {
            builder.AddProduct(item.Id, ProductType.Consumable);
        }

        builder.AddProduct(removeAdsProcuct, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }

    #region Store Events
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // testText.text = "Success";
        m_StoreController = controller;

        // Initialize store items
        CheckNonConsumable(removeAdsProcuct);
        InitializeStoreItems();
    }

    // Proccesin Purchase
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;
        print("Purchase Complete" + product.definition.id);
        // testText.text += "Purchase Complete: " + product.definition.id;

        if (product.definition.id != removeAdsProcuct)
        {
            GetCurrency(product);
        }
        else
        {
            BlockAds();
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Failed:" + error);
        // testText.text += "Failed:" + error;

        throw new NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("Failed_message:" + error + message);
        // testText.text += "Failed_message:" + error + message;

        throw new NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        // testText.text += "OnPurchaseFailed";

        throw new NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // testText.text += "OnPurchaseFailed";

        throw new NotImplementedException();
    }
    #endregion

    #region Store Settings
    public void Consumable_Purchase(Product item)
    {
        currentItem = item;
        m_StoreController.InitiatePurchase(currentItem);
    }

    public void BlockAds_Purchase()
    {
        Product item = m_StoreController.products.WithID(removeAdsProcuct);
        currentItem = item;
        m_StoreController.InitiatePurchase(currentItem);
    }

    private void CheckNonConsumable(string id)
    {
        if (m_StoreController != null)
        {
            var product = m_StoreController.products.WithID(id);
            if (product != null)
            {
                if (product.hasReceipt)
                {
                    BlockAds();
                }
                else
                {
                    ShowAds();
                }
            }
        }
    }

    // Remove Ads Settings
    private void BlockAds()
    {
        // print("Remove Ads");
        PlayerPrefs.SetInt("BlockAds", 1);
    }
    private void ShowAds()
    {
        // print("Show Ads");
        PlayerPrefs.SetInt("BlockAds", 0);
    }

    // Get Purchased Currencies
    private void GetCurrency(Product product)
    {
        // Find the corresponding consumable item
        ConsumableItem purchasedItem = ConsumableItems.Find(item => item.Id == product.definition.id);
        if (purchasedItem != null)
        {
            // Get the correct payout quantity from the purchased product
            int payoutQuantity = (int)product.definition.payout.quantity;
            int amount = purchasedItem.amount * payoutQuantity;

            if (product.definition.id.Contains("coin"))
            {
                CoinsManager.Instance.AddValue(amount);
            }
        }
    }

    private void InitializeStoreItems()
    {
        foreach (ConsumableItem item in ConsumableItems)
        {
            Product product = m_StoreController.products.WithID(item.Id);
            if (product != null)
            {
                // testText.text += "| Purchase: " + product.metadata.localizedTitle;
                StoreItemButton itemButton = Instantiate(CoinItem, CoinTransform);
                itemButton.InitProduct(product);
            }
        }
    }
    #endregion
}

[Serializable]
public class SkuDetails
{
    public string productId;
    public string type;
    public string title;
    public string name;
    public string iconUrl;
    public string description;
    public string price;
    public long price_amount_micros;
    public string price_currency_code;
    public string skuDetailsToken;
}

[Serializable]
public class PayloadData
{
    public string orderId;
    public string packageName;
    public string productId;
    public long purchaseTime;
    public int purchaseState;
    public string purchaseToken;
    public int quantity;
    public bool acknowledged;
}

[Serializable]
public class Payload
{
    public string json;
    public string signature;
    public List<SkuDetails> skuDetails;
    public PayloadData payloadData;
}

[Serializable]
public class Data
{
    public string Payload;
    public string Store;
    public string TransactionID;
}
