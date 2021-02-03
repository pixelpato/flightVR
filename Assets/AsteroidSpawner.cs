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

    public int timeIntervalMin = 3;
    public int timeIntervalMax = 6;
    private int _nextInterval;
    private float _timer = 0;


    private GameObject _currentAsteroid;

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
        _timer = 0;
        _nextInterval = Random.Range(timeIntervalMin, timeIntervalMax);
    }

    private void SpawnAsteroid()
    {
        var position = asteroidSpawnPoint.position;
        Vector3 spawnPos = new Vector3(position.x + Random.Range(-xOffset, xOffset), position.y+ Random.Range(0, xOffset), position.z);
        
        _currentAsteroid = Instantiate(asteroid, spawnPos, Quaternion.identity);

        StartCoroutine(ScaleOverTime(3));

        var rb = _currentAsteroid.GetComponent<Rigidbody>();
        Vector3 force = (_targetPos - spawnPos)  * Random.Range(minForce, maxForce);
        
        if (rb != null)
            rb.AddForce(force);
    }
    
    
    
    IEnumerator ScaleOverTime(float time)
    {
        Vector3 destinationScale =  _currentAsteroid.transform.localScale;

        _currentAsteroid.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 originalScale = _currentAsteroid.transform.localScale;
         
        float currentTime = 0.0f;
         
        do
        {
            _currentAsteroid.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
         
    }
}
