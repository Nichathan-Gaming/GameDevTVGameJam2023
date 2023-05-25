using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class FadingSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Color tmpColor;

    [SerializeField] float fadingSpeed;
    [SerializeField] float targetAlpha;

    [SerializeField] bool fadingIn;

    void Start()
    {
        tmpColor = sprite.color;
        fadingSpeed = 0.01f;
        fadingIn = false;
    }

    void Update()
    {
        if (fadingIn)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    void FadeIn()
    {
        if (tmpColor.a > targetAlpha)
        {
            tmpColor.a -= fadingSpeed;
            sprite.color = tmpColor;
        }
    }

    void FadeOut()
    {
        if (tmpColor.a < 1f)
        {
            tmpColor.a += fadingSpeed;
            sprite.color = tmpColor;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        fadingIn = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        fadingIn = false;
    }

}
