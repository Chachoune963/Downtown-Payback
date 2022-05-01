using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeDamager : BulletDamager
{

    public float thrustPerSpeed = 1f;
    public float speed = 20f;

    private Damageable aimTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (aimTarget == null)
        {
            if (collision.TryGetComponent(out Damageable possibleTarget))
            {
                if (possibleTarget == tireur)
                {
                    return;
                }

                aimTarget = possibleTarget;
            }
        }
    }

    private void FixedUpdate()
    {
        MoveTowardsTarget();
        if (destroyBullet)
        {
            Destroy(gameObject);
        }
    }

    private void MoveTowardsTarget()
    {
        if (aimTarget == null)
            return;
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        Rigidbody2D targetRb = aimTarget.gameObject.GetComponent<Rigidbody2D>();
        Vector2 targetPos = targetRb.position;
        Vector2 lookDir = targetPos - rb2d.position;
        Vector2 targetVelocity = new Vector2(targetPos.x - rb2d.position.x, targetPos.y - rb2d.position.y);
        targetVelocity = targetVelocity.normalized;
        // Debug.Log(rb.velocity);
        targetVelocity *= speed;
        rb2d.AddForce((targetVelocity - rb2d.velocity) * thrustPerSpeed);
        rb2d.SetRotation(Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f);
    }


}
