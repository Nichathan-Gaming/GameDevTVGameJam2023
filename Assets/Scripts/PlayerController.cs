using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    internal static PlayerController instance;

    internal Player player;
    const string PLAYER_PREFS = "PLAYER_PREFS";

    Rigidbody2D rb;

    [Header("The players movementSpeed")]
    [SerializeField] float speed=500;

    [Header("On Value Change Events")]
    /// <summary>
    /// Called whenever the player's silfr changes
    /// </summary>
    [SerializeField] UnityEvent<int> onSilfrChange;

    /// <summary>
    /// Called whenever the player's health changes
    /// </summary>
    [SerializeField] UnityEvent<int> onHealthChange;

    #region Dash Region
    bool canDash=true;
    [Header("Dash Variables")]
    [SerializeField] float dashPower=5;
    [SerializeField] float dashCooldown=1;
    [SerializeField] float dashTime=0.5f;
    float currentDashTime=0;
    float movementMultiplier = 1;
    #endregion

    [SerializeField] float maxBullets = 60;

    [SerializeField] float shotTimer=0.1f;
    float currentShotTimer=0;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletInstantiationLocation;
    List<Bullet> bullets = new List<Bullet>();

    [SerializeField] Transform gunTransform;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        string playerData = PlayerPrefs.GetString(PLAYER_PREFS, "");

        if (string.IsNullOrWhiteSpace(playerData))
        {
            //player = new Player("Bjornlief", 50, 5, 5, 50, 5, GameController.instance.guns[0].gun, GameController.instance.armors[0].armor, new Inventory());
            player = new Player("Bjornlief", 50, 5, 5, 50, null, null, onHealthChange, onSilfrChange);
        }
        else
        {
            player = JsonUtility.FromJson<Player>(playerData);
        }
    }

    private void Update()
    {
        if (GameController.instance.isGameActive)
        {
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if(movement != Vector2.zero)
            {
                rb.velocity = movement * movementMultiplier * speed*Time.deltaTime;

                if (currentDashTime < 0) movementMultiplier = 1;
                else currentDashTime -= Time.deltaTime;
            }
            else rb.velocity = Vector2.zero;

            if (Input.GetAxisRaw("Fire2") != 0) if (canDash) StartCoroutine(Dash());

            if(Input.GetAxisRaw("Fire1") != 0)
            {
                if(currentShotTimer > 0)
                {
                    currentShotTimer -= Time.deltaTime;
                }
                else
                {
                    currentShotTimer = shotTimer;
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        AudioManager.PlaySFX(SoundEffects.Shot);

        foreach (var bullet in bullets)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.transform.position = gunTransform.position;
                bullet.SetBullet(transform.position);
                return;
            }
        }

        if(bullets.Count < maxBullets)
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletInstantiationLocation).GetComponent<Bullet>();

            bullet.transform.position = gunTransform.position;
            bullet.SetBullet(transform.position);

            bullets.Add(bullet);
        }
    }

    IEnumerator Dash()
    {
        if(canDash)
        {
            canDash=false;

            movementMultiplier = dashPower;
            currentDashTime = dashTime;

            yield return new WaitForSeconds(dashCooldown);

            canDash = true;
        }
    }

    /// <summary>
    /// deal damage to the player, if the player is dead after, set the player death in the game controller
    /// </summary>
    /// <param name="damage">must be greater than 0</param>
    internal void TakeDamage(float damage)
    {
        if (!player.ReceiveDamage(damage)) GameController.instance.SetPlayerDeath();
    }
}