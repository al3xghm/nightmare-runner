using System.Collections;
using UnityEngine;
using OscJack;

// Collision class that manages the player's health, invincibility, and game over state.
// Handles collision detection with obstacles and applies damage with visual/audio feedback.
public class Collision : MonoBehaviour
{
    // ===== Health Settings =====
    [Header("Health Settings")]
    public int maxLives = 3;
    private int currentLives;

    // ===== Invincibility Settings =====
    [Header("Invincibility Settings")]
    public float invincibilityDuration = 2f;
    private bool isInvincible = false;

    // ===== Blink Settings =====
    [Header("Blink Settings")]
    public float blinkInterval = 0.1f;
    private Renderer[] playerRenderers;

    // ===== Sound Settings =====
    [Header("Sound Settings")]
    public AudioClip hitSound;
    private AudioSource audioSource;

    // ===== OSC Settings =====
    [Header("OSC Settings")]
    public OscConnection oscConnection;

    private void Start()
    {
        currentLives = maxLives;
        playerRenderers = GetComponentsInChildren<Renderer>(true);
        audioSource = GetComponent<AudioSource>();

        if (playerRenderers.Length == 0)
        {
            Debug.LogWarning("Aucun Renderer trouvé sur le joueur ou ses enfants !");
        }
        
        if (audioSource == null)
        {
            Debug.LogWarning("Pas d'AudioSource sur le Player !");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" && !isInvincible)
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentLives--;

        // Envoie signal OSC collision avec vies restantes
        if (oscConnection != null)
        {
            var client = new OscClient(oscConnection.host, oscConnection.port);
            client.Send("/collision", currentLives);
        }

        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        
        Debug.Log($"Vie perdue ! Vies restantes : {currentLives}/{maxLives}");

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(BecomeInvincible());
        }
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        
        float elapsedTime = 0f;
        
        while (elapsedTime < invincibilityDuration)
        {
            foreach (Renderer r in playerRenderers)
            {
                r.enabled = !r.enabled;
            }
            
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
        
        foreach (Renderer r in playerRenderers)
        {
            r.enabled = true;
        }
        
        isInvincible = false;
        
        Debug.Log("Invincibilité terminée !");
    }

    private void GameOver()
    {
        // Envoie signal OSC game over
        if (oscConnection != null)
        {
            var client = new OscClient(oscConnection.host, oscConnection.port);
            client.Send("/gameover", 1);
        }

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