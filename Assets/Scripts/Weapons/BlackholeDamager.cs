using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeDamager : BulletDamager
{

    public float forceMultiplier = 10f;
    public float expGrowthRate = 0.1f;

    private float scale = 1f;

    private void FixedUpdate()
    {
        destroyBullet = false;
        scale = scale * (1 + expGrowthRate * Time.fixedDeltaTime);
        transform.localScale = Vector3.one * scale;
        GetComponent<Rigidbody2D>().mass = 5f*scale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == null)
            return;
        if (collision.TryGetComponent(out Damageable target))
            if (target == tireur)
                return;

        collision.attachedRigidbody.AddForce((transform.position-collision.transform.position) * scale * forceMultiplier / Mathf.Pow((transform.position - collision.transform.position).magnitude,1));
    }
}
