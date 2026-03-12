using UnityEngine;
using UnityEngine.UI;

// Score class tracks the player's collected items and updates the on-screen score display.
// It also checks for a victory condition when a threshold is reached.
public class Score : MonoBehaviour
{
    // internal score counter
    private int ScoreInt;
    
    // UI Text element that shows the current score
    public Text ScoreText;
    
    // number of points required to trigger victory
    public int victoryThreshold = 30;

    // Increments the score by one and checks for victory condition
    public void ScorePlusOne()
    {
        ScoreInt++;

        // if threshold reached, notify UIManager to display victory screen
        if (ScoreInt >= victoryThreshold && UIManager.Instance != null)
        {
            UIManager.Instance.ShowVictory();
        }
    }

    // Every frame, update the score text in the UI
    private void Update()
    {
        ScoreText.text = ScoreInt.ToString() + " / " + victoryThreshold.ToString();
    }
}
