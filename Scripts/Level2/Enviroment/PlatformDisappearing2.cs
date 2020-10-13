using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDisappearing2 : MonoBehaviour, IPlatforms
{
    SpriteRenderer spriteRend;
    ParticleSystem ps;
    private void Start()
    {        
        spriteRend = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
    }
    public void OnCollision(GameObject go)
    {
        //Debug.Log("Destroy - "+transform.position.y);
        SoundObject.audioComp2.Play_JumpOnPlatformDisappearing();
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        GetComponent<Collider2D>().enabled = false;
        colorBool = true;
        if (!ps.isPlaying)
            ps.Play();
        StartCoroutine(Destroy());
        
    }
    bool colorBool = false;
    private void Update()
    {
        if(colorBool)
            spriteRend.color = Color.Lerp(spriteRend.color, new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, 0), 5f*Time.fixedDeltaTime);
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.6f);
        CanvasMod.spawner.SpawnPlatform();
    }
    
}
