using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawner : MonoBehaviour
{
    public static RingSpawner Instance { get; private set; }

    [Header("Prefab")]
    public GameObject ringPF;

    [Header("Ring Spawn Settings")]
    public float distanceToNextRing = 10;
    public float rotationTolerance = 10;
    public float positionTolerance = 5;
    public int maxCurrentRings = 3;

    [HideInInspector] public List<GameObject> rings = new List<GameObject>();

    public void spawnRing(Vector3 oldPos, GameObject ring)
    {

        // Random Rotation
        float rot = UnityEngine.Random.Range(-rotationTolerance, rotationTolerance);

        // Check if all rings are Spawned -> Init()
        if (rings.Count < maxCurrentRings) rings.Add(Instantiate(ringPF, oldPos, Quaternion.Euler(rot, rot, rot), gameObject.transform));
        
        // Change Position from hitted Ring
        else
        {
            int idx = -1;
            float dist = -1;

            foreach (GameObject r in rings)
            {
                if (Vector3.Distance(r.transform.position, ring.transform.position) > dist)
                {
                    dist = Vector3.Distance(r.transform.position, ring.transform.position);
                    idx = rings.IndexOf(r);
                }
            }

            // Set New Position 
            Vector3 pos = rings[idx].transform.position + (Vector3.forward * distanceToNextRing);

            // Add Random ratio
            ring.transform.position = new Vector3(pos.x + UnityEngine.Random.Range(-positionTolerance, positionTolerance), pos.y + UnityEngine.Random.Range(-positionTolerance, positionTolerance), pos.z + UnityEngine.Random.Range(-positionTolerance, positionTolerance));

            // Set new Rotation
            List<float> distList = new List<float>();

            // Get all Distance
            foreach (GameObject r in rings)
            {
                    distList.Add(Vector3.Distance(r.transform.position, rings[idx].transform.position));
            }

            // Find closest Ring
            int _idx = -1;

            foreach (float f in distList)
            {
                if (f > 0)
                {
                    if (_idx == -1) _idx = distList.IndexOf(f);
                    if (distList[_idx] > f) _idx = distList.IndexOf(f);
                }
            }

            // Look at Clostest Ring
            ring.transform.LookAt(rings[_idx].transform);
        }
    }

    /// <summary>
    /// Spawn the Rings
    /// </summary>
    public void Init()
    {
        for (int i = 0; i < maxCurrentRings; i++)
        {
            if (rings.Count == 0) spawnRing(Vector3.forward * distanceToNextRing, ringPF);
            else spawnRing(rings[rings.Count - 1].transform.position + (Vector3.forward * distanceToNextRing), ringPF);
        }

        Debug.Log(rings.Count + " Rings Spawned");
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }
}
