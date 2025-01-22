using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRotateAround : MonoBehaviour
{
    public GameObject target;
    public PlayerBox playerScript;

    void Update()
    {
        if (playerScript.direction == 1 || playerScript.direction == 3) // Right
            transform.RotateAround(target.transform.position, Vector3.forward, -100 * Time.deltaTime);

        else if (playerScript.direction == 2 || playerScript.direction == 4) // Left
            transform.RotateAround(target.transform.position, Vector3.forward, 100 * Time.deltaTime);
    }
}
