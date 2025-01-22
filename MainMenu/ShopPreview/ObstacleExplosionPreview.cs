using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleExplosionPreview : MonoBehaviour
{
    public GameObject obstacleObject;

    public Transform obstacleSpawnPoint;

    public Transform playerObject;

    public GameObject parentObject;

    GameObject obstacle;

    public IEnumerator ObstacleExplosion()
    {
        parentObject.SetActive(true);

        WaitForSeconds waitTime = new WaitForSeconds(2f);

        while (true)
        {

            yield return new WaitForSeconds(0.5f);

            obstacle = Instantiate(obstacleObject, obstacleSpawnPoint.transform.position, Quaternion.identity) as GameObject;
            obstacle.transform.parent = transform;

            yield return waitTime;
        }
    }

}
