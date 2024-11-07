using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Construct : MonoBehaviour
{
    bool isConstructMode;//건축중인지 체크
    ItemData data;

    public PlayerController controller;
    public Transform dropPosition;

    public GameObject inventoryWindow;
    public GameObject craftPanalCanvas;// 기본 베이스 UI
    public GameObject cancelInfoTxt;
    public GameObject buildUI;
    public Image buildUIImage;

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
    private float needItemIndex;
    private float needDuration = 5f;
    private float curDuration = 0f;
    private ItemData selectedItem;

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        dropPosition = CharacterManager.Instance.Player.dropPosition;
        CharacterManager.Instance.Player.controller.onCancelStruct += CancelStruct; // 취소 이벤트 등록
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
    /// Inventory내에 제작버튼
    /// </summary>
    public void OnCraftButton()
    {
        craftPanalCanvas.SetActive(true);
        inventoryWindow.SetActive(false);
    }

    /// <summary>
    /// 크래프트캔버스 취소하기버튼
    /// </summary>
    public void OnCancleButton()
    {
        craftPanalCanvas.SetActive(false);
        inventoryWindow.SetActive(true);
    }
    /// <summary>
    /// 인벤토리 건설하기버튼
    /// </summary>
    public void OnBuildButton()
    {
        CharacterManager.Instance.Player.controller.ToggleCursor();

        SelectStructure(inventory.selectedItem); //선택건축물세팅
        inventoryWindow.SetActive(false);
        cancelInfoTxt.SetActive(true);
    }

    /// <summary>
    /// 선택건축물 데이터를 담고있는 프리뷰,건축물 세팅 메소드
    /// </summary>    
    void SelectStructure(ItemData data)
    {
        previewStructure = Instantiate(data.previewStructure, playerTransform.position + playerTransform.forward, Quaternion.identity);
        structurePrefab = data.RealStructurePrefab;
    }

    /// <summary>
    /// 미리보기프리팹 위치값 매프레임 업데이트
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
    /// 왼쪽 마우스 클릭시 건설실행 (Duration값 만큼 딜레이) 
    /// </summary>
    public IEnumerator Build()
    {
        yield return new WaitForSeconds(needDuration);

        if (previewStructure && previewStructure.GetComponent<PreviewObject>().isBuildable()) //빌드가 가능 하다면
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
    /// 아이템을 소비해 건물을 지을때 사용
    /// </summary>    
    public void UseResource(float NeedValue)
    {
        needItemIndex = NeedValue;
    }

    /// <summary>
    /// 건축취소 Fkey입력시 호출함수
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


