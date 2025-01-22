using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles; // An array of the obstacles.
    private int randomObstacle; // Choosing a random obstacle from the array.

    public GameObject[] dirRightObstacles;
    private int randomDirRightObstacle;

    public GameObject[] dirLeftObstacles;
    private int randomDirLeftObstacle;

    public GameObject[] swapXObstacles;
    private int randomSwapXObstacle;

    public GameObject[] swapYObstacles;
    private int randomSwapYObstacle;

    public GameObject[] deadlyObstacles;
    private int randomDeadlyObstacle;

    public GameObject[] blindObstacles;
    private int randomBlindObstacle;

    public GameObject mimicObstacle;

    public float spawnDelay; // Spawn delay when starting the game.

    int lastIndexDefault = -1;
    int lastIndexRight = -1;
    int lastIndexLeft = -1;
    int lastIndexSwapX = -1;
    int lastIndexSwapY = -1;
    int lastIndexDeadly = -1;
    int lastIndexSecret = -1;
    int lastIndexBlind = -1;

    public PlayerBox playerScript;
    public GameMaster gameMasterScript;

    public float chanceToSpawnDirObstacle = 1f; // Default is 1 (0%), decreases by 0.05f (5%) each time an ordinary obstacle has spawned.

    public float chanceToSpawnSwapXObstacle;
    public float chanceToSpawnSwapYObstacle;

    public float chanceToSpawnDeadly;
    public float chanceToSpawnMimic;
    public float chanceToSpawnBlind;
    public float chanceToSpawnSecret;

    public GameObject[] secretObstacles;
    int randomSecretObstacle;

    public bool canCount;
    public bool hasSpawned;

    public float obstacleSpeed;
    //public float savedObstacleSpeed;

    public Unlockables unlockableScript;

    Scene currentScene;
    string sceneName;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    private void Start()
    {
        if (PlayerBox.extraLifeConsumed)
            obstacleSpeed = ES3.Load("obstacleSpeed", obstacleSpeed); // Loads the old & saved speed from death
        else
            obstacleSpeed = 4f; // Restores back to normal speed if Player has died with its extra life (Default 4) 3.7                 

        canCount = false;
        hasSpawned = true;

        StartCoroutine(SpawnStartDelay());
    }

    #region Increment Obstacle Speed
    public void IncrementObstacleSpeed()
    {
        // Normal
        if (currentScene.name == "Normal Game")
        {
            if (obstacleSpeed < 7f) // 7.5 = Max speed -> NEW Max 7
            {
                if (obstacleSpeed > 5.6f) // OLD: 6.2 NEW: 6 -> 5.6
                {
                    obstacleSpeed += 0.0025f;
                }
                else if (obstacleSpeed > 5.2f) // OLD: 5.8 NEW: 5.5 -> 5.3 -> 5.2
                {
                    obstacleSpeed += 0.005f;
                }
                else if (obstacleSpeed > 4.9f) // OLD: 5.4 NEW: 4.9
                {
                    obstacleSpeed += 0.01f;
                }
                else if (obstacleSpeed > 4.4f) // OLD: 4.8 NEW: 4.5
                {
                    obstacleSpeed += 0.025f;
                }
                else
                {
                    obstacleSpeed += 0.05f;
                }
            }
        }

        // Hard
        if (currentScene.name == "Hard Game")
        {
            if (obstacleSpeed < 7f) // 7.5 = Max speed -> NEW Max 7
            {
                if (obstacleSpeed > 5.6f) // OLD: 6.2 NEW: 6 -> 5.6
                {
                    obstacleSpeed += 0.0025f;
                }
                else if (obstacleSpeed > 5.2f) // OLD: 5.8 NEW: 5.5 -> 5.3 -> 5.2
                {
                    obstacleSpeed += 0.005f;
                }
                else if (obstacleSpeed > 4.9f) // OLD: 5.4 NEW: 4.9
                {
                    obstacleSpeed += 0.01f;
                }
                else if (obstacleSpeed > 4.4f) // OLD: 4.8 NEW: 4.5
                {
                    obstacleSpeed += 0.025f;
                }
                else
                {
                    obstacleSpeed += 0.05f;
                }
            }
        }

    }
    #endregion

    #region Increase Spawn Chance
    void IncreaseSpawnChance()
    {
        if (Score.currentScore >= 5 && chanceToSpawnDirObstacle > 0.6f)
            chanceToSpawnDirObstacle -= 0.05f;

        if (Score.currentScore >= 8)
        {
            if (chanceToSpawnSwapXObstacle > 0.75f)
                chanceToSpawnSwapXObstacle -= 0.05f;

            if (chanceToSpawnSwapYObstacle > 0.75f)
            chanceToSpawnSwapYObstacle -= 0.05f;
        }

        if (Score.currentScore >= 10 && chanceToSpawnDeadly > 0.85f)
            chanceToSpawnDeadly -= 0.02f;

        if (Score.currentScore >= 20)
        {
            if (chanceToSpawnMimic > 0.93f)
                chanceToSpawnMimic -= 0.01f;

            if (chanceToSpawnSecret > 0.94f)
                chanceToSpawnSecret -= 0.01f;

            if (chanceToSpawnBlind > 0.93f)
                chanceToSpawnBlind -= 0.01f;
        }
    }
    #endregion

    #region Spawn Obstacle
    public void SpawnObstacle()
    {        
        hasSpawned = true;

        if (Random.value > chanceToSpawnDirObstacle && Score.currentScore >= 5) // Chance to spawn a "new-rotate-direction-obstacle"
        {

            if (playerScript.direction == 1 || playerScript.direction == 3) // If Player is currently rotating RIGHT
            {
                do
                {
                    randomDirLeftObstacle = Random.Range(0, dirLeftObstacles.Length);
                }
                while (randomDirLeftObstacle == lastIndexLeft);

                Instantiate(dirLeftObstacles[randomDirLeftObstacle], transform.position, Quaternion.identity);

                IncreaseSpawnChance();

                chanceToSpawnDirObstacle = 1f;

                lastIndexLeft = randomDirLeftObstacle;
            }
            else if (playerScript.direction == 2 || playerScript.direction == 4) // If Player is currently rotating LEFT
            {
                do
                {
                    randomDirRightObstacle = Random.Range(0, dirRightObstacles.Length);
                }
                while (randomDirRightObstacle == lastIndexRight);

                Instantiate(dirRightObstacles[randomDirRightObstacle], transform.position, Quaternion.identity);

                IncreaseSpawnChance();

                chanceToSpawnDirObstacle = 1f;

                lastIndexRight = randomDirRightObstacle;
            }
        }
        else if (Random.value > chanceToSpawnSwapXObstacle && Score.currentScore >= 8) // Chance to spawn "swap-horizontal"
        {
            do
            {
                randomSwapXObstacle = Random.Range(0, swapXObstacles.Length);
            }
            while (randomSwapXObstacle == lastIndexSwapX);

            Instantiate(swapXObstacles[randomSwapXObstacle], transform.position, Quaternion.identity);

            IncreaseSpawnChance();

            chanceToSpawnSwapXObstacle = 1f;

            lastIndexSwapX = randomSwapXObstacle;
        }
        else if (Random.value > chanceToSpawnSwapYObstacle && Score.currentScore >= 8) // Chance to spawn "swap-vertical"
        {
            do
            {
                randomSwapYObstacle = Random.Range(0, swapYObstacles.Length);
            }
            while (randomSwapYObstacle == lastIndexSwapY);

            Instantiate(swapYObstacles[randomSwapYObstacle], transform.position, Quaternion.identity);

            IncreaseSpawnChance();

            chanceToSpawnSwapYObstacle = 1f;

            lastIndexSwapY = randomSwapYObstacle;
        }
        else if (Random.value > chanceToSpawnDeadly && Score.currentScore >= 10) // Chance to spawn "deadly"
        {
            do
            {
                randomDeadlyObstacle = Random.Range(0, deadlyObstacles.Length);
            }
            while (randomDeadlyObstacle == lastIndexDeadly);

            Instantiate(deadlyObstacles[randomDeadlyObstacle], transform.position, Quaternion.identity);

            IncreaseSpawnChance();

            chanceToSpawnDeadly = 1f;

            lastIndexDeadly = randomDeadlyObstacle;
        }
        else if (Random.value > chanceToSpawnMimic && Score.currentScore >= 20 && playerScript.canSpawnMimic && !playerScript.justDestroyedMimic) // Chance to spawn Mimic
        {
            Instantiate(mimicObstacle, transform.position, Quaternion.identity);

            IncreaseSpawnChance();

            chanceToSpawnMimic = 1f;
        }
        else if (Random.value > chanceToSpawnSecret && Score.currentScore >= 20 && !playerScript.destroyedSecret && !playerScript.destroyedBlind) // Chance to spawn Secret
        {
            do
            {
                randomSecretObstacle = Random.Range(0, secretObstacles.Length);
            }
            while (randomSecretObstacle == lastIndexSecret);

            Instantiate(secretObstacles[randomSecretObstacle], transform.position, Quaternion.identity);

            IncreaseSpawnChance();

            chanceToSpawnSecret = 1f;

            lastIndexSecret = randomSecretObstacle;
        }
        else if (Random.value > chanceToSpawnBlind && Score.currentScore >= 20)
        {
            do
            {
                randomBlindObstacle = Random.Range(0, blindObstacles.Length);
            }
            while (randomBlindObstacle == lastIndexBlind);

            Instantiate(blindObstacles[randomBlindObstacle], transform.position, Quaternion.identity);

            IncreaseSpawnChance();

            chanceToSpawnBlind = 1f;

            lastIndexBlind = randomBlindObstacle;
        }
        else // Spawn Default Obstacles
        {
            if (Random.value > 0.95) // 5% - Small chance to spawn same color cube
            {
                Instantiate(obstacles[randomObstacle], transform.position, Quaternion.identity);

                IncreaseSpawnChance();
            }
            else // Prevent from spawning same color cube. Spawns next color cube in array.
            {
                do
                {
                    randomObstacle = Random.Range(0, obstacles.Length);
                }
                while (randomObstacle == lastIndexDefault);

                Instantiate(obstacles[randomObstacle], transform.position, Quaternion.identity);

                IncreaseSpawnChance();

                lastIndexDefault = randomObstacle;
            }
        }

        hasSpawned = false;
    }
    #endregion

    IEnumerator SpawnStartDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnObstacle();
        canCount = true;
    }
}
