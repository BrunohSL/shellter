using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour {
    public RockSpawnerController rockSpawner;
    public PlayerController playerController;

    public int damageToGive;
    public float speed;
    public bool rockHit;

    void Start() {
        rockHit = false;
        playerController = FindObjectOfType<PlayerController>();
        rockSpawner = FindObjectOfType<RockSpawnerController>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Player") {
            Destroy(gameObject);
        }

        if (other.name == "Player" && playerController.playerPressingDown) {
            if (!playerController.facingRight) {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-20, 5, 0);
            }

            if (playerController.facingRight) {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(20, 5, 0);
            }
        }

        if (other.name == "Player" && playerController.playerPressingDown == false) {
            HealthManager.playerHealth--;
        }
    }
}
