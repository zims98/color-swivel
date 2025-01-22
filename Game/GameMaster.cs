using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMaster : MonoBehaviour
{
    //public CameraShake shakeScript; // Old Camera Shake
    
    public Score scoreScript;
    public ObstacleSpawner spawnerScript;
    public GameOverTrophy trophyScript;
    public Currency currencyScript;
    public LevelingManager levelingManagerScript;

    public LevelFader faderScript;

    [HideInInspector]
    public bool gameIsOver = false;

    bool skipExtraLife = false;

    public float menuPopUpDelay;

    private int totalScore;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI highscoreText;

    //public TextMeshProUGUI prevHighscoreText;
    //public GameObject prevHighscoreObject;

    public TextMeshProUGUI highscoreTextString;

    public Animator newBestAnim;

    public GameObject gameOverObject;
    public GameObject continueWindow;
    public GameObject pauseButtonObject;

    public Animator guideAnim;

    public static bool watchedAd;

    public TrophyNormal trophyNormalScript;
    public TrophyHard trophyHardScript;

    //LevelSystem levelSystem;
    //LevelSystemAnimated levelSystemAnimated;

    Scene currentScene;
    string sceneName;

    [SerializeField] bool[] boolArray = new bool[6]; // Default value when loading bool arrays
    [SerializeField] int[] intArray = new int[6]; // Default value when loading int arrays

    /*public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        this.levelSystemAnimated = levelSystemAnimated;
    }*/

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    void Start()
    {
        guideAnim.SetTrigger("GuideFadeOut");

        // Loading Trophies
        if (currentScene.name == "Normal Game")
        {
            trophyNormalScript.trophiesUnlockedNormal = ES3.Load<bool[]>("trophiesUnlockedNormal");
            trophyNormalScript.timesTrophiesUnlockedNormal = ES3.Load<int[]>("timesTrophiesUnlockedNormal");
            trophyNormalScript.canDisplayNewAnimationNormal = ES3.Load<bool[]>("canDisplayNewAnimationNormal");
        }

        if (currentScene.name == "Hard Game")
        {
            trophyHardScript.trophiesUnlockedHard = ES3.Load<bool[]>("trophiesUnlockedHard", boolArray);
            trophyHardScript.timesTrophiesUnlockedHard = ES3.Load<int[]>("timesTrophiesUnlockedHard", intArray);
            trophyHardScript.canDisplayNewAnimationHard = ES3.Load<bool[]>("canDisplayNewAnimationHard", boolArray);
        }
    }
    void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.R))
        {
            currencyScript.finalDisplayCurrency = 0;

            ES3.Save("finalDisplayCurrency", currencyScript.finalDisplayCurrency);
        }*/

        //Debug.Log("Experience: " + levelSystem.GetExperience());
        //Debug.Log("Animated Experience: " + levelSystemAnimated.GetAnimatedExperience());
        //Debug.Log("Level: " + levelSystem.GetLevelNumber());
        //Debug.Log("Animated Level: " + levelSystemAnimated.GetLevelNumber());
        //Debug.Log("Old Experience: " + levelSystem.GetOldExperience());

        /*if (Input.GetKey(KeyCode.L)) // DELETE BEFORE PUBLISH
        {
            levelSystem.oldExperience = 0;
            levelSystem.currentExperience = 0;
            levelSystem.level = 1;

            levelSystemAnimated.currentExperience = 0;
            levelSystemAnimated.level = 1;
        }*/       

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentScene.name == "Normal Game")
            {
                trophyNormalScript.trophiesUnlockedNormal[0] = false;
                trophyNormalScript.trophiesUnlockedNormal[1] = false;
                trophyNormalScript.trophiesUnlockedNormal[2] = false;
                trophyNormalScript.trophiesUnlockedNormal[3] = false;
                trophyNormalScript.trophiesUnlockedNormal[4] = false;
                trophyNormalScript.trophiesUnlockedNormal[5] = false;

                trophyNormalScript.canDisplayNewAnimationNormal[0] = true;
                trophyNormalScript.canDisplayNewAnimationNormal[1] = true;
                trophyNormalScript.canDisplayNewAnimationNormal[2] = true;
                trophyNormalScript.canDisplayNewAnimationNormal[3] = true;
                trophyNormalScript.canDisplayNewAnimationNormal[4] = true;
                trophyNormalScript.canDisplayNewAnimationNormal[5] = true;

                ES3.Save("trophiesUnlockedNormal", trophyNormalScript.trophiesUnlockedNormal);
                ES3.Save("timesTrophiesUnlockedNormal", trophyNormalScript.timesTrophiesUnlockedNormal);
                ES3.Save("canDisplayNewAnimationNormal", trophyNormalScript.canDisplayNewAnimationNormal);

                trophyNormalScript.trophiesUnlockedNormal = ES3.Load<bool[]>("trophiesUnlockedNormal");
                trophyNormalScript.timesTrophiesUnlockedNormal = ES3.Load<int[]>("timesTrophiesUnlockedNormal");
                trophyNormalScript.canDisplayNewAnimationNormal = ES3.Load<bool[]>("canDisplayNewAnimationNormal");
            }

            if (currentScene.name == "Hard Game")
            {
                trophyHardScript.trophiesUnlockedHard[0] = false;
                trophyHardScript.trophiesUnlockedHard[1] = false;
                trophyHardScript.trophiesUnlockedHard[2] = false;
                trophyHardScript.trophiesUnlockedHard[3] = false;
                trophyHardScript.trophiesUnlockedHard[4] = false;
                trophyHardScript.trophiesUnlockedHard[5] = false;

                trophyHardScript.canDisplayNewAnimationHard[0] = true;
                trophyHardScript.canDisplayNewAnimationHard[1] = true;
                trophyHardScript.canDisplayNewAnimationHard[2] = true;
                trophyHardScript.canDisplayNewAnimationHard[3] = true;
                trophyHardScript.canDisplayNewAnimationHard[4] = true;
                trophyHardScript.canDisplayNewAnimationHard[5] = true;

                ES3.Save("trophiesUnlockedHard", trophyHardScript.trophiesUnlockedHard);
                ES3.Save("timesTrophiesUnlockedHard", trophyHardScript.timesTrophiesUnlockedHard);
                ES3.Save("canDisplayNewAnimationHard", trophyHardScript.canDisplayNewAnimationHard);

                trophyHardScript.trophiesUnlockedHard = ES3.Load<bool[]>("trophiesUnlockedHard");
                trophyHardScript.timesTrophiesUnlockedHard = ES3.Load<int[]>("timesTrophiesUnlockedHard");
                trophyHardScript.canDisplayNewAnimationHard = ES3.Load<bool[]>("canDisplayNewAnimationHard");
            }



        }
    }

    #region Game Over
    public void GameOver() // The game is over and stats will be checked & saved
    {
        if (!gameIsOver)
        {
            gameIsOver = true;

            if (Score.currentScore > Score.highscore)
            {
                Score.prevHighscore = Score.highscore;
                scoreScript.newBestScore = true;
                Score.highscore = Score.currentScore;

                //CloudOnceServices.instance.SubmitScoreToLeaderboard(Score.highscore); // Submitting the game's highscore to the online leaderboard.

                /*if (GamesServices.connectedToGooglePlay) // Google Play Games Package (currently removed)
                {
                    Social.ReportScore(Score.highscore, GPGSIds.leaderboard_online_highscore, (bool success) =>
                    {
                        if (success)
                            Debug.Log("Updated Leaderboard");
                        else
                            Debug.Log("Unable to update Leaderboard");
                    });
                }*/

                Score.SaveHighscore();
            }

            totalScore = Score.currentScore;
            totalScoreText.text = totalScore.ToString();

            Score.LoadHighscore();
            highscoreText.text = Score.highscore.ToString("0");

            // Checking Score & Trophies


            // Currency
            currencyScript.currentDisplayCurrency = currencyScript.finalDisplayCurrency;
            Currency.myCurrency += currencyScript.totalCurrencyToGive;
            ES3.Save("myCurrency", Currency.myCurrency);
            currencyScript.finalDisplayCurrency = Currency.myCurrency;
            ES3.Save("finalDisplayCurrency", currencyScript.finalDisplayCurrency);

            // Experience & Leveling
            levelingManagerScript.GetStartExperienceEqualsCurrent();
            ES3.Save("experience", levelingManagerScript.startExperience);
            ES3.Save("level", levelingManagerScript.level);

            //currencyScript.currencyToGiveText.text = "+" + currencyScript.totalCurrencyToGive.ToString(); //

            currencyScript.totalCurrencyToGive = 0; // Maybe not needed?


            if (!skipExtraLife) // If Player has already consumed extra life once and died again. 
            {
                StartCoroutine(GameOverDelay());
            }

            // Leveling System
            /*levelSystemAnimated.GetAnimExpAndOldExp();

            ES3.Save("savedExperience", levelSystem.GetOldExperience());
            ES3.Save("animatedExperience", levelSystemAnimated.GetAnimatedExperience());
            ES3.Save("mainExperience", levelSystem.GetExperience());
            ES3.Save("level", levelSystem.GetLevelNumber());
            ES3.Save("animatedLevel", levelSystemAnimated.GetLevelNumber());*/

        }      
    }
    #endregion


    public void CloseWindow() // If Player skips extra life, directly jump to GameOver Menu
    {
        skipExtraLife = true;
        GameOver();
        gameOverObject.SetActive(true);

        // Trophies
        if (currentScene.name == "Normal Game")
            trophyNormalScript.UpdateTrophies();

        if (currentScene.name == "Hard Game")
            trophyHardScript.UpdateTrophies();

        currencyScript.currencyText.text = "<sprite index=0>" + currencyScript.currentDisplayCurrency.ToString();
        StartCoroutine(currencyScript.CurrencyUpdater());
        StartCoroutine(levelingManagerScript.AnimateExperience());

        if (scoreScript.newBestScore)
        {
            newBestAnim.SetTrigger("newBest");
            highscoreTextString.text = "NEW BEST SCORE";
            //prevHighscoreObject.SetActive(true);
            //prevHighscoreText.text = Score.prevHighscore.ToString("0");
        }
        else
        {
            highscoreTextString.text = "BEST SCORE";
        }
    }

    #region Continue Game
    public void ContinueGame() // If Player pressed on Continue button (AD)
    {
        ES3.Save("experience", levelingManagerScript.GetStartExperience());
        ES3.Save("level", levelingManagerScript.GetLevel());

        ES3.Save("totalCurrencyToGive", currencyScript.totalCurrencyToGive);
        ES3.Save("currencyToGive", currencyScript.currencyToGive);

        watchedAd = true;

        ES3.Save("obstacleSpeed", spawnerScript.obstacleSpeed);
        PlayerBox.extraLifeConsumed = true;

        if (currentScene.name == "Hard Game")
        {
            faderScript.FadeToLevel(1); // Fade to Hard Game           
        }

        if (currentScene.name == "Hard Game")
        {
            faderScript.FadeToLevel(2); // Fade to Hard Game
        }
        
    }
    #endregion

    #region Game Over Delay
    IEnumerator GameOverDelay() // If Player has already consumed extra life but dies again, start delay before showing GameOver Menu
    {
        yield return new WaitForSeconds(menuPopUpDelay);

        gameOverObject.SetActive(true);

        // Trophies
        if (currentScene.name == "Normal Game")
            trophyNormalScript.UpdateTrophies();

        if (currentScene.name == "Hard Game")
            trophyHardScript.UpdateTrophies();       

        currencyScript.currencyText.text = "<sprite index=0>" + currencyScript.currentDisplayCurrency.ToString();
        StartCoroutine(currencyScript.CurrencyUpdater());
        StartCoroutine(levelingManagerScript.AnimateExperience());

        if (scoreScript.newBestScore)
        {
            newBestAnim.SetTrigger("newBest");
            highscoreTextString.text = "NEW BEST SCORE";
            //prevHighscoreObject.SetActive(true);
            //prevHighscoreText.text = Score.prevHighscore.ToString("0");
        }
        else
        {
            highscoreTextString.text = "BEST SCORE";
        }
    }
    #endregion

    public void RetryGame()
    {     
        if (currentScene.name == "Normal Game")
        {
            faderScript.FadeToLevel(1);
            Score.currentScore = 0;
        }

        if (currentScene.name == "Hard Game")
        {
            faderScript.FadeToLevel(2);
            Score.currentScore = 0;
        }       
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Score.SaveHighscore();
            ES3.Save("timesRotated", Stats.timesRotated);
            ES3.Save("totalScoreStats", Stats.totalScoreStats);
        }        
    }

    private void OnApplicationQuit()
    {
        PlayerBox.extraLifeConsumed = false;
        watchedAd = false;
    }
}
