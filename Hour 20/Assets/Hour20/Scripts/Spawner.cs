using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject PowerUpPrefab;   // Power-up prefab
    public GameObject ObstaclePrefab;  // Original obstacle prefab
    public GameObject CoolerLampPrefab; // New Cooler Lamp prefab
    public float spawnCycle = 0.5f;   // Time interval between spawns

    GameManager manager;
    float elapsedTime;
    bool spawnPowerUp = true;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > spawnCycle)
        {
            GameObject temp;

            // Alternate between PowerUp, Obstacle, or Cooler Lamp
            if (spawnPowerUp)
            {
                temp = Instantiate(PowerUpPrefab) as GameObject;
            }
            else
            {
                // Randomly choose between the Obstacle and Cooler Lamp
                int spawnType = Random.Range(0, 2);
                if (spawnType == 0)
                {
                    temp = Instantiate(ObstaclePrefab) as GameObject;  // Spawn Obstacle
                }
                else
                {
                    temp = Instantiate(CoolerLampPrefab) as GameObject;  // Spawn Cooler Lamp
                }
            }

            // Set random X position for spawned objects
            Vector3 position = temp.transform.position;
            position.x = Random.Range(-3f, 3f);  // Random spawn position on X-axis
            temp.transform.position = position;

            // Assign the manager to the spawned object
            Collidable col = temp.GetComponent<Collidable>();
            if (col != null)
            {
                col.manager = manager;
            }

            // Reset the timer and alternate spawn (PowerUp vs. Obstacle/Cooler Lamp)
            elapsedTime = 0;
            spawnPowerUp = !spawnPowerUp;  // Toggle between PowerUp and Obstacles (and Cooler Lamp)
        }
    }
}
