using System.Collections;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxLives = 3;
    private int currentLives;

    [Header("Invincibility Settings")]
    public float invincibilityDuration = 2f;
    private bool isInvincible = false;

    [Header("Blink Settings")]
    public float blinkInterval = 0.1f;
    private Renderer playerRenderer;

    private void Start()
    {
        currentLives = maxLives;
        playerRenderer = GetComponent<Renderer>();
        
        if (playerRenderer == null)
        {
            playerRenderer = GetComponentInChildren<Renderer>();
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
            if (playerRenderer != null)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
            }
            
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
        
        if (playerRenderer != null)
        {
            playerRenderer.enabled = true;
        }
        
        isInvincible = false;
        
        Debug.Log("Invincibilité terminée !");
    }

    private void GameOver()
    {
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