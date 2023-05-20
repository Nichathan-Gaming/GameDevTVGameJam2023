using UnityEngine;

public class GameController : MonoBehaviour
{
    internal static GameController instance;

    [SerializeField] internal Gun[] guns;
    [SerializeField] internal Armor[] armors;

    private void Awake()
    {
        //convert to DontDestroyOnLoad if we make this multi-scene
        instance = this;
    }
}