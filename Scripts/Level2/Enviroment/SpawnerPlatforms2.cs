using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPlatforms2 : ISpawnerPlatforms
{
    [SerializeField]
    GameObject Platform;
    [SerializeField]
    GameObject Platform_Appearing;
    [SerializeField]
    GameObject Platform_Disappearing;
    [SerializeField]
    GameObject Platform_Teleporter;

    [SerializeField]
    GameObject Platform_Spring;
    //Для самой сложности значения
    //шанс спавна платформ
    int ch_Platform = 100;
    int ch_Platform_Appearing = 0;
    int ch_Platform_Disappearing= 0;
    int ch_Platform_Teleporter = 0;
    //шанс спавна доп объектов на платформах
    int ch_Spring = 5;
    //нужно просто сюда же
    Vector2 placeSpawner = new Vector2(0, 0);
    int randomValue = 0;
    GameObject varParent;
    float sizeAll = 1f;

    public override int maxHight { get; } = 2500;
    float counts;
    float pointsMaxChar;
    private void Awake()
    {
        CanvasMod.spawner = this;
    }
    
    internal override void RandomSpawnPlatform()
    {
        if(counts<=70 && lastLevelDifficulty != 999)
            ReturnDifficultyLevel();
        ChangeValuiesForRandomPlatform();

        bool isPossibleCreateDopPlatform = false;
        randomValue = Random.Range(0, 101);
        placeSpawner = new Vector2(Random.Range(-225, 226) * 0.01f, hight);

        if (randomValue <= ch_Platform)
        {
            platformsList.Enqueue(varParent = Instantiate(Platform, placeSpawner, transform.rotation));
            //Debug.Log("Spawn Platform");
        }
        else if (randomValue - ch_Platform <= ch_Platform_Appearing)              
        {
            platformsList.Enqueue(varParent = Instantiate(Platform_Appearing, placeSpawner, transform.rotation));
            isPossibleCreateDopPlatform = true;
            //Debug.Log("Spawn Appearing");
        }
        else if(randomValue - ch_Platform - ch_Platform_Appearing <= ch_Platform_Disappearing)
        {
            platformsList.Enqueue(varParent = Instantiate(Platform_Disappearing, placeSpawner, transform.rotation));
            isPossibleCreateDopPlatform = true;
            //Debug.Log("Spawn Disappearing");
        }
        else if(randomValue - ch_Platform - ch_Platform_Appearing - ch_Platform_Disappearing <= ch_Platform_Teleporter)
        {
            platformsList.Enqueue(varParent = Instantiate(Platform_Teleporter, placeSpawner, transform.rotation));
            isPossibleCreateDopPlatform = true;
            //Debug.Log("Spawn Teleporter");
        }
        //Debug.Log("RandomValue - " + randomValue);
        //устанавлваем размер
        //доп объект
        if (isPossibleCreateDopPlatform == false )
        {
            randomValue = Random.Range(0, 101);
            if (randomValue <= ch_Spring && sizePlatform == 1)
            {
                placeSpawner = new Vector2(Random.Range(-30, 30) * varParent.transform.localScale.x * 0.01f + placeSpawner.x, hight+0.12f);
                Instantiate(Platform_Spring, placeSpawner, transform.rotation, varParent.transform).transform.localScale = new Vector3(1 / varParent.transform.localScale.x * sizeAll, 1f, 1f);
            }
            varParent.transform.localScale = new Vector3(GetSizePlatform() * sizeAll, 1f, 1);
            if(varParent.transform.childCount > 0)
            {
                foreach (var t in varParent.GetComponentsInChildren<Transform>())
                    t.localScale = new Vector2(varParent.transform.localScale.x, t.localScale.y);
            }
        }
        
    }
    /*
     0 уровень 200 по высоте(200): обычные платформы и все
     1 уровень 300 по высоте(500): обычные платформы, изчезающие платформы вместе с спринг
     2 уровень 500 по высоте(1000): здесь все платформы, и спринг
     3 уровень     250 по высоте(1250): здесь чаще исчезающие вместе появляющимися
     4 уровень 500 по высотре (1750) здесь много телепортеров,зеленых на которых спринг
     5 уровень 250 по высоте 2000 здесь много зеленых маленбких телепорт и появляющиеся
     6 уровень 500 по высооте (2500) здесь телепорт плюс пропадающие вместе и все
         
         */
    internal override int ReturnDifficultyLevel()
    {
        if (trPlayer.position.y - lastPositionPlayerY >= deltaY)
        {
            //print("Change Diffuculty Level value - "+LevelDifficulty);
            LevelDifficulty += 1;  // ВРЕМЕННО
            switch (LevelDifficulty)
            {
                case 1:
                    lastPositionPlayerY = 200;
                    deltaY = 300;
                    break;
                case 2:
                    lastPositionPlayerY = 500;
                    deltaY = 500;
                    break;
                case 3:
                    lastPositionPlayerY = 1000;
                    deltaY = 250;
                    break;
                case 4:
                    lastPositionPlayerY = 1250;
                    deltaY = 500;
                    break;
                case 5:
                    lastPositionPlayerY = 1750;
                    
                    deltaY = 250;
                    break;
                case 6:
                    lastPositionPlayerY = 2000;
                    deltaY = 500;
                    break;
                case 7:
                    lastPositionPlayerY = 2500;
                    deltaY = 250;
                    if (SaveLoad.savePoints.points2 <= 2499)
                    {
                        SaveLoad.savePoints.points2 = 2501;
                        SaveLoad.SavePoints();
                        GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasMod>().DeleteLockLevel();
                    }
                    break;
            }
        }
        return LevelDifficulty;
    }
    internal override void ChangeValuiesForRandomPlatform()
    {
        if (LevelDifficulty - lastLevelDifficulty == 1)
        {
            //print("Level Difficulty changies");
            lastLevelDifficulty = LevelDifficulty;
            switch (LevelDifficulty)
            {
                case 0:
                    break;
                case 1:
                    ch_Platform = 50;
                    ch_Platform_Disappearing = 50;
                    ch_Spring = 7;
                    break;
                case 2:
                    ch_Platform = 25;
                    ch_Platform_Disappearing = 30;
                    ch_Platform_Appearing = 40;
                    ch_Platform_Teleporter = 5;
                    break;
                case 3:
                    ch_Platform = 10;
                    ch_Spring = 0;
                    ch_Platform_Disappearing = 45;
                    ch_Platform_Appearing = 45;
                    ch_Platform_Teleporter = 0;
                    sizePlatform = 55;
                    break;
                case 4:
                    ch_Platform = 80;
                    sizePlatform = 1;
                    ch_Spring = 50;
                    ch_Platform_Appearing = 0;
                    ch_Platform_Teleporter = 20;
                    ch_Platform_Disappearing = 0;
                    break;
                case 5:
                    ch_Platform = 90;
                    sizePlatform = 45;
                    ch_Spring = 0;
                    ch_Platform_Appearing = 0;
                    ch_Platform_Teleporter = 10;
                    ch_Platform_Disappearing = 0;
                    break;
                case 6:
                    ch_Platform = 10;
                    sizePlatform = 1;
                    ch_Spring = 40;
                    ch_Platform_Appearing = 0;
                    ch_Platform_Teleporter = 20;
                    ch_Platform_Disappearing = 70;
                    break;
                case 7:
                    lastLevelDifficulty = 999;
                    //Debug.Log($"Set levelDifficulty = 999 real -{lastLevelDifficulty}, ");
                    break;
            }
        }
        else if (lastLevelDifficulty == 999)
        {
            //Debug.Log($"Random - {LevelDifficulty}, last - {lastLevelDifficulty}, last Pos {lastPositionPlayerY}, del {deltaY}");
            if (trPlayer.position.y - lastPositionPlayerY >= deltaY)
            {
                lastPositionPlayerY += 250;
                ChangePropertiesSpawnPlatform(Random.Range(1,7));
            }
        }
    }
    internal override void ResetDifficultyLevel()
    {
        

        deltaY = 200;
        sizePlatform = 1;
        LevelDifficulty = 0;
        lastLevelDifficulty = 0;
        lastPositionPlayerY = 0;

        pointsMaxChar = SaveLoad.savePoints.points2;
        SaveLoad.saveCounts.counts2++;
        ChangeStartLevel();
        SaveLoad.SaveCountPlayedGames();
    }
    void ChangeStartLevel()
    {
        counts = SaveLoad.saveCounts.counts2;

        if (counts <= 10)
        {
            ch_Platform = 100;
            ch_Platform_Appearing = 0;
            ch_Platform_Disappearing = 0;
            ch_Platform_Teleporter = 0;
            ch_Spring = 12;
        }
        else if (counts <= 30)
        {
            ch_Platform = 80;
            ch_Platform_Appearing = 0;
            ch_Platform_Disappearing = 20;
            ch_Platform_Teleporter = 0;
            ch_Spring = 18;
        }
        else if (counts <= 70)
        {
            ch_Platform = 60;
            ch_Platform_Appearing = 0;
            ch_Platform_Disappearing = 40;
            ch_Platform_Teleporter = 0;
            ch_Spring = 30;
        }
        else if (counts > 70 && pointsMaxChar >= 2500)
        {
            //Debug.Log("Randomize level");
            lastLevelDifficulty = 999;
            deltaY = 0;
        }
        else
        {
            ch_Platform = 60;
            ch_Platform_Appearing = 0;
            ch_Platform_Disappearing = 35;
            ch_Platform_Teleporter = 5;
            ch_Spring = 30;
        }
    }
    void ChangePropertiesSpawnPlatform(int LevelDifficulty)
    {
        //Debug.Log($"Random properties2 - {LevelDifficulty}");
        switch (LevelDifficulty)
        {
            case 0:
                break;
            case 1:
                ch_Platform = 50;
                ch_Platform_Disappearing = 50;
                ch_Spring = 7;
                break;
            case 2:
                ch_Platform = 25;
                ch_Platform_Disappearing = 30;
                ch_Platform_Appearing = 40;
                ch_Platform_Teleporter = 5;
                break;
            case 3:
                ch_Platform = 10;
                ch_Spring = 0;
                ch_Platform_Disappearing = 45;
                ch_Platform_Appearing = 45;
                ch_Platform_Teleporter = 0;
                sizePlatform = 55;
                break;
            case 4:
                ch_Platform = 80;
                sizePlatform = 1;
                ch_Spring = 50;
                ch_Platform_Appearing = 0;
                ch_Platform_Teleporter = 20;
                ch_Platform_Disappearing = 0;
                break;
            case 5:
                ch_Platform = 90;
                sizePlatform = 45;
                ch_Spring = 0;
                ch_Platform_Appearing = 0;
                ch_Platform_Teleporter = 10;
                ch_Platform_Disappearing = 0;
                break;
            case 6:
                ch_Platform = 10;
                sizePlatform = 1;
                ch_Spring = 40;
                ch_Platform_Appearing = 0;
                ch_Platform_Teleporter = 20;
                ch_Platform_Disappearing = 70;
                break;
        }
    }
}
