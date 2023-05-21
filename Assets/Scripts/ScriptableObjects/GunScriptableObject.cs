
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Equipment/Gun", order = 0)]
public class GunScriptableObject : ScriptableObject
{
    public Gun gun;

    public Sprite gunSprite;

    public int cost;

    public BulletScriptableObject bulletScriptableObject;
}