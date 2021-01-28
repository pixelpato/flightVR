using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private void Awake()
    {
        LeanTween.scale(gameObject, Vector3.one * 1.3f, 2f).setEaseInOutQuad().setLoopPingPong().setDelay(Random.Range(0f, 4f));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ring collides with + " + other.tag);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Ring Hit Player");
            GameManagerHelper.Instance.IncreaseRingScore();
            RingSpawner.Instance.spawnRing(transform.position, gameObject);
        }
    }
}
