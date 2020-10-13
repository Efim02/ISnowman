using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCompCharLevel1 : MonoBehaviour
{
    [SerializeField]
    AudioClip sound_CrushIce;
    [SerializeField]
    AudioClip sound_JumpByIce;
    [SerializeField]
    AudioClip sound_FlyRocket1;
    [SerializeField]
    AudioClip sound_FlyRocket2;
    [SerializeField]
    AudioClip sound_FlyRocket3;
    [SerializeField]
    AudioClip sound_Spring1;
    [SerializeField]
    AudioClip sound_Spring2;
    [SerializeField]
    AudioClip sound_Loos;
    [SerializeField]
    AudioClip sound_JumpBySnow;
    

    float onSound;
    AudioSource audioSource;

    void Start()
    {
        SoundObject.audioComp1 = this;
        onSound = SaveLoad.save.onSound;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = onSound;
        UI_GameSetting.eventChangeSettingsAudio += ChangeSetting;
        CanvasMod.charMov.eventDeath += Play_Loos;
    }
    void ChangeSetting(float s, float m)
    {
        onSound = s;
        audioSource.volume = onSound;
    }
    public void Play_Spring()
    {
        switch(Random.Range(1,6))
        {
            case 1:
                audioSource.clip = sound_Spring1;
                audioSource.Play();
                break;
            default:
                audioSource.clip = sound_Spring2;
                audioSource.Play();
                break;

        }
    }
    public void Play_FlyRocket()
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                audioSource.clip = sound_FlyRocket1;
                audioSource.Play();
                break;
            case 2:
                audioSource.clip = sound_FlyRocket2;
                audioSource.Play();
                break;
            case 3:
                audioSource.clip = sound_FlyRocket3;
                audioSource.Play();
                break;

        }
    }
    public void Play_JumpByIce()
    {
        audioSource.clip = sound_JumpByIce;
        audioSource.Play();
    }
    public void Play_CrushIce()
    {
        audioSource.clip = sound_CrushIce;
        audioSource.Play();
    }
    public void Play_JumpBySnow()
    {
        audioSource.clip = sound_JumpBySnow;
        audioSource.Play();
    }
    public void Play_Loos()
    {
        switch(Random.Range(1,4))
        {
            case 1:
                audioSource.clip = sound_Loos;
                audioSource.Play();
                break;
        }
    }
    
    private void OnDestroy()
    {
        UI_GameSetting.eventChangeSettingsAudio -= ChangeSetting;
    }
}

