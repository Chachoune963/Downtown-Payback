using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : HandgunScript
{

    public int pellets = 7;

    public override void shoot(Damageable tireur)
    {
        if (lastFireTime > 0f)
        {
            return;
        }

        for (int i = 0; i < pellets; i++)
        {
            base.shoot(tireur);
            lastFireTime = 0f; //bricolage, mais �a marche: sans �a, la balle n'est tir�e qu'une fois puis bloqu�e par le cooldown
        }
        lastFireTime = cooldown;
    }

}
