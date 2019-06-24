using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //public AudioSource ambientSound;
    [HideInInspector] public AudioSource aS;


    public AudioClip[] clips;


    private void Awake()
    {
        aS = GetComponent<AudioSource>();
    }

    public void PlaySound(int clipN)
    {
        aS.PlayOneShot(clips[clipN]);
    }

    private void OnDisable()
    {
        //ambientSound.Stop();
        aS.Stop();
    }
}
