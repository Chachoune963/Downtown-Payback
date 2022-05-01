using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneygunScript : HandgunScript
{
    public int fireCost = 5;

    public override void shoot(Damageable tireur)
    {
        
        if (lastFireTime <= 0f && tireur.TryGetComponent(out PlayerController player))
        {
            player.augmanterDette(fireCost);
        }
        base.shoot(tireur);
    }
}
