using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerHelper : MonoBehaviour
{
    public static GameManagerHelper Instance { get; private set; }

    [Header("Round Settings")]
    [HideInInspector] public float currentMatchLength;
    [HideInInspector] public int ringsScored = 0;
    public int ringsToScore = 30;
    [HideInInspector] public bool isMatchRunning = false;
    [HideInInspector] public bool isReadyToStartMatch = true;

    public void StartRound()
    {
        Debug.Log("Start Round");
        currentMatchLength = 0;
        ringsScored = 0;
        UiManager.Instance.resetHUD();
        isMatchRunning = true;
    }

    public void IncreaseRingScore()
    {
        ringsScored += 1;
        UiManager.Instance.IncreaseRingPoints(ringsScored);
    }

    private void Update()
    {
        if (!isMatchRunning && isReadyToStartMatch)
        {
            StartRound();
        }

        if (isMatchRunning)
        {
            currentMatchLength += Time.deltaTime;
            UiManager.Instance.UpdateTimer(currentMatchLength);
            UiManager.Instance.UpdateHull();
        }
        if (ringsScored == ringsToScore)
        {
            isMatchRunning = false;
            Time.timeScale = 0;
            isReadyToStartMatch = false;
        }
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
    }
}
