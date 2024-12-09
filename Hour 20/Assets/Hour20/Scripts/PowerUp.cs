using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Material[] materials;  // Array to store different materials (colors)
    public float moveSpeed = 5f;  // Speed of movement (straight line)

    private Renderer powerUpRenderer;
    private bool isChanging = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Renderer component to change color
        powerUpRenderer = GetComponent<Renderer>();

        // Start changing color and shape if you want
        ChangePowerUpAppearance();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the PowerUp straight forward (along the Z-axis)
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    // This method will handle color, shape, and movement changes
    void ChangePowerUpAppearance()
    {
        if (!isChanging && materials.Length > 0)  // Ensure there are materials
        {
            StartCoroutine(AnimatePowerUp());
        }
        else
        {
            Debug.LogWarning("Materials array is empty or missing.");
        }
    }

    // Coroutine to change appearance
    IEnumerator AnimatePowerUp()
    {
        isChanging = true;

        // Change color (randomly choose a color)
        powerUpRenderer.material = materials[Random.Range(0, materials.Length)];

        // Change shape (scale it randomly)
        float randomScale = Random.Range(0.5f, 1.5f);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        // Wait for a moment before changing again
        yield return new WaitForSeconds(1f);

        // Change color again after a delay
        isChanging = false;
    }
}
