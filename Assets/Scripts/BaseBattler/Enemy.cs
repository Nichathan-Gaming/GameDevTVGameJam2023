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
    /// The movement speed of the enemy
    /// </summary>
    public override float movementSpeed { get; set; }

    /// <summary>
    /// the defense of the enemy
    /// </summary>
    public override int defense { get; set; }

    /// <summary>
    /// The damage dealt per attack
    /// </summary>
    public float damage;

    /// <summary>
    /// The time between attacks
    /// </summary>
    public float attackSpeed;

    /// <summary>
    /// The time it takes between attacks. The monster will attack when this is 0 and move when it is greater than 0
    /// </summary>
    public float attackCooldown;

    /// <summary>
    /// The type of battle mechanics for this enemy.
    /// </summary>
    public BattleType type;
}
