using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    private string currentLanguage;
    private Dictionary<string, string> localizedText;
    private static bool isReady = false;


    public MasMassivSentences masMassivSentences;

    public delegate void ChangeLangText();
    public event ChangeLangText OnLanguageChanged;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Belarusian || Application.systemLanguage == SystemLanguage.Ukrainian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
            }
            else
            {
                PlayerPrefs.SetString("Language", "en_US");
            }
            currentLanguage = PlayerPrefs.GetString("Language");
        }
        else currentLanguage = PlayerPrefs.GetString("Language");
        LoadLocalizedText(currentLanguage);
    }
    public void LoadLocalizedText(string langName)
    {
        string lang;

        Debug.Log(CurrentLanguage + "Langguage System");
        

        if (langName == "en_US")
            lang = "en_US";
        else
            lang = "ru_RU";
        string path = Application.streamingAssetsPath + "/Languages/" + lang + ".json";
        string dataAsJson;
#if UNITY_ANDROID && !UNITY_EDITOR
        WWW reader = new WWW(path);
        UnityWebRequest unityWebRequest = new UnityWebRequest(path);
        while(!reader.isDone) { }
        if (langName == "en_US")
            dataAsJson = reader.text;
        else
            dataAsJson = System.Text.Encoding.UTF8.GetString(reader.bytes, 3, reader.bytes.Length-3);
#else
        dataAsJson = File.ReadAllText(path);
#endif
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        localizedText = new Dictionary<string, string>();

        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

        CurrentLanguage =  langName;
        isReady = true;

        LoadLocalizedSentence(langName);
        OnLanguageChanged?.Invoke();
    }
    void LoadLocalizedSentence(string langName)
    {
        int sceneId = SceneManager.GetActiveScene().buildIndex;
        string lang;
        if (langName == "en_US")
            lang = "eng";
        else
            lang = "rus";

        Debug.Log(lang + "  - Langguage System , Command - to write sentences");
        string path = Application.streamingAssetsPath + "/Languages/Sentence/Level"+sceneId+"/" + lang + sceneId + ".json";
        string dataAsJson;
        print("Sentence path:   " + path);
        //dataAsJson = reader.text;
#if UNITY_ANDROID && !UNITY_EDITOR
        WWW reader = new WWW(path);
        UnityWebRequest unityWebRequest = new UnityWebRequest(path);
        while(!reader.isDone) { }
        if (langName == "en_US")
            dataAsJson = reader.text;
        else
            dataAsJson = System.Text.Encoding.UTF8.GetString(reader.bytes, 3, reader.bytes.Length-3);
#else
        dataAsJson = File.ReadAllText(path);
        Debug.Log("Game not on android!");
#endif
        masMassivSentences = JsonUtility.FromJson<MasMassivSentences>(dataAsJson);
    }
    public string[] GetMassivSentence(int index)
    {
        /*string[] mas = new string[masMassivSentences.masMassivs[index].mas.Length-1];
        for (int i= 0; i < masMassivSentences.masMassivs[index].mas.Length; i++)
        {
            mas[i] = masMassivSentences.masMassivs[index].mas[i].sentence;
        }
        return mas;*/
        return masMassivSentences.masMassivs[index].mas;
    }
    public string GetLocalizedValue(string key)
    {
        if(localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        else
        {
            throw new Exception("Localization text with key \"" + key + "\" not found");
        }
    }

    public string CurrentLanguage
    {
        get
        {
            return currentLanguage;
        }
        set
        {
            PlayerPrefs.SetString("Language", value);
            currentLanguage = PlayerPrefs.GetString("Language");
        }
    }
}
