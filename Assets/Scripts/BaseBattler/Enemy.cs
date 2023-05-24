using UnityEngine;

public enum BattleType
{
    RoamAndIgnore,
    FollowAndAttack,
    ChargeAndFlee,
    HideAndAttack,
    RoamAndShoot
}

[System.Serializable]
public class Enemy : Battler
{
    /// <summary>
    /// How heavy is this enemy? mass 70 is not push-able
    /// </summary>
    public float mass;

    /// <summary>
    /// The movement speed of the enemy
    /// </summary>
    public override float movementSpeed { 
        get
        {
            return Random.Range(movementSpeedMinMax.x, movementSpeedMinMax.y);
        }
        set
        {

        }
    }

    public Vector2 _movementSpeedMinMax;
    public Vector2 movementSpeedMinMax {
        get
        {
            return _movementSpeedMinMax;
        }
        set
        {
            if (value == null || value.y < value.x) throw new System.ArgumentOutOfRangeException($"Expected a non null value with y greater than or equal to x but instead received : {value}.");
            _movementSpeedMinMax = value;
        } 
    }

    /// <summary>
    /// the defense of the enemy
    /// </summary>
    public override int defense { get; set; }

    /// <summary>
    /// The damage dealt per attack
    /// </summary>
    public float damage;

    /// <summary>
    /// If the enemy's battle type is HideAndAttack, they will hide for this many seconds
    /// </summary>
    public float hideTimer;

    /// <summary>
    /// The time it takes between attacks. The monster will attack when this is 0 and move when it is greater than 0
    /// </summary>
    public float attackCooldown;

    /// <summary>
    /// The range at which the enemy will become hostile towards the player.
    /// </summary>
    public float detectionRange;

    /// <summary>
    /// The range at which the enemy can send an attack to the player.
    /// </summary>
    public float attackRange;

    /// <summary>
    /// The maximum number of bullets that this enemy can have active at once.
    /// </summary>
    public int maxBullets;

    /// <summary>
    /// The type of battle mechanics for this enemy.
    /// </summary>
    public BattleType type;
}
