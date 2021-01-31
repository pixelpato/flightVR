using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AsteroidSpawner : MonoBehaviour
{
    
    public GameObject player;
    private Vector3 _targetPos;
    
    public int xOffset = 35;
    public GameObject asteroid;

    public int minForce = 10;
    public int maxForce = 20;

    public int timeIntervalMin = 1;
    public int timeIntervalMax = 3;
    private int _nextInterval;
    private float _timer = 0;


    public Transform asteroidSpawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        _targetPos = player.transform.position;
        _nextInterval = Random.Range(timeIntervalMin, timeIntervalMax);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _nextInterval)
        {
            SpawnAsteroid();
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        Debug.Log("Asteroid: reset timer");
        _timer = 0;
        _nextInterval = Random.Range(timeIntervalMin, timeIntervalMax);
    }

    private void SpawnAsteroid()
    {
        Debug.Log("Asteroid: Spawn asteroid");
        var position = asteroidSpawnPoint.position;
        Vector3 spawnPos = new Vector3(position.x + Random.Range(-xOffset, xOffset), position.y+ Random.Range(0, xOffset), position.z);
        GameObject tempAsteroid = Instantiate(asteroid, spawnPos, Quaternion.identity);
        
        var rb = tempAsteroid.GetComponent<Rigidbody>();
        Vector3 force = (_targetPos - spawnPos)  * Random.Range(minForce, maxForce);
        Debug.Log("new force is " + force);
        
        if (rb != null)
            rb.AddForce(force);
    }
}
