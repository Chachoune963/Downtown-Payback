using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{

    public GameObject shopContainer;
    public GameObject prefabShopItem;

    public PlayerController player;

    public static bool isShopping = false;

    public bool enable { get; private set; } = false;
    public bool playerCanBuy => (player.dette <= (50 + 100 * Mathf.Pow(player.kills,2))) || (player.weapon.maxAmmo>0 && player.weapon.ammo<=0f);

    private List<ShopItemController> items = new List<ShopItemController>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initItems()
    {
        foreach (ShopItem item in ShopItem.items.Values)
        {
            if(player.dette >= item.minDebtUnlock && player.kills >= item.minKillsUnlock)
            {
                items.Add(addItem(item));
                Debug.Log(item.name + " ajouté au shop");
            }
        }
    }

    private void clearShop()
    {
        foreach(ShopItemController itemC in items)
        {
            Destroy(itemC.gameObject);
        }

        items.Clear();
    }

    public ShopItemController addItem(ShopItem item)
    {
        ShopItemController itemController = Instantiate(prefabShopItem, shopContainer.transform).GetComponent<ShopItemController>();
        itemController.shopItem = item;
        itemController.player = player;
        itemController.shop = this;

        return itemController;
    }

    // Permet de mettre a jour la liste des objets
    public void resetShop()
    {
        clearShop();
        initItems();
    }

    public void enableShop()
    {
        // Creation des items dans le shop
        initItems();

        // On reposition l'UI
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, -40, 0);
        enable = true;
        isShopping = true;
        Time.timeScale = 0.25f;
    }

    public void disableShop()
    {
        // On deplace l'UI tres loin
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(2076, 0, 0);
        enable = false;
        isShopping = false;

        Time.timeScale = 1f;

        // On detruit les items du shop pour les reconstruire la prochaine fois
        clearShop();
    }
}
