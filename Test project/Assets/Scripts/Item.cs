using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Ammo,
    Armor
}

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;

    
}

//[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    public int damage;
    public float range;
}

//[CreateAssetMenu(fileName = "New Ammo", menuName = "Inventory/Ammo")]
public class Ammo : Item
{
    public int amount;
}

//[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor")]
public class Armor : Item
{
    public int defense;
}

