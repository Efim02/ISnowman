using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTurtle : MonoBehaviour
{
    Vector2 direction;
    float speed = 2f;
    private void Start()
    {
        direction = new Vector2(-Mathf.Sign(transform.position.x), 0);
    }
    void Update()
    {
        transform.Translate(direction * 0.01f * speed);
    }
}