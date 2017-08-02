﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ThirdPersonCharacterLook))]
public abstract class CharacterManager : MonoBehaviour {

    [SerializeField]
    public CharacterStat stats;
    public int strengthMod { get; protected set; }
    public int agilityMod { get; protected set; }
    public int intMod { get; protected set; }
    public int armor { get; protected set; }
    [SerializeField]
    public int attackSpeed;
    [SerializeField]
    public float attackDelay;
    public float weaponReach;
    [SerializeField]
    public float totalReach;
    public int attackDamage { get; protected set; }
    public int weaponDamageMod { get; protected set; }

    public delegate void StatsChangeHandler();
    public event StatsChangeHandler statsChanged;

    [SerializeField]
    protected float attackTimer;
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float currentHealth;
    protected bool isDead;
    HealthManager healthBar;
    ThirdPersonCharacterLook m_Character; // A reference to the ThirdPersonCharacter on the object

    public void Start()
    {
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacterLook>();
        if (gameObject.tag == "Player")
        {
            healthBar = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<HealthManager>();
        }
        RecalculateStats(true);
        isDead = false;
    }

    public virtual void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetHealth(Mathf.Max(currentHealth / maxHealth, 0));
        }
    }

    public virtual void Attack()
    {
        if (attackTimer <= 0)
        {
            attackTimer = attackDelay;
            m_Character.Attack(delegate { AttackCallback(); });   //calls the attack damage calculations at the proper point in the swing
        }
    }

    public virtual void AttackCallback()
    {
        RaycastHit ray;
        if (Physics.Raycast(transform.position + transform.up * .5f, transform.forward, out ray, totalReach))
        {
            ray.collider.gameObject.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
            Debug.Log("dealt " + attackDamage + " damage");
        }

        // helper to visualise the ray in the scene view
        Debug.DrawLine(transform.position + transform.up * .5f, transform.position + transform.forward * totalReach + transform.up * .5f, Color.white, 1f);
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            m_Character.Die();
            isDead = true;
        }
        if (healthBar != null)
        {
            healthBar.SetHealth(Mathf.Max(currentHealth / maxHealth, 0));
        }
        m_Character.TakeDamage();
    }

    public virtual void RecalculateStats(bool resetHealth)
    {
        maxHealth = stats.level * (stats.strength + strengthMod + 5);
        attackDamage = weaponDamageMod + stats.strength + strengthMod;
        totalReach = weaponReach + 0.5f;

        attackDelay = 100f / attackSpeed;

        if (statsChanged != null)
        {
            statsChanged();
        }

        if (resetHealth)
        {
            currentHealth = maxHealth;
            if (healthBar != null)
            {
                healthBar.SetHealth(currentHealth / maxHealth);
            }
        }
    }

    public void Move(Vector2 direction, float angle)
    {
        m_Character.Move(direction, angle);
    }
}