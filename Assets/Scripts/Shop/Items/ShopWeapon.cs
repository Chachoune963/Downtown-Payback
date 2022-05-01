using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeapon : ShopItem
{
    public GameObject itemPrefab { get; }

    public ShopWeapon(string prefabPath, string name, int cost, int minDebtUnlock, int minKillsUnlock) :
        this(prefabPath, name, "", cost, minDebtUnlock, minKillsUnlock)
    { }

    public ShopWeapon(string prefabPath, string name, string desc, int cost, int minDebtUnlock, int minKillsUnlock) :
        base(getSpriteFromWeapon(prefabPath), name, desc, cost, minDebtUnlock, minKillsUnlock)
    {
        itemPrefab = getWeaponPrefab(prefabPath);   
    }

    // On charge le prefab a partir de son chemin d'acces
    protected static GameObject getWeaponPrefab(string path)
    {
        GameObject itemPrefab = Resources.Load<GameObject>(path);

        
        if (itemPrefab == null)
        {
            Debug.LogError("Erreur d'initalisation du ShopWeapon pour le Prefab " + path);
        }

        return itemPrefab;
    }

    // On cherche le sprite shop correspondant au prefab et si il n'existe pas on prend le sprite du prefab
    protected static Sprite getSpriteFromWeapon(string weaponPrefabPath)
    {
        string[] splitPath = weaponPrefabPath.Split('/');
        string resourceName = splitPath[splitPath.Length - 1];
        Sprite shopSprite = Resources.Load<Sprite>(SHOP_SPRITES_BASE_PATH + resourceName);

        
        if (shopSprite == null)
        {
            Debug.LogWarning("Pas de sprite shop pour " + weaponPrefabPath);

            SpriteRenderer spriteRenderer = getWeaponPrefab(weaponPrefabPath).GetComponent<SpriteRenderer>();

            if (spriteRenderer == null)
            {
                Debug.LogError("Impossible de trouver le SpriteRenderer " + weaponPrefabPath);
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
        player.changementArme(itemPrefab);
    }

}
