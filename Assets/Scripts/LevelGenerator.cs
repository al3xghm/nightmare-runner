using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LevelGenerator class that procedurally generates infinite level platforms.
// Tiles are spawned randomly as the player moves forward, and the game speed increases over time.
public class LevelGenerator : MonoBehaviour
{
    // Tile prefab variant 1 for random platform generation
    public GameObject Tile1;
    
    // Tile prefab variant 2 for random platform generation
    public GameObject Tile2;
    
    // Starting tile used at the beginning of the level
    public GameObject StartTile;

    // ===== Speed Settings =====
    // Initial game speed when level starts
    [Header("Speed Settings")]
    public float startSpeed = 4f;
    
    // Maximum speed the game can reach
    public float maxSpeed = 12f;
    
    // Rate at which speed increases per second
    public float speedIncreaseRate = 0.5f;

    // Tracks the next position where new tiles should be generated
    private float Index = 0;
    
    // Current game speed that affects tiles and camera movement
    private float currentSpeed;

    // Initializes the level by spawning starting tiles at different positions
    private void Start()
    {
        // Set initial speed to the configured start speed
        currentSpeed = startSpeed;
        
        // Spawn 5 start tiles in a line to create the initial playing area
        GameObject StartPlane1 = Instantiate(StartTile, transform);
        StartPlane1.transform.position = new Vector3(7, 0, 0);
        
        GameObject StartPlane2 = Instantiate(StartTile, transform);
        StartPlane2.transform.position = new Vector3(-1, 0, 0);
        
        GameObject StartPlane3 = Instantiate(StartTile, transform);
        StartPlane3.transform.position = new Vector3(-9, 0, 0);
        
        GameObject StartPlane4 = Instantiate(StartTile, transform);
        StartPlane4.transform.position = new Vector3(-17, 0, 0);
        
        GameObject StartPlane5 = Instantiate(StartTile, transform);
        StartPlane5.transform.position = new Vector3(-25, 0, 0);
    }

    // Updates game speed and generates new tiles as the player progresses
    private void Update()
    {
        // Gradually increase speed until max speed is reached
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncreaseRate * Time.deltaTime;
            // Clamp speed to never exceed max speed
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }

        // Move the level forward by moving the generator at current speed
        gameObject.transform.position += new Vector3(currentSpeed * Time.deltaTime, 0, 0);

        // Check if it's time to spawn new tiles
        if(transform.position.x >= Index)
        {
            // Randomly choose between Tile1 and Tile2 for first platform
            int RandomInt1 = Random.Range(0, 2);

            if(RandomInt1 == 1)
            {
                GameObject TempTile1 = Instantiate(Tile1, transform);
                TempTile1.transform.position = new Vector3(-16, 0, 0);
            }
            else if(RandomInt1 == 0)
            {
                GameObject TempTile1 = Instantiate(Tile2, transform);
                TempTile1.transform.position = new Vector3(-16, 0, 0);
            }

            // Randomly choose between Tile1 and Tile2 for second platform
            int RandomInt2 = Random.Range(0, 2);

            if(RandomInt2 == 1)
            {
                GameObject TempTile2 = Instantiate(Tile1, transform);
                TempTile2.transform.position = new Vector3(-24, 0, 0);
            }
            else if(RandomInt2 == 0)
            {
                GameObject TempTile2 = Instantiate(Tile2, transform);
                TempTile2.transform.position = new Vector3(-24, 0, 0);
            }

            // Set the next spawning position (every 15.95 units forward)
            Index = Index + 15.95f;
        }
    }
}
