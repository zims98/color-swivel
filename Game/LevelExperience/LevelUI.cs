using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour
{
    TextMeshProUGUI levelText;
    TextMeshProUGUI experienceText;

    Image experienceBarImage;
    LevelSystem levelSystem;
    LevelSystemAnimated levelSystemAnimated;

    bool updateExp;

    void Awake() // Referencing
    {
        levelText = transform.Find("levelText").GetComponent<TextMeshProUGUI>();
        experienceText = transform.Find("experienceText").GetComponent<TextMeshProUGUI>();

        //experienceBarImage = transform.Find("progressBar").Find("bar").GetComponent<Image>(); // Do you really need to find the parent object first?
        experienceBarImage = transform.Find("bar").GetComponent<Image>();
    }

    void SetExperienceBarSize(float experienceNormalized)
    {
        if (experienceBarImage != null)
            experienceBarImage.fillAmount = experienceNormalized;
    }

    void SetLevelNumber(int levelNumber)
    {
        if (levelText != null)
            levelText.text = "LEVEL " + (levelNumber);
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        //SetExperienceBarSize(levelSystem.GetOldExperienceNormalized()); // -
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        // Set the LevelSystemAnimated object
        this.levelSystemAnimated = levelSystemAnimated;

        // Update the starting values
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());

        // Subscribe to the changed events
        levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e)
    {
        // Level changed, update text
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e)
    {
        // Experience changed, update bar size
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());

        if (experienceText != null)
            experienceText.text = levelSystemAnimated.GetAnimatedExperience() + " / 2500";
    }

    void Start()
    {
        SetExperienceBarSize(levelSystem.GetOldExperienceNormalized());

        SetLevelNumber(levelSystemAnimated.GetLevelNumber()); // -

        experienceText.text = levelSystem.GetOldExperience() + " / 2500";

        StartCoroutine(UpdateExperienceDelay());
    }

    public void ButtonAddExperience() // UI Button for testing
    {
        levelSystem.AddExperience(250);
    }

    void Update()
    {
        if (updateExp)
            levelSystemAnimated.AnimateExperience();

        
    }



    IEnumerator UpdateExperienceDelay()
    {
        yield return new WaitForSeconds(1f);

        if (levelSystemAnimated.GetLevelNumber() < levelSystem.GetLevelNumber())
        {
            // Local level under target level
            updateExp = true;
        }
        else
        {
            // Local level equals the target level
            if (levelSystemAnimated.GetAnimatedExperience() < levelSystem.GetExperience())
            {
                updateExp = true;
            }
            else
            {                           

                updateExp = false;             
            }
            
            
        }
        //SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());
        //experienceText.text = levelSystemAnimated.GetAnimatedExperience() + " / 2500";
    }
}
