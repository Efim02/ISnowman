using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusAccelerate : MonoBehaviour, IPlatforms
{
    [SerializeField]
    GameObject Spawner_Urchin;
    public void OnCollision(GameObject go)
    {
        SoundObject.audioComp3.Play_ToSwim();
        var pos = go.transform.position;
        Instantiate(Spawner_Urchin, new Vector3(0, ((int)pos.y/2)*2) , transform.rotation);
        //print("On collision with bonusAccelerate");
        go.GetComponent<CharMoveLelel3>().isSwim = true;
        CanvasMod.spawner.isToSpawn = false;
        CameraMovement.isWithDampningMotion = 3;
        var list = CanvasMod.spawner.platformsList;
        foreach (GameObject gO in list)
        { Destroy(gO); }
        list.Clear();
        Destroy(gameObject);
    }
    
}
