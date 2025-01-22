using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMainMenu : MonoBehaviour
{

    LevelSystem levelSystem;
    LevelSystemAnimated levelSystemAnimated;

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        this.levelSystemAnimated = levelSystemAnimated;
    }

    void Start()
    {
        levelSystem.oldExperience = ES3.Load<int>("savedExperience", 0);
        levelSystemAnimated.currentExperience = ES3.Load<int>("animatedExperience", 0);
        levelSystem.currentExperience = ES3.Load<int>("mainExperience", 0);
        levelSystem.level = ES3.Load<int>("level", 0);
        levelSystemAnimated.level = ES3.Load<int>("animatedLevel", 0);

        levelSystem.GetOldAndCurrentExp();
        levelSystemAnimated.level = levelSystem.level;
    }
}
