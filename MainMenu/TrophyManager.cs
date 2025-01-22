using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyManager : MonoBehaviour
{

    public GameObject bronze, silver, gold, platinum, diamond, redEmerald;
    public GameObject bronzeLocked, silverLocked, goldLocked, platinumLocked, diamondLocked, redEmeraldLocked;

    public GameObject bronzeGlow, silverGlow, goldGlow, platinumGlow, diamondGlow, redEmeraldGlow;
    public GameObject bronzeSparkle, silverSparkle, goldSparkle, platinumSparkle, diamondSparkle, redEmeraldSparkle;

    //public GameObject bronzeText, silverText, goldText, platinumText, diamondText;

    //public bool bronzeUnlocked, silverUnlocked, goldUnlocked, platinumUnlocked, diamondUnlocked;

    void Start()
    {
        if (Score.highscore >= 50)
        {
            bronze.SetActive(true);
            bronzeLocked.SetActive(false);
            bronzeGlow.SetActive(true);
            bronzeSparkle.SetActive(true);
            //bronzeText.SetActive(true);
        }
        else
        {
            bronze.SetActive(false);
            bronzeLocked.SetActive(true);
            bronzeGlow.SetActive(false);
            bronzeSparkle.SetActive(false);
            //bronzeText.SetActive(false);
        }

        if (Score.highscore >= 100)
        {
            silver.SetActive(true);
            silverLocked.SetActive(false);
            silverGlow.SetActive(true);
            silverSparkle.SetActive(true);
            //silverText.SetActive(true);
        }
        else
        {
            silver.SetActive(false);
            silverLocked.SetActive(true);
            silverGlow.SetActive(false);
            silverSparkle.SetActive(false);
            //silverText.SetActive(false);
        }

        if (Score.highscore >= 150)
        {
            gold.SetActive(true);
            goldLocked.SetActive(false);
            goldGlow.SetActive(true);
            goldSparkle.SetActive(true);
            //goldText.SetActive(true);
        }
        else
        {
            gold.SetActive(false);
            goldLocked.SetActive(true);
            goldGlow.SetActive(false);
            goldSparkle.SetActive(false);
            //goldText.SetActive(false);
        }

        if (Score.highscore >= 200)
        {
            platinum.SetActive(true);
            platinumLocked.SetActive(false);
            platinumGlow.SetActive(true);
            platinumSparkle.SetActive(true);
            //platinumText.SetActive(true);
        }
        else
        {
            platinum.SetActive(false);
            platinumLocked.SetActive(true);
            platinumGlow.SetActive(false);
            platinumSparkle.SetActive(false);
            //platinumText.SetActive(false);
        }

        if (Score.highscore >= 250)
        {
            diamond.SetActive(true);
            diamondLocked.SetActive(false);
            diamondGlow.SetActive(true);
            diamondSparkle.SetActive(true);
            //diamondText.SetActive(true);
        }
        else
        {
            diamond.SetActive(false);
            diamondLocked.SetActive(true);
            diamondGlow.SetActive(false);
            diamondSparkle.SetActive(false);
            //diamondText.SetActive(false);
        }

        if (Score.highscore >= 300)
        {
            redEmerald.SetActive(true);
            redEmeraldLocked.SetActive(false);
            redEmeraldGlow.SetActive(true);
            redEmeraldSparkle.SetActive(true);
            //diamondText.SetActive(true);
        }
        else
        {
            redEmerald.SetActive(false);
            redEmeraldLocked.SetActive(true);
            redEmeraldGlow.SetActive(false);
            redEmeraldSparkle.SetActive(false);
            //diamondText.SetActive(false);
        }
    }

}
