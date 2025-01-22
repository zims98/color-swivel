using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{

    public GameObject spawnerObject;
    public ObstacleSpawner spawnerScript;

    public Unlockables unlockableScript;

    public GameObject playerObject;
    public PlayerBox playerScript;

    private void Awake()
    {       
        spawnerObject = GameObject.Find("Obstacle Spawner");
        spawnerScript = spawnerObject.GetComponent<ObstacleSpawner>();

        playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<PlayerBox>();
    }

    void Start()
    {
        // May be performance heavy
        unlockableScript.obstacleTrailSelected = ES3.Load<bool>("obstacleTrailSelected", false);

        if (unlockableScript.obstacleTrailSelected)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void Update()
    {        
        if (!PauseMenu.gameIsPaused)
        {
            if (playerScript.justDestroyedMimic)
            {
                float halfwaySpeed = Mathf.Lerp(spawnerScript.obstacleSpeed, 3f, 0.5f);
                transform.Translate(Vector2.down * halfwaySpeed * Time.deltaTime, Space.World);
            }
            else if (GameMaster.watchedAd)
            {
                float halfwaySpeed = Mathf.Lerp(spawnerScript.obstacleSpeed, 3f, 0.5f);
                transform.Translate(Vector2.down * halfwaySpeed * Time.deltaTime, Space.World);
                Debug.Log("using watchedAD speed");
            }
            else if (playerScript.destroyedSecret)
            {
                float halfwaySpeed = Mathf.Lerp(spawnerScript.obstacleSpeed, 3f, 0.5f);
                transform.Translate(Vector2.down * halfwaySpeed * Time.deltaTime, Space.World);
            }
            else if (playerScript.destroyedBlind)
            {
                transform.Translate(Vector2.down * 2.7f * Time.deltaTime, Space.World);
            }
            else if (playerScript.obstacleHasSlowSpeed)
            {
                float halfwaySpeed = Mathf.Lerp(spawnerScript.obstacleSpeed, 3f, 0.5f);
                transform.Translate(Vector2.down * halfwaySpeed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(Vector2.down * spawnerScript.obstacleSpeed * Time.deltaTime, Space.World);
            }
        }         
    }
}
