using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHealItem : ShopItem
{

    public int healPoints { get; }

    public ShopHealItem(string spritePath, string name, int cost, int minDebtUnlock, int minKillsUnlock, int healPoints) :
        this(spritePath, name, "", cost, minDebtUnlock, minKillsUnlock, healPoints)
    { }


    public ShopHealItem(string spritePath, string name, string desc, int cost, int minDebtUnlock, int minKillsUnlock, int healPoints) :
        base(getSprite(spritePath), name, desc, cost, minDebtUnlock, minKillsUnlock)
    {
        this.healPoints = healPoints;
    }

    // On cherche le sprite shop uniquement car uniquement accesible a partir du shop
    protected static Sprite getSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    // Methode appelée lors de l'achat, ici on heal le joueur
    override
    public void buyCallback(PlayerController player)
    {
        player.damageable.addDamage(-healPoints);
    }

}
