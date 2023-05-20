/// <summary>
/// 0 : Hraorbrynja (Quick Armor),
/// 1 : Blautrbrynja (Flexible Armor),
/// 2 : Skjoldrbrynja (Shield Armor)
/// </summary>
[System.Serializable]
public enum ArmorType
{
    Hraorbrynja,//speed
    Blautrbrynja,//turn
    Skjoldrbrynja//shield
}

/// <summary>
/// A typical Armor augments a players stats.
/// </summary>
[System.Serializable]
public class Armor
{
    /// <summary>
    /// The name of this armor.
    /// </summary>
    public string name;

    /// <summary>
    /// The health offset of this armor.
    /// </summary>
    public int health;

    /// <summary>
    /// The defense offset of this armor.
    /// </summary>
    public int defense;

    /// <summary>
    /// The movement speed offset of this armor.
    /// </summary>
    public float movementSpeed;

    /// <summary>
    /// The turn speed offset of this armor.
    /// </summary>
    public float turnSpeed;

    /// <summary>
    /// The type of armor.
    /// </summary>
    public ArmorType type;

    /// <summary>
    /// A default constructor for JSON.
    /// </summary>
    public Armor()
    {

    }

    /// <summary>
    /// Creates a new Armor object.
    /// </summary>
    /// <param name="name">The name of this armor.</param>
    /// <param name="health">The health offset of this armor.</param>
    /// <param name="defense">The defense offset of this armor.</param>
    /// <param name="movementSpeed">The movement speed offset of this armor.</param>
    /// <param name="turnSpeed">The turn speed; offset of this armor.</param>
    /// <param name="type">The type of weapon that this is.</param>
    public Armor(string name, int health, int defense, float movementSpeed, float turnSpeed, ArmorType type)
    {
        this.name = name;
        this.health = health;
        this.defense = defense;
        this.movementSpeed = movementSpeed;
        this.turnSpeed = turnSpeed;
        this.type = type;
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
