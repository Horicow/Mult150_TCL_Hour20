using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public GameManager manager;
    public float moveSpeed = 20f;
    public float timeAmount = 1.5f;
    public bool isPowerUp = false;  // Flag to determine if this is a power-up

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
    }

    // Handle collisions with the player
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isPowerUp)  // If it's a power-up
            {
                manager.AdjustTime(timeAmount);  // Add time
                Debug.Log("Player picked up a power-up!");
            }
            else  // If it's an obstacle
            {
                manager.AdjustTime(-timeAmount);  // Subtract time
                Debug.Log("Player hit an obstacle!");
            }

            Destroy(gameObject);  // Destroy the power-up or obstacle
        }
    }
}
