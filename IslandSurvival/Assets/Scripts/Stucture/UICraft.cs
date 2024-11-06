using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class UICraft : MonoBehaviour
{
    public ItemData craftitem;
    public UIInventory inventory;    
    public CraftSlot[] craftSlots;
    public HaveItemSlot[] haveItemSlots;

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
        CraftPanalCanvas.SetActive(false);
        ClearSelectedItemWindow();

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
    }
    private void Update()
    {
        UpdateCraft();
        //UpdateCraftUI();
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
    

    public void UpdateCraft()
    {
        for (int i = 0; i < craftSlots.Length; i++)
        {
            if (craftSlots[i].item != null)
            {                   
                craftSlots[i].Set();
            }            
        }
    }
    //public void UpdateCraftUI()
    //{
    //    if(inventory.slots == null)
    //        return;
    //    for (int i = 0; i < inventory.slots.Length; i++)
    //    {
    //        if (inventory.slots[i].item != null && inventory.slots[i].item.type == ItemType.Resource)
    //        {
    //            for (int j = 0; j < haveItemSlots.Length; j++)
    //            { 
    //                if (haveItemSlots[j].item == null)
    //                {
    //                    haveItemSlots[j].item = inventory.slots[i].item;
    //                    haveItemSlots[j].icon.sprite = inventory.slots[i].icon.sprite;
    //                    haveItemSlots[j].quatityText.text = inventory.slots[i].quatityText.text;
    //                }
    //                else
    //                {
    //                    haveItemSlots[j].item = null;
    //                }                
    //            }
    //        }
    //    }
    //}

    /// <summary>
    /// 크래프트캔버스 내 제작하기버튼
    /// </summary>
    public void OnStartCraftButton()
    {
        //ItemData data = CharacterManager.Instance.Player.itemData;
        DropStructure(selectedItem);
        CraftPanalCanvas.SetActive(false);
        CharacterManager.Instance.Player.controller.ToggleCursor();
    }

    /// <summary>
    /// 제작하기완료시 자동으로 필드에 드롭
    /// </summary>    
    void DropStructure(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }
    //TODO : 크래프트인벤에서 사용아이템삭제
    //TODO : 인벤에서 건축아이템삭제        
}