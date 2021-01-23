using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    // Static Accessability
    public static UiManager Instance { get; private set; }

    [Header("HUD")]
    public TextMeshProUGUI ringPointsText;
    public TextMeshProUGUI timeText;

    public void resetHUD()
    {
        timeText.text = "00:00";
        ringPointsText.text = "Rings: 0 / " + GameController.Instance.ringsToScore;
    }

    public void UpdateTimer(float matchTime)
    {
        string minutes = Mathf.Floor(matchTime / 60).ToString("00");
        string seconds = (matchTime % 60).ToString("00");

        timeText.text = string.Format("{0}:{1}", minutes, seconds);
    }

    public void IncreaseRingPoints(int currentAmount)
    {
        ringPointsText.text = "Rings: " + currentAmount + "/ " + GameController.Instance.ringsToScore;
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
