using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{

    public static int timesRotated;
    public static int totalAttempts;
    public static int totalScoreStats;

    public TextMeshProUGUI timesRotatedTM;
    public TextMeshProUGUI totalAttemptsTM;
    public TextMeshProUGUI totalScoreStatsTM;

    void Start()
    {
        timesRotated = ES3.Load<int>("timesRotated", 0);
        totalAttempts = ES3.Load<int>("totalAttempts", 0);
        totalScoreStats = ES3.Load<int>("totalScoreStats", 0);

        timesRotatedTM.text = timesRotated.ToString("#,#0");
        totalAttemptsTM.text = totalAttempts.ToString("#,#0");
        totalScoreStatsTM.text = totalScoreStats.ToString("#,#0");
    }

}
