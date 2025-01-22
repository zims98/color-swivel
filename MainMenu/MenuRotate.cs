using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotate : MonoBehaviour
{

    public RandomRotate randomRotate;

    public float speed;

    private void Update()
    {
        if (randomRotate.rotatingRight)
        {
            transform.Rotate(0, 0, -speed * Time.deltaTime);
        }
        else if (!randomRotate.rotatingRight)
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }
}
