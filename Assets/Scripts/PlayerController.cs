using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    internal Player player;
    const string PLAYER_PREFS = "PLAYER_PREFS";

    Rigidbody2D rb;

    [SerializeField] float speed=500;

    #region Dash Region
    bool canDash=true;
    [SerializeField] float dashPower=5;
    [SerializeField] float dashCooldown=1;
    [SerializeField] float dashTime=0.5f;
    float currentDashTime=0;
    float movementMultiplier = 1;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        string playerData = PlayerPrefs.GetString(PLAYER_PREFS, "");

        if (string.IsNullOrWhiteSpace(playerData))
        {
            //player = new Player("Bjornlief", 50, 5, 5, 50, 5, GameController.instance.guns[0].gun, GameController.instance.armors[0].armor, new Inventory());
            player = new Player("Bjornlief", 50, 5, 5, 50, 5, null, null, new Inventory());
        }
        else
        {
            player = JsonUtility.FromJson<Player>(playerData);
        }
    }

    private void Update()
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
}