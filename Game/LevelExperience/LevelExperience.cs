using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExperience : MonoBehaviour
{
    LevelSystem levelSystem;

    int targetScore = 1;

    int experienceToGive = 25;

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }

    void Update()
    {
        if (Score.currentScore >= targetScore)
        {
            levelSystem.AddExperience(experienceToGive);

            experienceToGive *= (int)1.1f;

            targetScore += 1;           
        }    
    }
}
