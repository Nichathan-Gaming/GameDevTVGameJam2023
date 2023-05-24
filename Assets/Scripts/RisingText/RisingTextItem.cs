using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RisingTextItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] float risingSpeed;
    [SerializeField] float fadeMultiplier;

    Color startColor;
    Color endColor;
    float fadeTimer;

    UnityAction<RisingTextItem> onItemEnd;

    private void Start()
    {
        startColor = text.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position += Vector3.up;
        fadeTimer += Time.deltaTime * fadeMultiplier;
        text.color = Color.Lerp(startColor, endColor, fadeTimer);

        if (fadeTimer > 1)
        {
            gameObject.SetActive(false);
            if(onItemEnd != null) onItemEnd(this);
        }
    }

    internal void DisplayMessage(string message, Vector3 startPosition, UnityAction<RisingTextItem> onItemEnd)
    {
        this.onItemEnd = onItemEnd;
        transform.position = startPosition;
        text.color = startColor;
        fadeTimer = 0;
        gameObject.SetActive(true);
        text.text = message;
    }
}