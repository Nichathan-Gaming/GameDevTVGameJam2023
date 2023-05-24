using UnityEngine;

public class EnemyDetectionController : MonoBehaviour
{
    internal bool isPlayerInRadius;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isPlayerInRadius = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isPlayerInRadius = false;
        }
    }
}