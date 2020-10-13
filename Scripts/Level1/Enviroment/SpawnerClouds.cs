using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerClouds : MonoBehaviour
{
    Queue<GameObject> cloudsList = new Queue<GameObject>();

    private Transform trPlayer;
    [SerializeField]
    GameObject go1;
    [SerializeField]
    GameObject go2;
    [SerializeField]
    GameObject go3;

    float randomValue;
    readonly float maxHeightSpawn = 20f;
    float lastHeightCreating;
    Vector3 placeCreating;
    readonly int chanceCreatingCloud = 30;

    void Start()
    {
        trPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        CanvasMod.charMov.eventDeath += ReCreate;
    }
    private void OnDestroy()
    {
        CanvasMod.charMov.eventDeath -= ReCreate;
    }

    void Update()
    {
        OnCreate();
    }
    void OnCreate()
    {
        if(lastHeightCreating - trPlayer.position.y+6 <= maxHeightSpawn)
        {
            randomValue = Random.Range(0,101);
            if(randomValue <= chanceCreatingCloud)
            {   
                Create();
                if (cloudsList.Count > 12)
                {
                    Destroy(cloudsList.Dequeue());
                }
            }
        }
    }
    void Create()
    {
        lastHeightCreating += Random.Range(2,5);
        placeCreating = new Vector3(Random.Range(-300, 301) * 0.01f, lastHeightCreating, 1);
        randomValue = Random.Range(1,4);
        switch (randomValue)
        {
            case 1:
                cloudsList.Enqueue(Instantiate(go1, placeCreating, transform.rotation));
                break;
            case 2:
                cloudsList.Enqueue(Instantiate(go2, placeCreating, transform.rotation));
                break;
            case 3:
                cloudsList.Enqueue(Instantiate(go3, placeCreating, transform.rotation));
                break;
        }
    }
    void ReCreate()
    {
        foreach (GameObject go in cloudsList)
            Destroy(go);
        cloudsList.Clear();
        lastHeightCreating = 0;
    }
}
