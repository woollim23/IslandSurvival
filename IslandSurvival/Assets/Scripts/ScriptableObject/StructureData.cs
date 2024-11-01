using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Constructable
{ 
    MiniHouse,
    BigHouse
}

[Serializable]
public class ItemDataConstructable
{
    public Constructable type;
    public float value;
    public float duration;
}


[CreateAssetMenu(fileName = "Structure", menuName = "NewStructure")]
public class StructureData : ScriptableObject
{
    [Header("StructureInfo")]    
    public string structureName;
    public string structureDescription;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;    

    [Header("Constructable")]
    public ItemDataConstructable[] Constructables;
    
}
