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
	}

    public void UpdateStats()
    {
        strength.text = "Strength: " + PlayerManager.playerManager.totalStrength;
        agility.text = "Agility: " + PlayerManager.playerManager.totalAgility;
        intelligence.text = "Intelligence: " + PlayerManager.playerManager.totalIntelligence;
    }
}
