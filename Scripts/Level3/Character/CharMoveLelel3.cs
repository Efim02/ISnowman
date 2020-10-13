using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMoveLelel3 : ICharMov
{
    bool isWasChangeMovableVariable=false;
    private bool Swiming = false;

    [SerializeField]
    GameObject PS_ForJump;
    internal bool isSwim
    {
        get
        {
            return Swiming;
        }
        set
        {
            if(Swiming != value)
            if(value == true)
                {
                    animator.SetBool("Swiming", true);
                    animator.Play("Swiming");
                    Swiming = value;
                }
            else
                {
                    animator.SetBool("Swiming", false);
                    Swiming = value;
                }
        }
    }
    float speedSwim = 6f;
    internal override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Platforms"))  // Спавн частиц под нами
        {
            Instantiate(PS_ForJump, new Vector3(tr.position.x, tr.position.y , 0), tr.rotation);
        }

    }
    internal override void Save()
    {
        if (SaveLoad.savePoints.points3 < scores)
        {
            //print("Saves.scores  - "+SaveLoad.save.points+"    scores - "+scores);
            SaveLoad.savePoints.points3 = (int)scores;
            SaveLoad.SavePoints();
        }
    }
    public override void Continue()
    {
        if (!isWasChangeMovableVariable)
            base.Continue();
        else isPause = false;
    }
    public override void Pause()
    {
        if (rb2D.simulated == true)
        {
            base.Pause();
            isWasChangeMovableVariable = false;
        }
        else
        {
            isWasChangeMovableVariable = true;
            isPause = true;
        }
    }
    internal override void OnCondition_1()
    {
        if(!Swiming)
            base.OnCondition_1();
        else
        {
            if (!isPause)
            {
                rb2D.velocity = tr.up * speedSwim;
                if (scores < tr.position.y)
                    scores = tr.position.y;
            }
        }
    }
    public override void ResetValueBeforeStart()
    {
        base.ResetValueBeforeStart();
        animator.SetBool("Swiming", false);
        isSwim = false;
    }
}
