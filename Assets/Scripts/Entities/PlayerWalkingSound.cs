using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingSound : MonoBehaviour
{
    public AudioSource footstep_Sound;

    [SerializeField]
    public AudioClip[] footstep_Clip;


    public Rigidbody2D player_Controller;

    private bool isWalking = true;

    private float SoundEffectsVolume = AudioSettings.soundEffectsVolume;

     //Start is called before the first frame update
     void Awake()
     {
        footstep_Sound = GetComponent<AudioSource>();
        //player_Controller = GetComponent<Rigidbody2D>();
     }

    // Update is called once per frame
    void Update()
    {
        CheckToPlaySound();
    }


    void CheckToPlaySound()
    {
        if (player_Controller.velocity.sqrMagnitude > 0.1 && !isWalking)
        {
            footstep_Sound.volume = SoundEffectsVolume*1;
            footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
            footstep_Sound.Play();

            isWalking = true;
            

        }
        else if(player_Controller.velocity.sqrMagnitude <= 0.1)
        {
            footstep_Sound.volume = 0;
            isWalking = false;
        }

    }


}
