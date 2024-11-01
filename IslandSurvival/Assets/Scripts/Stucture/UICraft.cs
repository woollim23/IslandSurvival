using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICraft : MonoBehaviour
{
    //UIInventory Âü°í

    public ItemSlot[] slots;
    public GameObject CraftPanalCanvas;
    public Transform slotPanel;
    public Transform dropPosition;

    [Header("Select Item")]
    public TextMeshProUGUI selectedStructureName;
    public TextMeshProUGUI selectedStructureDescription;
    public TextMeshProUGUI selectedNeedItemName;
    public TextMeshProUGUI selectedNeedItemValue;
    public GameObject craftButton;
    public GameObject cancelButton;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
