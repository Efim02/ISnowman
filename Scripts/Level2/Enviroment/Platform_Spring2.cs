using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Spring2 : MonoBehaviour, IPlatforms
{
    [SerializeField]
    GameObject PS;
    public void OnCollision(GameObject go)
    {
        CanvasMod.charMov.clampVelocity = 20f;              
        CanvasMod.charMov.Jump(2300f);
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        go.gameObject.GetComponent<Collider2D>().enabled = false;
        CanvasMod.spawner.isSpawnPlatformAfterFly = true;
        var psGO = Instantiate(PS, new Vector3(go.transform.position.x-0.01f, go.transform.position.y-0.4f, go.transform.position.z), transform.rotation, go.transform);
        SoundObject.audioComp2.Play_Spring();
        psGO.GetComponent<TrakingCharMovVelocityYForRemove>().go = go;
    }
}
