using System.Collections.Generic;
using UnityEngine;

public class RisingText : MonoBehaviour
{
    [SerializeField] RisingTextItem[] risingTextItems;

    Queue<string> messages = new Queue<string>();

    internal void DisplayMessage(string message)
    {
        foreach (var item in risingTextItems)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.DisplayMessage(message, transform.position, MessageTurnedOff);
                return;
            }
        }

        messages.Enqueue(message);
    }

    void MessageTurnedOff(RisingTextItem item)
    {
        if(messages.Count > 0)
        {
            item.DisplayMessage(messages.Dequeue(), transform.position, MessageTurnedOff);
        }
    }
}
