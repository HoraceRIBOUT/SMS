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

    public AudioSource soundSource;


    public void AddForce()
    {

    }

    public void AddMagic()
    {

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
