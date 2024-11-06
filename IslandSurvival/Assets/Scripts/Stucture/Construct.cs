using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//[System.Serializable]
//public class Craft
//{
//    public string craftName; // 이름
//    public GameObject RealStructurePrefab; // 실제 설치 될 프리팹
//    public GameObject previewStructure; // 미리 보기 프리팹
//}

public class Construct : MonoBehaviour
{
    bool isConstructMode;//건축중인지 체크

    public PlayerController controller;
    public Transform dropPosition;
    
    public GameObject inventoryWindow;
    public GameObject CraftPanalCanvas;// 기본 베이스 UI
    public GameObject CancelInfoTxt;

    public UICraft craftInventory;
    public UIInventory inventory;

    private bool isActivated = false;  // CraftManual UI 활성 상태

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
    private ItemData selectedItem;

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        dropPosition = CharacterManager.Instance.Player.dropPosition;
        CharacterManager.Instance.Player.controller.onCancelStruct += CancelStruct; // 취소 이벤트 등록
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
    /// Inventory내에 제작버튼
    /// </summary>
    public void OnCraftButton()
    {        
        CraftPanalCanvas.SetActive(true);
        inventoryWindow.SetActive(false);        
    }    

    public void OnCancleButton() //크래프트캔버스 취소하기버튼
    {        
        CraftPanalCanvas.SetActive(false);
        inventoryWindow.SetActive(true);        
    }
    public void OnBuildButton() //Inventory 건설하기버튼
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

    /// <summary>
    /// 아이템을 소비해 건물을 지을때 사용
    /// </summary>    
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
        CancelInfoTxt.SetActive(false);
    }

    private void ResetPreview()
    {                
        previewStructure = null;
        structurePrefab = null;
    }
}


