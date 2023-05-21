using UnityEngine;

/// <summary>
/// A typical BulletItem is instantiated when the gun shoots and saved in a list.
/// </summary>
public class BulletObject : MonoBehaviour 
{
    BulletScriptableObject bulletScriptableObject;

    Vector2 startPosition;

    /// <summary>
    /// The speed that the bullet travels.
    /// </summary>
    internal float bulletSpeed;

    /// <summary>
    /// The distance that the bullet can travel from the center of the gun.
    /// </summary>
    internal float bulletDistance;

    private void Update()
    {
        if(Vector2.Distance(startPosition, transform.position) > bulletScriptableObject.bulletDistance)
        {
            TurnOffBullet();
        }
    }

    internal void SetBullet(Vector2 startPosition, BulletScriptableObject bulletScriptableObject)
    {
        this.startPosition = startPosition;
        this.bulletScriptableObject = bulletScriptableObject;
        transform.position = startPosition;

        throw new System.NotImplementedException("Set the images of the bullet and other variables in BulletObject.");
    }

    void TurnOffBullet()
    {
        throw new System.NotImplementedException("TurnOffBullet of BulletObject has not been implemented yet.");
    }
}