using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.controller.onCancelStruct += CancelStruct; // 취소 이벤트 등록
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CancelStruct()
    {
        // 건축 취소 함수
    }
}
