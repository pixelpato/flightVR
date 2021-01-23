using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Round Settings")]
    [HideInInspector] public float currentMatchLength;
    [HideInInspector] public int ringsScored = 0;
    public int ringsToScore = 30;
    public bool isMatchRunning = false;
    public bool isReadyToStartMatch = true;

    public void StartRound()
    {
        Debug.Log("Start Roung");
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
        if (Input.GetButton("Accel") && !isMatchRunning && isReadyToStartMatch)
        {
            StartRound();
        }

        if (isMatchRunning)
        {
            currentMatchLength += Time.deltaTime;
            UiManager.Instance.UpdateTimer(currentMatchLength);
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
