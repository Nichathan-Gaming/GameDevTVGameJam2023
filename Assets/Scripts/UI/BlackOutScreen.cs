using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOutScreen : MonoBehaviour
{
    [SerializeField] Image imageComponent;
    [SerializeField] Color tempColor;

    [SerializeField] float fadingSpeed;
    [SerializeField] bool blackout;

    void Start()
    {
        tempColor = imageComponent.color;
        fadingSpeed = 0.01f;
        blackout = false;
    }

    void Update()
    {
        if (blackout)
        {
            BlackIn();
        }
        else
        {
            BlackAway();
        }
    }

    void BlackIn()
    {
        if (tempColor.a < 1f)
        {
            tempColor.a += fadingSpeed;
            imageComponent.color = tempColor;
        }
    }

    void BlackAway()
    {
        if (tempColor.a > 0f)
        {
            tempColor.a -= fadingSpeed;
            imageComponent.color = tempColor;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        blackout = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        blackout = false;
    }
}
