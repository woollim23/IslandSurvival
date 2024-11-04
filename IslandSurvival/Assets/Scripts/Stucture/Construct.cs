using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construct : MonoBehaviour
{
    bool isContruct;
    public PlayerController controller;
    CapsuleCollider capsuleCollider;

    public GameObject craftCanvas;

    void Start()
    {
        CharacterManager.Instance.Player.controller.onCancelStruct += CancelStruct; // 취소 이벤트 등록
        capsuleCollider = CharacterManager.Instance.Player.controller._capsuleCollider;
    }

    void Update()
    {
        //RaycastHit hit = 
        //Physics.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector3.down, 0.7f, LayerMask.GetMask("Ground"));
        //isContruct = Physics.Raycast()
    }


    void CancelStruct()
    {
        // 건축 취소 함수
    }
    public void OnCraftButton() //제작하기
    {
        /*
        제작가능 아이템일 경우
        제작하기 버튼 활성화
        * UI인벤토리 제작하기 버튼 추가구현 필요
        On제작하기버튼()
        */
        craftCanvas.SetActive(true);
        //크래프트내부에서 또 재작버튼 = 완성된 아이템이 인벤토리로 들어가게만듬
    }


    public void OnCancleButton() //취소하기
    {
        craftCanvas.SetActive(false);
    }
    public void OnBuildButton() 
    {
        //건축하기= 현재는 그냥 버튼을 누르게 되면 실행되게함 = UI인벤토리 추가구현 필요
        /*건축물일경우
        제작하기 버튼 활성화
        플레이어의 왼손 위치에 작은 건물모양 들고있기*/
    }

    private void isBuildable()
    { 
    
    }


    /*
     * UI인벤토리 추가구현 필요
     제작가능 아이템일 경우
    제작하기 버튼 활성화
    On제작하기버튼()
    {

    }

    건축물일경우
    제작하기 버튼 활성화

    


    
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



}
