using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICraft : MonoBehaviour
{
    //UIInventory ����

    //public ItemSlot[] slots;
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

    public string craftName; // �̸�
    public GameObject RealStructurePrefab; // ���� ��ġ �� ������
    public GameObject previewStructure; // �̸� ���� ������
     
}
