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
    protected AIState _aistate;
    public float safeDistance;
    
    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;
    
    public float playerDistance;
    
    public float fieldOfView = 120f;
    
    protected Animator _animator;
    private SkinnedMeshRenderer[] meshRenderers;
    
    public Animal _animal;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    void start()
    {
        
    }

    protected virtual void Update()
    {
        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);
        
        _animator.SetBool("Moving", _aistate != AIState.Idle);

        switch (_aistate)
        {
            case AIState.Idle:
            case AIState.Wandering:
                PassiveUpdate();
                break;
        }
    }

    public virtual void SetState(AIState state)
    {
        _aistate = state;

        switch (_aistate)
        {
            case AIState.Idle:
                agent.speed = _animal.walkSpeed;
                agent.isStopped = true;
                break;
            case AIState.Wandering:
                agent.speed = _animal.walkSpeed;
                agent.isStopped = false; //move
                break;
        }
        
        _animator.speed = agent.speed / _animal.walkSpeed;
    }

    protected virtual void PassiveUpdate()
    {
        if (_aistate == AIState.Wandering && agent.remainingDistance < 0.1f)
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
        if (_aistate != AIState.Idle) return;
        
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