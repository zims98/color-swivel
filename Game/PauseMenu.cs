using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public LevelFader faderScript;

    public static bool gameIsPaused = false;

    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        ES3.Save("timesRotated", Stats.timesRotated);
        ES3.Save("totalScoreStats", Stats.totalScoreStats);
        Score.SaveHighscore();
        GameMaster.watchedAd = false;
        faderScript.FadeToLevel(0);
        PlayerBox.extraLifeConsumed = false;
        Score.currentScore = 0;
    }
}
