using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {
    public Text strength;
    public Text agility;
    public Text intelligence;

	// Use this for initialization
	void Start ()
    {
        UpdateStats();
        PlayerManager.playerManager.statsChanged += UpdateStats;
	}

    public void UpdateStats()
    {
        strength.text = "Strength: " + PlayerManager.playerManager.stats.strength + " + " + PlayerManager.playerManager.strengthMod;
        agility.text = "Agility: " + PlayerManager.playerManager.stats.agility+ " + " + PlayerManager.playerManager.agilityMod;
        intelligence.text = "Intelligence: " + PlayerManager.playerManager.stats.intelligence + " + " + PlayerManager.playerManager.intMod;
    }
}
