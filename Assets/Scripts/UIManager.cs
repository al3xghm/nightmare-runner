using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// UIManager handles the game's UI states such as victory, game over and restarting.
// It uses a simple singleton pattern so other scripts can easily invoke UI changes.
public class UIManager : MonoBehaviour
{
    // singleton instance reference
    public static UIManager Instance;

    // UI panels and elements to toggle on/off
    public GameObject victoryPanel;
    public GameObject gameOverPanel;
    public AudioSource mainMusic;
    public GameObject scoreText;    
    public GameObject healthText;  

    // Awake sets up the singleton and ensures both end panels are hidden initially
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // hide panels on start
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    // Display the victory UI: stop music, hide gameplay HUD, pause the game, show victory panel, and unlock cursor
    public void ShowVictory()
    {
        if (mainMusic != null) mainMusic.Stop();
        if (scoreText != null) scoreText.SetActive(false);
        if (healthText != null) healthText.SetActive(false);
        Time.timeScale = 0;
        if (victoryPanel != null) victoryPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Display the game over UI with similar steps to ShowVictory
    public void ShowGameOver()
    {
        if (mainMusic != null) mainMusic.Stop();
        if (scoreText != null) scoreText.SetActive(false);
        if (healthText != null) healthText.SetActive(false);
        Time.timeScale = 0;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Restart the current scene and resume normal time scale
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}