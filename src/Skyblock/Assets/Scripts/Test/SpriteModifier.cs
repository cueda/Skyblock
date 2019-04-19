using UnityEngine;
using System;
using System.Collections;

public class SpriteModifier : MonoBehaviour
{
    private SpriteRenderer spRenderer;

    void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    public void MoveSpriteLeft()
    {
        transform.Translate(transform.right * -1);
    }

    public void MoveSpriteRight()
    {
        transform.Translate(transform.right);
    }

    public void TintSpriteBlue()
    {
        spRenderer.color = Color.blue;
    }

    public void TintSpriteGreen()
    {
        spRenderer.color = Color.green;
    }
}
