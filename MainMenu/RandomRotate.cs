using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    int direction;
    int timesToRotate;
    int loops = 0;

    int randomStartRotation;

    public float rotationDuration;

    bool isRotating;

    float time;
    public float targetTime;

    [HideInInspector]
    public bool rotatingRight;

    public bool startRotateRight;
    public bool startRotateLeft;


    void Start()
    {
        time = 0f;
        direction = Random.Range(1, 3);
        timesToRotate = Random.Range(1, 7);
        randomStartRotation = Random.Range(1, 5);

        if (randomStartRotation == 1)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (randomStartRotation == 2)
            transform.rotation = Quaternion.Euler(0, 0, 90);
        else if (randomStartRotation == 3)
            transform.rotation = Quaternion.Euler(0, 0, 180);
        else if (randomStartRotation == 4)
            transform.rotation = Quaternion.Euler(0, 0, 270);

        if (direction == 1)
            startRotateRight = true;
        else if (direction == 2)
            startRotateLeft = true;
    }

    void Update()
    {
        if (!isRotating)
        {
            time += Time.deltaTime;
        }

        // Rotating Right
        if (direction == 1)
        {
            rotatingRight = true;

            if (time >= targetTime && loops < timesToRotate)
            {
                for (int i = 0; i < timesToRotate; i++)
                {
                    StartCoroutine(Rotate(Vector3.forward, -90, rotationDuration));                  
                }

                loops++;
            }

            if (loops >= timesToRotate)
            {
                direction = 2;               
                timesToRotate = Random.Range(1, 7);
                loops = 0;
            }
        }

        // Rotating Left
        if (direction == 2)
        {
            rotatingRight = false;

            if (time >= targetTime && loops < timesToRotate)
            {
                for (int i = 0; i < timesToRotate; i++)
                {
                    StartCoroutine(Rotate(Vector3.forward, 90, rotationDuration));
                }

                loops++;
            }

            if (loops >= timesToRotate)
            {
                direction = 1;                
                timesToRotate = Random.Range(1, 5);
                loops = 0;
            }
        }
    }

    IEnumerator Rotate(Vector3 axis, float angle, float duration)
    {
        isRotating = true;
        time = 0f;
        
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = to;

        isRotating = false;      
    }
}
