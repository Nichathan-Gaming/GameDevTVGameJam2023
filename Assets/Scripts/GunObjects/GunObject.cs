using UnityEngine;

/// <summary>
/// The object for the gun
/// </summary>
public class GunObject : MonoBehaviour
{
    /// <summary>
    /// The gun for this object
    /// </summary>
    [SerializeField] GunScriptableObject gunScriptableObject;

    /// <summary>
    /// The SpriteRenderer that holds the gun sprite
    /// </summary>
    [SerializeField] SpriteRenderer gunSpriteRenderer;

    [SerializeField] BulletObject[] bulletObjects;


}