using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTrophies : MonoBehaviour
{
    [SerializeField] Image[] trophyImages;
    [SerializeField] Sprite[] lockedSpritesNormal;
    [SerializeField] Sprite[] lockedSpritesHard;
    [SerializeField] Sprite[] unlockedSpritesNormal;
    [SerializeField] Sprite[] unlockedSpritesHard;

    [SerializeField] GameObject[] sparkleObjects;

    [SerializeField] TextMeshProUGUI[] trophyCounterTexts;   
    int[] timesTrophiesUnlockedAnimated = new int[6] { 0, 0, 0, 0, 0, 0 };
    public int[] timesTrophiesUnlockedNormal; // Saves
    public int[] timesTrophiesUnlockedHard; // Saves

    public bool[] canDisplayNewAnimationNormal = new bool[6] { true, true, true, true, true, true }; // Saves
    public bool[] canDisplayNewAnimationHard = new bool[6] { true, true, true, true, true, true }; // Saves

    public bool[] trophiesUnlockedNormal; // Saves
    public bool[] trophiesUnlockedHard; // Saves

    int numOfTrophiesUnlocked = 0;
    int numOfTrophiesNotNewAnymore = 0;

    [SerializeField] int bronzeTargetScore, silverTargetScore, goldTargetScore, platinumTargetScore, diamondTargetScore, redEmeraldTargetScore;

    [SerializeField] Animator anim;
    [SerializeField] Animator[] textAnims;
    [SerializeField] Animator[] experienceAnims;

    [SerializeField] TextMeshProUGUI[] experienceTexts = new TextMeshProUGUI[6];
    [HideInInspector] public int experienceToGive;

    // -----------------------------------------------------------------------

    #region Animation Events

    public void ChangeBronzeEvent()
    {
        if (DifficultyManager.difficultyIsNormal)
        {
            trophyImages[0].sprite = unlockedSpritesNormal[0];
            sparkleObjects[0].SetActive(trophiesUnlockedNormal[0]);
            trophyCounterTexts[0].text = "x " + timesTrophiesUnlockedNormal[0];
            experienceAnims[0].SetTrigger("bronzeExperience");
        }

        if (DifficultyManager.difficultyIsHard)
        {
            trophyImages[0].sprite = unlockedSpritesHard[0];
            sparkleObjects[0].SetActive(trophiesUnlockedHard[0]);
            trophyCounterTexts[0].text = "x " + timesTrophiesUnlockedHard[0];
            experienceAnims[0].SetTrigger("bronzeExperience");
        }
    }

    public void ChangeSilverEvent()
    {
        if (DifficultyManager.difficultyIsNormal)
        {
            trophyImages[1].sprite = unlockedSpritesNormal[1];
            sparkleObjects[1].SetActive(trophiesUnlockedNormal[1]);
            trophyCounterTexts[1].text = "x " + timesTrophiesUnlockedNormal[1];
            experienceAnims[1].SetTrigger("silverExperience");
        }
        
        if (DifficultyManager.difficultyIsHard)
        {
            trophyImages[1].sprite = unlockedSpritesHard[1];
            sparkleObjects[1].SetActive(trophiesUnlockedHard[1]);
            trophyCounterTexts[1].text = "x " + timesTrophiesUnlockedHard[1];
            experienceAnims[1].SetTrigger("silverExperience");
        }
    }

    public void ChangeGoldEvent()
    {
        if (DifficultyManager.difficultyIsNormal)
        {
            trophyImages[2].sprite = unlockedSpritesNormal[2];
            sparkleObjects[2].SetActive(trophiesUnlockedNormal[2]);
            trophyCounterTexts[2].text = "x " + timesTrophiesUnlockedNormal[2];
            experienceAnims[2].SetTrigger("goldExperience");
        }
        
        if (DifficultyManager.difficultyIsHard)
        {
            trophyImages[2].sprite = unlockedSpritesHard[2];
            sparkleObjects[2].SetActive(trophiesUnlockedHard[2]);
            trophyCounterTexts[2].text = "x " + timesTrophiesUnlockedHard[2];
            experienceAnims[2].SetTrigger("goldExperience");
        }
    }

    public void ChangePlatinumEvent()
    {
        if (DifficultyManager.difficultyIsNormal)
        {
            trophyImages[3].sprite = unlockedSpritesNormal[3];
            sparkleObjects[3].SetActive(trophiesUnlockedNormal[3]);
            trophyCounterTexts[3].text = "x " + timesTrophiesUnlockedNormal[3];
            experienceAnims[3].SetTrigger("platinumExperience");
        }
        
        if (DifficultyManager.difficultyIsHard)
        {
            trophyImages[3].sprite = unlockedSpritesHard[3];
            sparkleObjects[3].SetActive(trophiesUnlockedHard[3]);
            trophyCounterTexts[3].text = "x " + timesTrophiesUnlockedHard[3];
            experienceAnims[3].SetTrigger("platinumExperience");
        }
    }

    public void ChangeDiamondEvent()
    {
        if (DifficultyManager.difficultyIsNormal)
        {
            trophyImages[4].sprite = unlockedSpritesNormal[4];
            sparkleObjects[4].SetActive(trophiesUnlockedNormal[4]);
            trophyCounterTexts[4].text = "x " + timesTrophiesUnlockedNormal[4];
            experienceAnims[4].SetTrigger("diamondExperience");
        }
        
        if (DifficultyManager.difficultyIsHard)
        {
            trophyImages[4].sprite = unlockedSpritesHard[4];
            sparkleObjects[4].SetActive(trophiesUnlockedHard[4]);
            trophyCounterTexts[4].text = "x " + timesTrophiesUnlockedHard[4];
            experienceAnims[4].SetTrigger("diamondExperience");
        }
    }

    public void ChangeRedEmeraldEvent()
    {
        if (DifficultyManager.difficultyIsNormal)
        {
            trophyImages[5].sprite = unlockedSpritesNormal[5];
            sparkleObjects[5].SetActive(trophiesUnlockedNormal[5]);
            trophyCounterTexts[5].text = "x " + timesTrophiesUnlockedNormal[5];
            experienceAnims[5].SetTrigger("redEmeraldExperience");
        }
        
        if (DifficultyManager.difficultyIsHard)
        {
            trophyImages[5].sprite = unlockedSpritesHard[5];
            sparkleObjects[5].SetActive(trophiesUnlockedHard[5]);
            trophyCounterTexts[5].text = "x " + timesTrophiesUnlockedHard[5];
            experienceAnims[5].SetTrigger("redEmeraldExperience");
        }
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

    IEnumerator TextDisplayNormal()
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

    IEnumerator TextDisplayHard()
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

    // --------------------------------------

    IEnumerator ExperienceTextDisplayNormal()
    {
        experienceTexts[0].text = "50 XP";
        experienceTexts[1].text = "100 XP";
        experienceTexts[2].text = "150 XP";
        experienceTexts[3].text = "300 XP";
        experienceTexts[4].text = "400 XP";
        experienceTexts[5].text = "500 XP";

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

    IEnumerator ExperienceTextDisplayHard()
    {
        experienceTexts[0].text = "100 XP";
        experienceTexts[1].text = "200 XP";
        experienceTexts[2].text = "300 XP";
        experienceTexts[3].text = "500 XP";
        experienceTexts[4].text = "700 XP";
        experienceTexts[5].text = "900 XP";

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
        for (int i = 0; i < trophyImages.Length; i++) // Declare default locked sprites depending on difficulty
        {
            if (DifficultyManager.difficultyIsNormal)
                trophyImages[i].sprite = lockedSpritesNormal[i];

            if (DifficultyManager.difficultyIsHard)
                trophyImages[i].sprite = lockedSpritesHard[i];
        }

        // Diffc. Done
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
        
        // Diffc. Done
        #region Increment Times Trophies Unlocked

        if (DifficultyManager.difficultyIsNormal)
        {
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
        }

        if (DifficultyManager.difficultyIsHard)
        {
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
        }

        #endregion // // 

        // --------------------------------------------------------------

        if (DifficultyManager.difficultyIsNormal)
        {
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
                        trophyImages[y].sprite = unlockedSpritesNormal[y];

                        trophyCounterTexts[y].text = "x " + timesTrophiesUnlockedNormal[y];           

                        StartCoroutine(TextDisplayNormal());
                        StartCoroutine(ExperienceTextDisplayNormal());

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
        }

        if (DifficultyManager.difficultyIsHard)
        {
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
                        trophyImages[y].sprite = unlockedSpritesHard[y];

                        trophyCounterTexts[y].text = "x " + timesTrophiesUnlockedHard[y];            

                        StartCoroutine(TextDisplayHard());
                        StartCoroutine(ExperienceTextDisplayHard());

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
        }



        // ---------------------------------------------------------------

        ES3.Save("trophiesUnlockedNormal", trophiesUnlockedNormal);
        ES3.Save("timesTrophiesUnlockedNormal", timesTrophiesUnlockedNormal);


    }
}