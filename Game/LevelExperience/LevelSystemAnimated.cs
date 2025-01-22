using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystemAnimated
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    LevelSystem levelSystem;

    bool isAnimating;

    float updateTimer;
    float updateTimerMax;

    public int level;
    public int currentExperience;
    int requiredExperience;

    public LevelSystemAnimated(LevelSystem levelSystem)
    {
        SetLevelSystem(levelSystem);
        updateTimerMax = 0.0010f;
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        level = levelSystem.GetLevelNumber();
        currentExperience = levelSystem.GetExperience();
        requiredExperience = levelSystem.GetRequiredExperience();

        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }

    public void AnimateExperience()
    {
        if (isAnimating)
        {
            // Check if its time to update
            updateTimer += Time.deltaTime;

            while (updateTimer > updateTimerMax)
            {
                // Time to update
                updateTimer -= updateTimerMax;

                UpdateAddExperience();
            }         
        }
    }

    void UpdateAddExperience()
    {
        if (level < levelSystem.GetLevelNumber())
        {
            // Local level under target level
            AddExperience();
        }
        else
        {
            // Local level equals the target level
            if (currentExperience < levelSystem.GetExperience())
            {
                AddExperience();
            }
            else
            {
                isAnimating = false;
            }
        }
    }

    void AddExperience()
    {
        currentExperience++;
        if (currentExperience >= requiredExperience)
        {
            level++;
            currentExperience = 0;

            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    /*public IEnumerator AddExperienceOverTime()
    {
        yield return new WaitForSeconds(1f); // Delay

        Debug.Log("Coroutine triggered");

        while (true)
        {
            if (level < levelSystem.GetLevelNumber() || currentExperience < levelSystem.GetExperience())
            {
                if (timer < duration)
                {
                    timer += Time.deltaTime;

                    int finalExperience = Mathf.CeilToInt(Mathf.Lerp(currentExperience, levelSystem.oldExperience, (timer / duration)));

                    // Set text

                    if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
                }
                if (timer >= duration)
                {
                    timer = duration;
                }

            }
            else
            {
                // Set text
            }

            if (currentExperience >= requiredExperience)
            {
                level++;
                currentExperience = 0;
                if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
            }

            yield return null;
        }
    }*/

    public int GetLevelNumber()
    {
        return level;
    }

    public float GetExperienceNormalized()
    {
        return (float)currentExperience / requiredExperience;
    }

    public int GetAnimatedExperience()
    {
        return currentExperience;
    }

    public int GetAnimExpAndOldExp()
    {
        return currentExperience = levelSystem.oldExperience;
    }
}
