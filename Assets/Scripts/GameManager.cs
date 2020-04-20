using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public int MaximumEnemies = 10;
    public int MaximumEnemiesAlive = 3;
    public int EnemiesSpawned = 0;

    [SerializeField]
    private GameObject entityPrefab;

    private float minEnemyX = 166;
    private float maxEnemyX = 324;
    private float minEnemyZ = 196;
    private float maxEnemyZ = 300;
    private float maxEnemyToughness = 4f;
    private float maxEnemySpeed = 20f;
    private float maxEnemyTurnSpeed = 100f;

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("spawnEnemy", 0f, 2f);
    }

    void spawnEnemy() {
        // All the entities minus 1 (to account for the player itself).
        var enemies = FindObjectsOfType<CollidableEntityController>();
        int curEnemies = enemies.Length - 1;

        for (int i = 0; i < MaximumEnemiesAlive - curEnemies; i++) {
            if (EnemiesSpawned < MaximumEnemies) {
                // Create a set of parameters to determine enemy's difficult.
                float enemyX = Random.Range(minEnemyX, maxEnemyX);
                float enemyZ = Random.Range(minEnemyZ, maxEnemyZ);
                float enemyRot = Random.Range(0, 360);
                float enemyToughness = Mathf.Floor(Random.Range(1f, maxEnemyToughness)) / 10;
                float enemySpeed = Random.Range(15, maxEnemySpeed);
                float enemyTurnSpeed = Random.Range(40, maxEnemyTurnSpeed);

                // Create a new enemy.
                var enemy = Instantiate(entityPrefab, new Vector3(enemyX, 1, enemyZ), Quaternion.Euler(0, enemyRot, 0));

                // Modify the parameters of the health controller accordingly.
                var healthController = enemy.GetComponent<HealthController>();
                healthController.maxHealth = enemyToughness;
                healthController.health = enemyToughness;

                // Modify the parameters of the collision controller accordingly.
                var controller = enemy.GetComponent<CollidableEntityController>();
                controller.speed = enemySpeed;
                controller.turnSpeed = enemyTurnSpeed;

                // Limiting the numebr of enemies to a given maximum.
                EnemiesSpawned++;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
