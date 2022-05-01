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
            lastFireTime = 0f; //bricolage, mais ça marche: sans ça, la balle n'est tirée qu'une fois puis bloquée par le cooldown
        }
        lastFireTime = cooldown;
    }

}
