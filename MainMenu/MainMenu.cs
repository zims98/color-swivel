using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public LevelFader faderScript;

    public Animator buttonParentAnim;

    public bool buttonsInteractable;

    //public Animator statsAndHighscoreAnim;

    void Start()
    {
        DifficultyManager.difficultyIsNormal = false;
        DifficultyManager.difficultyIsHard = false;

        buttonsInteractable = true;

        Score.LoadHighscore();

        if (Score.highscore > 0)
            CloudOnceServices.instance.SubmitScoreToLeaderboard(Score.highscore);

        Score.currentScore = 0;
    }

    public void PlayGame()
    {
        faderScript.FadeToLevel(1);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
