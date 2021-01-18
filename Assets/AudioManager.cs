using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip messageHeros;
    public AudioClip messageOther;
    public AudioClip blockDisappear;
    public AudioClip newSet;

    public AudioSource musicNormal;
    public AudioSource musicMagic;
    public AudioSource musicForce;

    public float targetForce = 0;
    public float targetMagic = 0;

    public AudioSource soundSource;

    public int force = 0;
    public int magic = 0;

    [Range(0, 1)]
    public float lerpValue = 0.4f;

    [ContextMenu("AddForce")]
    public void AddForce()
    {
        force++;
        magic = 0;
        ResolveMusic();
    }

    [ContextMenu("AddMagic")]
    public void AddMagic()
    {
        magic++;
        force = 0;
        ResolveMusic();
    }

    public void ResolveMusic()
    {
        if (force >= 2)
            targetForce = 1;
        else
            targetForce = 0;

        if (magic >= 2)
            targetMagic = 1;
        else
            targetMagic = 0;
    }

    public void Update()
    {
        if(targetMagic != musicMagic.volume)
        {
            musicMagic.volume = musicMagic.volume * lerpValue + targetMagic * (1 - lerpValue);
        }

        if(targetForce != musicForce.volume)
        {
            musicForce.volume = musicForce.volume * lerpValue + targetForce * (1 - lerpValue);
        }


    }


    public void PlayMessageHeros()
    {
        soundSource.PlayOneShot(messageHeros);
    }
    public void PlayMessageOther()
    {
        soundSource.PlayOneShot(messageOther);
    }
    public void PlayBlockDisappear()
    {
        soundSource.PlayOneShot(blockDisappear);
    }
    public void PlayNewSet()
    {
        soundSource.PlayOneShot(newSet);
    }
}
