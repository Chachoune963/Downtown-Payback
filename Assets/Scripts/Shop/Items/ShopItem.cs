using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    BOXING_GLOVES,
    KITKAT,
    KITKAT_SUPER,
    ASSAULT_RIFLE,
    HAND_GUN,
    MONEY_GUN,
    SHOTGUN,
    BEEGUN,
    DOUBLE_GATLING,
    LASERBEAM,
    BLACKHOLE,
    SWORD,
    JETPACK,
    SHIELDGENERATOR,
    BULLETPROOF,
    SUPERBULLETPROOF,
    MEGABULLETPROOF
}

public abstract class ShopItem
{

    public static Dictionary<Items, ShopItem> items = new Dictionary<Items, ShopItem>();

    public Sprite shopSprite { get; }
    public int cost { get; }
    public int minDebtUnlock { get; }
    public int minKillsUnlock { get; }
    public string name { get; }
    public string desc { get; }

    public static readonly string SHOP_SPRITES_BASE_PATH = "Sprites/shop/";

    // La fonction doit etre appelée une seule fois seulement
    public static void initItemsDictionary()
    {

        string preWeapon = "Prefabs/Armes/"; // Prefix pour chemin d'acces prefab armes
        string preAccessory = "Prefabs/Accessoires/"; // Prefix pour chemin d'acces prefab acessoires
        string preArmor = "Prefabs/Armor/";

        /*Enregistrement-version de développement
        // Ajout des armes
        items.Add(Items.BOXING_GLOVES, new ShopWeapon(preWeapon + "BoxingGloves", "Boxing gloves", "A classic in the ring.", 50, 5, 0)); //ok
        items.Add(Items.HAND_GUN, new ShopWeapon(preWeapon + "Handgun", "Hand gun", "The businessman's best friend.", 100, 0, 1)); //ok
        items.Add(Items.MONEY_GUN, new ShopWeapon(preWeapon + "Moneygun", "Money gun", "For the bitter tax-payer.", 1000, 3000, 1)); //ok
        items.Add(Items.ASSAULT_RIFLE, new ShopWeapon(preWeapon + "AssaultRifle", "Assault rifle","Sometimes you just have to clean up.", 1500, 0, 10));//ok
        items.Add(Items.SHOTGUN, new ShopWeapon(preWeapon + "Shotgun", "Shotgun", "You know you want to.", 1000, 0, 8)); //ok
        items.Add(Items.BEEGUN, new ShopWeapon(preWeapon + "Beegun", "BeeBee Gun", "Sponsored by honey.", 2000, 0, 15)); //ok
        items.Add(Items.DOUBLE_GATLING, new ShopWeapon(preWeapon + "DoubleGatling", "Twin-headed Gatling", "Somewhere, a weapons engineer\nhas one hell of a hangover.", 1000, 100, 15)); //ok
        items.Add(Items.LASERBEAM, new ShopWeapon(preWeapon + "BigFingBeam", "Big F-ing Laser", "How did this get here?", 10000, 3000, 30)); //ok
        items.Add(Items.BLACKHOLE, new ShopWeapon(preWeapon + "Blackholegun", "Singularity generator", "feels like your bank account!", 10000, 3000, 30)); //ok
        items.Add(Items.SWORD, new ShopWeapon(preWeapon + "Sword", "Longsword", "A more elegant weapon, from a less civilized age.", 800, 100, 5)); //ok

        // Ajout des soins
        items.Add(Items.KITKAT, new ShopHealItem("Sprites/shop_items/kitkat", "KitKat", "Have a break.\nWas it worth it? (Heals you)", 10, 0, 0, 10)); //ok
        items.Add(Items.KITKAT_SUPER, new ShopHealItem("Sprites/shop_items/kitkat", "Super KitKat", "Now you have super diabetes!", 50, 20, 5, 50)); //ok

        // Ajout acessoires
        items.Add(Items.JETPACK, new ShopAccessory(preAccessory + "Jetpack", "Jetpack", "PSCHOOOO", 500, 0, 0)); //ok
        items.Add(Items.SHIELDGENERATOR, new ShopAccessory(preAccessory + "Shieldgenerator", "Shield Generator", "Legal immunity isn't enough.", 1000, 1000, 15)); //ok

        //Ajout armures
        items.Add(Items.BULLETPROOF, new ShopArmor(preArmor + "Bulletproof", "bulletproof jacket", "Is this... nylon??", 100, 0, 1)); //ok
        items.Add(Items.SUPERBULLETPROOF, new ShopArmor(preArmor + "SuperBulletproof", "Superbulletproof jacket", "This time it HAS to be kevlar.", 500, 0, 10)); //ok
        items.Add(Items.MEGABULLETPROOF, new ShopArmor(preArmor + "MegaBulletproof", "Mega bulletproof jacket", "I can't believe it's not kevlar!", 1500, 0, 20)); //ok

        */
        //Version pratique
        items.Add(Items.KITKAT, new ShopHealItem("Sprites/shop_items/kitkat", "KitKat", "Have a break.\nWas it worth it? (Heals you)", 10, 0, 0, 10));
        items.Add(Items.KITKAT_SUPER, new ShopHealItem("Sprites/shop_items/kitkat", "Super KitKat", "Now you have super diabetes!", 50, 20, 5, 50));
        items.Add(Items.BOXING_GLOVES, new ShopWeapon(preWeapon + "BoxingGloves", "Boxing gloves", "A classic in the ring.", 50, 5, 0));
        items.Add(Items.SWORD, new ShopWeapon(preWeapon + "Sword", "Longsword", "A more elegant weapon, from a less civilized age.", 800, 100, 3));
        items.Add(Items.HAND_GUN, new ShopWeapon(preWeapon + "Handgun", "Hand gun", "The businessman's best friend.", 100, 0, 1));
        items.Add(Items.MONEY_GUN, new ShopWeapon(preWeapon + "Moneygun", "Money gun", "For the bitter tax-payer.", 1000, 3000, 1));
        items.Add(Items.SHOTGUN, new ShopWeapon(preWeapon + "Shotgun", "Shotgun", "You know you want to.", 1000, 0, 10));
        items.Add(Items.ASSAULT_RIFLE, new ShopWeapon(preWeapon + "AssaultRifle", "Assault rifle", "Sometimes you just have to clean up.", 1500, 0, 15));
        items.Add(Items.BULLETPROOF, new ShopArmor(preArmor + "Bulletproof", "bulletproof jacket", "Is this... nylon??", 300, 0, 4));
        items.Add(Items.SUPERBULLETPROOF, new ShopArmor(preArmor + "SuperBulletproof", "Superbulletproof jacket", "This time it HAS to be kevlar.", 800, 0, 10));
        items.Add(Items.MEGABULLETPROOF, new ShopArmor(preArmor + "MegaBulletproof", "Mega bulletproof jacket", "I can't believe it's not kevlar!", 2000, 0, 25));
        items.Add(Items.DOUBLE_GATLING, new ShopWeapon(preWeapon + "DoubleGatling", "Twin-headed Gatling", "Somewhere, a weapons engineer\nhas one hell of a hangover.", 8000, 100, 25));
        items.Add(Items.BEEGUN, new ShopWeapon(preWeapon + "Beegun", "BeeBee Gun", "Sponsored by honey.", 2000, 0, 15));
        items.Add(Items.LASERBEAM, new ShopWeapon(preWeapon + "BigFingBeam", "Big F-ing Laser", "How did this get here?", 10000, 3000, 35));
        items.Add(Items.BLACKHOLE, new ShopWeapon(preWeapon + "Blackholegun", "Singularity generator", "feels like your bank account!", 15000, 3000, 40));
        items.Add(Items.JETPACK, new ShopAccessory(preAccessory + "Jetpack", "Jetpack", "PSCHOOOO", 1500, 0, 30));
        items.Add(Items.SHIELDGENERATOR, new ShopAccessory(preAccessory + "Shieldgenerator", "Shield Generator", "Legal immunity isn't enough.", 1000, 1000, 20));

        Debug.Log("Dictionaire d'items initialisé");
    }

        public ShopItem(Sprite shopSprite, string name, string desc, int cost, int minDebtUnlock, int minKillsUnlock)
        {
        
        this.shopSprite = shopSprite;
        this.cost = cost;
        this.minDebtUnlock = minDebtUnlock;
        this.minKillsUnlock = minKillsUnlock;
        this.name = name;
        this.desc = desc;
    }

    public abstract void buyCallback(PlayerController player);

}
