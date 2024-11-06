using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Construct : MonoBehaviour
{
    bool isConstructMode;//���������� üũ
    ItemData data;

    public PlayerController controller;
    public Transform dropPosition;

    public GameObject inventoryWindow;
    public GameObject craftPanalCanvas;// �⺻ ���̽� UI
    public GameObject cancelInfoTxt;
    public GameObject buildUI;
    public Image buildUIImage;

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
    private float needItemIndex;
    private float needDuration = 5f;
    private float curDuration = 0f;
    private ItemData selectedItem;

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        dropPosition = CharacterManager.Instance.Player.dropPosition;
        CharacterManager.Instance.Player.controller.onCancelStruct += CancelStruct; // ��� �̺�Ʈ ���
        //needDuration = data.setDuration;
    }

    void Update()
    {
        if (previewStructure != null)
        {
            PreviewPositionUpdate();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            cancelInfoTxt.SetActive(false);

            buildUI.SetActive(true);
            BuildUISet();
            StartCoroutine("Build");            
        }
    }

    /// <summary>
    /// Inventory���� ���۹�ư
    /// </summary>
    public void OnCraftButton()
    {
        craftPanalCanvas.SetActive(true);
        inventoryWindow.SetActive(false);
    }

    /// <summary>
    /// ũ����Ʈĵ���� ����ϱ��ư
    /// </summary>
    public void OnCancleButton()
    {
        craftPanalCanvas.SetActive(false);
        inventoryWindow.SetActive(true);
    }
    /// <summary>
    /// �κ��丮 �Ǽ��ϱ��ư
    /// </summary>
    public void OnBuildButton()
    {
        CharacterManager.Instance.Player.controller.ToggleCursor();

        SelectStructure(inventory.selectedItem); //���ð��๰����
        inventoryWindow.SetActive(false);
        cancelInfoTxt.SetActive(true);
    }

    /// <summary>
    /// ���ð��๰ �����͸� ����ִ� ������,���๰ ���� �޼ҵ�
    /// </summary>    
    void SelectStructure(ItemData data)
    {
        previewStructure = Instantiate(data.previewStructure, playerTransform.position + playerTransform.forward, Quaternion.identity);
        structurePrefab = data.RealStructurePrefab;
    }

    /// <summary>
    /// �̸����������� ��ġ�� �������� ������Ʈ
    /// </summary>
    private void PreviewPositionUpdate()
    {
        Debug.DrawRay(playerTransform.position, transform.forward * range, Color.red);

        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform != null)
            {
                Vector3 location = hitInfo.point;
                previewStructure.transform.position = location;
            }
        }
    }

    /// <summary>
    /// ���� ���콺 Ŭ���� �Ǽ����� (Duration�� ��ŭ ������) 
    /// </summary>
    public IEnumerator Build()
    {
        yield return new WaitForSeconds(needDuration);

        if (previewStructure && previewStructure.GetComponent<PreviewObject>().isBuildable()) //���尡 ���� �ϴٸ�
        {
            Instantiate(structurePrefab, hitInfo.point, Quaternion.identity);
            Destroy(previewStructure);
            ResetPreview();
        }
        buildUI.SetActive(false);
    }

    private void BuildUISet()
    {       
        buildUIImage.fillAmount = curDuration / needDuration;
        
        if(curDuration >= needDuration)
            return;

        if (curDuration < needDuration)
        {
            curDuration += Time.deltaTime;

            if (curDuration >= needDuration)
            {
                curDuration = needDuration;
            }
        }
    }

    /// <summary>
    /// �������� �Һ��� �ǹ��� ������ ���
    /// </summary>    
    public void UseResource(float NeedValue)
    {
        needItemIndex = NeedValue;
    }

    /// <summary>
    /// ������� Fkey�Է½� ȣ���Լ�
    /// </summary>
    private void CancelStruct()
    {
        if (previewStructure != null)
            Destroy(previewStructure);

        ResetPreview();
        craftPanalCanvas.SetActive(false);
        cancelInfoTxt.SetActive(false);
        buildUI.SetActive(false);
    }

    private void ResetPreview()
    {
        previewStructure = null;
        structurePrefab = null;
    }
}


