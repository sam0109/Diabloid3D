using System;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacterLook))]
public class AICharacterControl : CharacterManager
{
    private NavMeshAgent agent;       // the navmesh agent required for the path finding
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
        agent = GetComponentInChildren<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    public override void Update()
    {
        base.Update();
        if(isDead)
        {
            print("sinking");
            transform.Translate(Vector3.down * 0.005f);  //sink into the ground on death
            if(transform.position.y <= -1)
            {
                Destroy(gameObject);
            }
        }
        else if (target != null)
        {
            if ((target.transform.position - transform.position).magnitude < viewDistance)
            {
                agent.SetDestination(target.position);

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    lookRotation = Mathf.Atan2(agent.desiredVelocity.x, agent.desiredVelocity.z);
                    Move(new Vector2(agent.desiredVelocity.x, agent.desiredVelocity.z).normalized, lookRotation);
                }
                else
                {
                    Move(Vector2.zero, lookRotation);
                    if (attackTimer <= 0 && !friendly)   //if we are next to the player and not attacking, then attack
                    {
                        attackTimer = attackDelay;
                        Attack();
                    }
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
    public void Interacted()
    {
        if (myType == NPCType.Shopkeeper)
        {
            GameObject myPopup = PopupHandler.popupHandler.MakePopup();
        }
    }
}
