using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmorPrefab : MonoBehaviour
{

    private PlayerController player;

    public void equip(PlayerController player)
    {
        this.player = player;
    }

    public void dequip()
    {

    }

    public abstract float damageListener(float baseDamage);
}
