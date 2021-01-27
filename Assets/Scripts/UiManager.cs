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
        if (timeText != null) timeText.text = "00:00";
        if (ringPointsText != null) ringPointsText.text = "Rings: 0 / " + GameManagerHelper.Instance.ringsToScore;
    }

    public void UpdateTimer(float matchTime)
    {
        string minutes = Mathf.Floor(matchTime / 60).ToString("00");
        string seconds = (matchTime % 60).ToString("00");

        if (timeText != null) timeText.text = string.Format("{0}:{1}", minutes, seconds);
    }

    public void IncreaseRingPoints(int currentAmount)
    {
        if (ringPointsText != null) ringPointsText.text = "Rings: " + currentAmount + "/ " + GameManagerHelper.Instance.ringsToScore;
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
