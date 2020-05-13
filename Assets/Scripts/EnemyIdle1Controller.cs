using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle1Controller : MonoBehaviour
{
    //Enemy Health Manager
    public int enemyHealth;
    public GameObject deathEffect;
    public int pointsOnDeath;
    // --------------------------------------

    public NinjaStarController spit;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy health Manager
        if (enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            ScoreManager.addPoints(pointsOnDeath);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Spit")
        {
            enemyHealth--;
        }
    }
}
