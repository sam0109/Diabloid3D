using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
    public float maxHealth;
    float currentHealth;
    HealthManager healthBar;
	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        healthBar = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<HealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(Mathf.Max(currentHealth / maxHealth, 0));
    }

    void RestoreHealth(float heal)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + heal);
        healthBar.SetHealth(currentHealth / maxHealth);
    }
}
