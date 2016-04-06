using System;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacterLook))]
public class AICharacterControl : MonoBehaviour
{
    public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public ThirdPersonCharacterLook character { get; private set; } // the character we are controlling
    public Transform target;                                    // target to aim for

    public float maxHealth;
    float currentHealth;

    bool isAttacking = true;
    public float attackSpeed;
    float attackReload;

    public float attackDistance;
    public float attackDamage;

    public float viewDistance;

    private float lookRotation;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacterLook>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent.updateRotation = false;
        agent.updatePosition = true;
        attackReload = attackSpeed;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (target != null)
        {
            if ((target.transform.position - transform.position).magnitude < viewDistance)
            {
                agent.SetDestination(target.position);

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    lookRotation = Mathf.Atan2(agent.desiredVelocity.x, agent.desiredVelocity.z);
                    character.Move(new Vector2(agent.desiredVelocity.x, agent.desiredVelocity.z).normalized, lookRotation, false);
                }
                else
                {
                    character.Move(Vector2.zero, lookRotation, false);
                    if (!isAttacking)   //if we are next to the player and not attacking, then attack
                    {
                        isAttacking = true;
                        Attack(target.gameObject);
                    }
                }

                if (isAttacking) //timer for attack delay
                {
                    attackReload -= Time.deltaTime;
                    if (attackReload <= 0)
                    {
                        attackReload = attackSpeed;
                        isAttacking = false;
                    }
                }
            }
            else
            {
                agent.SetDestination(transform.position);
                character.Move(Vector2.zero, lookRotation, false);
                if (!isAttacking)
                    isAttacking = true;
            }
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void Attack(GameObject target)
    {
        if (target.tag == "Player")
        {
            character.Attack(delegate { AttackCallback(); });   //calls the attack damage calculations at the proper point in the swing
        }
    }

    public void AttackCallback()
    {
        RaycastHit ray;
        if (Physics.Raycast(transform.position, transform.forward, out ray, attackDistance))
        {
            if (ray.collider.gameObject.tag == "Player")
            {
                ray.collider.gameObject.SendMessage("TakeDamage", attackDamage);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        print("took " + damage + " damage.");
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
