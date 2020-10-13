using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShark : MonoBehaviour, IPlatforms
{
    [SerializeField]
    GameObject shark1;
    [SerializeField]
    GameObject shark2;

    Transform childTr;

    static Transform directionShark1;
    static Transform directionShark2;

    Rigidbody2D rb2DChar;
    Transform target;
    Vector3 direction;
    float speed = 2.35f;
    bool isDelay = false;
    void Start()
    {
        CanvasMod.eventRestartLevel += DestroyForRestartLevel;
        rb2DChar = CanvasMod.charMov.rb2D;
        if (Random.Range(1, 3) == 1)
            childTr = Instantiate(shark1, transform).transform;
        else
            childTr = Instantiate(shark2, transform).transform;
    }
    public void OnCollision(GameObject go)
    {
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<Animator>().SetBool("isSwim", true);
        target = go.transform;
        if (directionShark2 == null)
        {
            //print("Set shark1");
            directionShark2 = transform;
            StartCoroutine(Delay(0.65f));
        }
        else
        {
           // print("Set shark2");
            directionShark1 = transform;
            target = directionShark2;
            speed = 6f;
            StartCoroutine(Delay(0.1f));
        }
    }
    bool isIf1 = true;
    void Update()
    {
        if (target != null && isDelay)
        {
            if (target != transform)
            {
                direction = target.position - transform.position;
                childTr.rotation = Quaternion.Euler(0, 0, Vector3.SignedAngle(Vector3.right, direction, Vector3.forward));
                if (direction.magnitude < 0.1f)
                {
                    if (target == rb2DChar.transform)
                    { CanvasMod.charMov.OnDeath("Shark"); }
                    //else if (transform == directionShark2)
                    //{
                    else
                    {
                        DestroySharks();
                    }
                    //}
                }
                transform.Translate(Time.fixedDeltaTime * Vector3.Normalize(direction) * speed);
            }
            if (directionShark1 != null && isIf1)
            {
                isIf1 = false;
                target = directionShark1;
                directionShark1 = null;
                directionShark2 = null;
            }
            if (Vector3.Distance(target.position, transform.position) > 20)
            {
                Destroy(gameObject);
            }
        }
        if (target ==null && rb2DChar.transform.position.y - transform.position.y > 10)
        {
            Destroy(gameObject);
        }
        if (rb2DChar.simulated == false && target !=null)
        {
            DestroyForRestartLevel();
        }
    }
    bool isIf2=true;
    void DestroySharks()  //ну тут типо они друг с другом начинают
    {
        if (isIf2)
        {
            isIf2 = false;
            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }
    IEnumerator Delay(float number)
    {
        yield return new WaitForSeconds(number);
        isDelay = true;
    }
    void DestroyForRestartLevel()
    {
       // CanvasMod.eventRestartLevel -= DestroyForRestartLevel;
        if (directionShark1 != null)
        {
            directionShark1 = null;
        }
        if (directionShark2 != null)
            directionShark2 = null;
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        CanvasMod.eventRestartLevel -= DestroyForRestartLevel;

    }
}
