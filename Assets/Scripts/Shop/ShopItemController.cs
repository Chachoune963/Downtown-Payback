using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{

    public Image itemImage;
    public Text titleText;
    public Text descText;
    public Text priceText;
    public Button buyButton;
    public Text buttonText;

    public string prefixPrice = "Cost : ";
    public string priceUnit = "BR";
    public string canBuyText = "BUY";
    public int canBuyTextFontSize = 40;
    public string cantBuyText = "too indebted!";
    public int cantBuyTextFontSize = 20;

    public AudioClip buySondEffect;

    public ShopItem shopItem;
    public PlayerController player;
    public ShopUIController shop;

    // Start is called before the first frame update
    void Start()
    {
        if (shopItem.shopSprite != null) 
        { 
            itemImage.sprite = shopItem.shopSprite;
        }

        titleText.text = shopItem.name;
        descText.text = shopItem.desc;
        priceText.text = prefixPrice + shopItem.cost.ToString() + " " + priceUnit;

        // Definit la fonction appelée par le bouton
        buyButton.onClick.AddListener(buttonListener);

        if(!shop.playerCanBuy)
        {
            buyButton.interactable = false;
            buttonText.text = cantBuyText;
            buttonText.fontSize = cantBuyTextFontSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonListener()
    {
        Debug.Log("Item " + shopItem.name + " acheté");
        player.augmanterDette(shopItem.cost);
        shopItem.buyCallback(player);

        playerBuySondEffect();
        shop.resetShop();
    }

    private void playerBuySondEffect()
    {
        player.playerAudio.PlayOneShot(buySondEffect, AudioSettings.soundEffectsVolume);
    }

}
