using System.Collections.Generic;
using UnityEngine;

public class SpawnerPlatforms3 : ISpawnerPlatforms
{
    [SerializeField]
    GameObject Platform_Disappearing;
    [SerializeField]
    GameObject Platform_MovingFrom;
    [SerializeField]
    GameObject Platform_Moving;
    [SerializeField]
    GameObject Platform_Deceptive;
    [SerializeField]
    GameObject Platform_Bayonet;
    [SerializeField]
    GameObject Platform_Vortex;
    [SerializeField]
    GameObject PlatformAction_Bubble;
    [SerializeField]
    GameObject PlatformAction_Accelerate;
    [SerializeField]
    GameObject PlatformAction_Shark;


    //Для самой сложности значения
    //шанс спавна платформ

    List<GameObject> bublies= new List<GameObject>();
    [SerializeField]
    int ch_P_Disappearing = 20;
    [SerializeField]
    int ch_P_MovingFrom = 16;
    [SerializeField]
    int ch_P_Moving = 17;
    [SerializeField]
    int ch_P_Deceptive = 16;
    [SerializeField]
    int ch_P_Bayonet = 17;
    [SerializeField]
    int ch_P_Vortex = 5;
    [SerializeField]
    int ch_PA_Bubble = 2;
    [SerializeField]
    int ch_PA_Accelerate=1;
    [SerializeField]
    int ch_PA_Shark = 10;
    //шанс спавна доп объектов на платформах
    //нужно просто сюда же
    Vector2 placeSpawner = new Vector2(0, 0);
    int randomValue = 0;
    GameObject varParent;
    float sizeAll = 1f;
    bool isNotToScale = false;
    public override int maxHight { get; } = 1800;
    float counts;
    float pointsMaxChar;
    private void Awake()
    {
        CanvasMod.spawner = this;
    }

    internal override void RandomSpawnPlatform()
    {
        if (counts <= 70 && lastLevelDifficulty != 999)
            ReturnDifficultyLevel();
        ChangeValuiesForRandomPlatform();

        randomValue = Random.Range(0, 101);
        placeSpawner = new Vector2(Random.Range(-225, 226) * 0.01f, hight+0.12f);

        if (randomValue <= ch_P_Disappearing)
        {
            platformsList.Enqueue(varParent = Instantiate(Platform_Disappearing, placeSpawner, transform.rotation));
        }
        else if (randomValue - ch_P_Disappearing <= ch_P_MovingFrom)
        {
            platformsList.Enqueue(varParent = Instantiate(Platform_MovingFrom, placeSpawner, transform.rotation));
        }
        else if (randomValue - ch_P_Disappearing - ch_P_MovingFrom <= ch_P_Moving)
        {
            platformsList.Enqueue(varParent = Instantiate(Platform_Moving, placeSpawner, transform.rotation));
        }
        else if (randomValue - ch_P_Disappearing - ch_P_MovingFrom - ch_P_Moving <= ch_P_Deceptive)
        {
            platformsList.Enqueue(varParent = Instantiate(Platform_Deceptive, placeSpawner, transform.rotation));
        }
        else if (randomValue - ch_P_Disappearing - ch_P_MovingFrom - ch_P_Moving - ch_P_Deceptive <= ch_P_Bayonet)
        {
            platformsList.Enqueue(varParent = Instantiate(Platform_Bayonet, placeSpawner, transform.rotation));
        }
        else if (randomValue - ch_P_Disappearing - ch_P_MovingFrom - ch_P_Moving - ch_P_Deceptive - ch_P_Bayonet <= ch_P_Vortex)
        {
            platformsList.Enqueue(varParent = Instantiate(Platform_Vortex, placeSpawner, transform.rotation));
        }
        else if (randomValue - ch_P_Disappearing - ch_P_MovingFrom - ch_P_Moving - ch_P_Deceptive - ch_P_Bayonet - ch_P_Vortex <= ch_PA_Bubble)
        {
            bublies.Add(varParent = Instantiate(PlatformAction_Bubble, placeSpawner, transform.rotation));
        }
        else if (randomValue - ch_P_Disappearing - ch_P_MovingFrom - ch_P_Moving - ch_P_Deceptive - ch_P_Bayonet - ch_P_Vortex - ch_PA_Bubble <= ch_PA_Accelerate)
        {
            platformsList.Enqueue(varParent = Instantiate(PlatformAction_Accelerate, placeSpawner, transform.rotation));
            isNotToScale = true;
        }
        else if (randomValue - ch_P_Disappearing - ch_P_MovingFrom - ch_P_Moving - ch_P_Deceptive - ch_P_Bayonet - ch_P_Vortex - ch_PA_Bubble - ch_PA_Accelerate <= ch_PA_Shark)
        {
            varParent = Instantiate(PlatformAction_Shark, placeSpawner, transform.rotation);
            isNotToScale = true;
        }
        else
        {
            platformsList.Enqueue(varParent = Instantiate(Platform_Moving, placeSpawner, transform.rotation));
        }


        //устанавлваем размер
        //доп объект
        if (!isNotToScale)
            varParent.transform.localScale = new Vector3(GetSizePlatform() * sizeAll, 1f, 1);
        isNotToScale = false;
    }
    /*
     0 уровень 150 по высоте(150): выезжающие платформы, передвигающиеся, остальное нуль
     1 уровень 200 по высоте(350): move платформы, появляются, с водорослями, и немного обманчивых, пол процента бонусам, исчезающие
     2 уровень 250 по высоте(600):  здесь воосновном обманчавые и водоросли, исчезающие, бонус только пузырь
     3 уровень 400 по высоте(1000): здесь может все появится
     4 уровень 300 по высотре (1300): акулы, ездящие, выезжающие, ассилирейт, воронка
     5 уровень 250 по высоте (1550): воронки, обманчавые, водоросли, ездящие, немного исчезающих
     6 уровень 250 по высооте (1800): акулы, водоросли, обманчивые, выезжающие, бонусы, исчезающие
         
         */
    internal override int ReturnDifficultyLevel()
    {
        if (trPlayer.position.y - lastPositionPlayerY >= deltaY)
        {
            
            LevelDifficulty += 1;  // ВРЕМЕННО
            switch (LevelDifficulty)
            {
                case 1:
                    lastPositionPlayerY = 150;
                    deltaY = 200;
                    break;
                case 2:
                    lastPositionPlayerY = 350;
                    deltaY = 250;
                    break;
                case 3:
                    lastPositionPlayerY = 600;
                    deltaY = 400;
                    break;
                case 4:
                    lastPositionPlayerY = 1000;
                    deltaY = 300;
                    break;
                case 5:
                    lastPositionPlayerY = 1300;
                    deltaY = 250;
                    break;
                case 6:
                    lastPositionPlayerY = 1550;
                    deltaY = 250;
                    break;
                case 7:
                    lastPositionPlayerY = maxHight;
                    deltaY = 250;
                    if (SaveLoad.savePoints.points3 <= maxHight-1)
                    {
                        SaveLoad.savePoints.points3 = maxHight+1;
                        SaveLoad.SavePoints();
                        //GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasMod>().DeleteLockLevel();
                    }
                    break;
            }
        }
        return LevelDifficulty;
    }
    internal override void ChangeValuiesForRandomPlatform()
    {
        if (LevelDifficulty - lastLevelDifficulty >= 1)
        {
            //print("Level Difficulty changies");
            lastLevelDifficulty = LevelDifficulty;
            switch (LevelDifficulty)
            {
                case 0:
                    break;
                case 1:
                    ch_P_Disappearing = 15;
                    ch_P_MovingFrom = 30;
                    ch_P_Moving = 26;
                    ch_P_Deceptive = 20;
                    ch_P_Bayonet = 8;
                    ch_PA_Bubble = 1;
                    break;
                case 2:
                    ch_P_Disappearing = 25;
                    ch_P_MovingFrom = 11;
                    ch_P_Moving = 10;
                    ch_P_Deceptive = 40;
                    ch_P_Bayonet = 13;
                    ch_PA_Bubble = 1;
                    break;
                case 3:
                    ch_P_Disappearing = 20;
                    ch_P_MovingFrom = 20;
                    ch_P_Moving = 15;
                    ch_P_Deceptive = 20;
                    ch_P_Bayonet = 10;
                    ch_P_Vortex = 3;
                    ch_PA_Bubble = 2;
                    ch_PA_Accelerate = 1;
                    ch_PA_Shark = 9;
                    break;
                case 4:
                    ch_P_Disappearing = 0;
                    ch_P_MovingFrom = 40;
                    ch_P_Moving = 40;
                    ch_P_Vortex = 10;
                    ch_PA_Bubble = 3;
                    ch_PA_Accelerate = 1;
                    ch_PA_Shark = 7;
                    ch_P_Deceptive = 0;
                    ch_P_Bayonet = 0;
                    break;
                case 5:
                    ch_P_Disappearing = 5;
                    ch_P_MovingFrom = 15;
                    ch_P_Moving = 25;
                    ch_P_Deceptive = 25;
                    ch_P_Bayonet = 18;
                    ch_P_Vortex = 10;
                    ch_PA_Accelerate = 2;
                    ch_PA_Bubble = 0;
                    ch_PA_Shark = 0;
                    break;
                case 6:
                    ch_P_Disappearing = 5;
                    ch_P_MovingFrom = 25;
                    ch_P_Moving = 0;
                    ch_P_Deceptive = 40;
                    ch_P_Bayonet = 15;
                    ch_P_Vortex = 0;
                    ch_PA_Bubble = 5;
                    ch_PA_Accelerate = 0;
                    ch_PA_Shark =10;
                    break;
                case 7:
                    lastLevelDifficulty = 999;
                    break;
            }
        }
        else if (lastLevelDifficulty == 999)
        {
            if (trPlayer.position.y - lastPositionPlayerY >= deltaY)
            {
                lastPositionPlayerY += 250;
                ChangePropertiesSpawnPlatform();
            }
        }
    }
    internal override void ResetDifficultyLevel()
    {
        ch_P_Disappearing = 0;
        ch_P_MovingFrom = 0;
        ch_P_Moving = 0;
        ch_P_Deceptive = 0;
        ch_P_Bayonet = 0;
        ch_P_Vortex = 0;
        ch_PA_Bubble = 0;
        ch_PA_Accelerate = 0;
        ch_PA_Shark = 0;

        deltaY = 150;
        sizePlatform = 1;
        LevelDifficulty = 0;
        lastLevelDifficulty = 0;
        lastPositionPlayerY = 0;

        Debug.Log("Saveload.Counts1  -  "+SaveLoad.saveCounts.counts1+"\n"+ "Saveload.Counts2  -  " + SaveLoad.saveCounts.counts2+"\n"+ "Saveload.Counts3  -  " + SaveLoad.saveCounts.counts3 + "points3 - "+SaveLoad.savePoints.points3);

        pointsMaxChar = SaveLoad.savePoints.points3;
        SaveLoad.saveCounts.counts3++;
        ChangeStartLevel();
        SaveLoad.SaveCountPlayedGames();
    }
    void ChangePropertiesSpawnPlatform()
    {
        float temp = 100;
        float randomizeVar = Random.Range(0, 41);
        temp -= randomizeVar;
        ch_P_Disappearing = (int)randomizeVar;

        randomizeVar = Random.Range(0, 41);
        temp -= randomizeVar;
        ch_P_MovingFrom = (int)randomizeVar;

        if (temp < 40)
        {
            randomizeVar = Random.Range(0, temp + 1);
        }
        else
        {
            randomizeVar = Random.Range(0, 41);
        }
        temp -= randomizeVar;
        ch_P_Moving = (int)randomizeVar;

        if (temp < 40)
        {
            randomizeVar = Random.Range(0, temp + 1);
        }
        else
        {
            randomizeVar = Random.Range(0, 41);
        }
        temp -= randomizeVar;
        ch_P_Deceptive = (int)randomizeVar;

        if (temp < 25)
        {
            randomizeVar = Random.Range(0, temp + 1);
        }
        else
        {
            randomizeVar = Random.Range(0, 26);
        }
        temp -= randomizeVar;
        ch_P_Bayonet = (int)randomizeVar;

        if (temp < 10)
        {
            randomizeVar = Random.Range(0, temp + 1);
        }
        else
        {
            randomizeVar = Random.Range(0, 11);
        }
        temp -= randomizeVar;
        ch_PA_Shark = (int)randomizeVar;

        if (temp < 10)
        {
            randomizeVar = Random.Range(0, temp + 1);
        }
        else
        {
            randomizeVar = Random.Range(0, 11);
        }
        temp -= randomizeVar;
        ch_P_Vortex = (int)randomizeVar;

        if (temp < 4)
        {
            randomizeVar = Random.Range(0, temp + 1);
        }
        else
        {
            randomizeVar = Random.Range(0, 5);
        }
        temp -= randomizeVar;
        ch_PA_Bubble = (int)randomizeVar;

        if (temp < 3)
        {
            randomizeVar = Random.Range(0, temp + 1);
        }
        else
        {
            randomizeVar = Random.Range(0, 4);
        }
        temp -= randomizeVar;
        ch_PA_Accelerate = (int)randomizeVar;

        if (temp > 0)
        {
            int procent = (int)temp / 4;
            int ostatoc = (int)temp % 4;
            ch_P_Deceptive += procent;
            ch_P_Disappearing += procent + ostatoc;
            ch_P_Moving += procent;
            ch_P_MovingFrom += procent;
        }
        Debug.Log($"Random Procents: \nDis = { ch_P_Disappearing } \nMovFr= { ch_P_MovingFrom} \nMovPl = { ch_P_Moving} \nDecep={ ch_P_Deceptive} \nBayon = {ch_P_Bayonet} \nVort={  ch_P_Vortex } \nBuble = { ch_PA_Bubble} \nAccelerate = {  ch_PA_Accelerate} \nShark = {ch_PA_Shark }");
    }
    void ChangeStartLevel()
    {
        counts = SaveLoad.saveCounts.counts3;
        if (pointsMaxChar < maxHight)
            if (counts <= 10)
            {
                ch_P_Disappearing = 10;
                ch_P_MovingFrom = 45;
                ch_P_Moving = 45;
            }
            else if (counts <= 30)
            {
                ch_P_Disappearing = 10;
                ch_P_MovingFrom = 41;
                ch_P_Moving = 43;
                ch_P_Vortex = 4;
                ch_PA_Bubble = 1;
                ch_PA_Accelerate = 1;
            }
            else if (counts <= 70)
            {
                ch_P_Disappearing = 16;
                ch_P_MovingFrom = 41;
                ch_P_Moving = 30;
                ch_P_Vortex = 9;
                ch_PA_Bubble = 2;
                ch_PA_Accelerate = 2;
            }
            else
            {
                ch_P_Disappearing = 10;
                ch_P_MovingFrom = 35;
                ch_P_Moving = 35;
                ch_P_Vortex = 16;
                ch_PA_Bubble = 3;
                ch_PA_Accelerate = 1;
            }
        else
        {
            lastLevelDifficulty = 999;
            deltaY = 0;
        }
    }
    public override void RestartLevel()
    {
        base.RestartLevel();
        foreach (var go in bublies) if (go != null) { Destroy(go);  }
        bublies.Clear();
    }
}
