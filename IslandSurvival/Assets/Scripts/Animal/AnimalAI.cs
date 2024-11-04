using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Idle,
    Wandering,
    Attacking,
    Runaway
}

public abstract class AnimalAI : MonoBehaviour
{
    [Header("AI")]
    protected NavMeshAgent agent;
    public float detecDistance;
    protected AIState aiState;
    
    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;
    
    protected float playerDistance;
    
    public float fieldOfView = 120f;
    
    public Animator animator;
    public Animal animal;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        SetState(AIState.Wandering);
        animator = GetComponent<Animator>();
        animal = GetComponent<Animal>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.transform.position);
        
        animator.SetBool("Moving", aiState != AIState.Idle);
    }
    
    public virtual void SetState(AIState state)
    {
        aiState = state;
    }
    
    protected virtual void PassiveUpdate()
    {
        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
            //health += amount * Time.deltaTime; //heal animal's hp when far from player
        }
    }
    
    void WanderToNewLocation()
    {
        if (aiState != AIState.Idle) return;
        
        SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;

        while (Vector3.Distance(transform.position, hit.position) < detecDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30) break;
        }

        return hit.position;
    }
    
    protected bool IsPlayerInFieldOfView()
    {
        Vector3 directonToPlayer = CharacterManager.Instance.Player.transform.position - transform.position;
        float angel = Vector3.Angle(transform.forward, directonToPlayer);
        return angel < fieldOfView * 0.5f;
    }
}