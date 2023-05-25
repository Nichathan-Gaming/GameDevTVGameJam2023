using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    internal UnityEvent<int> onSilfrChange;

    int _silfr = 10000;

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
            if(value < 0 || value > 999999) throw new System.ArgumentOutOfRangeException($"Expected a value greater than 0 and less than 1,000,000 but received {value}.");
            _silfr = value;
            if (onSilfrChange != null) onSilfrChange.Invoke(value);
        }
    }

    /// <summary>
    /// The head that the player has in their bag.
    /// </summary>
    public HeadType head = HeadType.Empty;

    //NOTE: the price given to vin is arbitrary right now.

    /// <summary>
    /// The number of Vatnvin that the player has in their bag.
    /// </summary>
    public Vin vatnvin = new Vin(VinType.Vatnvin, 5);
    /// <summary>
    /// The number of Barnvin that the player has in their bag.
    /// </summary>
    public Vin barnvin = new Vin(VinType.Barnvin, 50);
    /// <summary>
    /// The number of Ungrvin that the player has in their bag.
    /// </summary>
    public Vin ungrvin = new Vin(VinType.Ungrvin, 500);
    /// <summary>
    /// The number of Fullrvin that the player has in their bag.
    /// </summary>
    public Vin fullrvin = new Vin(VinType.Fullrvin, 5000);

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
    /// Constructs the Inventory with an onSilfrChange
    /// </summary>
    /// <param name="onSilfrChange"></param>
    public Inventory(UnityEvent<int> onSilfrChange)
    {
        this.onSilfrChange = onSilfrChange;
    }

    /// <summary>
    /// Determines if the Player can spend the allotted amount of money.
    /// </summary>
    /// <param name="cost">The cost of the purchase</param>
    /// <returns>cost less than or equal to silfr</returns>
    public bool CanBuy(int cost)
    {
        return cost <= silfr;
    }

    /// <summary>
    /// Attempts to buy count or vinType
    /// </summary>
    /// <param name="count">The amount to buy</param>
    /// <param name="vinType">The type to buy</param>
    /// <returns>True if count of vintype is bought</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Only throws if VinType is changed without updating here</exception>
    public bool BuyVin(int count, VinType vinType)
    {
        int cost;

        switch (vinType)
        {
            case VinType.Vatnvin:
                cost = vatnvin.cost * count;
                if (vatnvin.CanAdd(count) && CanBuy(cost))
                {
                    vatnvin.Add(count);
                    silfr -= cost;
                    return true;
                }
                return false;
            case VinType.Barnvin:
                cost = barnvin.cost * count;
                if (barnvin.CanAdd(count) && CanBuy(cost))
                {
                    barnvin.Add(count);
                    silfr -= cost;
                    return true;
                }
                return false;
            case VinType.Ungrvin:
                cost = ungrvin.cost * count;
                if (ungrvin.CanAdd(count) && CanBuy(cost))
                {
                    ungrvin.Add(count);
                    silfr -= cost;
                    return true;
                }
                return false;
            case VinType.Fullrvin:
                cost = fullrvin.cost * count;
                if (fullrvin.CanAdd(count) && CanBuy(cost))
                {
                    fullrvin.Add(count);
                    silfr -= cost;
                    return true;
                }
                return false;
            default:
                throw new System.ArgumentOutOfRangeException($"{nameof(vinType)} is not found.");
        }
    }

    /// <summary>
    /// Gets the cost of a single Vin
    /// </summary>
    /// <param name="vinType">The type of vin to get</param>
    /// <returns>vin.cost</returns>
    public int GetVinCost(VinType vinType)
    {
        return GetVinCost(vinType, 1);
    }

    /// <summary>
    /// Gets the cost of count vinType
    /// </summary>
    /// <param name="vinType">The type of vin to get</param>
    /// <param name="count">The number to multiple cost by</param>
    /// <returns>vin.cost * count</returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public int GetVinCost(VinType vinType, int count)
    {
        switch (vinType)
        {
            case VinType.Vatnvin:
                return vatnvin.cost * count;
            case VinType.Barnvin:
                return barnvin.cost * count;
            case VinType.Ungrvin:
                return ungrvin.cost * count;
            case VinType.Fullrvin:
                return fullrvin.cost * count;
            default:
                throw new System.ArgumentOutOfRangeException($"{nameof(vinType)} is not found.");
        }
    }

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