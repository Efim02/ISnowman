using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzicPlayer1 : MonoBehaviour
{
    float onMusic;
    AudioSource audioSource;
    
    [SerializeField]
    AudioClip audio1;
    [SerializeField]
    AudioClip audio2;
    [SerializeField]
    AudioClip audio3;
    [SerializeField]
    AudioClip audio4;
    System.DateTime time;
    float lengthAudio;
    void Start()
    {
        onMusic = SaveLoad.save.onMusic;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = onMusic;
        UI_GameSetting.eventChangeSettingsAudio += ChangeSetting;
        OnWhileMusic();
    }
    private void Update()
    {
        if (System.DateTime.Now.Subtract(time).TotalSeconds > lengthAudio)
            OnWhileMusic();
    }
    void OnWhileMusic()
    {
        switch(Random.Range(1,5))
        {
            case 1:
                audioSource.clip = audio1;
                break;
            case 2:
                audioSource.clip = audio2;
                break;
            case 3:
                audioSource.clip = audio3;
                break;
            case 4:
                audioSource.clip = audio4;
                break;
        }
        if(audioSource.enabled) audioSource.Play();

        time = System.DateTime.Now;
        lengthAudio = audioSource.clip.length;
    }
    void ChangeSetting(float s, float m)
    {
        onMusic = m;
        audioSource.volume = onMusic;
    }
    private void OnDestroy()
    {
        UI_GameSetting.eventChangeSettingsAudio -= ChangeSetting;
    }
}
