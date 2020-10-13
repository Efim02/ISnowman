using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform_Rocket : MonoBehaviour, IPlatforms
{
    public void OnCollision(GameObject go)
    {
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        go.GetComponent<CharMoveLevel1>().FlyOnRocket();
        SoundObject.audioComp1.Play_FlyRocket();
        Destroy(gameObject);
    }

}
