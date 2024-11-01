using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour, IDamagable
{
    [Header("Stats")]
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public ItemData[] dropOnDeath;

    private Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    public void TakePhysicalDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }

        // StartCoroutine(DamageFlash());
    }

    public virtual void Die()
    {
        for (int i = 0; i < dropOnDeath.Length; i++)
        {
            Instantiate(dropOnDeath[i].dropPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    // protected IEnumerator DamageFlash()
    // {
    //     for (int i = 0; i < meshRenderers.Length; i++)
    //     {
    //         meshRenderers[i].material.color =new Color(1.0f, 0.6f, 0.6f);
    //     }
    //     
    //     yield return new WaitForSeconds(0.1f);
    //
    //     for (int i = 0; i < meshRenderers.Length; i++)
    //     {
    //         meshRenderers[i].material.color = Color.white;
    //     }
    // }
}