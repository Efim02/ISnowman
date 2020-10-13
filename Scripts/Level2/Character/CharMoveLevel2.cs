using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMoveLevel2 : ICharMov
{
    public bool isSimPhycics = false;
    internal override void Save()
    {
        if (SaveLoad.savePoints.points2 < scores)
        {
            //print("Saves.scores  - "+SaveLoad.save.points+"    scores - "+scores);
            SaveLoad.savePoints.points2 = (int)scores;
            SaveLoad.SavePoints();
        }
    }
    internal override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isSimPhycics)
            StartCoroutine(Enumerator());
    }
    internal override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

    }
    public IEnumerator Enumerator()
    {
        isSimPhycics = false;
        yield return new WaitForSeconds(0.6f);
        CameraMovement.cameraMovement.damping = 2.5f;
        rb2D.simulated = true;
        print("simPhysics");
    }
    
}
