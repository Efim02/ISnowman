using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrakingCharMovVelocityYForRemove : MonoBehaviour
{
    Rigidbody2D rb2DPlayer;
    [HideInInspector]
    public GameObject go;
    void Start()
    {
        rb2DPlayer = go.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if( rb2DPlayer.velocity.y < 0 )
        {
            Destroy(gameObject);
        }
    }
}
