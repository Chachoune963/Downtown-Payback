using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandgunScript : WeaponScript
{
    public GameObject bulletPrefab;
    public float cooldown = 0.5f;
    public float shootforce = 20f;
    public float bulletDamage = 20f;
    public float randomSpreadAngle = 5f;

    protected float lastFireTime = 0f;

    public AudioSource pew_Sound;
    [SerializeField]
    public AudioClip[] pew_Clip;

    
    private void Start()
    {
        ammo = maxAmmo;


    }

    override public void shoot(Damageable tireur)
    {
        if (ammo <= 0)
        {
            return;
        }
        

        if (lastFireTime > 0f)
        {
            return;
        }

        ammo -= 1;

        float randomSpread = Random.Range(-randomSpreadAngle, randomSpreadAngle);

        Vector3 shootDirection = new Vector3((shootPoint.up.x * Mathf.Cos(randomSpread*Mathf.Deg2Rad)) + (shootPoint.up.y * Mathf.Sin(randomSpread * Mathf.Deg2Rad)), (shootPoint.up.y * Mathf.Cos(randomSpread * Mathf.Deg2Rad)) - (shootPoint.up.x * Mathf.Sin(randomSpread * Mathf.Deg2Rad)), shootPoint.up.z);

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Definition du tireur pour eviter l'auto kill
        BulletDamager bulletDamager = bullet.GetComponent<BulletDamager>();
        bulletDamager.tireur = tireur;
        bulletDamager.damage = bulletDamage;

        Rigidbody2D bulletrb2d = bullet.GetComponent<Rigidbody2D>();
        bulletrb2d.AddForce(shootDirection * shootforce, ForceMode2D.Impulse);
        lastFireTime = cooldown;

        Vector3 shootposition = shootPoint.position;
        //Gestion du son du tir
        //pew_Sound = GetComponent<AudioSource>();
        //pew_Sound.volume = AudioSettings.soundEffectsVolume * 0.4F;
        //pew_Sound.clip = pew_Clip[Random.Range(0, pew_Clip.Length)];
        AudioSource.PlayClipAtPoint(pew_Clip[Random.Range(0, pew_Clip.Length)],shootposition, AudioSettings.soundEffectsVolume * 0.4F);

    
    }

    private void FixedUpdate()
    {
        if (lastFireTime > 0f)
        {
            lastFireTime -= Time.fixedDeltaTime;
        }
    }


}
