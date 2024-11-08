using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HaveItemSlot : MonoBehaviour
{
    public ItemData item;
    public UIInventory inventory;
    public UICraft craftInventory;
    public HaveItemSlot haveItemSlot;

    public TextMeshProUGUI quatityText;
    public int quantity;

    public Image icon;
    public int index;


    private void Awake()
    {

    }

    private void OnEnable()
    {

    }

    /// <summary>
    /// ���� �κ��丮�� �ڿ� �����۸� �����ֱ�
    /// </summary>
    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quatityText.text = quantity > 1 ? quantity.ToString() : string.Empty;
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
    }
    
}
