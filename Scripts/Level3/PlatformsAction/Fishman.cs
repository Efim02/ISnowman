using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishman : MonoBehaviour
{
    Transform trChar;
    Vector3 direction;
    [SerializeField]
    bool isDeleting=false;

    bool isPause = false;
    Rigidbody2D rb2DChar;
    void Start()
    {
        rb2DChar = CanvasMod.charMov.rb2D;
        trChar = CanvasMod.charMov.transform;
        //Debug.Log("Spawn");
        CanvasMod.eventPause += Pause_Continue;
        CanvasMod.eventContinue += Pause_Continue;
    }

    bool isIf1=true;
    void FixedUpdate()
    {
        if(rb2DChar.simulated == true)
        {
            Destroy();
        }
        if (!isPause)
        {
            if (!isDeleting)
            {
                direction = trChar.position - transform.position;
                if (direction.magnitude < 0.25f )
                {
                    print("Loos character becouse fishman");
                    SoundObject.audioComp3.Play_LoosBuble();
                    CanvasMod.charMov.OnDeath("Fishman");
                    Destroy(PlatformBuble.varTemp);
                    PlatformBuble.varTemp = null;
                }
            }
            transform.Translate(Time.fixedDeltaTime * Vector3.Normalize(direction) * 8f);
        }
    }

    internal void Destroy()
    {
        if (isIf1 == true)
        {
            isIf1 = false;

            //print("Destroy");
            CanvasMod.eventPause -= Pause_Continue;
            CanvasMod.eventContinue -= Pause_Continue;
            isDeleting = true;

            direction = Vector3.zero - transform.position;
            StartCoroutine(Deleting());
        }
    }
    IEnumerator Deleting()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    void Pause_Continue()
    {
        isPause = !isPause;
    }
}
