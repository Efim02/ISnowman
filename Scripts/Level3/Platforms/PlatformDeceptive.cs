using UnityEngine;

public class PlatformDeceptive : MonoBehaviour, IPlatforms
{
    [SerializeField]
    GameObject copyObject;
    GameObject copy;
    void Start()
    {
        if (transform.position.x < 0)
            if (transform.position.x > -0.9f)
            {
                float x = 0;
                if (Random.Range(1, 3) == 1)
                    x = Random.Range(-225, (transform.position.x-1.3f)*100 ) * 0.01f;
                else
                    x = Random.Range((transform.position.x + 1.3f) * 100, 225f) * 0.01f;
                copy = Instantiate(copyObject, new Vector3(x, transform.position.y, transform.position.z), transform.rotation);
            }
            else
            {
                float x = Random.Range((transform.position.x + 1.3f) * 100, 225f)*0.01f;
                copy = Instantiate(copyObject, new Vector3(x, transform.position.y, transform.position.z), transform.rotation);
            }
        else
        {
            if (transform.position.x < 0.9f)
            {
                float x = 0;
                if (Random.Range(1, 3) == 1)
                    x = Random.Range((transform.position.x+1.3f)*100, 225) * 0.01f;
                else
                    x = Random.Range(-225f, (transform.position.x - 1.3f) * 100) * 0.01f;
                copy = Instantiate(copyObject, new Vector3(x, transform.position.y, transform.position.z), transform.rotation);
            }
            else
            {
                float x = Random.Range(-225f, (transform.position.x - 1.3f) * 100)*0.01f;
                copy = Instantiate(copyObject, new Vector3(x, transform.position.y, transform.position.z), transform.rotation);
            }
        }
    }
    public void OnCollision(GameObject go)
    {
        SoundObject.audioComp3.Play_JumpOnPlatform();
        CanvasMod.spawner.CheckHightPlatform((int)transform.position.y);
        do  copy.GetComponent<LerpAlpha>().colorBool = true;  while(false);
    }
    private void OnDestroy()
    {
        Destroy(copy);
    }
}
