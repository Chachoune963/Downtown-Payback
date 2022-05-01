using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float baseLife = 100f; // Nombre de vies max

    public float life { get; private set; }
    public Animator animator;

    public Func<float, float> damageListener { get; set; }

    private void Start()
    {
        life = baseLife;
    }

    // Update is called once per frame
    public void addDamage(float damage)
    {
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }
        

        if(damageListener != null)
        {
            life -= damageListener(damage);
        } else
        {
            life -= damage;
        }

        if (life <= 0f)
        {
            PlayerController player = gameObject.GetComponent<PlayerController>();
            if(player)
            {
                player.tuer();
            } else
            {
                Destroy(gameObject);
            }
        }

        if (life > baseLife)
        {
            life = baseLife;
        }
    }

}
