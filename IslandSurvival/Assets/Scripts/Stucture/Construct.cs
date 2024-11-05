using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Construct : MonoBehaviour
{
    bool isConstructMode;//건축중인지 체크

    public PlayerController controller;    

    public GameObject constructPrefab;

    public GameObject craftCanvas;    

    public GameObject CraftPanalCanvas;// 기본 베이스 UI


    private bool isActivated = false;  // CraftManual UI 활성 상태
    private bool isPreviewActivated = false; // 미리 보기 활성화 상태


    [SerializeField]
    private UICraft[] UICrafts;  //  탭에 있는 슬롯들. 

    private GameObject previewStructure; // 미리 보기 프리팹을 담을 변수
    private GameObject structurePrefab; // 실제 생성될 프리팹을 담을 변수 

    [SerializeField]
    private Transform tf_Player;  // 플레이어 위치

    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float range;

    //제작가능 아이템 : ItemType.Resource
    //건설가능 아이템 : ItemType.Constructable
    // ItemObject 스크립트에 IInteractable 상속 , Interaction스크립트에서 
    private void Start()
    {
        CharacterManager.Instance.Player.controller.onCancelStruct += CancelStruct; // 취소 이벤트 등록
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

    public void OnCraftButton() //메인씬에서 제작버튼
    {
        isActivated = true;
        CraftPanalCanvas.SetActive(true);
    }
    public void OnStartCraftButton() //크래프트캔버스 내 제작하기버튼
    {
        //if()
        /*
        제작가능 아이템일 경우
        제작하기 버튼 활성화
        * UI인벤토리 제작하기 버튼 추가구현 필요
        On제작하기버튼()
        
         크래프트내부에서 또 재작버튼 = 완성된 아이템이 인벤토리로 들어가게만듬
         */

    }

    public void OnCancleButton() //취소하기
    {
        isActivated = false;
        CraftPanalCanvas.SetActive(false);
    }
    public void OnBuildButton() 
    {
        //건축하기= 현재는 그냥 버튼을 누르게 되면 실행되게함 = 추가구현 필요
        /*건축물일경우
        제작하기 버튼 활성화
        */
        Build();
    }    

    /*     
    
    enum 건축물로 분류 하고, 건축물일경우 = 건축하기 버튼 활성화

    선구현------------------------------------------------------
    건축이 가능한 위치에 <<위치체크하는거<<튜터님 문의>>
                // 건축물을 그린컬러로 변경
                // 건축 진행 현황 UI / [SerializeFeild]건축 시간을 인스펙터 내에서 설정가능
                // Cancle 버튼 F Key 활성화 UI생성 (조건문 / 건축중일때만)
                //  

    건축이 불가능한 위치에
                // 건축물을 레드컬러로 변경
                // 마우스좌클릭시 건축불가 (삐-) 사운드 재생
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
        if (isPreviewActivated && previewStructure.GetComponent<PreviewObject>().isBuildable()) //빌드가 가능 하다면
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
    /// 건축취소 Fkey입력시 호출함수
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
