using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private void Awake()
    {
        // TODO LeanTween
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.tag);

        if (other.CompareTag("Player"))
        {
            RingSpawner.Instance.spawnRing(transform.position, gameObject);
        }
    }
}
