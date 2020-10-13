using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour, IPlatforms
{
    bool isMoveRight;
    
    void Start()
    {
        if (transform.position.x >= 0)
        {
            isMoveRight = false;
        }
        else { isMoveRight = true; }
    }
    public void OnCollision(GameObject go)
    {
        SoundObject.audioComp3.Play_JumpOnPlatform();
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
    }
    void Update()
    {
        if(isMoveRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 2f);
            if (transform.position.x > 2.3f)
                isMoveRight = false;
        }
        else
        {
            transform.Translate(-Vector3.right * Time.deltaTime *2f);
            if (transform.position.x < -2.3f)
                isMoveRight = true;
        }
    }
}
