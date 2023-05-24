using UnityEngine;

[CreateAssetMenu(fileName ="Enemy", menuName ="Enemy", order =0)]
public class EnemyScriptableObject : ScriptableObject
{
    public Enemy enemy;

    public AudioClip enemyCry;
    public Sprite hideSprite;
    public Sprite defaultSprite;

    public BulletScriptableObject bulletScriptableObject;

    public bool hasPenetration;

    /// <summary>
    /// Gets the damage that this enemy deals
    /// </summary>
    /// <returns></returns>
    internal float GetDamage()
    {
        return enemy.damage;
    }

    /// <summary>
    /// Deals damage to the enemy
    /// </summary>
    /// <param name="damage">a value greater than 0</param>
    /// <returns>health greater than 0</returns>
    internal bool ReceiveDamage(float damage)
    {
        if (damage <= 0) throw new System.ArgumentOutOfRangeException($"Expected a value greater than 0 but received {damage}.");

        return enemy.ReceiveDamage(damage);
    }

    /// <summary>
    /// The time to wait between attacks
    /// </summary>
    /// <returns>enemy.attackCooldown</returns>
    internal float GetAttackInterval()
    {
        return enemy.attackCooldown;
    }

    /// <summary>
    /// The time the enemy can hide for
    /// </summary>
    /// <returns></returns>
    internal float GetHideTimer()
    {
        return enemy.hideTimer;
    }
}