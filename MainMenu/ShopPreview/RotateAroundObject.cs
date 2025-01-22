using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundObject : MonoBehaviour
{

    public GameObject target;

    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.forward, -100 * Time.deltaTime);
    }

}
