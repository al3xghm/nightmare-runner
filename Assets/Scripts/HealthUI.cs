using UnityEngine;
using UnityEngine.UI;

// HealthUI class that displays the player's current health/lives as text and symbols on the UI.
// Updates the health display in real-time and changes color based on remaining lives.
public class HealthUI : MonoBehaviour
{
    // ===== References =====
    // Reference to the player's Collision component to get current and max lives
    [Header("References")]
    public Collision playerCollision;
    
    // UI Text element to display health symbols
    public Text healthText;

    // ===== Display Settings =====
    // Symbol displayed for each life (default is heart symbol)
    [Header("Display Settings")]
    public string healthPrefix = "♥ ";
    
    // Color displayed when player has normal health
    public Color fullHealthColor = Color.white;
    
    // Color displayed when player's health is low (warning color)
    public Color lowHealthColor = Color.red;
    
    // Threshold: if lives are at or below this value, display in low health color
    public int lowHealthThreshold = 1;

    // Initializes HealthUI by finding references to the player collision component and health text display
    private void Start()
    {
        // Try to find the player's Collision component if not already assigned
        if (playerCollision == null)
        {
            playerCollision = FindFirstObjectByType<Collision>();
        }

        // Try to find the Text component on this game object if not already assigned
        if (healthText == null)
        {
            healthText = GetComponent<Text>();
        }
    }

    // Updates the health display every frame
    private void Update()
    {
        // Only update if both references are valid
        if (playerCollision != null && healthText != null)
        {
            // Get current and maximum lives from the player
            int currentLives = playerCollision.GetCurrentLives();
            int maxLives = playerCollision.GetMaxLives();

            // Build health string: create one symbol per remaining life
            string hearts = "";
            for (int i = 0; i < currentLives; i++)
            {
                hearts += healthPrefix;
            }

            // Update the UI text with the health symbols
            healthText.text = hearts;

            // Change color based on remaining lives
            if (currentLives <= lowHealthThreshold)
            {
                // Display warning color if health is low
                healthText.color = lowHealthColor;
            }
            else
            {
                // Display normal color if health is good
                healthText.color = fullHealthColor;
            }
        }
    }
}
