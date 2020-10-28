using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroy : MonoBehaviour
{
    public float Radius = 5.0F;
    public float Power = 10.0F;
    private void OnDestroy()
    {
        GameObject fracturedStone = Instantiate(Resources.Load("StoneTwoFractured", typeof(GameObject))) as GameObject;
        fracturedStone.transform.parent = transform.parent;
        fracturedStone.transform.position = transform.position;
        
        //explosion velocity
        Vector3 explosionPos = transform.position;
        //+3 because otherwise it's not in the center, dunnow why
        explosionPos.y += 3;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, Radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
        
            if (rb != null)
                rb.AddExplosionForce(Power, explosionPos, Radius, 3.0F);
        }
    }
}
