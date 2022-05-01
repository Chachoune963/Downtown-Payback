using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamager : Damager
{

    public float damage = 20f;

    public float maxLifetime = 5f;

    protected bool destroyBullet;

    private void Start()
    {
        Destroy(gameObject, maxLifetime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        destroyBullet = true;
        
        if (collision.collider.TryGetComponent( out Damageable target))
        {

            //Debug.Log(target);
            //Debug.Log(tireur);
            if (target != tireur)
            {
                target.addDamage(damage);
            } else
            {
                destroyBullet = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (destroyBullet)
        {
            Destroy(gameObject);
        }
    }
}
