using System;
using UnityEngine;

public enum ItemType
{
    Consumable, //섭취가능
    Equipable, //장착가능
    Resource //제작가능자원
}

public enum ConsumableType
{
    Health,
    Hunger,
    Stemina,
    Doping
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
    public float duration;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;
}
