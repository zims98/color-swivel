using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrophyHard : MonoBehaviour
{
    [SerializeField] Image[] lockedSprites;
    [SerializeField] Sprite[] unlockedSprites;

    [SerializeField] GameObject[] sparkleObjects;

    [SerializeField] TextMeshProUGUI[] trophyCounterTexts;
    public int[] timesTrophiesUnlockedHard; // Saves
    int[] timesTrophiesUnlockedAnimated = new int[6] { 0, 0, 0, 0, 0, 0 };

    public bool[] canDisplayNewAnimationHard = new bool[6] { true, true, true, true, true, true }; // Saves - Default TRUE

    public bool[] trophiesUnlockedHard; // Saves

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
        sparkleObjects[0].SetActive(trophiesUnlockedHard[0]);
        trophyCounterTexts[0].text = "x " + timesTrophiesUnlockedHard[0];
        experienceAnims[0].SetTrigger("bronzeExperience");
    }

    public void ChangeSilverEvent()
    {
        lockedSprites[1].sprite = unlockedSprites[1];
        sparkleObjects[1].SetActive(trophiesUnlockedHard[1]);
        trophyCounterTexts[1].text = "x " + timesTrophiesUnlockedHard[1];
        experienceAnims[1].SetTrigger("silverExperience");
    }

    public void ChangeGoldEvent()
    {
        lockedSprites[2].sprite = unlockedSprites[2];
        sparkleObjects[2].SetActive(trophiesUnlockedHard[2]);
        trophyCounterTexts[2].text = "x " + timesTrophiesUnlockedHard[2];
        experienceAnims[2].SetTrigger("goldExperience");
    }

    public void ChangePlatinumEvent()
    {
        lockedSprites[3].sprite = unlockedSprites[3];
        sparkleObjects[3].SetActive(trophiesUnlockedHard[3]);
        trophyCounterTexts[3].text = "x " + timesTrophiesUnlockedHard[3];
        experienceAnims[3].SetTrigger("platinumExperience");
    }

    public void ChangeDiamondEvent()
    {
        lockedSprites[4].sprite = unlockedSprites[4];
        sparkleObjects[4].SetActive(trophiesUnlockedHard[4]);
        trophyCounterTexts[4].text = "x " + timesTrophiesUnlockedHard[4];
        experienceAnims[4].SetTrigger("diamondExperience");
    }

    public void ChangeRedEmeraldEvent()
    {
        lockedSprites[5].sprite = unlockedSprites[5];
        sparkleObjects[5].SetActive(trophiesUnlockedHard[5]);
        trophyCounterTexts[5].text = "x " + timesTrophiesUnlockedHard[5];
        experienceAnims[5].SetTrigger("redEmeraldExperience");
    }

    #endregion

    // ------------------------------------------------------------------------

    IEnumerator AnimationDelay()
    {
        for (int i = 0; i < canDisplayNewAnimationHard.Length; i++)
        {
            yield return new WaitForSeconds(1f);

            if (canDisplayNewAnimationHard[0] == true && trophiesUnlockedHard[0] == true)
            {
                canDisplayNewAnimationHard[0] = false;
                anim.SetTrigger("bronze");
            }

            if (canDisplayNewAnimationHard[1] == true && trophiesUnlockedHard[1] == true)
            {
                canDisplayNewAnimationHard[1] = false;
                anim.SetTrigger("silver");
            }

            if (canDisplayNewAnimationHard[2] == true && trophiesUnlockedHard[2] == true)
            {
                canDisplayNewAnimationHard[2] = false;
                anim.SetTrigger("gold");
            }

            if (canDisplayNewAnimationHard[3] == true && trophiesUnlockedHard[3] == true)
            {
                canDisplayNewAnimationHard[3] = false;
                anim.SetTrigger("platinum");
            }

            if (canDisplayNewAnimationHard[4] == true && trophiesUnlockedHard[4] == true)
            {
                canDisplayNewAnimationHard[4] = false;
                anim.SetTrigger("diamond");
            }

            if (canDisplayNewAnimationHard[5] == true && trophiesUnlockedHard[5] == true)
            {
                canDisplayNewAnimationHard[5] = false;
                anim.SetTrigger("redEmerald");
            }

            ES3.Save("canDisplayNewAnimationHard", canDisplayNewAnimationHard);
        }

        yield return null;
    }

    IEnumerator TextDisplay()
    {
        for (int a = 0; a < numOfTrophiesUnlocked; a++)
        {
            if (trophiesUnlockedHard[a] == true)
            {
                timesTrophiesUnlockedAnimated[a] = timesTrophiesUnlockedHard[a] - 1;
                trophyCounterTexts[a].text = "x " + timesTrophiesUnlockedAnimated[a];
            }
        }

        yield return new WaitForSeconds(1f); // Wait X seconds before updating counts

        if (Score.currentScore >= bronzeTargetScore)
        {
            textAnims[0].SetTrigger("bronzeCounter");
            trophyCounterTexts[0].text = "x " + timesTrophiesUnlockedHard[0];
        }


        if (Score.currentScore >= silverTargetScore)
        {
            textAnims[1].SetTrigger("silverCounter");
            trophyCounterTexts[1].text = "x " + timesTrophiesUnlockedHard[1];
        }


        if (Score.currentScore >= goldTargetScore)
        {
            textAnims[2].SetTrigger("goldCounter");
            trophyCounterTexts[2].text = "x " + timesTrophiesUnlockedHard[2];
        }


        if (Score.currentScore >= platinumTargetScore)
        {
            textAnims[3].SetTrigger("platinumCounter");
            trophyCounterTexts[3].text = "x " + timesTrophiesUnlockedHard[3];
        }


        if (Score.currentScore >= diamondTargetScore)
        {
            textAnims[4].SetTrigger("diamondCounter");
            trophyCounterTexts[4].text = "x " + timesTrophiesUnlockedHard[4];
        }


        if (Score.currentScore >= redEmeraldTargetScore)
        {
            textAnims[5].SetTrigger("redEmeraldCounter");
            trophyCounterTexts[5].text = "x " + timesTrophiesUnlockedHard[5];
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
            trophiesUnlockedHard[0] = true; // Bronze

        if (Score.highscore >= silverTargetScore)
            trophiesUnlockedHard[1] = true; // Silver

        if (Score.highscore >= goldTargetScore)
            trophiesUnlockedHard[2] = true; // Gold

        if (Score.highscore >= platinumTargetScore)
            trophiesUnlockedHard[3] = true; // Platinum

        if (Score.highscore >= diamondTargetScore)
            trophiesUnlockedHard[4] = true; // Diamond

        if (Score.highscore >= redEmeraldTargetScore)
            trophiesUnlockedHard[5] = true; // Red Emerald

        #endregion

        // --------------------------------------------------------------

        #region Increment Times Trophies Unlocked

        if (Score.currentScore >= bronzeTargetScore) // Bronze
            timesTrophiesUnlockedHard[0]++;

        if (Score.currentScore >= silverTargetScore) // Silver
            timesTrophiesUnlockedHard[1]++;

        if (Score.currentScore >= goldTargetScore) // Gold
            timesTrophiesUnlockedHard[2]++;

        if (Score.currentScore >= platinumTargetScore) // Platinum
            timesTrophiesUnlockedHard[3]++;

        if (Score.currentScore >= diamondTargetScore) // Diamond
            timesTrophiesUnlockedHard[4]++;

        if (Score.currentScore >= redEmeraldTargetScore) // Red Emerald
            timesTrophiesUnlockedHard[5]++;

        #endregion

        // --------------------------------------------------------------

        for (int i = 0; i < trophiesUnlockedHard.Length; i++) // Trophies (booleans)
        {
            if (trophiesUnlockedHard[i] == true)
                numOfTrophiesUnlocked++;
        }

        for (int i = 0; i < canDisplayNewAnimationHard.Length; i++)
        {
            if (canDisplayNewAnimationHard[i] == false)
                numOfTrophiesNotNewAnymore++;
        }

        for (int i = 0; i < numOfTrophiesUnlocked; i++) // Sprites & Counters & Visual Objects
        {
            for (int y = 0; y < numOfTrophiesNotNewAnymore; y++)
            {
                if (canDisplayNewAnimationHard[y] == false) // If Trophies are *NOT* new anymore
                {
                    lockedSprites[y].sprite = unlockedSprites[y];

                    trophyCounterTexts[y].text = "x " + timesTrophiesUnlockedHard[y]; // issue might be here              

                    StartCoroutine(TextDisplay());
                    StartCoroutine(ExperienceTextDisplay());

                    for (int x = 0; x < sparkleObjects.Length; x++) // Sparkle
                    {
                        sparkleObjects[y].SetActive(trophiesUnlockedHard[y]);
                    }
                }
            }

            if (canDisplayNewAnimationHard[i] == true) // If Trophies ARE new
            {
                StartCoroutine(AnimationDelay());
            }

        }

        // ---------------------------------------------------------------

        ES3.Save("trophiesUnlockedHard", trophiesUnlockedHard);
        ES3.Save("timesTrophiesUnlockedHard", timesTrophiesUnlockedHard);


    }
}