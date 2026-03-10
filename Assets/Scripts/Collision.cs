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
    private Renderer[] playerRenderers;

    private void Start()
    {
        currentLives = maxLives;
        playerRenderers = GetComponentsInChildren<Renderer>(true);

        if (playerRenderers.Length == 0)
        {
            Debug.LogWarning("Aucun Renderer trouvé sur le joueur ou ses enfants !");
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