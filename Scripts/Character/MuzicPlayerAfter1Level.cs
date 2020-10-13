using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzicPlayerAfter1Level : MonoBehaviour
{
    float onMusic;
    AudioSource audioSource;

    [SerializeField]
    AudioClip[] audioClips;
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
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        if (audioSource.enabled) audioSource.Play();

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
