using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleGatlingScript : HandgunScript
{
    public Transform shootPoint1;
    public Transform shootPoint2;

    public override void shoot(Damageable tireur)
    {
        if (lastFireTime > 0f)
        {
            return;
        }

        shootPoint = shootPoint1;
        base.shoot(tireur);
        shootPoint = shootPoint2;
        lastFireTime = 0f; //bricolage pour tirer plusieurs fois
        base.shoot(tireur);
        lastFireTime = cooldown;
    }
}
