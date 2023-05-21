using UnityEngine;

[CreateAssetMenu(fileName ="Armor", menuName ="Equipment/Armor", order = 0)]
public class ArmorScriptableObject : ScriptableObject
{
    public Armor armor;

    public Sprite armorSprite;

    public int cost;
}