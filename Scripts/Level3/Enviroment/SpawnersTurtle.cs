using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersTurtle : MonoBehaviour
{
    [SerializeField]
    GameObject GO;

    Transform trChar;
    float minusHight = 6;
    float sideX;
    GameObject gvar;
    private void Start()
    {
        trChar = CanvasMod.charMov.transform;
        StartCoroutine(SpawnFishs());
    }
    IEnumerator SpawnFishs()
    {
        minusHight = Random.Range(-1, 6);
        if (Random.Range(1, 3) == 1)
        {
            sideX = -5;
        }
        else
            sideX = 5;
        //spawn
        Destroy(gvar = Instantiate(GO, new Vector3(sideX, trChar.position.y + minusHight), Quaternion.identity), 8f);
        if (sideX > 0)
            gvar.transform.localScale = new Vector3(-1, 1, 1);
        //else
            //print("Massiv length easy fishs - is null!");
        yield return new WaitForSeconds(Random.Range(Random.Range(3,7), 8));
        StartCoroutine(SpawnFishs());
    }
}
