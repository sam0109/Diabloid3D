using System;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacterLook))]
public class AIManager : CharacterManager
{
    private UnityEngine.AI.NavMeshAgent agent;       // the navmesh agent required for the path finding
    public Transform target;                                    // target to aim for
    public enum NPCType { Shopkeeper, Enemy, Townsperson };
    public NPCType myType;
    public bool friendly;
    public float viewDistance;
    private float lookRotation;

    new private void Start()
    {
        base.Start();
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        if (myType == NPCType.Enemy)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    public override void Update()   //the main AI loop, TODO: should become a state machine
    {
        base.Update();
        if(isDead)
        {
            transform.Translate(Vector3.down * 0.005f);  //sink into the ground on death
            if(transform.position.y <= -1)
            {
                Destroy(gameObject);
            }
        }
        else if (target != null)
        {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            if (vectorToTarget.magnitude < viewDistance)
            {
                agent.SetDestination(target.position);
                lookRotation = Mathf.Atan2(agent.desiredVelocity.x, agent.desiredVelocity.z);

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    Move(new Vector2(agent.desiredVelocity.x, agent.desiredVelocity.z).normalized, lookRotation);
                }
                else
                {
                    Move(Vector2.zero, Mathf.Atan2(vectorToTarget.x, vectorToTarget.z));
                }

                if (!friendly &&
                    vectorToTarget.magnitude < totalReach)   //if we are next to the player and not attacking, then attack
                {
                    Attack();
                }
            }
            else
            {
                agent.SetDestination(transform.position);
                Move(Vector2.zero, lookRotation);
            }
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (isDead)
        {
            agent.enabled = false;
        }
    }
}