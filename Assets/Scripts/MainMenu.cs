using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LancerJeu()
    {
        // Charge la scène qui s'appelle exactement "Main"
        // (Vérifie que ta scène de jeu s'appelle bien "Main" dans tes dossiers !)
        SceneManager.LoadScene("Main");
    }
    
    public void Quitter()
    {
        Application.Quit();
    }
}