using UnityEngine;
using UnityEngine.AI;

public class PreyAI : AnimalAI
{
    float fleeDistance;
    
    protected override void Update()
    {
        base.Update();
        
        if (aiState == AIState.Wandering || aiState == AIState.Idle)
        {
            PassiveUpdate();
        }
    }
    
    public override void SetState(AIState state)
    {
        base.SetState(state);

        switch (aiState)
        {
            case AIState.Fleeing:
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
            SetState(AIState.Fleeing);
        }
    }
    
    void FleeingUpdate()
    {
        if(agent.remainingDistance < 0.1f)
        {
            agent.SetDestination(GetFleeLocation());
        }
        else
        {
            SetState(AIState.Wandering);
        }
    }
    
    Vector3 GetFleeLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (GetDestinationAngle(hit.position) > 90 || playerDistance < safeDistance)
        {

            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }
    
    float GetDestinationAngle(Vector3 targetPos)
    {
        return Vector3.Angle(transform.position - CharacterManager.Instance.Player.transform.position, transform.position + targetPos);
    }
}