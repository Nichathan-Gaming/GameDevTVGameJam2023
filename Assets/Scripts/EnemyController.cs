using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] string limiterName;

    [SerializeField] EnemyScriptableObject enemyScriptableObject;

    [SerializeField] Vector3 wanderDirection;

    bool isPlayerInRange;

    [SerializeField] Vector2 wanderTimerMinMax;
    float wanderTimer;
    float movementSpeed;

    [SerializeField] EnemyDetectionController attackController;
    [SerializeField] EnemyDetectionController detectionController;

    private void Start()
    {
        ResetWander();
    }

    private void Update()
    {
        if (detectionController.isPlayerInRadius) HandlePlayerInRange();
        else HandleWander();
    }

    /// <summary>
    /// Battle Types 
    /// RoamAndIgnore,
    /// FollowAndAttack,
    /// ChargeAndFlee,
    /// HideAndAttack,
    /// RoamAndShoot
    /// </summary>
    internal void HandlePlayerInRange()
    {
        switch (enemyScriptableObject.enemy.type)
        {
            case BattleType.RoamAndIgnore:
                HandleWander();
                break;
            case BattleType.FollowAndAttack:
                if (attackController.isPlayerInRadius)
                {
                    //can we attack again?
                }

                //move towards player
                break;
            case BattleType.ChargeAndFlee:
                //First, check if we can attack again, then charge, then flee until we can attack again.
                if (attackController.isPlayerInRadius)
                {
                    //can we attack again?
                }
                else
                {

                }
                break;
            case BattleType.HideAndAttack:
                //this is special, we need to change the players sprite renderer for a set amount of time
                break;
            case BattleType.RoamAndShoot:
                if (attackController.isPlayerInRadius)
                {
                    //and attack timer < 0 attack
                }

                HandleWander();
                break;
        }
    }

    /// <summary>
    /// The enemy moves in a random direction for a random amount of time.
    /// </summary>
    void HandleWander()
    {
        transform.position += wanderDirection * Time.deltaTime * movementSpeed;
        wanderTimer -= Time.deltaTime;

        if (wanderTimer < 0) ResetWander();
    }

    /// <summary>
    /// Resets the wander values
    /// </summary>
    void ResetWander()
    {
        ResetWanderTimer();
        ResetWanderDirection();
        movementSpeed = enemyScriptableObject.enemy.movementSpeed;
    }

    /// <summary>
    /// Sets the wander direction to a random vector 2 between -1 and 1
    /// </summary>
    void ResetWanderDirection()
    {
        wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    /// <summary>
    /// Resets the wanderTimer 
    /// </summary>
    void ResetWanderTimer()
    {
        wanderTimer = Random.Range(wanderTimerMinMax.x, wanderTimerMinMax.y);
    }

    /// <summary>
    /// The enemy has gone outside of its limiter
    /// </summary>
    /// <param name="collision">The limiter that was exited</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals(limiterName))
        {
            movementSpeed = enemyScriptableObject.enemy.movementSpeed;
            wanderDirection *= -1;
            ResetWanderTimer();
        }
    }
}
