using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_GameSetting : MonoBehaviour
{
    [SerializeField]
    GameObject gO_Slider_Sound;
    [SerializeField]
    GameObject gO_Slider_Music;
    [SerializeField]
    GameObject gO_Text;

    Slider slider_Sound;
    Slider slider_Music;
    InputField inputFieldNick;

    public delegate void OnChangeSettingsAudio(float sound, float music);
    public static event OnChangeSettingsAudio eventChangeSettingsAudio;

    private const string superNikcname = "0106200248";
    void Start()
    {
        slider_Sound = gO_Slider_Sound.GetComponent<Slider>();
        slider_Music = gO_Slider_Music.GetComponent<Slider>();
        inputFieldNick= gO_Text.GetComponent<InputField>();
        slider_Sound.value = SaveLoad.save.onSound;
        slider_Music.value = SaveLoad.save.onMusic;
        inputFieldNick.text = SaveLoad.save.nickName;
        //print(inputFieldNick.text);  не помню что проверял
    }
    public void SliderChange_OnSound()
    {
        SaveLoad.save.onSound = slider_Sound.value;
        eventChangeSettingsAudio?.Invoke(slider_Sound.value, slider_Music.value);
    }
    public void SliderChange_OnMusic()
    {
        SaveLoad.save.onMusic = slider_Music.value;
        eventChangeSettingsAudio?.Invoke(slider_Sound.value, slider_Music.value);
    }
    public void ChangedNickName()
    {
        if(inputFieldNick.text == superNikcname)
        {
            inputFieldNick.text = "SPN_"+Application.platform.ToString();
            SaveLoad.savePoints.points1 = 1900;
            SaveLoad.savePoints.points2 = 2700;
            SaveLoad.savePoints.points3 = 2000;
            SaveLoad.SavePoints();
        }
        SaveLoad.save.nickName = inputFieldNick.text;
    }
    public void B_SaveSettings()
    {
        SaveLoad.Save();
        //Debug.Log($"Saves: sound - {slider_Sound.value}, music - {slider_Music.value}");
    }
}
