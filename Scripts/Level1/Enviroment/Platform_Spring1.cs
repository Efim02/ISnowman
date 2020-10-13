using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Spring1 : MonoBehaviour, IPlatforms
{
    [SerializeField]
    GameObject PS_CharacterSpring;
    public void OnCollision(GameObject go)
    {
        CanvasMod.charMov.Jump(2000f);
        CanvasMod.charMov.clampVelocity = 18f; // прыгает на 8 юнитов + 1.5 юнит для приземления можно
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        go.GetComponent<Collider2D>().enabled = false;
        CanvasMod.spawner.isSpawnPlatformAfterFly = true;
        Instantiate(PS_CharacterSpring, transform.position,  transform.rotation);
        SoundObject.audioComp1.Play_Spring();
    }
}
