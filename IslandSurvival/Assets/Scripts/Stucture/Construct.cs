using System;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Craft
{
    public string craftName; // 이름
    public GameObject RealStructurePrefab; // 실제 설치 될 프리팹
    public GameObject previewStructure; // 미리 보기 프리팹
}

public class Construct : MonoBehaviour
{
    bool isConstructMode;//건축중인지 체크

    public GameObject inventoryWindow;
    public PlayerController controller;
    public Transform dropPosition;
    public GameObject constructPrefab;
    public GameObject CraftPanalCanvas;// 기본 베이스 UI
    public UICraft craftInventory;
    public UIInventory inventory;


    private bool isActivated = false;  // CraftManual UI 활성 상태    


    [SerializeField]
    private Craft[] craft;  //CratfSlot에 있는 슬롯들. 

    private GameObject previewStructure; // 미리 보기 프리팹을 담을 변수
    private GameObject structurePrefab; // 실제 생성될 프리팹을 담을 변수 

    [SerializeField]
    private Transform playerTransform;  // 플레이어 위치

    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float range;
    private float NeedItemIndex;
    private float NeedDuration;
    private int selectedIndex;

    //제작가능 아이템 : ItemType.Resource
    //건설가능 아이템 : ItemType.Constructable
    // ItemObject 스크립트에 IInteractable 상속 , Interaction스크립트에서 
    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        dropPosition = CharacterManager.Instance.Player.dropPosition;
        CharacterManager.Instance.Player.controller.onCancelStruct += CancelStruct; // 취소 이벤트 등록
        //selectedIndex = inventory.selectedItemIndex;
    }

    void Update()
    {       
        if (previewStructure != null)
        {
            PreviewPositionUpdate();            
        }   
        if(Input.GetButtonDown("Fire1"))
        {
            Build();
        }
    }    

    /// <summary>
    /// Inventory내에 제작버튼
    /// </summary>
    public void OnCraftButton()
    {
        isActivated = true;
        CraftPanalCanvas.SetActive(true);
        inventoryWindow.SetActive(false);
        
    }    

    public void OnCancleButton() //크래프트캔버스 취소하기버튼
    {
        isActivated = false;
        CraftPanalCanvas.SetActive(false);
        inventoryWindow.SetActive(true);
    }
    public void OnBuildButton() //Inventory 건설하기버튼
    {        
        SelectStructure(selectedIndex);
        CraftPanalCanvas.SetActive(false);    
        
    }
    void SelectStructure(int index)
    {
        previewStructure = Instantiate(craft[index].previewStructure, playerTransform.position + playerTransform.forward, Quaternion.identity);
        structurePrefab = craft[index].RealStructurePrefab;
    }




    /// <summary>
    /// 미리보기프리팹 위치값수정
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
        if (previewStructure && previewStructure.GetComponent<PreviewObject>().isBuildable()) //빌드가 가능 하다면
        {
            Instantiate(structurePrefab, hitInfo.point, Quaternion.identity);
            Destroy(previewStructure);
            ResetPreview();
        }
    }
    private void Building()
    {
        isConstructMode = true;

        // UI재생
        //인보크
        Instantiate(structurePrefab, hitInfo.point, Quaternion.identity);
        Destroy(previewStructure);
        ResetPreview();

    }

    public void UseResource(float NeedValue, float duration)
    {
         NeedItemIndex = NeedValue;
         NeedDuration = duration;
    }

    /// <summary>
    /// 건축취소 Fkey입력시 호출함수
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


