using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawnerController : MonoBehaviour
{
    public bool shouldSpawn;
    public float timeToSpawn;
    public GameObject enemy;
    public Transform firePoint;
    public EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        shouldSpawn = false;
        timeToSpawn = 0;
        enemyController = new EnemyController();
    }
    
    // Update is called once per frame
    void Update()
    {
        timeToSpawn += Time.deltaTime;

        if (timeToSpawn >= 5)
        {
            GameObject enemyInstance = Instantiate(enemy, firePoint.position, firePoint.rotation);
            timeToSpawn = 0;
        }
    }
}
