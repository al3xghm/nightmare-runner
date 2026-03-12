using UnityEngine;

// Movement class handles player lane switching, jumping, and gravity based on swipe controls.
public class Movement : MonoBehaviour
{
    // Cached transform of the player object for position changes
    private Transform Player;

    // Reference to swipe input handler
    public SwipeControls Controls;

    // Lane state flags (three possible lanes: left, center, right)
    private bool Lane1 = false;
    private bool Lane2 = true;
    private bool Lane3 = false;

    // Jump and gravity settings
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    // Extra multiplier when player holds fast-fall input
    public float fastFallMultiplier = 2f;

    // Current vertical speed due to jumping/falling
    private float verticalVelocity = 0f;
    // Whether the player is currently on the ground
    private bool isGrounded = true;
    // Y level considered ground height
    private float groundLevel = 0f;

    private void Start()
    {
        // cache the transform component and record initial y position as ground level
        Player = GetComponent<Transform>();
        groundLevel = Player.position.y;
    }

    private void Update()
    {
        // ---------------------------------------------
        // Lane movement: smoothly move the player into the targeted lane
        // ---------------------------------------------
        if(Lane3 == true && Player.position.z < 1.1f)
        {
            Player.position += new Vector3(0, 0, 10.5f * Time.deltaTime);
        }
        else if(Lane1 == true && Player.position.z > -1.1f)
        {
            Player.position += new Vector3(0, 0, -10.5f * Time.deltaTime);
        }
        else if(Lane2 == true && Player.position.z <= -0.1f)
        {
            Player.position += new Vector3(0, 0, 10.5f * Time.deltaTime);
        }
        else if(Lane2 == true && Player.position.z >= 0.1f)
        {
            Player.position += new Vector3(0, 0, -10.5f * Time.deltaTime);
        }

        // ---------------------------------------------
        // Jumping logic
        // ---------------------------------------------
        if (Controls.jump && isGrounded)
        {
            verticalVelocity = jumpForce;
            isGrounded = false;
        }

        if (!isGrounded)
        {
            float currentGravity = gravity;
            
            // apply faster fall if input active
            if (Controls.fastFall)
            {
                currentGravity *= fastFallMultiplier;
            }
            
            // integrate gravity over time
            verticalVelocity += currentGravity * Time.deltaTime;
            Player.position += new Vector3(0, verticalVelocity * Time.deltaTime, 0);
            
            // check ground collision and reset
            if (Player.position.y <= groundLevel)
            {
                Player.position = new Vector3(Player.position.x, groundLevel, Player.position.z);
                verticalVelocity = 0f;
                isGrounded = true;
            }
        }

        // ---------------------------------------------
        // Swipe input lane state updates
        // ---------------------------------------------
        #region ChangeBools
        if (Controls.swiperight == true && Lane3 == false && Lane1 == true)
        {
            // move from left lane to center
            Lane2 = true;
            Lane1 = false;
            Lane3 = false;
        }
        else if (Controls.swipeleft == true && Lane2 == true && Player.position.z <= 0.2f)
        {
            // move from center to left
            Lane1 = true;
            Lane2 = false;
            Lane3 = false;
        }
        else if (Controls.swiperight == true && Lane2 == true && Player.position.z >= -0.2f)
        {
            // move from center to right
            Lane3 = true;
            Lane1 = false;
            Lane2 = false;
        }
        else if (Controls.swipeleft == true && Lane1 == false && Lane3 == true)
        {
            // move from right back to center
            Lane2 = true;
            Lane1 = false;
            Lane3 = false;
        }
        #endregion
    }
}
