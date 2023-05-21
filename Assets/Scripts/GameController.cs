using UnityEngine;

public class GameController : MonoBehaviour
{
    internal static GameController instance;

    [SerializeField] internal GunScriptableObject[] guns;
    [SerializeField] internal ArmorScriptableObject[] armors;

    

    private void Awake()
    {
        //convert to DontDestroyOnLoad if we make this multi-scene
        instance = this;
    }
}
