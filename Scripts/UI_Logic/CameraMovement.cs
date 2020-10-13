using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public static CameraMovement cameraMovement;
    Transform trPlayer;

    public static int isWithDampningMotion = 2;
    public float damping = 2.5f;

   
    void Start()
    {
        cameraMovement = this;
        trPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       // SetNewSizeCamera();
    }
    void FixedUpdate()
    {
        MoveToward();
    }
    /*void Update()
    {
        Transform Target = trPlayer;
        LookObject = trPlayer;
        
        
        float distance = 0f;

        if (LookObject != null)
        {
            distance = Mathf.Abs(transform.position.y - LookObject.position.y);
        }
        transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, Target.position.y, transform.position.z),  Time.deltaTime / TimeMoving);
        
        
            
        
    }*/
    void MoveToward()
    {
        if (isWithDampningMotion == 0)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(0, trPlayer.position.y - 0.65f, -10), Time.deltaTime * damping);    //у=играем
        }
        else if (isWithDampningMotion == 1)
            transform.position = Vector3.Lerp(new Vector3(0, transform.position.y, -10), new Vector3(0, trPlayer.position.y, -10), Time.deltaTime * 100); // Летим
        else if (isWithDampningMotion == 2)
            transform.position = Vector3.Lerp(new Vector3(0, transform.position.y, -10), new Vector3(0, trPlayer.position.y + 3.5f, -10), Time.deltaTime * 0.5f);   //Death
        else if (isWithDampningMotion == 3)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(0, trPlayer.position.y + 1.3f, -10), Time.deltaTime * damping); //когда плывем и надо контролить спавнящиеся объекты сверху
        }
    }
    
    void SetNewSizeCamera()
    {
        float ratio = (float)Screen.height / Screen.width;
        float interimHeightScreen = 5f * ratio;
        float ortSize = interimHeightScreen / 2f;
        Camera.main.orthographicSize = ortSize;
    }
}
