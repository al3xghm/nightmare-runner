using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("References")]
    public Collision playerCollision;
    public Text healthText;

    [Header("Display Settings")]
    public string healthPrefix = "♥ ";
    public Color fullHealthColor = Color.white;
    public Color lowHealthColor = Color.red;
    public int lowHealthThreshold = 1;

    private void Start()
    {
        if (playerCollision == null)
        {
            playerCollision = FindFirstObjectByType<Collision>();
        }

        if (healthText == null)
        {
            healthText = GetComponent<Text>();
        }
    }

    private void Update()
    {
        if (playerCollision != null && healthText != null)
        {
            int currentLives = playerCollision.GetCurrentLives();
            int maxLives = playerCollision.GetMaxLives();

            string hearts = "";
            for (int i = 0; i < currentLives; i++)
            {
                hearts += healthPrefix;
            }

            healthText.text = hearts;

            if (currentLives <= lowHealthThreshold)
            {
                healthText.color = lowHealthColor;
            }
            else
            {
                healthText.color = fullHealthColor;
            }
        }
    }
}
