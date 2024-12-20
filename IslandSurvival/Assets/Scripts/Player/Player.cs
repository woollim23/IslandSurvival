﻿using System;
using UnityEngine;

public class Player : Singletone<Player>
{
    public PlayerController controller;
    public PlayerCondition condition;
    public PlayerAttack attack;
    public Equipment equip;
    public Animator animator;

    public ItemData itemData;
    public Action addItem;
    
    public Transform dropPosition;

    private void Awake()
    {
        // 1. 캐릭터 매니저에 접근. 없는 것을 확인하고 매니저 생성
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        attack = GetComponent<PlayerAttack>();
        equip = GetComponent<Equipment>();
        animator = GetComponentInChildren<Animator>();
    }
}