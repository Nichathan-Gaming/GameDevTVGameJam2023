using UnityEngine;

public class EnemyDetectionController : MonoBehaviour
{
    internal bool isPlayerInRadius;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isPlayerInRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isPlayerInRadius = false;
        }
    }
}