using UnityEngine;

/// <summary>
/// A typical BulletItem is instantiated when the gun shoots and saved in a list.
/// </summary>
public class BulletObject : MonoBehaviour 
{
    [SerializeField] BulletScriptableObject bulletScriptableObject;

    Vector2 startPosition;

    [SerializeField] SpriteRenderer spriteRenderer;

    /// <summary>
    /// The speed that the bullet travels.
    /// </summary>
    internal float bulletSpeed;

    /// <summary>
    /// The distance that the bullet can travel from the center of the gun.
    /// </summary>
    internal float bulletDistance;

    internal float bulletDamage;

    bool canDamagePlayer;
    bool canDamageMultiple;

    private void Update()
    {
        if(Vector2.Distance(startPosition, transform.position) > bulletScriptableObject.bulletDistance) gameObject.SetActive(false);

        transform.position = -Vector3.MoveTowards(transform.position, startPosition, bulletSpeed * Time.deltaTime);
    }

    internal void SetBullet(float bulletDamage, bool canDamageMultiple, bool canDamagePlayer, Vector2 startPosition, Vector2 aimPosition, BulletScriptableObject bulletScriptableObject)
    {
        gameObject.SetActive(true);
        this.canDamageMultiple = canDamageMultiple;
        this.canDamagePlayer = canDamagePlayer;
        this.bulletDamage = bulletDamage;
        this.startPosition = startPosition;
        this.bulletScriptableObject = bulletScriptableObject;
        transform.position = Vector3.MoveTowards(startPosition, aimPosition, bulletSpeed * Time.deltaTime);
        spriteRenderer.sprite = bulletScriptableObject.bulletSprite;
        transform.localScale = bulletScriptableObject.bulletSize;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (canDamagePlayer)
            {
                PlayerController.instance.TakeDamage(bulletDamage);
                gameObject.SetActive(canDamageMultiple);
            }
        }
        else
        {
            if (!canDamagePlayer)
            {
                EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
                if(!(enemyController == null || enemyController.isHiding))
                {
                    enemyController.ReceiveDamage(bulletDamage);
                    gameObject.SetActive(canDamageMultiple);
                }
            }
        }
    }
}