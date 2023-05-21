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
    /// What is the angle that bullets can shoot at 0-180
    /// </summary>
    public float bulletSpread;

    /// <summary>
    /// The sprite of this bullet.
    /// </summary>
    public Sprite bulletSprite;
}