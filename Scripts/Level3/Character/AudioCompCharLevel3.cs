using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCompCharLevel3 : MonoBehaviour
{
    [SerializeField]
    AudioClip ac_Platform;
    [SerializeField]
    AudioClip ac_PlatformDisappearing;
    [SerializeField]
    AudioClip ac_HitByFishman;
    [SerializeField]
    AudioClip ac_MovingPlatformFrom;

    [SerializeField]
    AudioClip[] ac_Vortex;
    [SerializeField]
    AudioClip[] ac_ToSwim;

    [SerializeField]
    AudioClip ac_Loos;
    [SerializeField]
    AudioClip ac_LoosAlga;
    [SerializeField]
    AudioClip ac_LoosBuble;

    float onSound;
    AudioSource audioSource;
    void Start()
    {
        SoundObject.audioComp3 = this;
        onSound = SaveLoad.save.onSound;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = onSound;
        CanvasMod.charMov.eventDeath += Play_Loos;
        UI_GameSetting.eventChangeSettingsAudio += ChangeSetting;
    }
    void ChangeSetting(float s, float m)
    {
        onSound =s;
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
    public void Play_JumpOnVortex()
    {
        audioSource.clip = ac_Vortex[Random.Range(0,ac_Vortex.Length)];
        audioSource.Play();
    }
    public void Play_ToSwim()
    {
        audioSource.clip = ac_ToSwim[Random.Range(0,ac_ToSwim.Length)];
        audioSource.Play();
    }
    public void Play_HitByFishman()
    {
        audioSource.clip = ac_HitByFishman;
        audioSource.Play();
    }
    public void Play_MoveFromPlatform()
    {
        if (audioSource.clip != ac_Vortex[0] && audioSource.clip != ac_Vortex[1])
        {
            audioSource.clip = ac_MovingPlatformFrom;
            audioSource.Play();
        }
    }
    public void Play_LoosBuble()
    {
        audioSource.clip = ac_LoosBuble;
        audioSource.Play();
    }
    public void Play_LoosAlga()
    {
        audioSource.clip = ac_LoosAlga;
        audioSource.Play();
    }
    public void Play_Loos()
    {
        if (audioSource.isPlaying == false)
            switch (Random.Range(1, 4))
            {
                case 1:
                    audioSource.clip = ac_Loos;
                    audioSource.Play();
                    break;
            }
    }
}
