using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construct : MonoBehaviour
{
    bool isContruct;
    public PlayerController controller;
    CapsuleCollider capsuleCollider;

    void Start()
    {
        CharacterManager.Instance.Player.controller.onCancelStruct += CancelStruct; // ��� �̺�Ʈ ���
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
        // ���� ��� �Լ�
    }


    /*
     ���۰��� �������� ���
    �����ϱ� ��ư Ȱ��ȭ
    On�����ϱ��ư()
    {

    }

    ���๰�ϰ��
    �����ϱ� ��ư Ȱ��ȭ

    On�����ϱ��ư()
    {
      �÷��̾��� �޼� ��ġ�� ���� �ǹ���� ����ֱ�
    }

    On


    
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



}
