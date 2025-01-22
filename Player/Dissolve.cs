using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    // This Dissolve Effect is done in reverse - aka "Appearing Effect"

    Material material;

    bool isDissolving = false;
    float fade;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;

        material.SetFloat("_Fade", fade);
        fade = 0f;

        isDissolving = true;
    }

    void Update()
    {
        StartCoroutine(DissolveDelay());
    }

    IEnumerator DissolveDelay()
    {
        yield return new WaitForSeconds(1f);

        if (isDissolving)
        {
            fade += Time.deltaTime;

            if (fade >= 1f)
            {
                fade = 1f;
                isDissolving = false;
            }

            material.SetFloat("_Fade", fade);
        }
    }
}
