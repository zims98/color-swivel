using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public bool camShakeActive = true;

    [Range(0, 1)] [SerializeField] float trauma;
    [SerializeField] float traumaMultiplier = 5f;
    [SerializeField] float traumaMagnitude = 0.8f;
    [SerializeField] float traumaRotMagnitude = 17f;
    [SerializeField] float traumaDecay = 1f;
    [SerializeField] float traumaDepthMagnitude = 1.3f;
    [SerializeField] float traumaFallOff = 0.3f;

    float timeCounter;

    public float Trauma
    {
        get
        {
            return trauma;
        }
        set
        {
            trauma = Mathf.Clamp01(value);
        }
    }

    float GetFloat(float seed)
    {
        return (Mathf.PerlinNoise(seed, timeCounter) - 0.5f) * 2;
    }

    Vector3 GetVec3()
    {
        return new Vector3(GetFloat(1), GetFloat(10), GetFloat(100) * traumaDepthMagnitude);
    }

    private void Update()
    {
        if (camShakeActive && Trauma > 0)
        {
            timeCounter += Time.deltaTime * Mathf.Pow(trauma, traumaFallOff) * traumaMultiplier;
            Vector3 newPos = GetVec3() * traumaMagnitude * Trauma;
            transform.localPosition = newPos;
            transform.localRotation = Quaternion.Euler(newPos * traumaRotMagnitude);
            Trauma -= Time.deltaTime * traumaDecay * Trauma;
        }
        else
        {
            Vector3 newPos = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime);
            transform.localPosition = newPos;
            transform.localRotation = Quaternion.Euler(newPos * traumaRotMagnitude);
        }
    }
}
