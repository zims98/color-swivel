using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvePreview : MonoBehaviour
{
    Material material;

    public bool isDissolving = false;
    public float fade;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;

        material.SetFloat("_Fade", fade);
        fade = 0f;      
    }

    void Update()
    {
        StartCoroutine(DissolveDelay());
        material.SetFloat("_Fade", fade);
    }

    public IEnumerator DissolveDelay()
    {
        //WaitForSeconds waitTime = new WaitForSeconds(3);
        
        yield return new WaitForSeconds(1f);
        isDissolving = true;

        while (isDissolving)
        {         
            if (isDissolving)
            {
                fade += Time.deltaTime;

                if (fade >= 1f)
                {
                    fade = 1f;
                    isDissolving = false;

                    yield return new WaitForSeconds(1.5f);

                    fade = 0f;
                }

                material.SetFloat("_Fade", fade);
            }

            //yield return waitTime;
            yield return new WaitForSeconds(3);
        }      
    }
}
