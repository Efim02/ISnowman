using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform2 : MonoBehaviour, IPlatforms
{
    public void OnCollision(GameObject go)
    {
        SoundObject.audioComp2.Play_JumpOnPlatform();
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        if(!GetComponent<ParticleSystem>().isPlaying)
            GetComponent<ParticleSystem>().Play();
    }
}
