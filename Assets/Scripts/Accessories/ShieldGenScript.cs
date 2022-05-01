using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenScript : AccessoryScript
{
    public GameObject shield;

    private bool keepShieldOn;

    private void Start()
    {
        charge = maxCharge;
    }

    public override void activate(PlayerController player)
    {
        if (charge < 0f)
        {
            return;
        }
        
        keepShieldOn = true;
        shield.SetActive(true);

        charge -= Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        if (keepShieldOn)
        {
            keepShieldOn = false;
        }
        else
        {
            shield.SetActive(false);
        }
    }

}
