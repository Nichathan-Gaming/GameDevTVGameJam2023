using UnityEngine;

public class PlayerController : MonoBehaviour
{
    internal Player player;
    const string PLAYER_PREFS = "PLAYER_PREFS";

    private void Start()
    {
        string playerData = PlayerPrefs.GetString(PLAYER_PREFS, "");

        if (string.IsNullOrWhiteSpace(playerData))
        {
            player = new Player("Bjornlief", 50, 5, 5, 50, 5, GameController.instance.guns[0], GameController.instance.armors[0], new Inventory());
        }
        else
        {
            player = JsonUtility.FromJson<Player>(playerData);
        }
    }

    private void Update()
    {
        
    }
}