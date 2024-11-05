using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Construct : MonoBehaviour
{
    bool isConstructMode;//���������� üũ

    public PlayerController controller;    

    public GameObject constructPrefab;

    public GameObject craftCanvas;    

    public GameObject CraftPanalCanvas;// �⺻ ���̽� UI


    private bool isActivated = false;  // CraftManual UI Ȱ�� ����
    private bool isPreviewActivated = false; // �̸� ���� Ȱ��ȭ ����


    [SerializeField]
    private UICraft[] UICrafts;  //  �ǿ� �ִ� ���Ե�. 

    private GameObject previewStructure; // �̸� ���� �������� ���� ����
    private GameObject structurePrefab; // ���� ������ �������� ���� ���� 

    [SerializeField]
    private Transform tf_Player;  // �÷��̾� ��ġ

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
        CharacterManager.Instance.Player.controller.onCancelStruct += CancelStruct; // ��� �̺�Ʈ ���
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
            Window();

        if (isPreviewActivated)
            PreviewPositionUpdate();

        if (Input.GetButtonDown("Fire1"))
            Build();

        //RaycastHit hit = 
        //Physics.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector3.down, 0.7f, LayerMask.GetMask("Ground"));
        //isContruct = Physics.Raycast()
    }
    private void Window()
    {
        if (!isActivated)
            OnCraftButton();
        else
            OnCancleButton();
    }

    public void OnCraftButton() //���ξ����� ���۹�ư
    {
        isActivated = true;
        CraftPanalCanvas.SetActive(true);
    }
    public void OnStartCraftButton() //ũ����Ʈĵ���� �� �����ϱ��ư
    {
        //if()
        /*
        ���۰��� �������� ���
        �����ϱ� ��ư Ȱ��ȭ
        * UI�κ��丮 �����ϱ� ��ư �߰����� �ʿ�
        On�����ϱ��ư()
        
         ũ����Ʈ���ο��� �� ���۹�ư = �ϼ��� �������� �κ��丮�� ���Ը���
         */

    }

    public void OnCancleButton() //����ϱ�
    {
        isActivated = false;
        CraftPanalCanvas.SetActive(false);
    }
    public void OnBuildButton() 
    {
        //�����ϱ�= ����� �׳� ��ư�� ������ �Ǹ� ����ǰ��� = �߰����� �ʿ�
        /*���๰�ϰ��
        �����ϱ� ��ư Ȱ��ȭ
        */
        Build();
    }    

    /*     
    
    enum ���๰�� �з� �ϰ�, ���๰�ϰ�� = �����ϱ� ��ư Ȱ��ȭ

    ������------------------------------------------------------
    ������ ������ ��ġ�� <<��ġüũ�ϴ°�<<Ʃ�ʹ� ����>>
                // ���๰�� �׸��÷��� ����
                // ���� ���� ��Ȳ UI / [SerializeFeild]���� �ð��� �ν����� ������ ��������
                // Cancle ��ư F Key Ȱ��ȭ UI���� (���ǹ� / �������϶���)
                //  

    ������ �Ұ����� ��ġ��
                // ���๰�� �����÷��� ����
                // ���콺��Ŭ���� ����Ұ� (��-) ���� ���
    */

    
    public void CraftSlotClick(int slotNumber)
    {
        previewStructure = Instantiate(UICrafts[slotNumber].previewStructure, tf_Player.position + tf_Player.forward, Quaternion.identity);
        structurePrefab = UICrafts[slotNumber].RealStructurePrefab;
        isPreviewActivated = true;
        CraftPanalCanvas.SetActive(false);
    }


    private void PreviewPositionUpdate()
    {
        if (Physics.Raycast(tf_Player.position, tf_Player.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform != null)
            {
                Vector3 _location = hitInfo.point;
                previewStructure.transform.position = _location;

                Debug.Log(_location);
                Debug.Log(previewStructure.transform.position);
            }
        }
    }

    private void Build()
    {
        if (isPreviewActivated && previewStructure.GetComponent<PreviewObject>().isBuildable()) //���尡 ���� �ϴٸ�
        {
            Instantiate(structurePrefab, hitInfo.point, Quaternion.identity);
            Destroy(previewStructure);
            isActivated = false;
            isPreviewActivated = false;
            previewStructure = null;
            structurePrefab = null;
        }        
    }



    /// <summary>
    /// ������� Fkey�Է½� ȣ���Լ�
    /// </summary>
    private void CancelStruct()
    {
        if (isPreviewActivated)
            Destroy(previewStructure);

        isActivated = false;
        isPreviewActivated = false;

        previewStructure = null;
        structurePrefab = null;

        CraftPanalCanvas.SetActive(false);
    }


}
