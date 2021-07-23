using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudioManager : MonoBehaviour
{
    public AudioSource Earthquake;
    public AudioSource Rolling;


    public void PlayEarthquakeSound()
    {
        Earthquake.Play();
    }

    public void PlayRollingSound()
    {
        Rolling.Play();
    }
}
