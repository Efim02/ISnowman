using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingJellyFish : MonoBehaviour
{
    Vector3 direction;
    float speed = 1f;
    void Start()
    {
        direction = new Vector2(-Mathf.Sign(transform.position.x) ,2);
        if (transform.position.x > 0)
            GetComponent<SpriteRenderer>().flipX = true;
    }

    void Update()
    {
        transform.Translate(direction * speed* Time.deltaTime);
    }
}
