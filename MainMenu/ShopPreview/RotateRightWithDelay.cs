using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRightWithDelay : MonoBehaviour
{
    bool isRotating;

    float time;
    public float targetTime;

    public float rotationDuration;

    public IEnumerator rotateCoroutine;

    void Start()
    {
        time = 0f;
        
    }

    void Update()
    {
        rotateCoroutine = Rotate(Vector3.forward, -90, rotationDuration);

        if (!isRotating)
        {
            time += Time.deltaTime;
        }

        if (time >= targetTime)
        {
            StartCoroutine(rotateCoroutine);
        }
    }

    public void ResetPosiiton()
    {
        isRotating = false;
        transform.rotation = Quaternion.identity;
    }

    public IEnumerator Rotate(Vector3 axis, float angle, float duration)
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
