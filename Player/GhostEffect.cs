using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{

    float currentTime;
    public float startTime;

    public GameObject ghostEffect;
    private PlayerBox playerScript;

    void Start()
    {
        playerScript = GetComponent<PlayerBox>();
    }

    void Update()
    {
        if (playerScript.isRotating)
        {
            if (currentTime <= 0)
            {
                GameObject instance = (GameObject)Instantiate(ghostEffect, transform.position, transform.rotation);
                Destroy(instance, 1f);
                currentTime = startTime;
            } 
            else
            {
                currentTime -= Time.deltaTime;
            }
        }
    }

}
