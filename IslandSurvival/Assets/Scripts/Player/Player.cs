using System;
using UnityEngine;

public class Player : Singletone<Player>
{
    public PlayerController controller;
    public PlayerCondition condition;
    //public Equipment equip;

    //public ItemData itemData;
    public Action addItem;

    public Transform dropPosition;

    private void Awake()
    {
        // 1. ĳ���� �Ŵ����� ����. ���� ���� Ȯ���ϰ� �Ŵ��� ����
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        //equip = GetComponent<Equipment>();
    }
}
