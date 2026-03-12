using UnityEngine;
using UnityEngine.SceneManagement;

// MainMenu class handles the main menu UI interactions.
// Provides functionality to start the game or exit the application.
public class MainMenu : MonoBehaviour
{
    // Called when the player presses the "Start Game" button.
    // Loads the main gameplay scene.
    public void LancerJeu()
    {
        SceneManager.LoadScene("Main");
    }
    
    // Called when the player presses the "Quit" button.
    // Closes the application.
    public void Quitter()
    {
        Application.Quit();
    }
}