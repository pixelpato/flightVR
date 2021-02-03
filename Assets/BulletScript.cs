using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BulletScript : MonoBehaviour
{
 
    public float Radius = 3.0F;
    public int MinPower = 700;
    public int MaxPower = 2000;
    public GameObject ExplosionVFX;

    private GameObject _lastHittenAsteroid;

    private void OnTriggerEnter(Collider other)
    {
        DestroyNewAsteroid(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        DestroyNewAsteroid(other.gameObject);
    }


    private void DestroyNewAsteroid(GameObject asteroid)
    {
        if (asteroid.layer == 10)
        {
            _lastHittenAsteroid = asteroid;
            GameObject explosion = Instantiate(ExplosionVFX);
            explosion.transform.position = asteroid.transform.position;

            Destroy(asteroid.GetComponent<Rigidbody>());
            Destroy(asteroid.GetComponent<SphereCollider>());

            foreach (Transform t in asteroid.transform)
            {
                
                //add components
                t.gameObject.AddComponent<Rigidbody>();
                Rigidbody rb = t.GetComponent<Rigidbody>();
                rb.useGravity = false;

                t.gameObject.AddComponent<MeshCollider>();
                MeshCollider meshCollider = t.GetComponent<MeshCollider>();
                meshCollider.convex = true;
            
                //add force
                if (rb != null)
                {
                    Random random = new Random();
                    rb.AddExplosionForce(random.Next(MinPower,MaxPower),asteroid.transform.position,Radius);
                }
                Destroy(t.gameObject,3f);
            }

            StartCoroutine(ScaleOverTime(3, asteroid));

        }
    }
    
    
    
    IEnumerator ScaleOverTime(float time, GameObject asteroid)
    {
        Vector3 destinationScale = new Vector3(0, 0, 0);
        Vector3 originalScale = new Vector3(150, 150, 150);
        float currentTime = 0.0f;
        do
        {
            foreach (Transform t in asteroid.transform)
            {
                t.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            }
          
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
         
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    



    private void DestroyAsteroid(GameObject asteroid)
    {
        if (asteroid.layer == 10)
        {
            Debug.Log("hit an asteroid");
   
            
            GameObject fracturedStone = Instantiate(Resources.Load("StoneTwoFractured", typeof(GameObject))) as GameObject;
            fracturedStone.transform.parent = asteroid.transform.parent;
            fracturedStone.transform.position = asteroid.transform.position;
            
        
            GameObject explosion = Instantiate(ExplosionVFX);
            explosion.transform.position = asteroid.transform.position;

            Destroy(asteroid);
            Random random = new Random();
            
            
            foreach (Transform t in fracturedStone.transform)
            {
                var rb = t.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Debug.Log("adding force");
                    rb.AddExplosionForce(random.Next(MinPower,MaxPower),fracturedStone.transform.position,Radius);
                    Debug.Log(" velocity" + rb.velocity);
                }
                Destroy(t.gameObject,3f);
            }
            //destroy fractured parts/parent object, the old asteroid and this bullet
            Destroy(fracturedStone,5f);
            Destroy(this);
            
        }
    }
}
