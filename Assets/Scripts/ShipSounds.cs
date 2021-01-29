using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSounds : MonoBehaviour
{
    // Static Accessability
    public static ShipSounds Instance { get; private set; }

    public AudioSource audioSource;


    private void Start () {
        audioSource = GetComponent<AudioSource>(); 
    }

    public void volume(float vol) {
        audioSource.volume = vol;
        // Debug.Log(vol);
    }
}
