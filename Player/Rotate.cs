using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;

    public PlayerBox playerScript;

    void Update()
    {
        if (playerScript.direction == 1 || playerScript.direction == 3)
            transform.Rotate(0, 0, -speed * Time.deltaTime);

        else if (playerScript.direction == 2 || playerScript.direction == 4)
            transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
