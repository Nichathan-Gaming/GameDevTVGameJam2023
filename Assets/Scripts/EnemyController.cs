using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] string limiterName;

    [SerializeField] EnemyScriptableObject enemyScriptableObject;

    [SerializeField] Vector3 wanderDirection;

    bool canAttack=true;

    [SerializeField] Vector2 wanderTimerMinMax;
    float wanderTimer;
    float movementSpeed;

    #region hiding Variables
    internal bool isHiding
    {
        get;
        private set;
    }
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    Coroutine hideCoroutine;
    #endregion

    [SerializeField] EnemyDetectionController attackController;
    [SerializeField] CircleCollider2D attackCollider;
    [SerializeField] EnemyDetectionController detectionController;
    [SerializeField] CircleCollider2D detectionCollider;

    #region Bullet Variables
    [SerializeField] GameObject bulletObjectPrefab;
    [SerializeField] Transform bulletObjectInstantiationLocation;
    List<BulletObject> bulletObjects = new List<BulletObject>();
    #endregion

    bool returnToCenter = false;
    Vector3 centerPosition;

    private void Start()
    {
        attackCollider.radius = enemyScriptableObject.enemy.attackRange;
        detectionCollider.radius = enemyScriptableObject.enemy.detectionRange;
        GetComponent<Rigidbody2D>().mass = enemyScriptableObject.enemy.mass;
        ResetWander();
    }

    private void Update()
    {
        if (returnToCenter && !attackController.isPlayerInRadius) HandleReturnToCenter();
        else if (detectionController.isPlayerInRadius) HandlePlayerInRange();
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
                    if (canAttack)
                        StartCoroutine(AttackTimer());

                //move towards player
                MoveTowardsPlayer(1);
                break;
            case BattleType.ChargeAndFlee:
                //First, check if we can attack again, then charge, then flee until we can attack again.
                if (canAttack)//charge and attack
                {
                    if (attackController.isPlayerInRadius)
                        StartCoroutine(AttackTimer());
                    else MoveTowardsPlayer(2);
                }
                else//flee
                    MoveTowardsPlayer(-2);
                break;
            case BattleType.HideAndAttack:
                //this is special, we need to change the players sprite renderer for a set amount of time
                if (isHiding)
                {
                    MoveTowardsPlayer(1);
                    if(attackController.isPlayerInRadius && canAttack)
                    {
                        if(hideCoroutine != null) StopCoroutine(hideCoroutine);
                        DisplayHide(false);
                        StartCoroutine(AttackTimer());
                    }
                }
                else
                {
                    HandleWander();
                    if (canAttack)
                    {
                        hideCoroutine = StartCoroutine(HideTimer());
                    }
                }
                break;
            case BattleType.RoamAndShoot:
                if (attackController.isPlayerInRadius)
                {
                    StartCoroutine(HandleShoot());
                }

                HandleWander();
                break;
        }
    }

    /// <summary>
    /// if can attack, try to shoot
    /// 
    /// bullets cannot exceed enemy max and turning a bullet on takes precedence over instantiation
    /// </summary>
    /// <returns></returns>
    IEnumerator HandleShoot()
    {
        if (canAttack)
        {
            canAttack = false;

            bool noShot = true;
            foreach(var bullet in bulletObjects)
            {
                if (!bullet.gameObject.activeInHierarchy)
                {
                    bullet.SetBullet(
                        enemyScriptableObject.GetDamage(),
                        enemyScriptableObject.hasPenetration,
                        true,
                        transform.position,
                        PlayerController.instance.transform.position,
                        enemyScriptableObject.bulletScriptableObject
                    );
                    noShot = false;
                }
            }

            if (noShot)
            {
                var bullet = Instantiate(bulletObjectPrefab, bulletObjectInstantiationLocation).GetComponent<BulletObject>();
                bullet.SetBullet(
                    enemyScriptableObject.GetDamage(),
                    enemyScriptableObject.hasPenetration,
                    true,
                    transform.position,
                    PlayerController.instance.transform.position,
                    enemyScriptableObject.bulletScriptableObject
                );
                bulletObjects.Add(bullet);
            }

            yield return new WaitForSeconds(enemyScriptableObject.GetAttackInterval());

            canAttack = true;
        }
    }

    /// <summary>
    /// Toggles the enemy's hiding
    /// </summary>
    void DisplayHide(bool isHiding)
    {
        this.isHiding = isHiding;
        if (isHiding)
        {
            animator.enabled = false;
            spriteRenderer.sprite = enemyScriptableObject.hideSprite;
        }
        else
        {
            animator.enabled = true;
            spriteRenderer.sprite = enemyScriptableObject.defaultSprite;
        }
    }

    /// <summary>
    /// Controls the display of the enemy's hiding
    /// </summary>
    /// <returns></returns>
    IEnumerator HideTimer()
    {
        if (!isHiding)
        {
            DisplayHide(true);
            yield return new WaitForSeconds(enemyScriptableObject.GetHideTimer());
            DisplayHide(false);
        }
    }

    /// <summary>
    /// Attacks then waits the cooldown time before turning on canAttack again.
    /// </summary>
    /// <returns>time to wait.</returns>
    IEnumerator AttackTimer()
    {
        if (canAttack)
        {
            PlayerController.instance.TakeDamage(enemyScriptableObject.GetDamage());
            canAttack = false;
            yield return new WaitForSeconds(enemyScriptableObject.GetAttackInterval());
            canAttack = true;
        }
    }

    /// <summary>
    /// Moves towards the player
    /// </summary>
    /// <param name="multiplier">*2 if charging, *-2 if fleeing, *1 otherwise</param>
    void MoveTowardsPlayer(float multiplier)
    {
        transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, movementSpeed*Time.deltaTime);
        wanderTimer -= Time.deltaTime;

        if (wanderTimer < 0) ResetWander();
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
    /// Moves the enemy towards the center of their limiter
    /// </summary>
    void HandleReturnToCenter()
    {
        transform.position = Vector3.MoveTowards(transform.position, centerPosition, Time.deltaTime * movementSpeed*2);
        wanderTimer -= Time.deltaTime;

        if (wanderTimer < 0 || Vector3.Distance(transform.position, centerPosition) < 0.1f)
        {
            ResetWander();
            returnToCenter = false;
        }
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
    /// Deals damage to this enemy
    /// 
    /// -Do not call if this enemy is hiding
    /// </summary>
    /// <param name="damage">The damage to deal</param>
    /// <returns>health greater than 0 after calculations</returns>
    /// <exception cref="System.AccessViolationException">if enemy is hiding</exception>
    internal bool ReceiveDamage(float damage)
    {
        if (isHiding) throw new System.AccessViolationException($"Enemy {name} cannot receive damage while hiding. This can mess with non-penetrable bullets.");

        return enemyScriptableObject.ReceiveDamage(damage);
    }

    /// <summary>
    /// The enemy has gone outside of its limiter
    /// </summary>
    /// <param name="collision">The limiter that was exited</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals(limiterName))
        {
            centerPosition = collision.transform.position;
            movementSpeed = enemyScriptableObject.enemy.movementSpeed;
            ResetWanderTimer();
            returnToCenter = true;
        }
    }
}
