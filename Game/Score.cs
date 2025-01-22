using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int currentScore = 0;
    public TextMeshProUGUI highscoreText;

    public static int highscore;
    public static int prevHighscore;

    [HideInInspector]
    public bool newBestScore; // Referencing to "GameMaster"

    public Animator gainFiveScoreAnim;
    public Animator loseFiveScoreÁnim;
    public float timer = 0f;
    public float duration = 1f;
    public int currentDisplayScore;
    public int finalDisplayScore;
    public bool incrementScore;
    bool decrementScore;

    private void Awake()
    {
        LoadHighscore();

        highscoreText.text = highscore.ToString("0");
    }

    public void SecretGainScore()
    {
        FindObjectOfType<AudioManager>().Play("PlayerScoreGain5");
        gainFiveScoreAnim.SetTrigger("GainFiveScore");
        incrementScore = true;
        currentDisplayScore = currentScore;
        currentScore += 5;
        StartCoroutine(GainScoreOverTime());
    }
    public void SecretLoseScore()
    {
        FindObjectOfType<AudioManager>().Play("PlayerScoreLose5");
        loseFiveScoreÁnim.SetTrigger("LoseFiveScore");       
        decrementScore = true;
        currentDisplayScore = currentScore;
        currentScore -= 5;
        StartCoroutine(LoseScoreOverTime());     
    }
    public IEnumerator GainScoreOverTime()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            if (currentDisplayScore < currentScore)
            {
                if (timer >= duration)
                {
                    timer = 0f;
                    incrementScore = false;
                    currentDisplayScore = currentScore;
                    break;
                }

                if (timer < duration)
                {
                    timer += Time.deltaTime;

                    int finalScore = Mathf.CeilToInt(Mathf.Lerp(currentDisplayScore, currentScore, (timer / duration)));
                    scoreText.text = finalScore.ToString();
                }
            }

            yield return null;
        }
    }

    public IEnumerator LoseScoreOverTime()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            if (currentDisplayScore > currentScore)
            {
                if (timer >= duration)
                {
                    timer = 0f;
                    decrementScore = false;
                    currentDisplayScore = currentScore;
                    break;
                }

                if (timer < duration)
                {
                    timer += Time.deltaTime;

                    int finalScore = Mathf.CeilToInt(Mathf.Lerp(currentDisplayScore, currentScore, (timer / duration)));
                    scoreText.text = finalScore.ToString();
                }
            }

            yield return null;
        }
    }



    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R)) // RESET HIGHSCORE
        {
            highscore = 0;
            prevHighscore = 0;
            SaveHighscore();
            LoadHighscore();
        }

        if (incrementScore || decrementScore)
            return;

        scoreText.text = currentScore.ToString();

        /*if (currentScore > highscore)
        {
            prevHighscore = highscore;
            newBestScore = true;
            highscore = currentScore;

            SaveHighscore();
        }*/

        //Debug.Log("Current Score: " + currentScore);
                  
    }

    public static void SaveHighscore()
    {
        ES3.Save("highscore", highscore);
    }

    public static void LoadHighscore()
    {
        highscore = ES3.Load<int>("highscore", 0);
    }
}
