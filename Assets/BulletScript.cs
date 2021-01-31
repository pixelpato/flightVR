using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BulletScript : MonoBehaviour
{
 
    public float Radius = 10.0F;
    public int MinPower = 100;
    public int MaxPower = 500;
    public GameObject ExplosionVFX;


    private void OnTriggerEnter(Collider other)
    {
        DestroyAsteroid(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        DestroyAsteroid(other.gameObject);
    }
    
    
    private void DestroyAsteroid(GameObject asteroid)
    {
        if (asteroid.layer == 10)
        {
            GameObject fracturedStone = Instantiate(Resources.Load("StoneTwoFractured", typeof(GameObject))) as GameObject;
            fracturedStone.transform.parent = asteroid.transform.parent;
            fracturedStone.transform.position = asteroid.transform.position;
        
            GameObject explosion = Instantiate(ExplosionVFX);
            explosion.transform.position = asteroid.transform.position;

            Random random = new Random();


            foreach (Transform t in fracturedStone.transform)
            {
                var rb = t.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Debug.Log("adding force");
                    rb.AddExplosionForce(random.Next(MinPower,MaxPower), transform.position,Radius);
                    Debug.Log(" velocity" + rb.velocity);
                }
                Destroy(t.gameObject,5f);
            }
            Destroy(fracturedStone,5f);
        }
    }
}
