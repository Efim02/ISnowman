using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageUpdating : MonoBehaviour
{
    [SerializeField]
    int SpeedRotation = 8;
    void Update()
    {
        transform.Rotate(0,0,SpeedRotation);
    }
}
