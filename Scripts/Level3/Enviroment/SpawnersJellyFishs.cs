using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersJellyFishs : MonoBehaviour
{
    [SerializeField]
    GameObject g1;
    [SerializeField]
    GameObject g2;

    Transform trChar;
    float temp = 0;
    float lastHight = 0;
    float minusHight = 6;
    float sideX;
    float tempSecond=0;
    void Start()
    {
        trChar = CanvasMod.charMov.transform;
    }

    void Update()
    {
        if(trChar.position.y - lastHight > temp)
        {
            
            temp = Random.Range(20, 40);
            lastHight = trChar.position.y;
            Spawn();
        }
        tempSecond += 0.01f;
        if (tempSecond > 6)
            Spawn();
    }
    void Spawn()
    {
        tempSecond = 0;
        minusHight = Random.Range(-4, 6);
        if (Random.Range(1, 3) == 1)
        {
            sideX = -5;
        }
        else
            sideX = 5;
        switch (Random.Range(1, 4))
        {
            case 1:
                Destroy(Instantiate(g1, new Vector3(sideX, trChar.position.y - minusHight), Quaternion.identity), 10f);
                break;
            case 2:
                Destroy(Instantiate(g2, new Vector3(sideX, trChar.position.y - minusHight), Quaternion.identity), 10f);
                break;
            case 3:
                Destroy(Instantiate(g1, new Vector3(sideX, trChar.position.y - minusHight), Quaternion.identity), 10f);
                Destroy(Instantiate(g2, new Vector3(sideX + 2 * Mathf.Sign(sideX), trChar.position.y - minusHight), Quaternion.identity), 10f);
                break;
        }
    }
}
