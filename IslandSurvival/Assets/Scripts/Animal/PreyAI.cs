using UnityEngine;
using UnityEngine.AI;

public class PreyAI : AnimalAI
{
    float fleeDistance;
    
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
                RunawayUpdate();
                break;
        }
    }
    
    public override void SetState(AIState state)
    {
        base.SetState(state);

        switch (aiState)
        {
            case AIState.Runaway:
                agent.speed = animal.runSpeed;
                agent.isStopped = false;
                break;
        }
        
        animator.speed = agent.speed / animal.walkSpeed;
    }
    
    protected override void PassiveUpdate()
    {
        base.PassiveUpdate();

        if (playerDistance < detecDistance)
        {
            SetState(AIState.Runaway);
        }
    }
    
    void RunawayUpdate()
    {
        Vector3 directionAwayFromPlayer = transform.position - CharacterManager.Instance.Player.transform.position;
        Vector3 runPosition = transform.position + directionAwayFromPlayer.normalized * fleeDistance;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(runPosition, out hit, 5.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}