using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackScript : AccessoryScript
{
    public float thrust = 500f;

    public TrailRenderer trail;

    private bool keepJetpackOn = false;
    private Collider2D col;

    private void Start()
    {
        charge = maxCharge;
    }

    public override void activate(PlayerController player)
    {
        trail.emitting = true;
        col = player.GetComponent<Collider2D>();
        if (charge > 0f)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.AddForce(rb.transform.up * thrust);
            keepJetpackOn = true;
            col.enabled = false;
        }

        if (charge > -0.1f)
        {
            charge -= Time.fixedDeltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (keepJetpackOn)
        {
            keepJetpackOn = false;
        }
        else
        {
            trail.emitting = false;
            if (col != null)
            {
                col.enabled = true;
            }
        }
    }
}
