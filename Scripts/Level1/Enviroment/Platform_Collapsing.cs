using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Collapsing : MonoBehaviour, IPlatforms
{
    // Start is called before the first frame update
    [SerializeField]
    Sprite sprite0;
    int Condition = 1;
    SpriteRenderer spriteRend;
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        if (!SpawnerPlatforms1.isNeedEasyBrockenCollapsingPlatform)
        {
            Condition = Random.Range(0, 2);
            SwitchConditionSprite();
        }
        else
        {
            Condition = 0;
            SwitchConditionSprite();
        }
    }
    public void OnCollision(GameObject go)
    {
       
        Condition -=1 ;
        SwitchConditionSprite(); // смена состояния 
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        if (Condition <=-1)
        {
            CanvasMod.spawner.SpawnPlatform();
            SoundObject.audioComp1.Play_CrushIce();
            Destroy(gameObject);
        }
        else
            SoundObject.audioComp1.Play_JumpByIce();
        
    }
    void SwitchConditionSprite()
    {
        switch (Condition)
        {
            case 0:
                spriteRend.sprite = sprite0;
                break;
        }
    }
}
