using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingInstance : MonoBehaviour
{
    public static LevelingInstance instance;

    [SerializeField] LevelUI levelUI;
    [SerializeField] LevelVisuals levelVisuals;
    [SerializeField] LevelExperience levelExperience;
    //[SerializeField] GameMaster gameMaster; // - Only in Game Scenes -
    [SerializeField] LevelMainMenu levelMainMenu;

    LevelSystemAnimated levelSystemAnimated;
    LevelSystem levelSystem;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        //gameMaster = FindObjectOfType<GameMaster>();

        levelSystem = new LevelSystem();
        //LevelSystem levelSystem = new LevelSystem();
        levelUI.SetLevelSystem(levelSystem);
        levelExperience.SetLevelSystem(levelSystem);

        /*if (gameMaster != null)
            gameMaster.SetLevelSystem(levelSystem);*/

        if (levelMainMenu != null)
            levelMainMenu.SetLevelSystem(levelSystem);

        levelSystemAnimated = new LevelSystemAnimated(levelSystem);
        levelUI.SetLevelSystemAnimated(levelSystemAnimated);
        levelVisuals.SetLevelSystemAnimated(levelSystemAnimated);

        /*if (gameMaster != null)
            gameMaster.SetLevelSystemAnimated(levelSystemAnimated);*/

        if (levelMainMenu != null)
            levelMainMenu.SetLevelSystemAnimated(levelSystemAnimated);

    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        this.levelSystemAnimated = levelSystemAnimated;
    }
}
