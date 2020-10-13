using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMoveLevel1 : ICharMov
{
    
    [SerializeField]
    GameObject PS_ForJump;
    [SerializeField]
    GameObject PS_ForFlyRocket;
    GameObject PS_ForRemove;

    bool isFlyOnRocket = false;
    private float startHight;
    
    
    
    public void FlyOnRocket()
    {
        rb2D.simulated = false;
        startHight = tr.position.y;
        CameraMovement.isWithDampningMotion = 1;
        CanvasMod.spawner.isSpawnPlatformAfterFly = true;
        rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        isFlyOnRocket = true;
        PS_ForRemove = Instantiate(PS_ForFlyRocket, new Vector3(tr.position.x, tr.position.y-0.5f, 0), tr.rotation, tr);
        //print("Spawn rocket PS");
    } //полет на ракете
    internal override void OnCondition_1()
    {
        base.OnCondition_1();
        if (isFlyOnRocket && !isPause)
        {
            animator.SetBool("FlyRocket", true);
            tr.Translate(Time.fixedDeltaTime * Vector2.up * 15);
            if (tr.position.y - startHight > 46f)
            {
                isFlyOnRocket = false;
                CameraMovement.isWithDampningMotion = 0;
                rb2D.simulated = true;
                animator.SetBool("FlyRocket", false);
                Destroy(PS_ForRemove);
                PS_ForRemove = null;
                Jump(2000f);
            }
        }
    }   //вызов метода во время состояния 1
    internal override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.gameObject.CompareTag("Platforms"))  // Спавн частиц под нами
        {
            Instantiate(PS_ForJump, new Vector3(tr.position.x, tr.position.y + 0.3f, 0), tr.rotation);
        }

    }   //чек смерти пока что такой
    public override void ResetValueBeforeStart()
    {
        base.ResetValueBeforeStart();
        isFlyOnRocket = false;
        Destroy(PS_ForRemove);
        animator.SetBool("FlyRocket", false);
    }
    public override void Continue()
    {
        if(!isFlyOnRocket)
            rb2D.simulated = true;
        base.Continue();
    }
    internal override void Save()
    {
        if(SaveLoad.savePoints.points1 < scores)
        {
            //print("Saves.scores  - "+SaveLoad.save.points+"    scores - "+scores);
            SaveLoad.savePoints.points1 = (int)scores;
            SaveLoad.SavePoints();
        }
    }
}
    