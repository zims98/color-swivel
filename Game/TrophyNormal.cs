using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrophyNormal : MonoBehaviour
{
    [SerializeField] Image[] lockedSprites;
    [SerializeField] Sprite[] unlockedSprites;

    [SerializeField] GameObject[] sparkleObjects;

    [SerializeField] TextMeshProUGUI[] trophyCounterTexts;
    public int[] timesTrophiesUnlockedNormal = new int[6] { 0, 0, 0, 0, 0, 0 }; // Saves
    int[] timesTrophiesUnlockedAnimated = new int[6] { 0, 0, 0, 0, 0, 0 };

    public bool[] canDisplayNewAnimationNormal = new bool[6] { true, true, true, true, true, true }; // Saves

    public bool[] trophiesUnlockedNormal; // Saves

    [SerializeField] Animator anim;

    int numOfTrophiesUnlocked = 0;
    int numOfTrophiesNotNewAnymore = 0;

    [SerializeField] int bronzeTargetScore, silverTargetScore, goldTargetScore, platinumTargetScore, diamondTargetScore, redEmeraldTargetScore;

    [SerializeField] Animator[] textAnims;
    [SerializeField] Animator[] experienceAnims;

    // -----------------------------------------------------------------------

    #region Animation Events

    public void ChangeBronzeEvent()
    {
        lockedSprites[0].sprite = unlockedSprites[0];
        sparkleObjects[0].SetActive(trophiesUnlockedNormal[0]);
        trophyCounterTexts[0].text = "x " + timesTrophiesUnlockedNormal[0];
        experienceAnims[0].SetTrigger("bronzeExperience");
    }

    public void ChangeSilverEvent()
    {
        lockedSprites[1].sprite = unlockedSprites[1];
        sparkleObjects[1].SetActive(trophiesUnlockedNormal[1]);
        trophyCounterTexts[1].text = "x " + timesTrophiesUnlockedNormal[1];
        experienceAnims[1].SetTrigger("silverExperience");
    }

    public void ChangeGoldEvent()
    {
        lockedSprites[2].sprite = unlockedSprites[2];
        sparkleObjects[2].SetActive(trophiesUnlockedNormal[2]);
        trophyCounterTexts[2].text = "x " + timesTrophiesUnlockedNormal[2];
        experienceAnims[2].SetTrigger("goldExperience");
    }

    public void ChangePlatinumEvent()
    {
        lockedSprites[3].sprite = unlockedSprites[3];
        sparkleObjects[3].SetActive(trophiesUnlockedNormal[3]);
        trophyCounterTexts[3].text = "x " + timesTrophiesUnlockedNormal[3];
        experienceAnims[3].SetTrigger("platinumExperience");
    }

    public void ChangeDiamondEvent()
    {
        lockedSprites[4].sprite = unlockedSprites[4];
        sparkleObjects[4].SetActive(trophiesUnlockedNormal[4]);
        trophyCounterTexts[4].text = "x " + timesTrophiesUnlockedNormal[4];
        experienceAnims[4].SetTrigger("diamondExperience");
    }

    public void ChangeRedEmeraldEvent()
    {
        lockedSprites[5].sprite = unlockedSprites[5];
        sparkleObjects[5].SetActive(trophiesUnlockedNormal[5]);
        trophyCounterTexts[5].text = "x " + timesTrophiesUnlockedNormal[5];
        experienceAnims[5].SetTrigger("redEmeraldExperience");
    }

    #endregion

    // ------------------------------------------------------------------------

    IEnumerator AnimationDelay()
    {
        for (int i = 0; i < canDisplayNewAnimationNormal.Length; i++)
        {
            yield return new WaitForSeconds(1f);

            if (canDisplayNewAnimationNormal[0] == true && trophiesUnlockedNormal[0] == true)
            {
                canDisplayNewAnimationNormal[0] = false;
                anim.SetTrigger("bronze");                
            }

            if (canDisplayNewAnimationNormal[1] == true && trophiesUnlockedNormal[1] == true)
            {
                canDisplayNewAnimationNormal[1] = false;
                anim.SetTrigger("silver");               
            }

            if (canDisplayNewAnimationNormal[2] == true && trophiesUnlockedNormal[2] == true)
            {
                canDisplayNewAnimationNormal[2] = false;
                anim.SetTrigger("gold");               
            }

            if (canDisplayNewAnimationNormal[3] == true && trophiesUnlockedNormal[3] == true)
            {
                canDisplayNewAnimationNormal[3] = false;
                anim.SetTrigger("platinum");               
            }

            if (canDisplayNewAnimationNormal[4] == true && trophiesUnlockedNormal[4] == true)
            {
                canDisplayNewAnimationNormal[4] = false;
                anim.SetTrigger("diamond");               
            }

            if (canDisplayNewAnimationNormal[5] == true && trophiesUnlockedNormal[5] == true)
            {
                canDisplayNewAnimationNormal[5] = false;
                anim.SetTrigger("redEmerald");             
            }

            ES3.Save("canDisplayNewAnimationNormal", canDisplayNewAnimationNormal);
        }

        yield return null;
    }

    IEnumerator TextDisplay()
    {       
        for (int a = 0; a < numOfTrophiesUnlocked; a++)
        {
            if (trophiesUnlockedNormal[a] == true)
            {
                timesTrophiesUnlockedAnimated[a] = timesTrophiesUnlockedNormal[a] - 1;
                trophyCounterTexts[a].text = "x " + timesTrophiesUnlockedAnimated[a];                               
            }           
        }

        yield return new WaitForSeconds(1f); // Wait X seconds before updating counts

        if (Score.currentScore >= bronzeTargetScore)
        {
            textAnims[0].SetTrigger("bronzeCounter");
            trophyCounterTexts[0].text = "x " + timesTrophiesUnlockedNormal[0];
        }


        if (Score.currentScore >= silverTargetScore)
        {
            textAnims[1].SetTrigger("silverCounter");
            trophyCounterTexts[1].text = "x " + timesTrophiesUnlockedNormal[1];
        }


        if (Score.currentScore >= goldTargetScore)
        {
            textAnims[2].SetTrigger("goldCounter");
            trophyCounterTexts[2].text = "x " + timesTrophiesUnlockedNormal[2];
        }


        if (Score.currentScore >= platinumTargetScore)
        {
            textAnims[3].SetTrigger("platinumCounter");
            trophyCounterTexts[3].text = "x " + timesTrophiesUnlockedNormal[3];
        }


        if (Score.currentScore >= diamondTargetScore)
        {
            textAnims[4].SetTrigger("diamondCounter");
            trophyCounterTexts[4].text = "x " + timesTrophiesUnlockedNormal[4];
        }


        if (Score.currentScore >= redEmeraldTargetScore)
        {
            textAnims[5].SetTrigger("redEmeraldCounter");
            trophyCounterTexts[5].text = "x " + timesTrophiesUnlockedNormal[5];
        }
    }

    IEnumerator ExperienceTextDisplay()
    {
        yield return new WaitForSeconds(0.5f);

        if (Score.currentScore >= bronzeTargetScore) // Bronze
            experienceAnims[0].SetTrigger("bronzeExperience");

        if (Score.currentScore >= silverTargetScore) // Silver
            experienceAnims[1].SetTrigger("silverExperience");

        if (Score.currentScore >= goldTargetScore) // Gold
            experienceAnims[2].SetTrigger("goldExperience");

        if (Score.currentScore >= platinumTargetScore) // Platinum
            experienceAnims[3].SetTrigger("platinumExperience");

        if (Score.currentScore >= diamondTargetScore) // Diamond
            experienceAnims[4].SetTrigger("diamondExperience");

        if (Score.currentScore >= redEmeraldTargetScore) // Red Emerald
            experienceAnims[5].SetTrigger("redEmeraldExperience");
    }

    public void UpdateTrophies()
    {
        #region Check If Trophies Unlocked

        if (Score.highscore >= bronzeTargetScore)
            trophiesUnlockedNormal[0] = true; // Bronze

        if (Score.highscore >= silverTargetScore)
            trophiesUnlockedNormal[1] = true; // Silver

        if (Score.highscore >= goldTargetScore)
            trophiesUnlockedNormal[2] = true; // Gold

        if (Score.highscore >= platinumTargetScore)
            trophiesUnlockedNormal[3] = true; // Platinum

        if (Score.highscore >= diamondTargetScore)
            trophiesUnlockedNormal[4] = true; // Diamond

        if (Score.highscore >= redEmeraldTargetScore)
            trophiesUnlockedNormal[5] = true; // Red Emerald

        #endregion

        // --------------------------------------------------------------

        #region Increment Times Trophies Unlocked

        if (Score.currentScore >= bronzeTargetScore) // Bronze
            timesTrophiesUnlockedNormal[0]++;

        if (Score.currentScore >= silverTargetScore) // Silver
            timesTrophiesUnlockedNormal[1]++;

        if (Score.currentScore >= goldTargetScore) // Gold
            timesTrophiesUnlockedNormal[2]++;

        if (Score.currentScore >= platinumTargetScore) // Platinum
            timesTrophiesUnlockedNormal[3]++;

        if (Score.currentScore >= diamondTargetScore) // Diamond
            timesTrophiesUnlockedNormal[4]++;

        if (Score.currentScore >= redEmeraldTargetScore) // Red Emerald
            timesTrophiesUnlockedNormal[5]++;

        #endregion

        // --------------------------------------------------------------

        for (int i = 0; i < trophiesUnlockedNormal.Length; i++) // Trophies (booleans)
        {
            if (trophiesUnlockedNormal[i] == true)
                numOfTrophiesUnlocked++;         
        }

        for (int i = 0; i < canDisplayNewAnimationNormal.Length; i++)
        {
            if (canDisplayNewAnimationNormal[i] == false)
                numOfTrophiesNotNewAnymore++;
        }       

        for (int i = 0; i < numOfTrophiesUnlocked; i++) // Sprites & Counters & Visual Objects
        {
            for (int y = 0; y < numOfTrophiesNotNewAnymore; y++)
            {
                if (canDisplayNewAnimationNormal[y] == false) // If Trophies are *NOT* new anymore
                {
                    lockedSprites[y].sprite = unlockedSprites[y];

                    trophyCounterTexts[y].text = "x " + timesTrophiesUnlockedNormal[y]; // issue might be here              

                    StartCoroutine(TextDisplay());
                    StartCoroutine(ExperienceTextDisplay());

                    for (int x = 0; x < sparkleObjects.Length; x++) // Sparkle
                    {
                        sparkleObjects[y].SetActive(trophiesUnlockedNormal[y]);
                    }
                }              
            }

            if (canDisplayNewAnimationNormal[i] == true) // If Trophies ARE new
            {
                StartCoroutine(AnimationDelay());
            }

        }        

        // ---------------------------------------------------------------

        ES3.Save("trophiesUnlockedNormal", trophiesUnlockedNormal);
        ES3.Save("timesTrophiesUnlockedNormal", timesTrophiesUnlockedNormal);
        

    }
}
