using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.controller.onCancelStruct += CancelStruct; // ��� �̺�Ʈ ���
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CancelStruct()
    {
        // ���� ��� �Լ�
    }
}
