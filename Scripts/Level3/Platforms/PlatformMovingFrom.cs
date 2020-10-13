using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovingFrom : MonoBehaviour, IPlatforms
{
    Vector3 startPosition;
    bool isMove = false;
    Transform tr;
    void Start()
    {
        tr = CanvasMod.charMov.transform;
        startPosition = transform.position;
        if (startPosition.x >= 0)
        {
            transform.position = new Vector3(5f, transform.position.y, transform.position.z);
        }
        else { transform.position = new Vector3(-5f, transform.position.y, transform.position.z); }
    }
    public void OnCollision(GameObject go)
    {
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
    }
    bool isWhile = true;
    float distance = 0;
    void Update()
    {
        if (tr.position.y + 1f > transform.position.y)
            if (isWhile)
            { isMove = true; isWhile = false; distance = Vector3.Distance(transform.position, startPosition); SoundObject.audioComp3.Play_MoveFromPlatform(); }
        if (isMove)
            transform.position=Vector3.Lerp(transform.position, startPosition, Time.deltaTime* distance * 2.2f);

    }
}
