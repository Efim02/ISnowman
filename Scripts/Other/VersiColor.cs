using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VersiColor : MonoBehaviour
{
    Text text;
    float iteration = 1;
    Color32 color;
    float speedDeltaColor = 2f;
    float frameWork = 0;
    void Start()
    {
        text = GetComponent<Text>();
        color = GetComponent<Text>().color;
        // StartCoroutine(UpdateE());
    }
    private void Update()
    {

        /* switch (iteration)
         {
             case 1:
                 text.color = Color32.Lerp(color, new Color32(255, 255, 0, 255), Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor);
                 if (Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor > 0.98f)
                 {
                     color = text.color;
                     iteration += 1; 
                 }
                 break;
             case 2:
                 text.color = Color32.Lerp(new Color32(255, 0, 0, 255), color, Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor);
                 if (Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor < 0.02f)
                 {
                     color = text.color;
                     iteration += 1;
                 }
                 break;
             case 3:
                 text.color = Color32.Lerp(color, new Color32(255, 0, 255, 255), Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor);
                 if (Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor > 0.98f)
                 {
                     color = text.color;
                     iteration += 1;
                 }
                 break;
             case 4:
                 text.color = Color32.Lerp(new Color32(0, 0, 255, 255), color, Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor);
                 if (Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor < 0.02f)
                 {
                     color = text.color;
                     iteration += 1;
                 }
                 break;
             case 5:
                 text.color = Color32.Lerp(color, new Color32(0, 255, 255, 255), Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor);
                 if (Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor > 0.98f)
                 {
                     color = text.color;
                     iteration += 1;
                 }
                 break;
             case 6:
                 text.color = Color32.Lerp(new Color32(0, 255, 0, 255), color, Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor);
                 if (Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor < 0.02f)
                 {
                     color = text.color;
                     iteration = 1;
                 }
                 break;
         }*/
        frameWork += Time.deltaTime;
        switch (iteration)
        {
            case 1:
                text.color = Color32.Lerp(color, new Color32(255, 255, 0, 255), frameWork * speedDeltaColor);
                if (frameWork * speedDeltaColor > 1)
                {
                    color = text.color;
                    iteration += 1;
                    frameWork = 0;
                }
                break;
            case 2:
                text.color = Color32.Lerp(color, new Color32(255, 0, 0, 255), frameWork * speedDeltaColor);
                if (frameWork * speedDeltaColor > 1)
                {
                    color = text.color;
                    iteration += 1;
                    frameWork = 0;
                }
                break;
            case 3:
                text.color = Color32.Lerp(color, new Color32(255, 0, 255, 255), frameWork * speedDeltaColor);
                if (frameWork * speedDeltaColor > 1)
                {
                    color = text.color;
                    iteration += 1;
                    frameWork = 0;
                }
                break;
            case 4:
                text.color = Color32.Lerp(color , new Color32(0, 0, 255, 255), frameWork * speedDeltaColor);
                if (frameWork * speedDeltaColor > 1)
                {
                    color = text.color;
                    iteration += 1;
                    frameWork = 0;
                }
                break;
            case 5:
                text.color = Color32.Lerp(color, new Color32(0, 255, 255, 255), frameWork * speedDeltaColor);
                if (frameWork * speedDeltaColor > 1)
                {
                    color = text.color;
                    iteration += 1;
                    frameWork = 0;
                }
                break;
            case 6:
                text.color = Color32.Lerp(color, new Color32(0, 255, 0, 255), frameWork * speedDeltaColor);
                if (frameWork * speedDeltaColor > 1)
                {
                    color = text.color;
                    iteration = 1;
                    frameWork = 0;
                }
                break;
        }
    }

    IEnumerator UpdateE()
    {
        yield return new WaitForSeconds( Mathf.Abs(Mathf.Sin(Time.time)) * 10);
        color = text.color;
        if (iteration != 6)
        {
            iteration += 1;
            Debug.Log(Mathf.Abs(Mathf.Sin(Time.time)) * speedDeltaColor);
        }
        else
            iteration = 1;

        StartCoroutine(UpdateE());
    }
}
