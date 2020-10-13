using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBayonet : MonoBehaviour, IPlatforms 
{
    Transform trChild;
    ParticleSystem ps;
    bool isBayonet = false;
    bool isCanLoos = false;
    Vector3 startScale;
    //Vector3 startPosition;
    float delta=0;
    void Start()
    {
        trChild = transform.GetChild(0);
        startScale = trChild.localScale;
        ps = GetComponent<ParticleSystem>();
        //startPosition = trChild.position;
    }
    public void OnCollision(GameObject go)
    {
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        if (isBayonet && delta > 0.9f)
        { do { SoundObject.audioComp3.Play_LoosAlga(); go.GetComponent<ICharMov>().OnDeath("fell on a bayonets"); } while (false); }
        else
            SoundObject.audioComp3.Play_JumpOnPlatform();
        ps.Play();
        isBayonet = true;
    }
    
    void Update()
    {

        if (isBayonet)
        {
            delta += Time.deltaTime*3f;
            trChild.localScale = Vector3.Lerp(startScale, new Vector3(1, 1, 1),delta);
            /*trChild.position = Vector3.Lerp(startPosition, 
                        new Vector3(trChild.position.x, startPosition.y + 0.225f, trChild.position.z), delta);*/
        }
    }
}
