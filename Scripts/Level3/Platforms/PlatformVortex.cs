using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVortex : MonoBehaviour, IPlatforms
{
    SpriteRenderer spRen;

    Collider2D collider;
    void Start()
    {
        spRen = GetComponent<SpriteRenderer>();
        collider = GetComponent<EdgeCollider2D>();
    }
    void Update()
    {
        if (!collider.isTrigger)
        { if (spRen.sortingOrder != 6)
                spRen.sortingOrder = 6; }
        else if(collider.isTrigger)
        {
            if (spRen.sortingOrder != 0)
                spRen.sortingOrder = 0;
        }
    }
    public void OnCollision(GameObject go)
    {
        SoundObject.audioComp3.Play_JumpOnVortex();
        CanvasMod.charMov.clampVelocity = 25f;
        CanvasMod.charMov.Jump(3300f);
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        go.gameObject.GetComponent<Collider2D>().enabled = false;
        CanvasMod.spawner.isSpawnPlatformAfterFly = true;
    }

    
}
