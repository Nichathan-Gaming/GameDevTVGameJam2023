/// <summary>
/// A typical Battler, holds the base values needed for the Player and base Enemy scripts.
/// </summary>
[System.Serializable]
public abstract class Battler
{
    /// <summary>
    /// The unique name of the battler.
    /// </summary>
    public string name;

    /// <summary>
    /// The health of the battler. When this is 0 or below, the battler is dead.
    /// </summary>
    public int health;

    /// <summary>
    /// The protected movement speed of this battler.
    /// </summary>
    protected float _movementSpeed;

    /// <summary>
    /// The movement speed of the battler. Should not be 0 or less.
    /// </summary>
    public abstract float movementSpeed { get; set; }

    protected int _defense;

    /// <summary>
    /// The defense of the battler. Reduces the damage that they take.
    /// </summary>
    public abstract int defense {get; set;}

    /// <summary>
    /// The base constructor for JSON use
    /// </summary>
    public Battler()
    {

    }

    /// <summary>
    /// Create a new Battler object.
    /// </summary>
    /// <param name="name">The battler name</param>
    /// <param name="health">The battler initial health</param>
    /// <param name="movementSpeed">The battler movement speed</param>
    /// <param name="defense">The battler defense</param>
    public Battler(string name, int health, float movementSpeed, int defense)
    {
        this.name = name;
        this.health = health;
        _movementSpeed = movementSpeed;
        _defense = defense;
    }

    /// <summary>
    /// Reduces player health by damage/defense
    /// </summary>
    /// <param name="damage">The damage to deal to the player</param>
    /// <returns>health > 0 after calculations</returns>
    public virtual bool ReceiveDamage(float damage)
    {
        health -= (int) damage/defense;

        return health > 0;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    /// <summary>
    /// Converts this to JSON
    /// </summary>
    /// <returns>A JSON representation of this object</returns>
    public override string ToString()
    {
        return UnityEngine.JsonUtility.ToJson(this);
    }
}
