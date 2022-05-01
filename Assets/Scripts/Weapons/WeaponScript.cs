using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponScript : MonoBehaviour
{
    public Transform shootPoint;

    public float overallPower; //équation conseillée: P = bulletdamage/cooldown (*0.5 si melee, *2 si laser)

    public int maxAmmo;
    public int ammo;

    public abstract void shoot(Damageable tireur);

}
