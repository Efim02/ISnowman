using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeltaTextLine : MonoBehaviour
{

    void Start()
    {
        string t = "Максимальный результат:";
        GetComponent<Text>().text = $"{t}\t--------------------------------" + transform.position.y + "--------------------------------";
    }
}
