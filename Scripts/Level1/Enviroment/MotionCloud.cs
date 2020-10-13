using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionCloud : MonoBehaviour
{
    int directionMoving;
    void Start()
    {
        if (Random.Range(0, 2) == 0)
            directionMoving = -1;
        else
            directionMoving = 1;

    }
    void Update()
    {
        transform.Translate(new Vector3(directionMoving , 0 , 0 ) * Time.deltaTime * 0.3f);
        if (transform.position.x > 4.7)
            transform.position = new Vector3(-4.5f, transform.position.y, transform.position.z);
        if (transform.position.x < -4.7f)
            transform.position = new Vector3(4.5f, transform.position.y, transform.position.z);
    }
}
