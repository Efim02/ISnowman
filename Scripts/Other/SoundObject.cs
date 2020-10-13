using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [SerializeField]
    AudioClip sound_Click;
    [SerializeField]
    AudioClip ac_EnergiesNoHave;

    float onSound;
    AudioSource audioSource;
    public static AudioCompCharLevel1 audioComp1;
    public static AudioCompCharLevel2 audioComp2;
    public static AudioCompCharLevel3 audioComp3;
    private void Awake()
    {
        audioComp1 = null;
        audioComp2 = null;
        audioComp3 = null;
    }
    void Start()
    {
        CanvasMod.soundObject = this;
        onSound = SaveLoad.save.onSound;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = onSound;
        UI_GameSetting.eventChangeSettingsAudio += ChangeSetting;
        audioSource.volume = 2.5f;
    }
    void ChangeSetting(float s, float m)
    {
        onSound = s;
        audioSource.volume = onSound;
    }
    public void Play_Click()
    {
        if (audioSource.clip != ac_EnergiesNoHave || !audioSource.isPlaying)
        {
            audioSource.clip = sound_Click;
            audioSource.Play();
        }
    }
    public void Play_EnergiesNoHave()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = ac_EnergiesNoHave;
            audioSource.Play();
        }
    }
    private void OnDestroy()
    {
        UI_GameSetting.eventChangeSettingsAudio -= ChangeSetting;
    }
}
