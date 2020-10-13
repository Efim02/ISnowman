using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_ScrollRaiting : MonoBehaviour
{
    [SerializeField]
    ScrollRect scroll;
    [SerializeField]
    RectTransform element;

    public void UpdateList(List<ClassSaveDataUser> list)
    {
        ClearList();
        UI_ScrollElementRaiting logicElement;
        int counter = 0;
        foreach(var u in list)
        {
            counter += 1;
            var instance = Instantiate(element.gameObject) as GameObject;
            instance.transform.SetParent(scroll.content, false);
            logicElement = instance.GetComponent<UI_ScrollElementRaiting>();
            logicElement.T_Number.text = counter.ToString();
            logicElement.T_NickName.text = u.nickName;
            logicElement.T_Score.text = u.score.ToString();

            //Debug.Log(u.nickName + " - Nickname, " + u.id + "  - id,  " + u.score + "   u.score,  "+"  ADD ELEMENT IN LIST");
        }
    }
    public void ClearList()
    {
        foreach(Transform tr in scroll.content)
        {
            Destroy(tr.gameObject);
        }
    }
}
