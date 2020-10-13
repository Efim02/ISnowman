using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISpawnerPlatforms : MonoBehaviour
{
    internal Queue<GameObject> platformsList = new Queue<GameObject>();
    internal Rigidbody2D rb2DPlayer;
    internal Transform trPlayer;
    internal Collider2D collPlayer;

    [HideInInspector]
    public int hight = 0; //высота  спавна платформ 
    internal int lastHightPlatform = 0;
    [HideInInspector]
    public bool isSpawnPlatformAfterFly = false;
    internal bool isToSpawn = true;

    internal int LevelDifficulty = 0;
    internal int lastLevelDifficulty = 0;
    internal float lastPositionPlayerY = 0;
    internal float deltaY = 0f;

    internal int sizePlatform = 1;
    public abstract int maxHight { get;}
    internal virtual void Start()
    {
        rb2DPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        trPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        collPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>();
    }
    bool isPossibleSetPositionY = true;
    internal virtual void FixedUpdate()
    {
        if (rb2DPlayer.velocity.y < -0.5f)
        {
            isSpawnPlatformAfterFly = false;
            collPlayer.enabled = true;
            if (rb2DPlayer.velocity.y < -20f && isPossibleSetPositionY == true)
            {
                isPossibleSetPositionY = false;
                trPlayer.position = new Vector3(trPlayer.position.x, 6, trPlayer.position.z);
                CameraMovement.cameraMovement.gameObject.transform.position = new Vector3(0, trPlayer.position.y + 6, -10);
                CameraMovement.isWithDampningMotion = 1;
            }
        }
        if (isSpawnPlatformAfterFly)
        {
            CheckHightPlatform((((int)(trPlayer.position.y * 0.5f)) * 2)-2);
        }
    }
    public virtual void SpawnPlatform()
    {
        hight += 2;
        //print("Hight: "+hight);
        if (isToSpawn)
        {
            RandomSpawnPlatform();
            if (platformsList.Count < 6)
            {
                SpawnPlatform();
            }
            if (platformsList.Count > 6)
            {
                Destroy(platformsList.Dequeue());
            }
            if (hight - 8 < lastHightPlatform)
            {
                SpawnPlatform();
            }
        }
    }
    public virtual void RestartLevel()
    {
        isPossibleSetPositionY = true;
        isSpawnPlatformAfterFly = false;
        lastHightPlatform = 4;
        hight = 0;
        foreach (GameObject gO in platformsList)
        { Destroy(gO); }
        platformsList.Clear();
        ResetDifficultyLevel();
        isToSpawn = true;
    }
    public virtual void CheckHightPlatform(int hightPlatform)
    {
        if (hightPlatform - lastHightPlatform > 0)
        {
            lastHightPlatform = hightPlatform;
            if (hight - 8 < lastHightPlatform)
                SpawnPlatform();
        }
    }
    internal abstract void RandomSpawnPlatform();
    internal abstract int ReturnDifficultyLevel();
    internal abstract void ResetDifficultyLevel();
    internal abstract void ChangeValuiesForRandomPlatform();
    internal float GetSizePlatform()
    {
        if (sizePlatform == 1)
            return 1f;
        else if (sizePlatform == -1)
            return Random.Range(45, 101) * 0.01f;
        else
            return sizePlatform * 0.01f;
    }
}
