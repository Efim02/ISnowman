using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerUrchins : MonoBehaviour
{
    [SerializeField]
    GameObject Urchin;
    Transform trChar;

    Queue<GameObject> urchinsList = new Queue<GameObject>();

    int hight;
    int startHigth;
    int higthSpawns = 102;
    
    Vector3 placeSpawner;
    private void Start()
    {
        trChar = CanvasMod.charMov.transform;
        hight = CanvasMod.spawner.hight;
        startHigth = hight;
        CanvasMod.eventRestartLevel += DestroySpawnerUrchins;
    }
    bool b1While = true;
    bool b2While = true;
    private void Update()
    {
        if(trChar.position.y+16 > hight)
        {
            if (trChar.position.y + 16 - startHigth < higthSpawns)
                SpawnUrchin();
            else if (trChar.position.y - 12 > hight)
            {
                //print("Destroy spawner urchins");
                DestroySpawnerUrchins(); 
            }
            else if (trChar.position.y - 6 > hight)
            {
                if (b1While)
                {
                    //print("Character end swiming. start jumping");
                    b1While = false;
                    CameraMovement.isWithDampningMotion = 0;
                    trChar.gameObject.GetComponent<CharMoveLelel3>().isSwim = false;
                }
            }
            else
            {
                if (b2While)
                {
                    //print("Start spawn platforms because end spawn urchins");
                    b2While = false;
                    CanvasMod.spawner.isToSpawn = true;
                    CanvasMod.spawner.hight = startHigth + higthSpawns - 10;
                    CanvasMod.spawner.CheckHightPlatform(hight + 4);
                }
            }
            
        }
    }
    void SpawnUrchin()
    {
        if (hight - trChar.position.y < 10)
        {
            hight += 4;
            if (Random.Range(1, 6) < 3)
                placeSpawner = new Vector3(Random.Range(-225, 226) * 0.01f, hight, 0);
            else
                placeSpawner = new Vector3(Random.Range(-105, 106) * 0.01f, hight, 0);
            if (urchinsList.Count < 7)
                urchinsList.Enqueue(Instantiate(Urchin, placeSpawner, transform.rotation));
            else
            {
                //print("Urchin: Change position created urchin!");
                var temp = urchinsList.Dequeue();
                temp.transform.position = placeSpawner;
                urchinsList.Enqueue(temp);
            }
        }
    }
    void DestroySpawnerUrchins()
    {
        foreach (var a in urchinsList)
            Destroy(a);
        urchinsList.Clear();
       // print("Destroy spawner urchins");
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        CanvasMod.eventRestartLevel -= DestroySpawnerUrchins;
    }

}
