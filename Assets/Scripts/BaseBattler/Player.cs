using UnityEngine;
/// <summary>
/// A typical Player extends from Battler and has extra values for tracking the status of a player.
/// </summary>
[System.Serializable]
public class Player : Battler
{
    /// <summary>
    /// A players base max health.
    /// </summary>
    int _maxHealth;

    /// <summary>
    /// The max health of the player. Cannot be less than health. 
    /// Absolute max is 9,999
    /// </summary>
    public int maxHealth
    {
        get
        {
            return _maxHealth + equippedArmor.health;
        }
        set
        {
            _maxHealth = Mathf.Clamp(value, health, 9999);
        }
    }

    /// <summary>
    /// The base speed at which the player can turn
    /// </summary>
    float _turnSpeed;

    /// <summary>
    /// The speed at which the player can turn.
    /// </summary>
    public float turnSpeed
    {
        get
        {
            return _turnSpeed + equippedArmor.turnSpeed + equippedGun.turnSpeed;
        }
        set
        {
            _turnSpeed = Mathf.Clamp(value, 0, 999);
        }
    }

    /// <summary>
    /// The current equipped gun.
    /// (A list of all owned guns is found in inventory)
    /// </summary>
    public Gun equippedGun = new Gun();

    /// <summary>
    /// The current equipped armor.
    /// (A list of all owned armors is found in inventory)
    /// </summary>
    public Armor equippedArmor = new Armor();

    /// <summary>
    /// The players inventory.
    /// </summary>
    public Inventory inventory = new Inventory();

    /// <summary>
    /// The movement speed of this player.
    /// </summary>
    public override float movementSpeed { 
        get
        {
            return _movementSpeed + equippedArmor.movementSpeed + equippedGun.movementSpeed;
        }
        set 
        { 
            _movementSpeed = Mathf.Clamp(value, 0, 999);
        }
    }

    /// <summary>
    /// The defense of the player
    /// </summary>
    public override int defense 
    { 
        get
        {
            return _defense + equippedArmor.defense;
        }
        set
        {
            _defense = Mathf.Clamp(value, 0, 999);
        }
    }

    /// <summary>
    /// Default constructor for JSON.
    /// </summary>
    public Player() { }

    /// <summary>
    /// Constructs a Player object with the given values.
    /// </summary>
    /// <param name="name">The Players name</param>
    /// <param name="health">The amount of health</param>
    /// <param name="maxHealth">The maximum health</param>
    /// <param name="movementSpeed">The speed at which the player moves</param>
    /// <param name="turnSpeed">The speed at which the player turns</param>
    /// <param name="defense">The players defense</param>
    /// <param name="equippedGun">The gun that the player has equipped</param>
    /// <param name="equippedArmor">The armor that the player has equipped</param>
    /// <param name="inventory">The players current inventory</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the maxHealth is less than health.</exception>
    public Player(string name, int health, float movementSpeed, int defense, int maxHealth, float turnSpeed, Gun equippedGun, Armor equippedArmor, Inventory inventory) 
        : base(name, health, movementSpeed, defense)
    {
        if (maxHealth < health) throw new System.ArgumentOutOfRangeException($"maxHealth ({maxHealth}) cannot be less that health ({health}).");
        this.maxHealth = maxHealth;
        _turnSpeed = turnSpeed;
        this.equippedGun = equippedGun;
        this.equippedArmor = equippedArmor;
        this.inventory = inventory;
    }

    /// <summary>
    /// Reduces the players health by damage / (defense + armor)
    /// </summary>
    /// <param name="damage">The damage to deal : must be &gt 0</param>
    /// <returns>health > 0 after calculations</returns>
    public override bool ReceiveDamage(float damage)
    {
        health -= (int) damage / defense;

        return health > 0;
    }

    public void UseVin(VinType vinType)
    {
        int healAmount = inventory.UseVin(vinType);

        health = Mathf.Clamp(health + healAmount, 0, maxHealth);
    }

    public void UseVin(VinType vinType, int count)
    {
        int healAmount = inventory.UseVin(vinType, count);

        health = Mathf.Clamp(health + healAmount, 0, maxHealth);
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
    /// Creates a JSON representation.
    /// </summary>
    /// <returns>A JSON representation of this.</returns>
    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}
