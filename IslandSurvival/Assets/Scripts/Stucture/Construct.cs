using System;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Craft
{
    public string craftName; // �̸�
    public GameObject RealStructurePrefab; // ���� ��ġ �� ������
    public GameObject previewStructure; // �̸� ���� ������
}

public class Construct : MonoBehaviour
{
    bool isConstructMode;//���������� üũ

    public GameObject inventoryWindow;
    public PlayerController controller;
    public Transform dropPosition;
    public GameObject constructPrefab;
    public GameObject CraftPanalCanvas;// �⺻ ���̽� UI
    public UICraft craftInventory;


    private bool isActivated = false;  // CraftManual UI Ȱ�� ����    


    [SerializeField]
    private Craft[] craft;  //CratfSlot�� �ִ� ���Ե�. 

    private GameObject previewStructure; // �̸� ���� �������� ���� ����
    private GameObject structurePrefab; // ���� ������ �������� ���� ���� 

    [SerializeField]
    private Transform playerTransform;  // �÷��̾� ��ġ

    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float range;

    //���۰��� ������ : ItemType.Resource
    //�Ǽ����� ������ : ItemType.Constructable
    // ItemObject ��ũ��Ʈ�� IInteractable ��� , Interaction��ũ��Ʈ���� 
    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        dropPosition = CharacterManager.Instance.Player.dropPosition;
        CharacterManager.Instance.Player.controller.onCancelStruct += CancelStruct; // ��� �̺�Ʈ ���
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && previewStructure == null)
            Window();

        if (previewStructure != null)
        {
            PreviewPositionUpdate();
        }        
    }
    private void Window()
    {
        if (!isActivated)
            OnCraftButton();
        else
            OnCancleButton();
    }

    /// <summary>
    /// �� ���۹�ư
    /// </summary>
    public void OnCraftButton()
    {
        isActivated = true;
        CraftPanalCanvas.SetActive(true);
        inventoryWindow.SetActive(false);
    }
    public void OnStartCraftButton() //ũ����Ʈĵ���� �� �����ϱ��ư
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        DropStructure(data);

    }

    /// <summary>
    /// �����ϱ�Ϸ�� �ڵ����� �ʵ忡 ���
    /// </summary>    
    void DropStructure(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void OnCancleButton() //ũ����Ʈĵ���� �� ����ϱ��ư
    {
        isActivated = false;
        CraftPanalCanvas.SetActive(false);
        inventoryWindow.SetActive(true);
    }
    public void OnBuildButton() //UIInventory �� �Ǽ��ϱ��ư
    {         
        for (int i = 0; i < craft.Length; i++)
        {
            previewStructure = Instantiate(craft[i].previewStructure, playerTransform.position + playerTransform.forward, Quaternion.identity);
            structurePrefab = craft[i].RealStructurePrefab;

        }
        CraftPanalCanvas.SetActive(false);        
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
    /// ������� Fkey�Է½� ȣ���Լ�
    /// </summary>
    private void CancelStruct()
    {
        if (previewStructure != null)
            Destroy(previewStructure);

        ResetPreview();
        CraftPanalCanvas.SetActive(false);
    }

    private void ResetPreview()
    {
        isActivated = false;        
        previewStructure = null;
        structurePrefab = null;
    }
}


