using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanvasMod : MonoBehaviour
{
    [SerializeField]
    GameObject p_MainMenu;
    [SerializeField]
    GameObject p_Settings;
    [SerializeField]
    GameObject p_Raiting;
    [SerializeField]
    GameObject p_GM_Pause;
    [SerializeField]
    GameObject p_GM_Loos;
    [SerializeField]
    GameObject p_GameInterface;
    [SerializeField]
    GameObject p_ForSeeAd;
    [SerializeField]
    GameObject p_ImageLockLevel;
    [SerializeField]
    GameObject p_LoadingNextLevel;
    [SerializeField]
    GameObject p_LoadingPreviousLevel;
    [SerializeField]
    GameObject p_HelpNextLevel;

    Text textScoresForLoos;
    Text textDelay;
    [Space]
    [Header("Other")]
    [SerializeField]
    GameObject go_B_Pause;

    Button B_ForLockNextLevel;

    public static ISpawnerPlatforms spawner;
    public static ICharMov charMov;
    public static SoundObject soundObject;

    public delegate void OnPauseGame();
    public static event OnPauseGame eventPause;

    public delegate void OnContinueGame();
    public static event OnContinueGame eventContinue;

    public delegate void OnRestartLevel();
    public static event OnRestartLevel eventRestartLevel;
    private void Awake()
    {
        SaveLoad.UpdateGameValueSettings(); // пока что обнова как то так

        soundObject = null;
        eventPause = null;
        eventContinue = null;

        SaveLoad.Load();
        SaveLoad.LoadPoints();
        SaveLoad.LoadEnergies();
        SaveLoad.LoadCountPlayedGame();
        if (SaveLoad.savePoints.id != SaveLoad.save.id)
        {
            if (SaveLoad.save.id != -1 || SaveLoad.savePoints.id != -1)
            {
                SaveLoad.savePoints = new ClassSavePoints();
                SaveLoad.savePoints.id = SaveLoad.save.id;
                SaveLoad.SavePoints();
            }
        }
        UpdateGamePoints();
    }
    private void UpdateGamePoints()   //обновление сохраненных данных
    {
        if (SaveLoad.savePoints.points3 == 0)
        {
            SaveLoad.savePoints.points3 = 11;
            SaveLoad.SavePoints();
        }
    }
    private void Start()
    {
        textScoresForLoos = p_GM_Loos.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        textDelay = p_GameInterface.GetComponentInChildren<Text>();
        if (p_ImageLockLevel.activeInHierarchy)
        {
            B_ForLockNextLevel = p_ImageLockLevel.transform.parent.GetComponentInParent<Button>();
            DeleteLockLevel();
        }
        charMov.eventDeath += Death;
    }
    public void B_Start()
    {
        if (EnergyBar.Energies > 0)
        {
            spawner.RestartLevel();
            spawner.SpawnPlatform();
            charMov.Condition = 0;                                                //          charMov.Continue(); //нужно ли
            p_GameInterface.SetActive(true);
            p_MainMenu.SetActive(false);
        }
        else
            isNonEnergies();
    }
    public void B_Setting()
    {
        p_Settings.SetActive(true);
        p_MainMenu.SetActive(false);
    }
    public void B_Raiting()
    {
        p_Raiting.SetActive(true);
        p_MainMenu.SetActive(false);
    }
    public void B_Exit()
    {
        Application.Quit();
    }
    //Main menu end
    //Dop buttons begins
    public void BackInMenu(bool isFromGame)
    {
        if (isFromGame)
        {
            RestartLevel();
            p_GM_Pause.SetActive(false);
            p_GM_Loos.SetActive(false);
            p_MainMenu.SetActive(true);
        }
        else
        {
            p_Settings.SetActive(false);
            p_Raiting.SetActive(false);
            p_MainMenu.SetActive(true);
        }
    }
    public void B_Continue()
    {
        p_GM_Pause.SetActive(false);
        StartCoroutine(DelayBeforeContinueGame());
        p_GameInterface.SetActive(true);
    }
    public void B_TryStartAgain()
    {
        if (EnergyBar.Energies > 0)
        {
            RestartLevel();
            charMov.Condition = 0; //Что бы устнавливлось состояние 0
            spawner.SpawnPlatform();
            p_GM_Loos.SetActive(false);
            p_GameInterface.SetActive(true);
        }
        else
            isNonEnergies();
    }
    public void B_Pause()
    {
        p_GM_Pause.SetActive(true);
        eventPause?.Invoke();
        p_GameInterface.SetActive(false);
    }
    //Dop events 
    public void Death()
    {
        p_GameInterface.SetActive(false);
        textScoresForLoos.text = ((int)charMov.scores).ToString();
        charMov.scores = 0;
        p_GM_Loos.SetActive(true);
    }
    void RestartLevel()
    {
        eventRestartLevel?.Invoke();
        charMov.ResetValueBeforeStart();
                                //Что бы устнавливлось состояние 0
        spawner.RestartLevel();
    }
    IEnumerator DelayBeforeContinueGame()
    {
        go_B_Pause.SetActive(false);
        textDelay = p_GameInterface.GetComponentInChildren<Text>();
        textDelay.enabled = true;
        yield return new WaitForSeconds(1);
        textDelay.text = "2";
        yield return new WaitForSeconds(1);
        textDelay.text = "1";
        yield return new WaitForSeconds(1);
        textDelay.text = "0";
        yield return new WaitForSeconds(0.3f);
        textDelay.enabled = false;
        textDelay.text = "3";
        go_B_Pause.SetActive(true);
        eventContinue?.Invoke();
    }
    void isNonEnergies()
    {
        soundObject.Play_EnergiesNoHave();
        Debug.Log("NonEnergies");
    }
    public void B_SeeAd()
    {
        p_ForSeeAd.SetActive(true);
        p_MainMenu.SetActive(false);
    }
    public void B_Back()
    {
        p_ForSeeAd.SetActive(false);
        p_MainMenu.SetActive(true);
    }
    public void B_SeeAd1()
    {
        Ads.PlayAds();
    }
    public void DeleteLockLevel()  //каждому экземпляру присвоить число в зависимости от уровня и открывать ссылки на уровень
    {                               // 2) замок убрать 3)высветить табличку
        if (p_ImageLockLevel != null)
        if (SaveLoad.GetPointsOnLevel() >= spawner.maxHight)
        {
            p_ImageLockLevel.SetActive(false);
            B_ForLockNextLevel.interactable = true;
        }
    }
    public void OpenNextLevel()
    {
        p_LoadingNextLevel.SetActive(true);
        SaveLoad.save.idSceneLastOpen = SceneManager.GetActiveScene().buildIndex + 1;
       
        SaveLoad.Save();
        Debug.Log("Open Next Level");
        StartCoroutine(p_LoadingNextLevel.GetComponent<StartGame_LoadGame>().LoadScene(SaveLoad.save.idSceneLastOpen));
    }
    public void OpenPreviousLevel()
    {
        p_LoadingPreviousLevel.SetActive(true);
        SaveLoad.save.idSceneLastOpen = SceneManager.GetActiveScene().buildIndex - 1;
        SaveLoad.Save();
        StartCoroutine(p_LoadingNextLevel.GetComponent<StartGame_LoadGame>().LoadScene(SaveLoad.save.idSceneLastOpen));
    }
    public void B_HelpForNextLevel()
    {
        p_HelpNextLevel.SetActive(true);
    }
}
