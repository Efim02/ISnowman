using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPlanets: MonoBehaviour
{
    Queue<GameObject> planetsList = new Queue<GameObject>();

    private Transform trPlayer;
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    GameObject EmtpyObject;

    float randomValue;
    float lastHeightCreating = 0;
    Vector3 placeCreating;
    private readonly int chanceCreatingPlanet = 80;

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
        if (lastHeightCreating - trPlayer.position.y <= 0)
        {
            randomValue = Random.Range(0, 101);
            if (randomValue <= chanceCreatingPlanet)
            {
                Create();
                if (planetsList.Count > 3)
                {
                    Destroy(planetsList.Dequeue());
                }
            }
        }
    }
    void Create()
    {
        randomValue = 0;
        if (lastHeightCreating == 0)
            randomValue += Random.Range(4, 10);
        else
            randomValue += Random.Range(12, 18);
        lastHeightCreating += randomValue;
        placeCreating = new Vector3(Random.Range(-90, 91) * 0.01f + Random.Range(-2.5f,2.6f), lastHeightCreating, 2);
        randomValue = Random.Range(0, sprites.Length);
        var go = Instantiate(EmtpyObject, placeCreating, transform.rotation);
        go.GetComponent<SpriteRenderer>().sprite = sprites[(int)randomValue];
        lastHeightCreating += Random.Range(10, 15);
        planetsList.Enqueue(go);
    }
    void ReCreate()
    {
        foreach (GameObject go in planetsList)
            Destroy(go);
        planetsList.Clear();
        lastHeightCreating = 0;
    }
}
