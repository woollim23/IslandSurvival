using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PredatorAI : AnimalAI
{
    [Header("Combat")]
    public int damage;
    public float attackRate;
    public float lastAttackTime;
    public float attackDistance;

    protected override void Update()
    {
        base.Update();

        switch (_aistate)
        {
            case AIState.Attacking:
                AttackingUpdate();
                break;
        }
    }
    public override void SetState(AIState state)
    {
        base.SetState(state);
        
        switch (_aistate)
        {
            case AIState.Attacking:
                agent.speed = _animal.runSpeed;
                break;
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

    void AttackingUpdate()
    {
        if (playerDistance < attackDistance && IsPlayerinFieldOfView())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                CharacterManager.Instance.Player.controller.GetComponent<IDamagable>().TakePhysicalDamage(damage);
                _animator.speed = 1;
                _animator.SetTrigger("Attack"); //animator, parameter, trigger
            }
        }
        else
        {
            if (playerDistance < detecDistance)
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
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

    bool IsPlayerinFieldOfView()
    {
        Vector3 directionToPlayer = CharacterManager.Instance.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }
}