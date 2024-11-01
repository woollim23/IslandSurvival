using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class PreyAI : AnimalAI
{
    protected override void Update()
    {
        base.Update();
        
        switch (aiState)
        {
            case AIState.Idle:
            case AIState.Wandering:
                PassiveUpdate();
                break;
            case AIState.Runaway:
                //RunawayUpdate();
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
                break;
        }
        
        animator.speed = agent.speed / animal.walkSpeed;
    }
}