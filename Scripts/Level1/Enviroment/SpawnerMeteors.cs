using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMeteors : MonoBehaviour
{
    [SerializeField]
    public bool isSpawnMeteors = true;
    [SerializeField]
    GameObject go_Meteor;
    int minSec = 10;
    int maxSec = 30;


    void Start()
    {
        StartCoroutine(SpawnMeteor(Random.Range(minSec, maxSec)));
    }
    IEnumerator SpawnMeteor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(go_Meteor);
        StartCoroutine(SpawnMeteor(Random.Range(minSec, maxSec)));
    }
}
