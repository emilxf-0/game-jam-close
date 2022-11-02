using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnProjectile), 2, 0.2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnProjectile()
    {
        var spawnPoint = new Vector2(Random.Range(-9, 9), 5);
        Instantiate(projectile, spawnPoint, Quaternion.Euler(0, 0, 0));
    }
}
