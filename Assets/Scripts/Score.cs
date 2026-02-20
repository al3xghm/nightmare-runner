using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int ScoreInt;
    public Text ScoreText;
    public int victoryThreshold = 30;

    public void ScorePlusOne()
    {
        ScoreInt++;

        if (ScoreInt >= victoryThreshold && UIManager.Instance != null)
        {
            UIManager.Instance.ShowVictory();
        }
    }

    private void Update()
    {
        ScoreText.text = ScoreInt.ToString() + " / " + victoryThreshold.ToString();
    }
}
