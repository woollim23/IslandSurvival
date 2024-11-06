using System;
using System.Collections;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;
    public Canvas mainCanvas;
    public GameObject gameOverCanvas;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition thirst { get { return uiCondition.thirst; } }
    public Condition temperature { get { return uiCondition.temperature; } }

    public float healthDecay;

    public event Action onTakeDamage;
    public event Action onDeadEvent;
    public bool isDead = false;

    void Update()
    {
        // 시간당 지속 변화값 반영
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        thirst.Subtract(thirst.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

 
        if (hunger.curValue <= 0f || thirst.curValue <= 0f || temperature.curValue <= 0f)
        {
            health.Subtract(healthDecay * Time.deltaTime);
        }

        if (health.curValue == 0f && !isDead)
        {
            Die();
        }
    }

    public void DecreaseTemperature(float amount)
    {
        temperature.Subtract(amount);
    }

    public void IncreaseTemperature(float amount)
    {
        temperature.Add(amount);
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void DrinkWater(float amount)
    {
        thirst.Add(amount);
    }

    public void UpStamina(float amount)
    {
        stamina.Add(amount);
    }

    public void Doping(float value, float duration)
    {
        // 캐릭터 스펙 증가 아이템일 경우 : 현재는 속도 증가 아이템, 호박하나만 있음
        // TODO : 음식 효과를 여러게 저장할 수 있게 리스트 구현, 데이터 관리 로직 구현
        StartCoroutine(DopingDuration(value, duration));
    }

    IEnumerator DopingDuration(float value, float duration)
    {
        float tempSpeed = CharacterManager.Instance.Player.controller.moveSpeed;
        // value 만큼 이속 증가
        CharacterManager.Instance.Player.controller.moveSpeed += value;

        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            yield return null; // 한 프레임 대기
        }

        CharacterManager.Instance.Player.controller.moveSpeed = tempSpeed;
    }

    public void Die()
    {
        Debug.Log("죽었다");
        isDead = true;
        onDeadEvent?.Invoke();
        CharacterManager.Instance.Player.controller.canLook = false;
        CharacterManager.Instance.Player.controller.isInputBlocked = true;

        mainCanvas.gameObject.SetActive(false);
        gameOverCanvas.SetActive(true);

        Cursor.visible = true; // 커서를 화면에 보이도록 설정
        Cursor.lockState = CursorLockMode.None; // 커서가 자유롭게 움직이도록 설정
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
}
