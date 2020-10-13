using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCompCharLevel2 : MonoBehaviour
{
    [SerializeField]
    AudioClip ac_Platform;
    [SerializeField]
    AudioClip ac_PlatformDisappearing;
    [SerializeField]
    AudioClip ac_PlatformAppearing;
    [SerializeField]
    AudioClip[] ac_Teleports;
    [SerializeField]
    AudioClip[] ac_Springs;
    [SerializeField]
    AudioClip ac_Loos;

    float onSound;
    AudioSource audioSource;
    void Start()
    {
        SoundObject.audioComp2 = this;
        onSound = SaveLoad.save.onSound;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = onSound;
        CanvasMod.charMov.eventDeath += Play_Loos;
        UI_GameSetting.eventChangeSettingsAudio += ChangeSetting;
    }
    void ChangeSetting(float s, float m)
    {
        onSound = s;
        audioSource.volume = onSound;
    }
    private void OnDestroy()
    {
        UI_GameSetting.eventChangeSettingsAudio -= ChangeSetting;
    }
    public void Play_JumpOnPlatform()
    {
        audioSource.clip = ac_Platform;
        audioSource.Play();
    }
    public void Play_JumpOnPlatformDisappearing()
    {
        audioSource.clip = ac_PlatformDisappearing;
        audioSource.Play();
    }
    public void Play_JumpOnPlatformAppearing()
    {
        audioSource.clip = ac_PlatformAppearing;
        audioSource.Play();
    }
    public void Play_Teleport()
    {
        AudioClip clip = ac_Teleports[Random.Range(0, ac_Teleports.Length)];
        audioSource.Play();
        GameObject go = new GameObject("Sound " + clip.name);
        go.transform.position = Camera.main.transform.position;
        AudioSource aus = go.AddComponent<AudioSource>();
        aus.clip = clip;

        Object.Destroy(go, clip.length + 0.2f);
        aus.Play();
    }
    public void Play_Spring()
    {
        if(Random.Range(0,101)<60)
        audioSource.clip = ac_Springs[Random.Range(0,ac_Springs.Length)];
        audioSource.Play();
    }
    public void Play_Loos()
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                audioSource.clip = ac_Loos;
                audioSource.Play();
                break;
        }
    }
}
