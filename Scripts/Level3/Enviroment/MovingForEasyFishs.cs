using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingForEasyFishs : MonoBehaviour
{
    Vector2 startPos;
    Vector2 toPlace;
    private void Start()
    {
        startPos = transform.position;
        toPlace = new Vector2(-Mathf.Sign(transform.position.x)*6, transform.position.y);
    }
    float deltaTime = 0;
    void Update()
    {
        if (deltaTime < 1)
        {
            deltaTime += 0.003f;
            transform.position = Vector3.Slerp(startPos, toPlace, deltaTime);
        }
    }
}
