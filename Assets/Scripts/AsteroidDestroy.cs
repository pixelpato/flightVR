using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AsteroidDestroy : MonoBehaviour
{
    public float Radius = 10.0F;
    public int MinPower = 100;
    public int MaxPower = 500;
    public GameObject ExplosionVFX;



    private void OnDestroy()
    {
        GameObject fracturedStone = Instantiate(Resources.Load("StoneTwoFractured", typeof(GameObject))) as GameObject;
        fracturedStone.transform.parent = transform.parent;
        fracturedStone.transform.position = transform.position;
        
        GameObject explosion = Instantiate(ExplosionVFX);
        explosion.transform.position = transform.position;

        Random random = new Random();


        foreach (Transform t in fracturedStone.transform)
        {
            var rb = t.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("adding force");
                rb.AddExplosionForce(random.Next(MinPower,MaxPower), fracturedStone.transform.position,Radius);
                Debug.Log(" velocity" + rb.velocity);

            }
            Destroy(t.gameObject,5f);
        }
        Destroy(fracturedStone,5f);
    }
}
