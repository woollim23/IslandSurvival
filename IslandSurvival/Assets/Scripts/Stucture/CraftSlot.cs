using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.PostProcessing.SubpixelMorphologicalAntialiasing;

public class CraftSlot : MonoBehaviour
{
    public ItemData item; //SO넣어주기
    public UIInventory inventory;
    public UICraft craftInventory;    

    public Button button;
    public Image icon;
    private Outline outline;

    public int index;
    public bool equipped;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = equipped;
    }

    public void Set()
    {        
        icon.sprite = item.icon;   

        if (outline != null)
        {
            outline.enabled = equipped;
        }
    }
    public void Clear()
    {
        item = null;
    }


    public void OnClickCraftButton()
    {
        craftInventory.SelectCraftItem(index);
    }
}