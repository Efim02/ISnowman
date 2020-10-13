using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform1 : MonoBehaviour, IPlatforms
{
    public void OnCollision(GameObject go)
    {
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        SoundObject.audioComp1.Play_JumpBySnow();
        
    }
}   
