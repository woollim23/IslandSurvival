using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PreyAI : AnimalAI
{
    protected override void Update()
    {
        base.Update();

        switch (aistate)
        {
            case AIState.Fleeing:
                FleeingUpdate();
                break;
        }
    }
    
    public override void SetState(AIState state)
    {
        base.SetState(state);
        
        switch (aistate)
        {
            case AIState.Fleeing:
                agent.speed = animal.runSpeed;
                break;
        }
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