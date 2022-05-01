using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeaponScript : WeaponScript
{

    public LineRenderer lineRenderer;

    public float dps = 100f;
    public float range = 100f;

    bool keepLaserOn;

    public AudioSource pew_Sound;
    [SerializeField]
    public AudioClip[] pew_Clip;

    private void Start()
    {
        ammo = maxAmmo;
    }
    public override void shoot(Damageable tireur)
    {
        if (ammo <= 0)
        {
            return;
        }

        ammo -= 1;

        lineRenderer.enabled = true;
        keepLaserOn = true;

        RaycastHit2D raycast = Physics2D.Raycast(shootPoint.position, shootPoint.transform.up, range);

        //print("shooting laser");

        if (raycast.collider != null)
        {
            //print("laser hit "+raycast.collider+" at "+raycast.point+" from "+shootPoint.position);
            if (raycast.collider.TryGetComponent(out Damageable target))
            {
                //print("applying damage");
                target.addDamage(dps * Time.fixedDeltaTime);
            }
            lineRenderer.SetPosition(0, shootPoint.localPosition);
            lineRenderer.SetPosition(1, shootPoint.localPosition + Vector3.up*raycast.distance);
        }
        else
        {
            lineRenderer.SetPosition(0, shootPoint.localPosition);
            lineRenderer.SetPosition(1, shootPoint.localPosition + Vector3.up*range);
        }

        //Gestion du son du tir
        pew_Sound = GetComponent<AudioSource>();
        pew_Sound.volume = AudioSettings.soundEffectsVolume * 0.6F;
        pew_Sound.clip = pew_Clip[Random.Range(0, pew_Clip.Length)];
        pew_Sound.Play();

    }

    private void FixedUpdate()
    {
        if (keepLaserOn)
        {
            keepLaserOn = false;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
