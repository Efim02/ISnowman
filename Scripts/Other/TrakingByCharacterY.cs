using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrakingByCharacterY : MonoBehaviour
{
    public static Transform trCharacter;
    void Start()
    {
        trCharacter = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, trCharacter.position.y, transform.position.z);
    }
}
