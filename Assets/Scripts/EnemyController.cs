using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Enemy Patroll
    public float moveSpeed;
    public bool moveRight;
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;

    //public Transform edgeCheck;
    // --------------------------------------

    //Enemy Health Manager
    public int enemyHealth;
    public GameObject deathEffect;
    public int pointsOnDeath;
    // --------------------------------------

    public NinjaStarController spit;
    public bool enemyDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy Patrol
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall)
            moveRight = !moveRight;

        if (moveRight)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        // --------------------------------

        // Enemy health Manager
        if (enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            ScoreManager.addPoints(pointsOnDeath);
            Destroy(gameObject);
        }
        // ---------------------------------

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Spit")
        {
            enemyHealth--;
        }

        if (other.tag == "Spike")
        {
            Destroy(gameObject);
            enemyDestroyed = true;
        }
    }
}
