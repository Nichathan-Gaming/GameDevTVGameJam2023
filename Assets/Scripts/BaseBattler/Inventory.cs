using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 0 : Ulfrhoufuo,
/// 1 : Fenrirhoufuo,
/// 2 : Helhoufuo,
/// 3 : Jormungandhoufuo,
/// 4 : Empty - The player does not currently have a head.
/// </summary>
[System.Serializable]
public enum HeadType
{
    Ulfrhoufuo,
    Fenrirhoufuo,
    Helhoufuo,
    Jormungandhoufuo,
    Empty
}

/// <summary>
/// A typical Inventory holds an array of various potions, armors and weapons. The players silver and a single head.
/// </summary>
[System.Serializable]
public class Inventory
{
    int _silfr = 0;

    /// <summary>
    /// The players silver. Cannot go below 0 or above 999,999.
    /// </summary>
    public int silfr
    {
        get
        {
            return _silfr;
        }
        set
        {
            _silfr = Mathf.Clamp(value, 0, 999999);
        }
    }

    /// <summary>
    /// The head that the player has in their bag.
    /// </summary>
    public HeadType head = HeadType.Empty;

    /// <summary>
    /// The number of Vatnvin that the player has in their bag.
    /// </summary>
    public Vin vatnvin = new Vin(VinType.Vatnvin);
    /// <summary>
    /// The number of Barnvin that the player has in their bag.
    /// </summary>
    public Vin barnvin = new Vin(VinType.Barnvin);
    /// <summary>
    /// The number of Ungrvin that the player has in their bag.
    /// </summary>
    public Vin ungrvin = new Vin(VinType.Ungrvin);
    /// <summary>
    /// The number of Fullrvin that the player has in their bag.
    /// </summary>
    public Vin fullrvin = new Vin(VinType.Fullrvin);

    /// <summary>
    /// The list of armors that the player has unlocked.
    /// </summary>
    public List<Armor> armors = new List<Armor>();
    /// <summary>
    /// The list of guns that the player has unlocked.
    /// </summary>
    public List<Gun> guns = new List<Gun>();

    /// <summary>
    /// A default Constructor for JSON.
    /// </summary>
    public Inventory() { }

    /// <summary>
    /// Uses the vinType one time.
    /// </summary>
    /// <param name="vinType">The Vin to use.</param>
    /// <returns>(int) vinType</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when vinType is not tracked in the switch.</exception>
    public int UseVin(VinType vinType)
    {
        switch(vinType)
        {
            case VinType.Vatnvin:
                return vatnvin.Use();
            case VinType.Barnvin:
                return barnvin.Use();
            case VinType.Ungrvin:
                return ungrvin.Use();
            case VinType.Fullrvin:
                return fullrvin.Use();
            default:
                throw new System.ArgumentOutOfRangeException($"{nameof(vinType)} is not found.");
        }
    }

    /// <summary>
    /// Uses the vinType count times.
    /// </summary>
    /// <param name="vinType">The Vin to use.</param>
    /// <param name="count">The number to use.</param>
    /// <returns>(int) vinType * count</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when vinType is not tracked in the switch.</exception>
    public int UseVin(VinType vinType, int count)
    {
        switch (vinType)
        {
            case VinType.Vatnvin:
                return vatnvin.Use(count);
            case VinType.Barnvin:
                return barnvin.Use(count);
            case VinType.Ungrvin:
                return ungrvin.Use(count);
            case VinType.Fullrvin:
                return fullrvin.Use(count);
            default:
                throw new System.ArgumentOutOfRangeException($"{nameof(vinType)} is not found.");
        }
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