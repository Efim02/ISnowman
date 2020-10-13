using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameInterface : MonoBehaviour
{
    [SerializeField]
    GameObject scoresObject;
    [Space]
    [SerializeField]
    GameObject scoresWithLine;
    GameObject scoresLineForRemove;
    [SerializeField]
    Color color;
    [SerializeField]
    Color ForLineScores;
    [SerializeField]
    GameObject go_CanvasText;
    GameObject ob;

    [SerializeField]
    string[] masTexts_forFirsts; 
    [SerializeField]
    string[] masTexts_forStart;
    [SerializeField]
    string[] masTexts_forAfter100 ;
    [SerializeField]
    string[] masTexts_forAfter500 ;
    [SerializeField]
    string[] masTexts_forAfter1000 ;
    [SerializeField]
    string[] masTexts_forInfinity ;
    int deltaHightForSpawnText = 50;

    Transform trPlayer;
    Text scoresText;
    float scoresLastMax;

    LocalizationManager localizationManager;
    private void Awake()
    {
        if (localizationManager == null)
        {
            localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();
        }
        localizationManager.OnLanguageChanged += ChangedMassivSentences;
    }
    void Start()
    {
        ChangedMassivSentences();

        scoresText = scoresObject.GetComponent<Text>();
        trPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        scoresLastMax = SaveLoad.GetPointsOnLevel();
        if (scoresLastMax < 11)
            scoresLastMax = 11;
        CanvasMod.charMov.eventDeath += LoadScores;
        CanvasMod.charMov.eventDeath += () => Destroy(ob);
        //SaveLoad.ResetValuies();
    }
    void Update()
    {
        if(trPlayer != null)
        scoresText.text = ((int)trPlayer.position.y).ToString();                      //устаналиваем значение высоты визуально
        if (scoresLastMax < trPlayer.position.y + 15 && scoresLineForRemove == null && scoresLastMax != 11)  //Следим когда заспавнить и удалить линию максимального результата
        {
            //print("Spawn line");
            scoresLineForRemove = Instantiate(scoresWithLine, new Vector3(0, scoresLastMax, 6), trPlayer.rotation);
            scoresLineForRemove.GetComponentInChildren<Text>().color = ForLineScores;
        }
        SpawnTexts();
    }
    void LoadScores()
    {
        scoresLastMax = SaveLoad.GetPointsOnLevel();
        if (scoresLineForRemove != null)
        {
            Destroy(scoresLineForRemove);
            scoresLineForRemove = null;
        }
    }
    
    void SpawnTexts()
    {
        if (ob == null)
        {
            if (SaveLoad.GetPointsOnLevel() == 11 && trPlayer.position.y < 10)
            {
                ob = Instantiate(go_CanvasText, new Vector3(0, trPlayer.position.y + 18, 5), transform.rotation);
                ob.GetComponent<ForGameText>().text.text = masTexts_forFirsts[Random.Range(0, masTexts_forFirsts.Length)];
            }
            else if (trPlayer.position.y < 30)
            {
                ob = Instantiate(go_CanvasText, new Vector3(0, trPlayer.position.y + 18, 5), transform.rotation);
                ob.GetComponent<ForGameText>().text.text = masTexts_forStart[Random.Range(0, masTexts_forStart.Length)];
                ob.GetComponent<ForGameText>().text.alignment = (TextAnchor)GetTextAlignment();
                deltaHightForSpawnText = Random.Range(40, 101);
            }
            else if (trPlayer.position.y > 100 && trPlayer.position.y < 500)
            {
                ob = Instantiate(go_CanvasText, new Vector3(0, trPlayer.position.y + 18, 5), transform.rotation);
                ob.GetComponent<ForGameText>().text.text = masTexts_forAfter100[Random.Range(0, masTexts_forAfter100.Length)];
                ob.GetComponent<ForGameText>().text.alignment = (TextAnchor)GetTextAlignment();
                deltaHightForSpawnText = Random.Range(40, 101);
            }
            else if (trPlayer.position.y > 500  && trPlayer.position.y < 1000)
            {
                ob = Instantiate(go_CanvasText, new Vector3(0, trPlayer.position.y + 18, 5), transform.rotation);
                ob.GetComponent<ForGameText>().text.text = masTexts_forAfter500[Random.Range(0, masTexts_forAfter500.Length)];
                ob.GetComponent<ForGameText>().text.alignment = (TextAnchor)GetTextAlignment();
                deltaHightForSpawnText = Random.Range(40, 101);
            }
            else if (trPlayer.position.y > 1000 && trPlayer.position.y < 1500)
            {
                ob = Instantiate(go_CanvasText, new Vector3(0, trPlayer.position.y + 18, 5), transform.rotation);
                ob.GetComponent<ForGameText>().text.text = masTexts_forAfter1000[Random.Range(0, masTexts_forAfter1000.Length)];
                ob.GetComponent<ForGameText>().text.alignment = (TextAnchor)GetTextAlignment();
                deltaHightForSpawnText = Random.Range(40, 101);
            }
            else if (trPlayer.position.y > 1500 && trPlayer.position.y < 2400)
            {
                ob = Instantiate(go_CanvasText, new Vector3(0, trPlayer.position.y + 18, 5), transform.rotation);
                ob.GetComponent<ForGameText>().text.text = masTexts_forInfinity[Random.Range(0, masTexts_forInfinity.Length)];
                ob.GetComponent<ForGameText>().text.alignment = (TextAnchor)GetTextAlignment();
                deltaHightForSpawnText = Random.Range(40, 101);
            }
            else if(trPlayer.position.y > 2400)
            {
                switch(Random.Range(1,5))
                {
                    case 1:
                        ob = Instantiate(go_CanvasText, new Vector3(0, trPlayer.position.y + 18, 5), transform.rotation);
                        ob.GetComponent<ForGameText>().text.text = masTexts_forAfter100[Random.Range(0, masTexts_forAfter100.Length)];
                        ob.GetComponent<ForGameText>().text.alignment = (TextAnchor)GetTextAlignment();
                        deltaHightForSpawnText = Random.Range(40, 101);
                        break;
                    case 2:
                        ob = Instantiate(go_CanvasText, new Vector3(0, trPlayer.position.y + 18, 5), transform.rotation);
                        ob.GetComponent<ForGameText>().text.text = masTexts_forAfter500[Random.Range(0, masTexts_forAfter500.Length)];
                        ob.GetComponent<ForGameText>().text.alignment = (TextAnchor)GetTextAlignment();
                        deltaHightForSpawnText = Random.Range(40, 101);
                        break;
                    case 3:
                        ob = Instantiate(go_CanvasText, new Vector3(0, trPlayer.position.y + 18, 5), transform.rotation);
                        ob.GetComponent<ForGameText>().text.text = masTexts_forAfter1000[Random.Range(0, masTexts_forAfter1000.Length)];
                        ob.GetComponent<ForGameText>().text.alignment = (TextAnchor)GetTextAlignment();
                        deltaHightForSpawnText = Random.Range(40, 101);
                        break;
                    case 4:
                        ob = Instantiate(go_CanvasText, new Vector3(0, trPlayer.position.y + 18, 5), transform.rotation);
                        ob.GetComponent<ForGameText>().text.text = masTexts_forInfinity[Random.Range(0, masTexts_forInfinity.Length)];
                        ob.GetComponent<ForGameText>().text.alignment = (TextAnchor)GetTextAlignment();
                        deltaHightForSpawnText = Random.Range(40, 101);
                        break;
                }
            }
            if (ob != null)
                ob.GetComponentInChildren<Text>().color = color;

        }
        if(ob != null)
        if (trPlayer.position.y - ob.transform.position.y > deltaHightForSpawnText)
        {
            Destroy(ob);
            ob = null;
        }
    }
    TextAlignment GetTextAlignment()
    {
        
        switch (Random.Range(0, 3))
        {
            case 0:
                return TextAlignment.Left;
            case 1:
                return TextAlignment.Center;
            case 2:
                return TextAlignment.Right;
        }
        return TextAlignment.Left;
    }

    void ChangedMassivSentences()
    {
        masTexts_forFirsts = localizationManager.GetMassivSentence(0);
        masTexts_forStart = localizationManager.GetMassivSentence(1); 
        masTexts_forAfter100 = localizationManager.GetMassivSentence(2);
        masTexts_forAfter500 = localizationManager.GetMassivSentence(3);
        masTexts_forAfter1000 = localizationManager.GetMassivSentence(4);
        masTexts_forInfinity = localizationManager.GetMassivSentence(5);
        Debug.Log("Changed sentences");
    }
    private void OnDestroy()
    {
        localizationManager.OnLanguageChanged -= ChangedMassivSentences;
    }
}
