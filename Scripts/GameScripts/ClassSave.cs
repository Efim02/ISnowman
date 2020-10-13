using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClassSave
{
    public float onMusic;
    public float onSound;
    public string nickName;
    public int idSceneLastOpen = 1;
    public int id = -1;
    public ClassSave(float onMusic, float onSound, string nickName)
    {
        this.onMusic = onMusic;
        this.onSound = onSound;
        this.nickName = nickName;
    }
    public ClassSave()
    {
        this.onMusic = 0.5f;
        this.onSound = 0.5f;
        this.nickName = "Нет имени";
    }
}
[System.Serializable]
public class ClassSaveOld
{
    public bool onMusic;
    public bool onSound;
    public string nickName;
    public int idSceneLastOpen = 1;
    public int id = -1;
    public ClassSaveOld(bool onMusic, bool onSound, string nickName)
    {
        this.onMusic = onMusic;
        this.onSound = onSound;
        this.nickName = nickName;
    }
    public ClassSaveOld()
    {
        this.onMusic = true;
        this.onSound = true;
        this.nickName = "Нет имени";
    }
}
