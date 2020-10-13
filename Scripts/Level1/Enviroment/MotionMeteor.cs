using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionMeteor : MonoBehaviour
{


    Vector2 direction;
    void Start()
    {
        
        switch(Random.Range(0 ,2))
        {
            case 0:
                transform.rotation =Quaternion.Euler(new Vector3(0,0,45f));
                direction = new Vector2(-1, 0);
                GetComponent<ParticleSystem>().startRotation3D = new Vector3(0,0, 5.498f);
                transform.position = new Vector3(+13f, CanvasMod.spawner.hight + Random.Range(10,20), 20);
                break;

            case 1:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 135));
                direction = new Vector2(-1, 0);
                GetComponent<ParticleSystem>().startRotation3D = new Vector3(0, 0, 3.927f);
                transform.position = new Vector3(-13f, CanvasMod.spawner.hight + Random.Range(10,20), 20);

                break;

        }
        StartCoroutine(OnDeleted());
    }

    void Update()
    {
        transform.Translate( direction * Time.deltaTime * 15f);
    }
    IEnumerator OnDeleted()
    {
        yield return new WaitForSeconds(10);
     //   Debug.Log("Deleted");
        Destroy(gameObject);
    }
}
