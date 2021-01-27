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
        if (other.CompareTag("Player"))
        {
            GameManagerHelper.Instance.IncreaseRingScore();
            RingSpawner.Instance.spawnRing(transform.position, gameObject);
        }
    }
}
