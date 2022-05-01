using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingGloveScript : WeaponScript
{
    public float cooldown = 0.5f;
    public float damage = 50f;
    public float shootRadius = 1f;

    public Animator animator;

    float lastFireTime = 0f;

    public AudioSource pew_Sound;
    [SerializeField]
    public AudioClip[] pew_Clip;

    override

    public void shoot(Damageable tireur)
    {
        if (lastFireTime > 0f)
        {
            return;
        }

        animator.SetTrigger("PunchTrigger");

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(shootPoint.position, shootRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Damageable target))
            {

                if (target != tireur)
                {
                    target.addDamage(damage);
                }
            }
        }

        lastFireTime = cooldown;

        Vector3 shootposition = shootPoint.position;
        //Gestion du son du tir
        //pew_Sound = GetComponent<AudioSource>();
        //pew_Sound.volume = AudioSettings.soundEffectsVolume*0.1F;
        //pew_Sound.clip = pew_Clip[Random.Range(0, pew_Clip.Length)];
        //pew_Sound.Play()
        AudioSource.PlayClipAtPoint(pew_Clip[Random.Range(0, pew_Clip.Length)], shootposition, AudioSettings.soundEffectsVolume * 0.5f);
    }

    private void FixedUpdate()
    {
        if (lastFireTime > 0f)
        {
            lastFireTime -= Time.fixedDeltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(shootPoint.position, shootRadius);
    }
}
