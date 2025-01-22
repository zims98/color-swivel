using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSecret : MonoBehaviour
{

    float speed = 2.2f;

    float targetTime = 2.4f;
    float currentTime;

    Animator anim;
    public ParticleSystem effect;

    bool canCount = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (canCount)
            currentTime += Time.deltaTime;

        if (currentTime >= targetTime)
        {
            canCount = false;
            anim.SetTrigger("RevealColor");
            Instantiate(effect, transform.position, Quaternion.identity);

            currentTime = 0f;
        }

        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
    }
}
