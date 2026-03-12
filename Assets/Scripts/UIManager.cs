using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject victoryPanel;
    public GameObject gameOverPanel;
    public AudioSource mainMusic;
    public GameObject scoreText;    
    public GameObject healthText;  

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

        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

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

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}