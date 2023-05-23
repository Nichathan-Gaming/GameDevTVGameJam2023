using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyScriptableObject enemyScriptableObject;

    [SerializeField] float enemySpeed;
    [SerializeField] Vector3 wanderDirection;

    bool isPlayerInRange;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (isPlayerInRange) HandlePlayerInRange();
        else transform.position += wanderDirection * Time.deltaTime * enemySpeed;
    }

    /// <summary>
    /// Battle Types 
    /// RoamAndIgnore,
    /// FollowAndAttack,
    /// ChargeAndFlee,
    /// HideAndAttack,
    /// RoamAndShoot
    /// </summary>
    void HandlePlayerInRange()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print(collision.tag);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}