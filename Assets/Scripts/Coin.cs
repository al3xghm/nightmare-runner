using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Coin class that handles coin collection in the game.
/// When collected by the player, it increases the score and plays a sound effect.
/// </summary>
public class Coin : MonoBehaviour
{
    // Reference to the Score script to update the player's score
    private Score ScoreText;
    
    // Sound effect played when the coin is collected
    public AudioClip collectSound; 
    // Initializes the coin by finding the ScoreText object and getting its Score component
    private void Start()
    {
        ScoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Score>();
    }

    // Continuously rotates the coin around the Z axis for visual effect
    private void Update()
    {
        gameObject.transform.Rotate(0, 0, 0.5f);
    }

    // Called when another collider enters the trigger zone of this coin
    // Increases the score, plays the collection sound, and destroys the coin
    private void OnTriggerEnter(Collider other)
    {
        ScoreText.ScorePlusOne();
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        Destroy(gameObject);
    }
}
