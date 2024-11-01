﻿using UnityEngine;
using UnityEngine.InputSystem;

public class Equipment : MonoBehaviour
{
    public Equip curEquip; // 현재 장착 정보
    public Transform equipParent; // 장비를 달아줄 위치(카메라 위치)

    private PlayerController controller;

    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
    }

    public void EquipNew(ItemData data)
    {
        // 새로운 아이템 장착 
        UnEquip(); // 기존 장비 장착 해제
        // 현재 장착할 데이터 넣어줌
        curEquip = Instantiate(data.equipPrefab, equipParent).GetComponent<Equip>();

        if (curEquip.doesStatIncrease == true)
            EquipIncrease();
    }

    public void UnEquip()
    {
        if (curEquip != null)
        {
            if (curEquip.doesStatIncrease == true)
                UnEquipIncrease();
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }

    public void EquipIncrease()
    {
        // 장비 장착 효과
        controller.moveSpeed += curEquip.increase;
    }

    public void UnEquipIncrease()
    {
        // 장비 장착 효과 해제
        controller.moveSpeed -= curEquip.increase;
    }
}