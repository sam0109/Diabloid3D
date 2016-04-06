using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerManager;
    public int baseStrength, baseAgility, baseIntelligence, level;
    public int totalStrength, totalAgility, totalIntelligence, armor, attackSpeed;
    public float attackDelay;
    public int attackDistance;
    public int attackDamage;
    int weaponDamage;

    float maxHealth;
    float currentHealth;
    HealthManager healthBar;
    StatsManager statsManager;
    AbilityManager abilityManager;

	// Use this for initialization
	void Start () {
        playerManager = this;
        healthBar = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<HealthManager>();
        statsManager = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<StatsManager>(includeInactive: true);
        abilityManager = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<AbilityManager>(includeInactive: true);
        RecalculateIntrinsicStats();
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth / maxHealth);
    }

    public void MeleeAttack()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(Mathf.Max(currentHealth / maxHealth, 0));
    }

    public void UpdateItemStats()
    {
        totalAgility = baseAgility;
        totalIntelligence = baseIntelligence;
        totalStrength = baseStrength;
        armor = 0;
        attackSpeed = 150;
        attackDistance = 1;
        weaponDamage = 0;

        bool weaponEquipped = false;
        for (int i = 0; i < Inventory.inventory.paperDoll.Length; i++)
        {
            Item temp = Inventory.inventory.GetItemInSlot(i);
            switch (temp.itemType)
            {
                case (ItemType.Bow):
                case (ItemType.Axe):
                case (ItemType.Hammer):
                case (ItemType.Sword):
                    if (weaponEquipped)
                    {
                        attackDistance = Mathf.Min(attackDistance, temp.range);
                        attackSpeed = attackSpeed + temp.speed;
                        weaponDamage = (temp.damage + weaponDamage) / 4; //divide by 2 to average, divide by 2 more to account for extra speed.
                    }
                    else
                    {
                        attackDistance = temp.range;
                        attackSpeed = temp.speed;
                        weaponDamage = temp.damage;
                    }
                    weaponEquipped = true;
                    break;
                case (ItemType.Body):
                case (ItemType.Feet):
                case (ItemType.Hands):
                case (ItemType.Helmet):
                case (ItemType.Ring):
                case (ItemType.Shield):
                    armor += temp.armor;
                    totalAgility += temp.agility;
                    totalIntelligence += temp.intelligence;
                    totalStrength += temp.strength;
                    break;
                case (ItemType.None):
                    if (temp.name != "")
                    {
                        Debug.Log("No item type! Cannot set equipped stats.");
                    }
                    break;
                default:
                    Debug.Log("Unrecognized item type equipped");
                    break;
            }
        }
        RecalculateIntrinsicStats();
    }

    public void RecalculateIntrinsicStats()
    {
        maxHealth = level * (totalStrength + 5);
        if (attackSpeed <= 10)
        {
            attackDelay = 10;
        }
        else
        {
            attackDelay = 100 / attackSpeed;
        }
        if (statsManager)
        {
            statsManager.UpdateStats();
        }
        attackDamage = weaponDamage + totalStrength;
    }
}
