using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreviewRaycast : MonoBehaviour
{
    public ObstacleExplosionPreview obstacleExplosionPreviewScript;
    public GameObject parentObject;

    public ParticleSystem explosionEffect;
    [HideInInspector] public ParticleSystem go;

    public float rayDistance;
    public LayerMask obstacleMask;
    RaycastHit2D hitTop;

    void OnDisable()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        hitTop = Physics2D.Raycast(transform.position, -transform.right, rayDistance, obstacleMask);

        if (hitTop.collider != null)
        {
            if (hitTop.collider.tag == "Green")
            {                
                go = Instantiate(explosionEffect, hitTop.collider.gameObject.transform.position, hitTop.collider.gameObject.transform.rotation) as ParticleSystem;
                go.transform.parent = transform;
                go.transform.localScale = new Vector3(1, 1, 1);

                Destroy(hitTop.collider.gameObject);

            }
        }

        if (parentObject != null && go != null)
        {
            
        }

        
        

        Debug.DrawRay(transform.position, -transform.right * rayDistance, Color.red);

    }
}
