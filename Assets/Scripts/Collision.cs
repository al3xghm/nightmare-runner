using System.Collections;
using UnityEngine;

// Collision class that manages the player's health, invincibility, and game over state.
// Handles collision detection with obstacles and applies damage with visual/audio feedback.
public class Collision : MonoBehaviour
{
    // ===== Health Settings =====
    // Maximum number of lives the player can have
    [Header("Health Settings")]
    public int maxLives = 3;
    
    // Current remaining lives of the player
    private int currentLives;

    // ===== Invincibility Settings =====
    // Duration in seconds for which the player is invincible after taking damage
    [Header("Invincibility Settings")]
    public float invincibilityDuration = 2f;
    
    // Flag indicating whether the player is currently in invincible state
    private bool isInvincible = false;

    // ===== Blink Settings =====
    // Time interval in seconds between each renderer blink during invincibility
    [Header("Blink Settings")]
    public float blinkInterval = 0.1f;
    
    // Array of all renderer components on the player and its children for blinking effect
    private Renderer[] playerRenderers;

    // ===== Sound Settings =====
    // Audio clip played when the player takes damage
    [Header("Sound Settings")]
    public AudioClip hitSound;
    
    // AudioSource component for playing sound effects
    private AudioSource audioSource;

    // Initializes player state: sets current lives to max, collects all renderers for blinking,
    // and gets the audio source. Logs warnings if required components are missing.
    private void Start()
    {
        currentLives = maxLives;
        playerRenderers = GetComponentsInChildren<Renderer>(true);
        audioSource = GetComponent<AudioSource>();

        // Check if renderers were found for the blinking effect
        if (playerRenderers.Length == 0)
        {
            Debug.LogWarning("Aucun Renderer trouvé sur le joueur ou ses enfants !");
        }
        
        // Check if audio source is available for sound effects
        if (audioSource == null)
        {
            Debug.LogWarning("Pas d'AudioSource sur le Player !");
        }
    }

    // Called when this collider enters a trigger collider.
    // Checks if collision is with an obstacle and applies damage if player is not invincible.
    private void OnTriggerEnter(Collider other)
    {
        // Only take damage from obstacles when not invincible
        if (other.tag == "Obstacle" && !isInvincible)
        {
            TakeDamage();
        }
    }

    // Reduces current lives by 1, plays hit sound, and triggers either game over or invincibility period.
    private void TakeDamage()
    {
        currentLives--;

        // Play the hit sound effect if both the clip and audio source exist
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        
        // Log the damage intake for debugging purposes
        Debug.Log($"Vie perdue ! Vies restantes : {currentLives}/{maxLives}");

        // Check if player is out of lives
        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            // Start the invincibility period with blinking effect
            StartCoroutine(BecomeInvincible());
        }
    }

    // Coroutine that makes the player invincible for a duration and applies a blinking visual effect.
    // The player blinks off and on periodically during the invincibility period.
    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        
        float elapsedTime = 0f;
        
        // Blink the player on and off until invincibility duration expires
        while (elapsedTime < invincibilityDuration)
        {
            // Toggle visibility of all renderer components
            foreach (Renderer r in playerRenderers)
            {
                r.enabled = !r.enabled;
            }
            
            // Wait for the blink interval before toggling again
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
        
        // Ensure the player is visible at the end of invincibility
        foreach (Renderer r in playerRenderers)
        {
            r.enabled = true;
        }
        
        // Exit invincible state
        isInvincible = false;
        
        // Log the end of invincibility period for debugging
        Debug.Log("Invincibilité terminée !");
    }

    // Called when the player runs out of lives. Triggers the game over screen through the UI manager.
    private void GameOver()
    {
        // Display the game over screen if UIManager instance exists
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowGameOver();
        }
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public int GetMaxLives()
    {
        return maxLives;
    }
}