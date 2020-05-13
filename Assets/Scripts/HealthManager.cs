using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    public int playerMaxHealth;
    public static int playerHealth;
    Text text;
    private LevelManager levelManager;
    public bool isDead;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        playerHealth = playerMaxHealth;

        levelManager = FindObjectOfType<LevelManager>();

        isDead = false;
    }

	// Update is called once per frame
	void Update () {
		if(playerHealth <= 0 && !isDead) {
            playerHealth = 0;
            levelManager.respawnPlayer();
            isDead = true;
        }
        text.text = "" + playerHealth;
	}

    public static void hurtPlayer(int damageToGive) {
        playerHealth -= damageToGive;
    }

    public void fullHealth() {
        playerHealth = playerMaxHealth;
    }
}
