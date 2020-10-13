using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpAlpha : MonoBehaviour
{
    internal bool colorBool = false;
    SpriteRenderer spriteRend;
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (colorBool)
            spriteRend.color = Color.Lerp(spriteRend.color, new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, 0), 5f * Time.fixedDeltaTime);
    }
}
