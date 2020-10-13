using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Teleporter : MonoBehaviour, IPlatforms
{
    public void OnCollision(GameObject go)
    {
        //Debug.Log("Teleporter");
        SoundObject.audioComp2.Play_Teleport();
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        CanvasMod.spawner.isSpawnPlatformAfterFly = true;
        go.transform.Translate(0, 31.5f, 0);
        CameraMovement.cameraMovement.damping = 5f;
        go.GetComponent<Rigidbody2D>().simulated = false;
        go.GetComponent<CharMoveLevel2>().isSimPhycics = true;
    }
    
}
