using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelingManager : MonoBehaviour
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experienceBarImage;

    public int level = 1;
    [SerializeField] int displayLevel;
    int levelGoal;

    [SerializeField] int currentExperience = 0; // Current experience in the bar
    int requiredExperience = 2500;
    [SerializeField] int totalExperience; // Total experience earned in current game
    int visualStartExperience;
    public int startExperience; // Starting experience in the bar
    
    [SerializeField] int visualExperience; // Displaying/Animating experience for the bar & text
    [SerializeField] int progressExperience;

    int experienceToGive = 25;

    int targetScore = 1;

    float timer = 0f;
    float duration = 1f;

    #region Get Variables
    public int GetStartExperience()
    {
        return startExperience;
    }

    public int GetStartExperienceEqualsCurrent()
    {
        return startExperience = currentExperience;
    }

    public int GetLevel()
    {
        return level;
    }

    float GetAnimatedExperienceNormalized()
    {
        return (float)visualExperience / requiredExperience;
    }

    float GetStartExperienceNormalized()
    {
        return (float)startExperience / requiredExperience;
    }
    #endregion

    void Awake()
    {
        

        OnLevelChanged += LevelingManager_OnLevelChanged;
        OnExperienceChanged += LevelingManager_OnExperienceChanged;
    }

    #region Events
    private void LevelingManager_OnExperienceChanged(object sender, EventArgs e) // Event: When experience changes
    {
        SetExperienceBarSize(GetAnimatedExperienceNormalized());

        SetExperienceText(visualExperience);
    }

    private void LevelingManager_OnLevelChanged(object sender, EventArgs e) // Event: When level changes
    {
        SetLevelNumberAnimated(displayLevel);        
    }
    #endregion

    void Start()
    {
        startExperience = ES3.Load<int>("experience", 0);

        currentExperience = startExperience;

        totalExperience = startExperience;    

        levelGoal = requiredExperience;
        
        visualStartExperience = startExperience;
        visualExperience = startExperience;

        level = ES3.Load<int>("level", 0);
        displayLevel = level;

        SetExperienceBarSize(GetStartExperienceNormalized());
        SetExperienceText(startExperience);
        SetLevelNumberAnimated(level);
    }

    void Update()
    {
        RecieveExperience();
        UpdateLeveling();        
    }

    #region Set Variables
    void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBarImage.fillAmount = experienceNormalized;
    }

    void SetExperienceText(int experience)
    {
        experienceText.text = experience + " / " + requiredExperience;
    }

    void SetLevelNumberAnimated(int levelNumber)
    {
        levelText.text = "LEVEL " + (levelNumber);
    }
    #endregion

    public IEnumerator AnimateExperience()
    {
        yield return new WaitForSeconds(1f);
        
        while (true)
        {
            if (progressExperience < totalExperience)
            {
                if (timer < duration)
                {
                    timer += Time.deltaTime;

                    visualExperience = ((int)Mathf.Lerp(visualStartExperience, totalExperience, (timer / duration)));
                    progressExperience = ((int)Mathf.Lerp(visualStartExperience, totalExperience, (timer / duration)));

                    while (progressExperience >= levelGoal)
                    {
                        displayLevel++;
                        if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);

                        levelGoal += requiredExperience;                       
                    }

                    while (visualExperience >= requiredExperience)
                    {
                        visualExperience -= requiredExperience;
                    }

                    if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
                }
                if (timer >= duration)
                {
                    timer = duration;                   
                }
            }

            yield return null;
        }
    }

    void RecieveExperience() // Increase exp. based on score
    {
        if (Score.currentScore >= targetScore)
        {
            currentExperience += experienceToGive; // Apply experience
            totalExperience += experienceToGive;

            experienceToGive = (int)(experienceToGive * 1.1f); // Multiply experience to give

            targetScore += 3;
        }
    }

    void UpdateLeveling()
    {
        while (currentExperience >= requiredExperience)
        {
            level++;
            currentExperience -= requiredExperience;           
        }
    }    
}
