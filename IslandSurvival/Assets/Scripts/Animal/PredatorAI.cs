using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PredatorAI : AnimalAI
{
    [Header("Combat")]
    public int damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;
    private NavMeshPath path;
    private Renderer[] meshRenderers;
    private AudioSource audioSource;
    public float fadeTime;
    public float maxVolume;
    private float targetVolume;

    private void Awake()
    {
        meshRenderers = GetComponentsInChildren<Renderer>();
        targetVolume = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = targetVolume;
        audioSource.Play();
    }

    protected override void Update()
    {
        base.Update();

        switch (aiState)
        {
            case AIState.Idle:
            case AIState.Wandering:
                PassiveUpdate();
                break;
            case AIState.Attacking:
                AttackingUpdate();
                break;
        }
    }
    
    public override void SetState(AIState state)
    {
        base.SetState(state);

        switch (aiState)
        {
            case AIState.Attacking:
                agent.speed = animal.runSpeed;
                agent.isStopped = false;
                audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, (maxVolume / fadeTime) * Time.deltaTime);
                break;
        }
        
        animator.speed = agent.speed / animal.walkSpeed;
    }
    
    void AttackingUpdate()
    {
        if (playerDistance < attackDistance && IsPlayerInFieldOfView())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                CharacterManager.Instance.Player.controller.GetComponent<IDamagable>().TakePhysicalDamage(damage);
                animator.speed = 1;
                animator.SetTrigger("Attack");
                
                StartCoroutine(DamageFlash());
            }
        }
        else
        {
            if (playerDistance < detecDistance)
            {
                agent.isStopped = false;
                path = new NavMeshPath();
                if (agent.CalculatePath(CharacterManager.Instance.Player.transform.position, path))
                {
                    agent.SetDestination(CharacterManager.Instance.Player.transform.position);
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(AIState.Wandering);
                }
            }
            else
            {
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                SetState(AIState.Wandering);
            }
        }
    }
    
    protected override void PassiveUpdate()
    {
        base.PassiveUpdate();

        if (playerDistance < detecDistance)
        {
            SetState(AIState.Attacking);
        }
    }
    
    protected IEnumerator DamageFlash()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color =new Color(1.0f, 0.6f, 0.6f);
        }
        
        yield return new WaitForSeconds(0.1f);
    
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = Color.white;
        }
    }
}