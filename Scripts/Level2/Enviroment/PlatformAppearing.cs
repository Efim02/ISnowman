using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAppearing : MonoBehaviour, IPlatforms
{
    Transform trPlayer;
    bool isWork = false;
    ParticleSystem ps;
    void Start()
    {
        trPlayer = CanvasMod.charMov.transform;
        ps = GetComponentInChildren<ParticleSystem>();
    }
    public void OnCollision(GameObject go)
    {
        SoundObject.audioComp2.Play_JumpOnPlatformAppearing();
        
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        
    }
    void Update()
    {
        if (transform.position.y - trPlayer.position.y < 1.25f)
            isWork = true;
        if(isWork)
        {
           /* if (!ps.isPlaying && ps != null)
                ps.Play();*/
            transform.localScale = Vector3.Lerp( transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 3 );
        }
    }
}
