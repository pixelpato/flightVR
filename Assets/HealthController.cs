using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public NewPlayerController NewPlayerController;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            NewPlayerController.CurrentHP -= NewPlayerController.AsteroidDamage;


        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 10)
        {
            NewPlayerController.CurrentHP -= NewPlayerController.AsteroidDamage;


        }
    }
}
