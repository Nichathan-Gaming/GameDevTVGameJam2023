using System.ComponentModel;
using UnityEngine;
/// <summary>
/// 0 : Vatnvin - 5 (water wine) weak potion,
/// 1 : Barnvin - 50 (child wine) medium potion,
/// 2 : Ungrvin - 500 (young wine) medium-strong potion,
/// 3 : Fullrvin - 5,000 (full wine) strong potion
/// </summary>
[System.Serializable]
public enum VinType
{
    Vatnvin = 5, //(water wine) weak potion
    Barnvin = 50, //(child wine) medium potion
    Ungrvin = 500, //(young wine) medium-strong potion
    Fullrvin = 5000 //(full wine) strong potion
}

/// <summary>
/// A typical Vin heals a character. max : 999 
/// </summary>
[System.Serializable]
public class Vin
{
    /// <summary>
    /// The type of Vin.
    /// </summary>
    public VinType type;

    /// <summary>
    /// The cost of this Vin
    /// </summary>
    public int cost;
    
    /// <summary>
    /// The count of this Vin remaining.
    /// </summary>
    int _count = 0;

    /// <summary>
    /// Gets the _count and sets between 0 and 999.
    /// </summary>
    public int count {
        get
        {
            return _count;
        }
        set {
            _count = Mathf.Clamp(value, 0, maxVinCount);
        }
    }

    /// <summary>
    /// The player cannot have any more than 999 vin.
    /// </summary>
    public int maxVinCount { get { return 999; } }

    /// <summary>
    /// Default Vin constructor for JSON
    /// </summary>
    public Vin() { }

    /// <summary>
    /// Constructs a Vin of a certain type with a count of 0;
    /// </summary>
    /// <param name="type">The type of this. 0-3</param>
    public Vin(VinType type, int cost)
    {
        this.type = type;
        this.cost = cost;
    }

    /// <summary>
    /// Determines if the count can be added to this.
    /// </summary>
    /// <param name="count">The number to try to add.</param>
    /// <returns>True if the player can add count.</returns>
    public bool CanAdd(int count)
    {
        return (_count + count) <= maxVinCount;
    }

    /// <summary>
    /// Adds count to Vin
    /// </summary>
    /// <param name="count">the number of vin to add</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when receiving a value less than 1.</exception>
    public void Add(int count)
    {
        if (count < 1) throw new System.ArgumentOutOfRangeException($"Vin.Add expected a value greater than 0 but received {count}.");
        if (!CanAdd(count)) throw new System.ArgumentOutOfRangeException($"The count ({count}) is too high. Use CanAdd and ensure that this.count+count < maxVinCount.");
        _count += count;
    }

    /// <summary>
    /// Attempts to use a single item.
    /// </summary>
    /// <returns>(int) type</returns>
    public int Use()
    {
        if(count-->0) return (int) type;
        return 0;
    }

    /// <summary>
    /// Attempts to use (count) items.
    /// </summary>
    /// <param name="count">The number to use.</param>
    /// <returns>(int) type * count</returns>
    public int Use(int count)
    {
        if (this.count - count < 0) return 0;

        this.count -= count;
        return ((int)type) * count;
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
    /// Converts this to JSON.
    /// </summary>
    /// <returns>A JSON representation of this vin.</returns>
    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}
