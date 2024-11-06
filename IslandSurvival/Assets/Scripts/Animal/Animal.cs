using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Animal : MonoBehaviour, IDamagable
{
    [Header("Stats")]
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public ItemData[] dropOnDeath;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakePhysicalDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("Hit");
        if (health <= 0)
        {
            Die();
        }

        StartCoroutine(DamageFlash());
    }

    public void Die()
    {
        for (int i = 0; i < dropOnDeath.Length; i++)
        {
            Instantiate(dropOnDeath[i].dropPrefab, transform.position + Vector3.up * 2, quaternion.identity);
        }
        animator.SetTrigger("Death");
        Destroy(gameObject);
    }

    IEnumerator DamageFlash()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = new Color(1.0f, 0.6f, 0.6f);
        }
        
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = Color.white;
        }
    }
}