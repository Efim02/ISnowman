using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urchin : MonoBehaviour
{
    float z;
    private void Start()
    {
        z = Random.Range(-3, 4);
    }
    private void Update()
    {
        transform.Rotate(0, 0, z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //print("Collision urchin with player");
            CameraMovement.isWithDampningMotion = 0;
            ((CharMoveLelel3)CanvasMod.charMov).isSwim = false;
            CanvasMod.charMov.OnDeath("Urchin");

        }
    }
}
