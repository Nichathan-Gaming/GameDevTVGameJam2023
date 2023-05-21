using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    internal Player player;
    const string PLAYER_PREFS = "PLAYER_PREFS";

    Rigidbody2D rb;
    Animator anim;

    #region Dash Region
    bool canDash;
    [SerializeField] float dashPower=5;
    [SerializeField] float dashCooldown=1;
    float movementMultiplier = 1;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        string playerData = PlayerPrefs.GetString(PLAYER_PREFS, "");

        if (string.IsNullOrWhiteSpace(playerData))
        {
            player = new Player("Bjornlief", 50, 5, 5, 50, 5, GameController.instance.guns[0].gun, GameController.instance.armors[0].armor, new Inventory());
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
            rb.AddForce(movement * movementMultiplier);

            if(movementMultiplier!=1) movementMultiplier = 1;
        }
        else rb.velocity = Vector2.zero;

        anim.SetFloat("MoveY", rb.velocity.y);
        anim.SetFloat("MoveX", rb.velocity.x);

        if (Input.GetAxisRaw("Fire2") != 0) if (canDash) StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        if(canDash)
        {
            canDash=false;

            movementMultiplier = dashPower;

            yield return new WaitForSeconds(dashCooldown);

            canDash = true;
        }
    }
}