using UnityEngine;
/// <summary>
/// 0 : Magazine, 1 : Energy
/// </summary>
[System.Serializable]
public enum AmmunitionType
{
    Magazine,
    Energy
}

/// <summary>
/// A typical gun (also called Hraordrepa) controls the damage that a Player deals.
/// </summary>
[System.Serializable]
public class Gun
{
    /// <summary>
    /// The name of this gun.
    /// </summary>
    public string name;

    /// <summary>
    /// The damage that this gun deals when a bullet hits an enemy.
    /// </summary>
    public float damage;

    /// <summary>
    /// The rate at which ammunition is expended.
    /// </summary>
    public float fireRate;

    /// <summary>
    /// The size of the gun magazine/battery
    /// </summary>
    public int capacity;

    /// <summary>
    /// The speed at which ammunition is replenished. 
    /// </summary>
    public float reloadSpeed;

    /// <summary>
    /// The type of ammunition that this gun uses
    /// </summary>
    public AmmunitionType type;

    /// <summary>
    /// The value that a players movement speed is increased or decreased.
    /// </summary>
    public float movementSpeed;

    /// <summary>
    /// The value that a players turn speed is increased or decreased.
    /// </summary>
    public float turnSpeed;

    /// <summary>
    /// Constructs a default Gun class.
    /// </summary>
    public Gun() { }

    /// <summary>
    /// Constructs a gun class with the default parameters.
    /// </summary>
    /// <param name="name">The name of the gun.</param>
    /// <param name="damage">The damage that each bullet does.</param>
    /// <param name="fireRate">The rate at which this gun dispenses bullets.</param>
    /// <param name="capacity">The maximum number of bullets that this gun can hold at once.</param>
    /// <param name="currentAmmunition">The current number of bullets left in this gun.</param>
    /// <param name="reloadSpeed">The speed at which this gun can reload.</param>
    /// <param name="type">The type of gun that is.</param>
    /// <param name="movementSpeed">The players augmented movement speed</param>
    /// <param name="turnSpeed">The players augmented turn speed</param>
    public Gun(string name, float damage, float fireRate, int capacity, float reloadSpeed, AmmunitionType type, float movementSpeed, float turnSpeed)
    {
        this.name = name;
        this.damage = damage;
        this.fireRate = fireRate;
        this.capacity = capacity;
        this.reloadSpeed = reloadSpeed;
        this.type = type;
        this.movementSpeed = movementSpeed;
        this.turnSpeed = turnSpeed;
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
    /// Creates a JSON representation of this.
    /// </summary>
    /// <returns>A JSON representation of this.</returns>
    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}
