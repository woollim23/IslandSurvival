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

    public GameObject CraftPanalCanvas;// �⺻ ���̽� UI
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


    //TODO : �κ��丮�� �־��ֱ� // �ϴ� �ʵ忡 �����
    //TODO : �κ��丮���� ��������

    private void Awake()
    {
        //item = inventory.slot.item; //�κ��丮�� ���� �����۵�����SO�� �־���
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
    private void Update()
    {
        UpdateCraftUI();
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
    /// ������ ���۹�
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
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].item != null && inventory.slots[i].item.type == ItemType.Resource)
            {
                for (int j = 0; j < haveItemSlots.Length; j++)
                { 
                    if (haveItemSlots[j].item == null)
                    {
                        haveItemSlots[j].item = inventory.slots[i].item;
                        haveItemSlots[j].icon.sprite = inventory.slots[i].icon.sprite;
                        haveItemSlots[j].quatityText.text = inventory.slots[i].quatityText.text;
                    }
                    else
                    {
                        haveItemSlots[j].item = null;
                    }                
                }
            }
        }
    }

    public void OnStartCraftButton() //ũ����Ʈĵ���� �� �����ϱ��ư
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        DropStructure(selectedItem);

    }

    /// <summary>
    /// �����ϱ�Ϸ�� �ڵ����� �ʵ忡 ���
    /// </summary>    
    void DropStructure(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    //TODO : ũ����Ʈ�κ����� �������ۻ���
    //TODO : �κ����� ��������ۻ���
    

    
}