using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool GameStarted = false;

    [Header("UI Panels")]
    public GameObject introPanel;
    public GameObject victoryPanel;
    public GameObject gameOverPanel;

    [Header("Intro Settings")]
    public Text introText;
    public Text skipText;
    public float introDuration = 5f;

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

        GameStarted = false;
        Time.timeScale = 1;

        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        Debug.Log("PlayIntro started");
        
        if (skipText != null)
        {
            skipText.gameObject.SetActive(true);
            Debug.Log("SkipText activated");
        }

        if (introText != null)
        {
            Debug.Log("IntroText found! Setting test text...");
            introText.text = "TEST - Si vous voyez ceci, le texte fonctionne!";
            introText.color = Color.red;
            yield return new WaitForSecondsRealtime(2f);
            
            introText.text = "";
            introText.color = Color.white;
            yield return new WaitForSecondsRealtime(0.5f);

            yield return StartCoroutine(TypeText("Tu es dans ton pire cauchemar...", 0.05f));
            yield return new WaitForSecondsRealtime(1.5f);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SkipIntro();
                yield break;
            }

            introText.text = "";
            yield return new WaitForSecondsRealtime(0.3f);

            yield return StartCoroutine(TypeText("Collecte 30 peluches pour t'échapper !", 0.05f));
            yield return new WaitForSecondsRealtime(1.5f);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SkipIntro();
                yield break;
            }

            introText.text = "";
            yield return new WaitForSecondsRealtime(0.3f);

            yield return StartCoroutine(TypeText("Cours pour ta survie !", 0.05f));
            yield return new WaitForSecondsRealtime(1f);
        }
        else
        {
            yield return new WaitForSecondsRealtime(introDuration);
        }

        if (skipText != null)
        {
            skipText.gameObject.SetActive(false);
        }

        if (introPanel != null) introPanel.SetActive(false);
        StartGame();
    }

    private IEnumerator TypeText(string message, float typingSpeed)
    {
        Debug.Log("TypeText called with: " + message);
        introText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            introText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        Debug.Log("TypeText finished. Final text: " + introText.text);
    }

    private void Update()
    {
        if (introPanel != null && introPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            SkipIntro();
        }
    }

    private void SkipIntro()
    {
        StopAllCoroutines();
        if (introPanel != null) introPanel.SetActive(false);
        if (skipText != null) skipText.gameObject.SetActive(false);
        StartGame();
    }

    public void StartGame()
    {
        GameStarted = true;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Victory()
    {
        GameStarted = false;
        Time.timeScale = 0;

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameOver()
    {
        GameStarted = false;
        Time.timeScale = 0;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        GameStarted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
