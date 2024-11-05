using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICraft : MonoBehaviour
{
    //UIInventory 참고

    //public ItemSlot[] slots;
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

    public string craftName; // 이름
    public GameObject RealStructurePrefab; // 실제 설치 될 프리팹
    public GameObject previewStructure; // 미리 보기 프리팹
     
}
