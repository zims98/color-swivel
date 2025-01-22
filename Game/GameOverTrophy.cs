using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverTrophy : MonoBehaviour
{
    public Image bronzeLockedSprite, silverLockedSprite, goldLockedSprite, platinumLockedSprite, diamondLockedSprite, redEmeraldLockedSprite;
    public Sprite bronzeUnlockedSprite, silverUnlockedSprite, goldUnlockedSprite, platinumUnlockedSprite, diamondUnlockedSprite, redEmeraldUnlockedSprite;

    public GameObject bronzeGlow, silverGlow, goldGlow, platinumGlow, diamondGlow, redEmeraldGlow;
    public GameObject bronzeSparkle, silverSparkle, goldSparkle, platinumSparkle, diamondSparkle, redEmeraldSparkle;

    public GameObject bronzeText, silverText, goldText, platinumText, diamondText, redEmeraldText;

    public bool canDisplayNewBronze = true, silverIsNotNewAnymore, goldIsNotNewAnymore, platinumIsNotNewAnymore, diamondIsNotNewAnymore, redEmeraldIsNotNewAnymore;

    public TrophyCounter trophyCounterScript;
    public TextMeshProUGUI bronzeCounterText, silverCounterText, goldCounterText, platinumCounterText, diamondCounterText, redEmeraldCounterText;

    Animator anim;

    void Start()
    {
        canDisplayNewBronze = ES3.Load<bool>("bronzeBool", false);
        silverIsNotNewAnymore = ES3.Load<bool>("silverBool", false);
        goldIsNotNewAnymore = ES3.Load<bool>("goldBool", false);
        platinumIsNotNewAnymore = ES3.Load<bool>("platinumBool", false);
        diamondIsNotNewAnymore = ES3.Load<bool>("diamondBool", false);
        redEmeraldIsNotNewAnymore = ES3.Load<bool>("redEmeraldBool", false);

        anim = GetComponent<Animator>();

        Debug.Log(canDisplayNewBronze);
    }

    void SetCounterText()
    {
        // Bronze
        if (Score.highscore >= 5)
            bronzeCounterText.text = "x " + trophyCounterScript.timesBronzeUnlocked;
        else bronzeCounterText.text = "";

        //if (trophyCounterScript.timesBronzeUnlocked == 0 && bronzeIsNotNewAnymore)
          //  bronzeCounterText.text = "x " + 1;

        // Silver
        if (Score.highscore >= 10)
            silverCounterText.text = "x " + trophyCounterScript.timesSilverUnlocked;
        else silverCounterText.text = "";

        //if (trophyCounterScript.timesSilverUnlocked == 0 && silverIsNotNewAnymore)
          //  silverCounterText.text = "x " + 1;

        // Gold
        if (Score.highscore >= 15)
            goldCounterText.text = "x " + trophyCounterScript.timesGoldUnlocked;
        else goldCounterText.text = "";

        //if (trophyCounterScript.timesGoldUnlocked == 0 && goldIsNotNewAnymore)
          //  goldCounterText.text = "x " + 1;

        // Platinum
        if (Score.highscore >= 20)
            platinumCounterText.text = "x " + trophyCounterScript.timesPlatinumUnlocked;
        else platinumCounterText.text = "";

        //if (trophyCounterScript.timesPlatinumUnlocked == 0 && platinumIsNotNewAnymore)
          //  platinumCounterText.text = "x " + 1;

        // Diamond
        if (Score.highscore >= 25)
            diamondCounterText.text = "x " + trophyCounterScript.timesDiamondUnlocked;
        else diamondCounterText.text = "";

        //if (trophyCounterScript.timesDiamondUnlocked == 0 && diamondIsNotNewAnymore)
          //  diamondCounterText.text = "x " + 1;

        // Red Emerald
        if (Score.highscore >= 30)
            redEmeraldCounterText.text = "x " + trophyCounterScript.timesRedEmeraldUnlocked;
        else redEmeraldCounterText.text = "";

        //if (trophyCounterScript.timesRedEmeraldUnlocked == 0 && redEmeraldIsNotNewAnymore)
          //  redEmeraldCounterText.text = "x " + 1;       
    }

    IEnumerator AnimationDelayBronze()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("bronze");
    }

    public void UpdateTrophies()
    {        
        if (Score.highscore >= 5) // BRONZE
        {
            if (!canDisplayNewBronze)
            {
                ChangeBronzeSpriteEvent();
            }           

            if (canDisplayNewBronze) // if bronze IS NEW
            {
                bronzeText.SetActive(true); // "NEW" Text
                StartCoroutine(AnimationDelayBronze());

                canDisplayNewBronze = false; // bronze is NOT NEW anymore
                ES3.Save("bronzeBool", canDisplayNewBronze);
            }          
        }

        if (Score.highscore >= 10) // SILVER
        {
            silverLockedSprite.sprite = silverUnlockedSprite;
            silverGlow.SetActive(true);
            silverSparkle.SetActive(true);

            if (!silverIsNotNewAnymore) // if silver IS NEW
            {
                //silverText.SetActive(true); // "NEW" Text

                silverIsNotNewAnymore = true; // silver is NOT NEW anymore
                ES3.Save("silverBool", silverIsNotNewAnymore);
            }
        }

        if (Score.highscore >= 15) // GOLD
        {
            goldLockedSprite.sprite = goldUnlockedSprite;
            goldGlow.SetActive(true);
            goldSparkle.SetActive(true);

            if (!goldIsNotNewAnymore) // if gold IS NEW
            {
                //goldText.SetActive(true); // "NEW" Text

                goldIsNotNewAnymore = true; // gold is NOT NEW anymore
                ES3.Save("goldBool", goldIsNotNewAnymore);
            }
        }

        if (Score.highscore >= 20) // PLATINUM
        {
            platinumLockedSprite.sprite = platinumUnlockedSprite;
            platinumGlow.SetActive(true);
            platinumSparkle.SetActive(true);

            if (!platinumIsNotNewAnymore) // if platinum IS NEW
            {
                //platinumText.SetActive(true); // "NEW" Text

                platinumIsNotNewAnymore = true; // platinum is NOT NEW anymore
                ES3.Save("platinumBool", platinumIsNotNewAnymore);
            }
        }

        if (Score.highscore >= 25) // DIAMOND
        {
            diamondLockedSprite.sprite = diamondUnlockedSprite;
            diamondGlow.SetActive(true);
            diamondSparkle.SetActive(true);

            if (!diamondIsNotNewAnymore) // if diamond IS NEW
            {
                //diamondText.SetActive(true); // "NEW" Text

                diamondIsNotNewAnymore = true; // diamond is NOT NEW anymore
                ES3.Save("diamondBool", diamondIsNotNewAnymore);
            }
        }

        if (Score.highscore >= 30) // RED EMERALD
        {
            redEmeraldLockedSprite.sprite = redEmeraldUnlockedSprite;
            redEmeraldGlow.SetActive(true);
            redEmeraldSparkle.SetActive(true);

            if (!redEmeraldIsNotNewAnymore) // if redEmerald IS NEW
            {
                //redEmeraldText.SetActive(true); // "NEW" Text

                redEmeraldIsNotNewAnymore = true; // redEmerald is NOT NEW anymore
                ES3.Save("redEmeraldBool", redEmeraldIsNotNewAnymore);
            }
        }

        trophyCounterScript.CheckTimesUnlockedTrophies();
        SetCounterText();
    }

    public void ChangeBronzeSpriteEvent()
    {
        bronzeLockedSprite.sprite = bronzeUnlockedSprite;
        bronzeGlow.SetActive(true);
        bronzeSparkle.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // DEBUGGING! MUST BE DELETED WHEN PUBLISHED
        {
            canDisplayNewBronze = true; // bronze is NEW again
            silverIsNotNewAnymore = false; // silver is NOT NEW anymore
            goldIsNotNewAnymore = false;
            platinumIsNotNewAnymore = false;
            diamondIsNotNewAnymore = false;
            redEmeraldIsNotNewAnymore = false;
            trophyCounterScript.timesBronzeUnlocked = 0;
            ES3.Save("bronzeBool", canDisplayNewBronze);
            ES3.Save("silverBool", silverIsNotNewAnymore);
            ES3.Save("goldBool", goldIsNotNewAnymore);
            ES3.Save("platinumBool", platinumIsNotNewAnymore);
            ES3.Save("diamondBool", diamondIsNotNewAnymore);
            ES3.Save("redEmeraldBool", redEmeraldIsNotNewAnymore);
            canDisplayNewBronze = ES3.Load<bool>("bronzeBool", false);
            silverIsNotNewAnymore = ES3.Load<bool>("silverBool", false);
            goldIsNotNewAnymore = ES3.Load<bool>("goldBool", false);
            platinumIsNotNewAnymore = ES3.Load<bool>("platinumBool", false);
            diamondIsNotNewAnymore = ES3.Load<bool>("diamondBool", false);
            redEmeraldIsNotNewAnymore = ES3.Load<bool>("redEmeraldBool", false);
        }


    }
}
