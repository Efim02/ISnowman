using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlatformBuble : MonoBehaviour, IPlatforms
{
    [SerializeField]
    GameObject Fishman;

    float timeSpawnFishman = 0.9f;
    bool variable = false;
    bool sizeVarAnim = true;
    Transform TrChar;
    float speedChar = 5f;

    float startPosition = 0f;

    List<Fishman> listFishmans = new List<Fishman>();

    Vector3 placeSpawn;
    bool isPause=false;
    internal static GameObject varTemp;
    void Start()
    {
        startPosition = transform.position.y;
        TrChar = CanvasMod.charMov.gameObject.transform;
        CanvasMod.eventRestartLevel += OnDestroy;
        CanvasMod.eventPause += Pause_Continue;
        CanvasMod.eventContinue += Pause_Continue;
    }
    public void OnCollision(GameObject go)
    {
        varTemp = gameObject;

        transform.parent = go.transform;

        CanvasMod.spawner.isToSpawn = false;

        CanvasMod.charMov.isMoveX = false;
        CanvasMod.charMov.rb2D.simulated = false;
        CanvasMod.charMov.animator.SetBool("isJump", false);
        CanvasMod.charMov.animator.SetBool("Fall", false);
        CanvasMod.charMov.animator.SetBool("Land", false);
        CanvasMod.charMov.animator.Play("Stand");



        variable = true;
        

        StartCoroutine(ToSpawnEnemies());   //запуск спавна врагов
    }
    private void FixedUpdate()
    {
        
        if (variable && transform.position.y - startPosition <= 50 && !isPause)
        {
            TrChar.Translate(Time.fixedDeltaTime * Vector3.up * speedChar);
            transform.position = Vector3.Lerp(transform.position, new Vector3(TrChar.position.x, TrChar.position.y + 0.65f), Time.deltaTime);
            if(TrChar.position.x <-0.1f || TrChar.position.x>0.1f)
                TrChar.Translate(new Vector3(-Mathf.Sign(transform.position.x),0,0) * Time.deltaTime*2f);
            if (transform.position.y - startPosition > 44)
                OnSpawner();
            if (transform.position.y - startPosition > 50)
            {
                Destroy(gameObject);
            }
        }
    }
    void Update()
    {
        if (sizeVarAnim)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.92f, 1f), Time.deltaTime);
            if (transform.localScale.x < 0.94f)
                sizeVarAnim = false;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 0.92f), Time.deltaTime);
            if (transform.localScale.y < 0.94f)
                sizeVarAnim = true;
        }
        if (TrChar.position.y - transform.position.y > 10 && !variable)
        { Destroy(gameObject); };

    }
    bool isWhile = true;
    void OnSpawner()
    {
        if (isWhile)
        {
            isWhile = false;
            CanvasMod.spawner.isToSpawn = true;
            CanvasMod.spawner.hight =(int)startPosition + 44;
            CanvasMod.spawner.CheckHightPlatform((int)startPosition + 44);
        }
    }
    private void OnDestroy()
    {
        //print("Destroy  bubble");
        CanvasMod.eventRestartLevel -= OnDestroy;
        CanvasMod.eventPause -= Pause_Continue;
        CanvasMod.eventContinue -= Pause_Continue;
        if (variable)
        {
            CanvasMod.charMov.isMoveX = true;
            CanvasMod.charMov.rb2D.simulated = true;
        }
}

    IEnumerator ToSpawnEnemies()
    {
        yield return new WaitForSeconds(timeSpawnFishman);
        if(!isPause) SpawnEnemy();
        StartCoroutine(ToSpawnEnemies());
    }
    void SpawnEnemy()
    {
        placeSpawn = new Vector3(Random.Range(-6, 6), transform.position.y - 8);
        var go = Instantiate(Fishman, placeSpawn, transform.rotation);
        float varFloat = 0;
        if (placeSpawn.x > 0)
            varFloat = -1;
        else
            varFloat =1;
        go.transform.localScale = new Vector3(varFloat,1,1);
        listFishmans.Add(go.GetComponent<Fishman>());
    }
    void Pause_Continue()
    {
        isPause = !isPause;
    }

}
