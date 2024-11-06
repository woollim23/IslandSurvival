using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//[System.Serializable]
//public class Craft
//{
//    public string craftName; // �̸�
//    public GameObject RealStructurePrefab; // ���� ��ġ �� ������
//    public GameObject previewStructure; // �̸� ���� ������
//}

public class Construct : MonoBehaviour
{
    bool isConstructMode;//���������� üũ

    public PlayerController controller;
    public Transform dropPosition;
    
    public GameObject inventoryWindow;
    public GameObject CraftPanalCanvas;// �⺻ ���̽� UI
    public GameObject CancelInfoTxt;

    public UICraft craftInventory;
    public UIInventory inventory;

    private bool isActivated = false;  // CraftManual UI Ȱ�� ����

    private GameObject previewStructure; // �̸� ���� �������� ���� ����
    private GameObject structurePrefab; // ���� ������ �������� ���� ���� 

    [SerializeField]
    private Transform playerTransform;  // �÷��̾� ��ġ

    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float range;
    private float NeedItemIndex;
    private float NeedDuration;
    private ItemData selectedItem;

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        dropPosition = CharacterManager.Instance.Player.dropPosition;
        CharacterManager.Instance.Player.controller.onCancelStruct += CancelStruct; // ��� �̺�Ʈ ���
        //selectedItem = inventory.selectedItem;
    }

    void Update()
    {
        if (previewStructure != null)
        {
            PreviewPositionUpdate();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            CancelInfoTxt.SetActive(false);
            Build();
        }       
    }

    /// <summary>
    /// Inventory���� ���۹�ư
    /// </summary>
    public void OnCraftButton()
    {        
        CraftPanalCanvas.SetActive(true);
        inventoryWindow.SetActive(false);        
    }    

    public void OnCancleButton() //ũ����Ʈĵ���� ����ϱ��ư
    {        
        CraftPanalCanvas.SetActive(false);
        inventoryWindow.SetActive(true);        
    }
    public void OnBuildButton() //Inventory �Ǽ��ϱ��ư
    {
        CharacterManager.Instance.Player.controller.ToggleCursor();

        SelectStructure(inventory.selectedItem);
        inventoryWindow.SetActive(false);
        CancelInfoTxt.SetActive(true);        
    }
    
    void SelectStructure(ItemData data)
    {
        previewStructure = Instantiate(data.previewStructure, playerTransform.position + playerTransform.forward, Quaternion.identity);
        structurePrefab = data.RealStructurePrefab;
    }

    /// <summary>
    /// �̸����������� ��ġ������
    /// </summary>
    private void PreviewPositionUpdate()
    {
        Debug.DrawRay(playerTransform.position, transform.forward*range, Color.red);

        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform != null)
            {
                Vector3 location = hitInfo.point;
                previewStructure.transform.position = location;
            }
        }
    }

    private void Build()
    {
        if (previewStructure && previewStructure.GetComponent<PreviewObject>().isBuildable()) //���尡 ���� �ϴٸ�
        {
            Instantiate(structurePrefab, hitInfo.point, Quaternion.identity);
            Destroy(previewStructure);
            ResetPreview();
        }
    }
    private void Building()
    {
        isConstructMode = true;

        // UI���
        //�κ�ũ
        Instantiate(structurePrefab, hitInfo.point, Quaternion.identity);
        Destroy(previewStructure);
        ResetPreview();

    }

    /// <summary>
    /// �������� �Һ��� �ǹ��� ������ ���
    /// </summary>    
    public void UseResource(float NeedValue, float duration)
    {
         NeedItemIndex = NeedValue;
         NeedDuration = duration;
    }

    /// <summary>
    /// ������� Fkey�Է½� ȣ���Լ�
    /// </summary>
    private void CancelStruct()
    {
        if (previewStructure != null)
            Destroy(previewStructure);

        ResetPreview();
        CraftPanalCanvas.SetActive(false);
        CancelInfoTxt.SetActive(false);
    }

    private void ResetPreview()
    {                
        previewStructure = null;
        structurePrefab = null;
    }
}


