using TMPro;
using UnityEngine;
using static UnityEditor.Progress;


public class UICraft : MonoBehaviour
{
    public ItemData item;
    public UIInventory inventory;
    public CraftSlot craftItemSlots;
    public CraftSlot[] craftSlots;
    public HaveItemSlot[] haveItemSlots;

    public GameObject CraftPanalCanvas;// 기본 베이스 UI
    public Transform CraftSlotsPanel;
    public Transform CraftNeedItemSlotsPanel;

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

    private void Awake()
    {
        //item = inventory.slot.item; //인벤토리와 같은 아이템데이터SO를 넣어줌
    }
    private void Start()
    {
        CraftPanalCanvas.SetActive(false);

        craftSlots = new CraftSlot[CraftSlotsPanel.childCount];
        for (int i = 0; i < craftSlots.Length; i++)
        {
            craftSlots[i] = CraftSlotsPanel.GetChild(i).GetComponent<CraftSlot>();
            craftSlots[i].index = i;
            craftSlots[i].craftInventory = this;
        }

        //haveItemSlots = new HaveItemSlot[CraftSlotsPanel.childCount];
        //for (int i = 0; i < haveItemSlots.Length; i++)
        //{
        //    haveItemSlots[i] = CraftSlotsPanel.GetChild(i).GetComponent<HaveItemSlot>();
        //    // haveItemSlots[i].index = i;
        //    //haveItemSlots[i].craftInventory = this;
        //}
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
    }

    public void GetInventoryItem()
    {
        for (int i = 0; i < haveItemSlots.Length; i++)
        {
            haveItemSlots[i].item = inventory.slots[i].item;
            haveItemSlots[i].icon = inventory.slots[i].icon;
            haveItemSlots[i].quatityText.text = inventory.slots[i].quatityText.text;
        }
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

        if (inventory.slot.item.type == ItemType.Resource)
        {
            for (int i = 0; i < haveItemSlots.Length; i++)
            {
                if (haveItemSlots[i].item != null)
                {
                    GetInventoryItem();
                }
                else
                {
                    haveItemSlots[i].item = null;
                }

            }
        }
    }


}