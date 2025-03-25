using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;

public class StoreItemButton : MonoBehaviour
{
    private Product myProduct;
    public TMP_Text NameTxt;
    public TMP_Text PriceTxt;

    public void InitProduct(Product product)
    {
        myProduct = product;
        NameTxt.text = product.metadata.localizedTitle;
        PriceTxt.text = product.metadata.localizedPriceString;
    }

    public void Buy()
    {
        Shop.Instance.Consumable_Purchase(myProduct);
    }
}