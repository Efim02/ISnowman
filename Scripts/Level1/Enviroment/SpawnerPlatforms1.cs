using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPlatforms1 : ISpawnerPlatforms
{
    [SerializeField]
    GameObject Platform;
    [SerializeField]
    GameObject Spring;
    [SerializeField]
    GameObject Rocket;
    [SerializeField]
    GameObject Platform_Collapsing;

    int ch_Platform = 100;
    int ch_Platform_Collapsing = 0;
    //шанс спавна доп объектов на платформах
    int ch_Rocket = 5;
    int ch_Spring = 10;

    //нужно просто сюда же
    Vector2 placeSpawner = new Vector2(0, 0);
    int randomValue = 0;
    GameObject varParent;
    public static bool isNeedEasyBrockenCollapsingPlatform = false;
    float sizeAll = 0.8f;

    public override int maxHight { get; } = 1700;
    float counts;
    float pointsMaxChar;
    private void Awake()
    {
        CanvasMod.spawner = this;
    }
   
    internal override void RandomSpawnPlatform()
    {
        if (counts <= 70  && lastLevelDifficulty != 999 )
            ReturnDifficultyLevel();
        ChangeValuiesForRandomPlatform();
        
        randomValue = Random.Range(0, 101);
        placeSpawner = new Vector2(Random.Range(-225, 226) * 0.01f, hight);

        if(randomValue <= ch_Platform)
        {
            platformsList.Enqueue(varParent = Instantiate(Platform, placeSpawner, transform.rotation));
        }
        else                          // можно так но так как у нас всего две не актуально if(randomValue-ch_Platform <=ch_Platform_Collapsing)
        {
            platformsList.Enqueue(varParent = Instantiate(Platform_Collapsing, placeSpawner, transform.rotation));
        }
        varParent.transform.localScale = new Vector3( GetSizePlatform()*sizeAll , 1f, 1);  //устанавлваем размер
        //доп объект
        randomValue = Random.Range(0 , 101);

        if (ch_Spring >= randomValue)
        {
            placeSpawner = new Vector2(Random.Range(-30, 30) * varParent.transform.localScale.x * 0.01f + placeSpawner.x, hight + 0.2f);
            Instantiate(Spring, placeSpawner, transform.rotation, varParent.transform).transform.localScale = new Vector3(1/varParent.transform.localScale.x * sizeAll, 1f, 1f);
        }
        else if (ch_Spring <= randomValue && randomValue < ch_Rocket + ch_Spring)
        {
            placeSpawner = new Vector2(Random.Range(-30, 30) * 0.01f * varParent.transform.localScale.x + placeSpawner.x, hight + 0.2f);
            Instantiate(Rocket, placeSpawner, transform.rotation, varParent.transform).transform.localScale = new Vector3(1 / varParent.transform.localScale.x, 1f, 1f);
        }
    }

    /*   Первый уровень сложности до 300, будут спавнится просто платформы   /Цвет\ /синий-белый
        *   Второй уровень сложности с 300 до 500, спавняться 40 / 60 ледяные и простые платформы   /голубой - ледяной какой нить 
        *   Третий уровень сложности с 500 до 1000: спавняться ледяные уменьшенные и простые, а так же чаще спавняться пружинки /зеленый
        *   Четвертый уровень сложности 1000 - 1400 спавняться 50 /50 воосновном мелкие с пружинками 40 процентов       /Желтый
        *   Пятый уровень сложности  1400 - 1900 после тясячи  50/50  либо чтото придумаем что то еще а так начинается полный рандом.     /Оранжевый

        *   /// Если ничего не придумаем значит цвета потом по кругу: красный розовый-пурпурный, феолетовый, черный потом //взрыв или чтото такое ии у нас разные цвета
         
    */
    // для сложности игры придумали уровни
    internal override int ReturnDifficultyLevel()
    {
        if (trPlayer.position.y - lastPositionPlayerY >= deltaY)
        {
            LevelDifficulty += 1;  // ВРЕМЕННО
            //print("Change Diffuculty Level value - " + LevelDifficulty);
            switch (LevelDifficulty)
            {
                case 1:
                    lastPositionPlayerY = 300;
                    deltaY = 200;
                    break;
                case 2:
                    lastPositionPlayerY = 500;
                    deltaY = 500;
                    break;
                case 3:
                    lastPositionPlayerY = 1000;
                    deltaY = 400;
                    break;
                case 4:
                    lastPositionPlayerY = 1400;
                    deltaY = 300;
                    break;
                case 5:
                    lastPositionPlayerY = 1700;
                    if (SaveLoad.savePoints.points1 <= 1699)
                    {
                        SaveLoad.savePoints.points1 = 1700;
                        SaveLoad.SavePoints();
                        GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasMod>().DeleteLockLevel();
                    }
                    deltaY = 250;
                    //Debug.Log($"LastLevelRandom {LevelDifficulty}, last - {lastLevelDifficulty}");
                    break;
            }
        }
        return LevelDifficulty;
    }
    internal override void ChangeValuiesForRandomPlatform()
    {
        if( LevelDifficulty - lastLevelDifficulty >= 1)
        {
            lastLevelDifficulty = LevelDifficulty;
            //print("Level Difficulty changies last - " + lastLevelDifficulty);
            switch (LevelDifficulty)
            {
                case 0:
                    break;
                case 1:
                    ch_Platform = 60;
                    ch_Platform_Collapsing = 40;
                    ch_Spring = 18;
                    break;
                case 2:
                    ch_Platform = 30;
                    ch_Platform_Collapsing = 70;
                    sizePlatform = -1;
                    ch_Spring = 27;
                    ch_Rocket = 7;
                    break;
                case 3:
                    ch_Platform = 20 ;
                    ch_Platform_Collapsing = 80;
                    isNeedEasyBrockenCollapsingPlatform = true;
                    sizePlatform = 55;
                    ch_Spring = 60;
                    ch_Rocket = 12;
                    break;
                case 4:
                    ch_Platform = 50;
                    ch_Platform_Collapsing = 50;
                    isNeedEasyBrockenCollapsingPlatform = false;
                    sizePlatform = -1;
                    ch_Spring = 38;
                    ch_Rocket = 15;
                    break;
                case 5:
                    lastLevelDifficulty = 999;
                    //Debug.Log($"Set levelDifficulty = 999 real -{lastLevelDifficulty}, ");
                    break;

            }
        }
        else if (lastLevelDifficulty == 999)
        {
            if (trPlayer.position.y - lastPositionPlayerY >= deltaY)
            {
                lastPositionPlayerY += 250;
                
                ChangePropertiesSpawnPlatformAfter999(Random.Range(1, 5));
            }
        }
    }
    void ChangePropertiesSpawnPlatformAfter999(int LevelDifficulty)
    {
        //Debug.Log($"Random properties2 - {LevelDifficulty}");
        switch (LevelDifficulty)
        {
            case 0:
                break;
            case 1:
                ch_Platform = 60;
                ch_Platform_Collapsing = 40;
                ch_Spring = 18;
                break;
            case 2:
                ch_Platform = 30;
                ch_Platform_Collapsing = 70;
                sizePlatform = -1;
                ch_Spring = 27;
                ch_Rocket = 7;
                break;
            case 3:
                ch_Platform = 20;
                ch_Platform_Collapsing = 80;
                isNeedEasyBrockenCollapsingPlatform = true;
                sizePlatform = 55;
                ch_Spring = 60;
                ch_Rocket = 12;
                break;
            case 4:
                ch_Platform = 50;
                ch_Platform_Collapsing = 50;
                isNeedEasyBrockenCollapsingPlatform = false;
                sizePlatform = -1;
                ch_Spring = 38;
                ch_Rocket = 15;
                break;

        }
    }

    internal override void ResetDifficultyLevel()
    {
       

        ch_Rocket = 5;
        isNeedEasyBrockenCollapsingPlatform = false;
        sizePlatform = 1;
        LevelDifficulty = 0;
        lastLevelDifficulty = 0;
        lastPositionPlayerY = 0;
        deltaY = 300;

        SaveLoad.saveCounts.counts1++;
        ChangeStartLevel();
        SaveLoad.SaveCountPlayedGames();
        pointsMaxChar = SaveLoad.savePoints.points1;
    }
    void ChangeStartLevel()
    {
        counts = SaveLoad.saveCounts.counts1;

        if(counts <= 10)
        {
            ch_Platform = 100;
            ch_Platform_Collapsing = 0;
            ch_Spring = 10;
        }
        else if(counts <= 30)
        {
            ch_Platform = 75;
            ch_Platform_Collapsing = 25;
            ch_Spring = 14;
        }
        else if(counts <= 70)
        {
            ch_Platform = 40;
            ch_Platform_Collapsing = 60;
            ch_Spring = 25;
        }
        else if (counts > 70 && pointsMaxChar>=1700)
        {
            lastLevelDifficulty = 999;
            deltaY = 0;
        }
        else
        {
            ch_Platform = 60;
            ch_Platform_Collapsing = 40;
            ch_Spring = 30;
        }
    }
}