using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    public int level;
    public int currentExperience;
    int requiredExperience;
    public int oldExperience;

    public LevelSystem()
    {
        level = 1;
        currentExperience = 0;
        requiredExperience = 2500;
        oldExperience = currentExperience;
    }

    public void AddExperience(int amount)
    {
        
        currentExperience += amount;

        while (currentExperience >= requiredExperience)
        {
            level++;
            currentExperience -= requiredExperience; // Resets currentExperience
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public float GetExperienceNormalized()
    {
        return (float)currentExperience / requiredExperience;
    }

    public int GetExperience()
    {
        return currentExperience;
    }

    public int GetRequiredExperience()
    {
        return requiredExperience;
    }

    public float GetOldExperienceNormalized()
    {
        return (float)oldExperience / requiredExperience;
    }

    public int GetOldExperience()
    {
        return oldExperience;
    }

    public int GetOldAndCurrentExp()
    {
        return oldExperience = currentExperience;
    }
}
