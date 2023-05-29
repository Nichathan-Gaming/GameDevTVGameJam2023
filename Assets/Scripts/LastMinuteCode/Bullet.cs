using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Vector2 baseDamage;
    float damage;
    [SerializeField] Vector2 baseSpeed;
    [SerializeField] float speed;

    Vector3 startPos;

    [SerializeField] float maxDistance=25;

    private void Update()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, startPos, -speed * Time.deltaTime);
        transform.position = new Vector3(newPos.x, newPos.y, 0);

        if (Vector3.Distance(transform.position, startPos) > maxDistance) gameObject.SetActive(false);
    }

    internal void SetBullet(Vector3 startPos)
    {
        gameObject.SetActive(true);
        this.startPos = startPos;

        damage = Random.Range(baseDamage.x, baseDamage.y) * (GameController.instance.gameTimer/30f * 1.1f);
        speed = Random.Range(baseSpeed.x, baseSpeed.y) * (GameController.instance.gameTimer / 30f * 1.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            collision.GetComponent<MonsterItem>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}