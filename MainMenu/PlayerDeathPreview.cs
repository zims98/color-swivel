using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathPreview : MonoBehaviour
{

    public GameObject playerObject;
    public GameObject explosionEffect;

    public GameObject clone;

    public IEnumerator PreviewPlayerDeath()
    {
        WaitForSeconds waitTime = new WaitForSeconds(3);

        while (true)
        {
            playerObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            playerObject.SetActive(false);
            clone = Instantiate(explosionEffect, transform.position, Quaternion.identity, gameObject.transform);
            Destroy(clone, 2.5f);
            yield return waitTime;
        }       
        
    }

}
