using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    public GameManager manager;
    public Material normalMat;  // Normal material for the player
    public Material phasedMat;  // Phased material for the player

    [Header("Gameplay")]
    public float bounds = 3f;   // Boundaries for player movement
    public float strafeSpeed = 4f;  // Speed for horizontal movement
    public float phaseCooldown = 2f; // Cooldown for phasing

    private Renderer meshRenderer;
    private Collider playerCollider;
    private bool canPhase = true;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();  // If the Renderer is on the player object
        if (meshRenderer == null)
        {
            // If Renderer isn't found on the Player object, try getting it from child objects
            meshRenderer = GetComponentInChildren<Renderer>();
        }

        playerCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement logic
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * strafeSpeed;
        Vector3 position = transform.position;
        position.x += xMove;

        position.x = Mathf.Clamp(position.x, -bounds, bounds);
        transform.position = position;

        // Phasing logic (when the player presses the jump button)
        if (Input.GetButtonDown("Jump") && canPhase)
        {
            canPhase = false;
            PhaseOut();  // Phasing out
            Invoke("PhaseIn", phaseCooldown);  // Phasing back in after cooldown
        }
    }

    // Phase the player out (change material and disable collider)
    private void PhaseOut()
    {
        if (meshRenderer != null)  // Check if the Renderer is available
        {
            meshRenderer.material = phasedMat;  // Change material to phased one
        }
        else
        {
            Debug.LogWarning("Renderer not found on Player. Make sure the Player has a Renderer component.");
        }
        playerCollider.enabled = false;  // Disable collider during phase-out
        Debug.Log("Player phased out!");
    }

    // Phase the player back in (reset material and enable collider)
    private void PhaseIn()
    {
        if (meshRenderer != null)  // Check if the Renderer is available
        {
            meshRenderer.material = normalMat;  // Change material back to normal
        }
        else
        {
            Debug.LogWarning("Renderer not found on Player. Make sure the Player has a Renderer component.");
        }
        playerCollider.enabled = true;  // Enable collider during phase-in
        canPhase = true;
        Debug.Log("Player phased in!");
    }

    // Method to adjust time (called from Collidable script)
    public void AdjustTime(float amount)
    {
        manager.AdjustTime(amount);
    }
}
