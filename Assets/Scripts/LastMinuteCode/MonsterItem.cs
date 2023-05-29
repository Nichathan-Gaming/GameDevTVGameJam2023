using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterItem : MonoBehaviour
{
    [SerializeField] float levelMultiplier = 1.1f;

    [SerializeField] Vector2 baseHealth;
    float health;
    [SerializeField] Vector2 baseDamage;
    float damage;
    [SerializeField] Vector2 baseSpeed;
    [SerializeField] float speed;

    Transform player;
    bool isDash;
    [SerializeField] Vector2 dashMinMax = new Vector2(3,6);
    float dashTime;
    float currentDashTime;
    Vector3 lastPosition;

    float attackSpeed = 0.5f;
    float currentAttackTimer = 0;
    bool attackingPlayer=false;
    PlayerController playerController;

    [SerializeField] Canvas canvas;

    bool hasStarted = false;

    [SerializeField] TMP_Text healthText;

    private void Update()
    {
        if (GameController.instance.isGameActive && hasStarted)
        {
            if (isDash)
            {
                transform.position = Vector3.MoveTowards(transform.position, lastPosition, speed * Time.deltaTime);
                currentDashTime -= Time.deltaTime;

                if (currentDashTime < 0)
                {
                    currentDashTime = dashTime;
                    lastPosition = player.position;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            if (attackingPlayer)
            {
                if (currentAttackTimer < attackSpeed) currentAttackTimer += Time.deltaTime;
                else
                {
                    currentAttackTimer = 0;
                    playerController.TakeDamage(damage);
                }
            }
        }
    }

    internal void SetMonster(float level, bool isDash, Transform player, Vector3 startPosition)
    {
        this.player = player;
        playerController = player.GetComponent<PlayerController>();
        canvas.worldCamera = Camera.main;
        lastPosition = player.position;
        transform.position = startPosition;
        gameObject.SetActive(true);
        this.isDash = isDash;
        dashTime = Random.Range(dashMinMax.x, dashMinMax.y);
        currentDashTime = dashTime;
        health = Random.Range(baseHealth.x, baseHealth.y) * (level * levelMultiplier);
        damage = Random.Range(baseDamage.x, baseDamage.y) * (level * levelMultiplier);
        speed = Random.Range(baseSpeed.x, baseSpeed.y) * (level * levelMultiplier) * (isDash ? 1.5f : 1);

        transform.localScale *= 1 + (level/10f);
        hasStarted = true;

        healthText.text = ""+ (int)health;
    }

    internal void TakeDamage(float damage)
    {
        health -= damage;
        healthText.text = "" + (int) health;

        if (health <= 0)
        {
            gameObject.SetActive(false);
            hasStarted = false;
            GameController.instance.UpdateKills();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player")) attackingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player")) attackingPlayer = false;
    }
}
