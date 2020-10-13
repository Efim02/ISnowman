using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersEasyFishs : MonoBehaviour
{
    [SerializeField]
    GameObject[] masGO;

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
        minusHight = Random.Range(-1, 5);
        if (Random.Range(1, 3) == 1)
        {
            sideX = -5;
        }
        else
            sideX = 5;
        //spawn
        if (masGO.Length != 0)
        {
            Destroy(gvar = Instantiate(masGO[Random.Range(0, masGO.Length)], new Vector3(sideX, trChar.position.y + minusHight), Quaternion.identity), 6f);
            if (sideX < 0)
                gvar.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
            print("Massiv length easy fishs - is null!");
        yield return new WaitForSeconds(Random.Range(5, 12));
        StartCoroutine(SpawnFishs());
    }
}
