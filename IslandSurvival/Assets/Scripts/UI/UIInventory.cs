using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private ItemData hamData;
    [SerializeField] private ItemData staminaPortionData;
    [SerializeField] private ItemData healthPortionData;

    public ItemSlot[] slots;
    public ItemSlot slot;
    public GameObject inventoryWindow;
    public Transform slotPanel;
    public Transform dropPosition;

    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject CraftButton;
    public GameObject constructButton;
    public GameObject dropButton;
    public GameObject cookButton;

    private PlayerController controller;
    private PlayerCondition condition;
    private Construct construct;

    ItemData selectedItem;
    int selectedItemIndex = 0;

    int curEquipIndex;

    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        controller.inventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount]; // 슬롯 판낼 자식의 갯수, 14개

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        ClearSelectedItemWindow();
    }


    void Update()
    {

    }

    void ClearSelectedItemWindow()
    {
        selectedItem = null;

        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);
        cookButton.SetActive(false);
        CraftButton.SetActive(false);
        constructButton.SetActive(false);
    }

    public void Toggle()
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        // 현재 슬롯에 아이템이 스택 가능한지
        if (data.canStack)
        {
            ItemSlot itemslot = GetItemStack(data);
            if (itemslot != null)
            {
                itemslot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }

        // 현재 슬롯이 꽉차있다면,
        // 다른 비어 있는 슬롯 가져온다.
        ItemSlot emptySlot = GetEmptySlot();

        // 있다면, 슬롯에 아이템 추가
        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        // 자리가 아예 없다면, 버려야지
        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null;
    }

    public void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
        // 360도 각도 중 랜덤으로 회전하여 떨어짐
    }

    public void UpdateUI()
    {
        // 슬롯 배열을 순회
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                // 데이터가 있다면 세팅해라    
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // 슬롯에 아이템이 있고 양이 최대양보다 적다면
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                // 슬롯을 반환
                return slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                // 비어있는 슬롯 반환
                return slots[i];
            }
        }
        return null;
    }


    public void SelectItem(int index)
    {

        if (slots[index].item == null) return;

        selectedItem = slots[index].item;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.displayName;
        selectedItemDescription.text = selectedItem.description;

        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            selectedItemStatName.text += selectedItem.consumables[i].type.ToString() + "\n";
            selectedItemStatValue.text += selectedItem.consumables[i].value.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.type == ItemType.Equipable && !slots[index].equipped);
        unequipButton.SetActive(selectedItem.type == ItemType.Equipable && slots[index].equipped);
        CraftButton.SetActive(selectedItem.type == ItemType.Resource);
        constructButton.SetActive(selectedItem.type == ItemType.Constructable);
        dropButton.SetActive(selectedItem != null);

        cookButton.SetActive(selectedItem.displayName == "물" || selectedItem.displayName == "스테이크" || selectedItem.displayName == "사과");

    }

    public void OnUseButton()
    {
        if (selectedItem.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Doping:
                        condition.Doping(selectedItem.consumables[i].value, selectedItem.consumables[i].duration);
                        break;
                    case ConsumableType.Thirst:
                        condition.DrinkWater(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Stamina:
                        condition.UpStamina(selectedItem.consumables[i].value);
                        break;

                }
            }
            RemoveSelectedItem();
        }
    }

    public void OnCookButton()
    {
        Debug.Log(slots[selectedItemIndex].item.displayName);
        if(selectedItem.displayName == "물")
        {
            slots[selectedItemIndex].item = staminaPortionData;
        }
        else if(selectedItem.displayName == "사과")
        {
            slots[selectedItemIndex].item = healthPortionData;
            Debug.Log(slots[selectedItemIndex].item.displayName);
        }
        else if(selectedItem.displayName == "스테이크")
        {
            slots[selectedItemIndex].item = hamData;
        }
        UpdateUI();
        ClearSelectedItemWindow();
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem);
        RemoveSelectedItem();
    }

    void RemoveSelectedItem()
    {
        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }

    public void OnEquipButton()
    {
        if (slots[curEquipIndex].equipped)
        {
            UnEquip(curEquipIndex);
        }

        slots[selectedItemIndex].equipped = true;
        curEquipIndex = selectedItemIndex;
        CharacterManager.Instance.Player.equip.EquipNew(selectedItem);
        UpdateUI();
        SelectItem(selectedItemIndex);
    }

    void UnEquip(int index)
    {
        slots[index].equipped = false;
        CharacterManager.Instance.Player.equip.UnEquip();
        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);
        }
    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
    }

    public void OnCraftButton() //제작하기
    {

        if (selectedItem.type == ItemType.Resource)
        {
            for (int i = 0; i < selectedItem.constructables.Length; i++)
            {
                switch (selectedItem.constructables[i].type)
                {
                    case ConstructableType.Log:
                        construct.UseResource(selectedItem.constructables[i].Needvalue, selectedItem.constructables[i].setDuration);
                        break;
                    case ConstructableType.Stone:
                        construct.UseResource(selectedItem.constructables[i].Needvalue, selectedItem.constructables[i].setDuration);
                        break;
                }
            }
            RemoveSelectedItem();            
        }
    }

    public void OnConstructButton() //건설하기
    {
        inventoryWindow.SetActive(false);

        if (selectedItem.type == ItemType.Constructable)
        {
            for (int i = 0; i < selectedItem.constructables.Length; i++)
            {
                switch (selectedItem.constructables[i].type)
                {
                    case ConstructableType.Log:
                        construct.UseResource(selectedItem.constructables[i].Needvalue, selectedItem.constructables[i].setDuration);
                        break;
                    case ConstructableType.Stone:
                        construct.UseResource(selectedItem.constructables[i].Needvalue, selectedItem.constructables[i].setDuration);
                        break;
                }
            }
            RemoveSelectedItem();
        }
    }
}
