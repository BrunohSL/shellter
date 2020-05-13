using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStarController : MonoBehaviour {

    public float speed;

    public PlayerController player;

    public EnemyController enemy;

    public bool enemyHit;

    public GameObject enemyDeathEffect;

    public GameObject impactEffect;

    public int pointsForKill;

    public float rotationSpeed;

    public int damageToGive;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        enemyHit = false;

        if (player.transform.localScale.x < 0) { //if player is facing left
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);

        GetComponent<Rigidbody2D>().angularVelocity = rotationSpeed;

        if (enemyHit == true)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            enemy.enemyHealth -= 1;
            //enemyHit = false;
        }
	}

    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Enemy") {
            enemyHit = true;
            //Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            //enemy.enemyHealth--;
        }
        if(other.tag != "Player" && other.tag != "Enemy") { 
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

}
