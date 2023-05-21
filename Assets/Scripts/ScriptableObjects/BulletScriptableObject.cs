using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Bullet", order = 0)]
public class BulletScriptableObject : ScriptableObject
{
    /// <summary>
    /// The size of the bullet. (Probably for scale)
    /// </summary>
    public Vector2 bulletSize;

    /// <summary>
    /// The speed that the bullet travels at.
    /// </summary>
    public float bulletSpeed;

    /// <summary>
    /// The distance that the bullet can travel from the origin.
    /// </summary>
    public float bulletDistance;

    /// <summary>
    /// The sprite of this bullet.
    /// </summary>
    public Sprite bulletSprite;
}