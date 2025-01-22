using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObstacles : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.position += -transform.up * speed * Time.deltaTime;       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DestroyZone")
        {
            Destroy(gameObject);
        }
    }
}
