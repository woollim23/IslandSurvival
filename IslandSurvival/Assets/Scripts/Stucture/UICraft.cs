using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;
using UnityEngine.InputSystem.XR;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using static UnityEngine.Rendering.PostProcessing.SubpixelMorphologicalAntialiasing;
using static UnityEditor.Progress;

public class UICraft : MonoBehaviour
{
    public ItemData item;
    public CraftSlot craftItemSlots;
    public CraftSlot[] craftSlots;
    public HaveItemSlot[] haveItemSlot;

    public GameObject CraftPanalCanvas;// 기본 베이스 UI
    public Transform CraftSlotsPanel;
    public Transform CraftNeedItemSlotsPanel;
    public Transform dropPosition;

    [Header("Select Item")]
    public TextMeshProUGUI selectedStructureName;
    public TextMeshProUGUI selectedStructureDescription;
    public TextMeshProUGUI selectedNeedItemName;
    public TextMeshProUGUI selectedNeedItemValue;
    public GameObject craftButton;
    public GameObject cancelButton;

    private PlayerController controller;
    private PlayerCondition condition;

    ItemData selectedItem;
    public int selectedItemIndex = 0;


    //TODO : 인벤토리에 넣어주기 // 일단 필드에 드랍됨
    //TODO : 인벤토리에서 가져오기

    private void Start()
    {  
        craftSlots = new CraftSlot[CraftSlotsPanel.childCount];
        for (int i = 0; i < craftSlots.Length; i++)
        {
            craftSlots[i] = CraftSlotsPanel.GetChild(i).GetComponent<CraftSlot>();
            craftSlots[i].index = i;
            craftSlots[i].craftInventory = this;            
        }
        ClearSelectedItemWindow();        
    }

    void ClearSelectedItemWindow()
    {
        selectedItem = null;

        selectedStructureName.text = string.Empty;
        selectedStructureDescription.text = string.Empty;
        selectedNeedItemName.text = string.Empty;
        selectedNeedItemValue.text = string.Empty;

        craftButton.SetActive(true);
        cancelButton.SetActive(true);
    }
    /// <summary>
    /// 선택한 제작물
    /// </summary>    
    public void SelectCraftItem(int index)
    {

        if (craftSlots[index].item == null) return;

        selectedItem = craftSlots[index].item;
        selectedItemIndex = index;

        selectedStructureName.text = selectedItem.displayName;
        selectedStructureDescription.text = selectedItem.description;

        selectedNeedItemName.text = string.Empty;
        selectedNeedItemValue.text = string.Empty;

        for (int i = 0; i < selectedItem.constructables.Length; i++)
        {
            selectedNeedItemName.text += selectedItem.constructables[i].type.ToString() + "\n";
            selectedNeedItemValue.text += selectedItem.constructables[i].Needvalue.ToString() + "\n";
        }

        craftButton.SetActive(selectedItem.type == ItemType.Constructable);
    }

    public void UpdateCraftUI()
    {
        if (item.type == ItemType.Constructable)
        {
            for (int i = 0; i < craftSlots.Length; i++)
            {
                if (craftSlots[i].item == null)
                {
                    craftSlots[i].Set();
                }
            }
        }

        if (item.type == ItemType.Resource)
        {
            for (int i = 0; i < haveItemSlot.Length; i++)
            {
                if (haveItemSlot[i].item != null)
                {
                    haveItemSlot[i].Set();
                }
                else
                {
                    haveItemSlot[i].item = null;
                }
            
            }
        }
    }


}