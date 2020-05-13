using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawnerController : MonoBehaviour
{
    public float timeToShoot;
    public GameObject rock;
    public Transform firePoint;
    public float shootInterval;
    // Start is called before the first frame update
    void Start()
    {
        timeToShoot = 0;
    }

    // Update is called once per frame
    void Update()
    {

        timeToShoot += Time.deltaTime;

        if (timeToShoot >= shootInterval)
        {
            GameObject rockInstance = Instantiate(rock, firePoint.position, firePoint.rotation);
            timeToShoot = 0;
        }
    }
}
