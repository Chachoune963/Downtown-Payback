using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAccessory : ShopItem
{
    public GameObject itemPrefab { get; }

    public ShopAccessory(string prefabPath, string name, int cost, int minDebtUnlock, int minKillsUnlock) :
        this(prefabPath, name, "", cost, minDebtUnlock, minKillsUnlock)
    { }

    public ShopAccessory(string prefabPath, string name, string desc, int cost, int minDebtUnlock, int minKillsUnlock) :
        base(getSpriteFromWeapon(prefabPath), name, desc, cost, minDebtUnlock, minKillsUnlock)
    {
        itemPrefab = getPrefab(prefabPath);   
    }

    // On charge le prefab a partir de son chemin d'acces
    protected static GameObject getPrefab(string path)
    {
        GameObject itemPrefab = Resources.Load<GameObject>(path);

        
        if (itemPrefab == null)
        {
            Debug.LogError("Erreur d'initalisation du ShopAccessory pour le Prefab " + path);
        }

        return itemPrefab;
    }

    // On cherche le sprite shop correspondant au prefab et si il n'existe pas on prend le sprite du prefab
    protected static Sprite getSpriteFromWeapon(string prefabPath)
    {
        string[] splitPath = prefabPath.Split('/');
        string resourceName = splitPath[splitPath.Length - 1];
        Sprite shopSprite = Resources.Load<Sprite>(SHOP_SPRITES_BASE_PATH + resourceName);

        
        if (shopSprite == null)
        {
            Debug.LogWarning("Pas de sprite shop pour " + prefabPath);

            SpriteRenderer spriteRenderer = getPrefab(prefabPath).GetComponent<SpriteRenderer>();

            if (spriteRenderer == null)
            {
                Debug.LogError("Impossible de trouver le SpriteRenderer " + prefabPath);
            }
            else
            {
                shopSprite = spriteRenderer.sprite;
            }
        }

        return shopSprite;
    }

    // Methode appelée lors de l'achat, ici on équipe l'arme au joueur
    override
    public void buyCallback(PlayerController player)
    {
        player.changementAccessory(itemPrefab);
    }

}
