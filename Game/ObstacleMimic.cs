using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMimic : MonoBehaviour
{
    float mimicSpeed = 2.4f;

    int timesToRotate = 2; // 3
    int currentRotations = 0;

    float rotationDuration = 0.4f; // 0.5

    int direction;

    float targetTime = 1f; // 0.75
    float currentTime;



    void Start()
    {
        direction = Random.Range(1, 3);

        int randomStartRotation = Random.Range(1, 5);

        if (randomStartRotation == 1)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (randomStartRotation == 2)
            transform.rotation = Quaternion.Euler(0, 0, 90);
        else if (randomStartRotation == 3)
            transform.rotation = Quaternion.Euler(0, 0, 180);
        else if (randomStartRotation == 4)
            transform.rotation = Quaternion.Euler(0, 0, 270);
    }

    void Update()
    {
        //transform.position += -transform.up * mimicSpeed * Time.deltaTime;

        transform.Translate(Vector2.down * mimicSpeed * Time.deltaTime, Space.World);

        if (currentRotations < timesToRotate)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= targetTime)
            {
                if (direction == 1) // Right
                {
                    StartCoroutine(Rotate(Vector3.forward, -90, rotationDuration));
                }
                else if (direction == 2) // Left
                {
                    StartCoroutine(Rotate(Vector3.forward, 90, rotationDuration));
                }

                currentRotations++;
                currentTime = 0;
            }
        }
        
    }

    IEnumerator Rotate(Vector3 axis, float angle, float duration)
    {
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
    }

}
