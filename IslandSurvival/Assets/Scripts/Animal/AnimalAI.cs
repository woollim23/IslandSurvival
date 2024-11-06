using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;
using Random = Unity.Mathematics.Random;

public enum AIState
{
    Idle,
    Wandering,
    Attacking,
    Fleeing
}

public abstract class AnimalAI : MonoBehaviour
{
    [Header("AI")]
    protected NavMeshAgent agent;
    public float detecDistance;
    protected AIState aistate;
    public float safeDistance;
    
    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;
    
    public float playerDistance;
    
    public float fieldOfView = 120f;
    
    public Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;
    public Animal animal;
    private float lookDirection;
    private Vector3 previousPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        previousPosition = transform.position;
    }

    protected virtual void Update()
    {
        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);
        if(animator != null)
            animator.SetBool("Moving", aistate != AIState.Idle);
        Vector3 movement = transform.position - previousPosition;
        
        if (movement.x < -0.1f)
        {
            lookDirection = -1;
        }
        else if (movement.x > 0.1f)
        {
            lookDirection = 1;
        }
        else
        {
            lookDirection = 0;
        }
        if (animator != null)
            animator.SetFloat("lookDirection", lookDirection);
        
        switch (aistate)
        {
            case AIState.Idle:
            case AIState.Wandering:
                PassiveUpdate();
                break;
        }
    }

    public virtual void SetState(AIState state)
    {
        aistate = state;

        switch (aistate)
        {
            case AIState.Idle:
                agent.speed = animal.walkSpeed;
                agent.isStopped = true;
                break;
            case AIState.Wandering:
                agent.speed = animal.walkSpeed;
                agent.isStopped = false; //move
                break;
        }

        animator.speed = agent.speed / animal.walkSpeed;
    }

    protected virtual void PassiveUpdate()
    {
        if (aistate == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", UnityEngine.Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        if (playerDistance < detecDistance)
        {
            SetState(AIState.Attacking);
        }
    }

    void WanderToNewLocation()
    {
        if (aistate != AIState.Idle) return;
        
        SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit; 
        
        NavMesh.SamplePosition(transform.position + (UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
        
        int i = 0;

        while (Vector3.Distance(transform.position, hit.position) < detecDistance)
        {
            NavMesh.SamplePosition(transform.position + (UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30) break;
        }
        
        return hit.position;
    }
}