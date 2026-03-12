using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SwipeControls handles input from keyboard or swipe gestures and presents them as simple boolean flags.
// A singleton pattern is used so other scripts (like Movement) can access it easily.
public class SwipeControls : MonoBehaviour
{
    // --- Singleton (kept so Movement.cs can automatically find this script) ---
    private static SwipeControls instance;
    public static SwipeControls Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<SwipeControls>();
                if (instance == null)
                {
                    // create a new GameObject if none exists in scene
                    instance = new GameObject("Spawned SwipeControls", typeof(SwipeControls)).GetComponent<SwipeControls>();
                }
            }
            return instance;
        }
        set { instance = value; }
    }

    // --- Command variables ---
    // Keep the exact names "swipeleft" etc. so that Movement script works without modification
    public bool swipeleft, swiperight, swipeUp, jump, fastFall;

    // --- Public properties (read-only for other scripts) ---
    public bool Swipeleft { get { return swipeleft; } }
    public bool Swiperight { get { return swiperight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool Jump { get { return jump; } }
    public bool FastFall { get { return fastFall; } }
    // LateUpdate is used to reset inputs and check for new key presses each frame.
    public void LateUpdate()
    {
        // 1. Reset all input flags at the start of the frame
        swipeleft = swiperight = swipeUp = jump = fastFall = false;

        // 2. Check keyboard keys (PC only) and set corresponding flags
        CheckInput();
    }

    private void CheckInput()
    {
        // Move LEFT (Arrow, A or Q)
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q))
        {
            swipeleft = true;
        }
        
        // Move RIGHT (Arrow or D)
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            swiperight = true;
        }
        
        // Jump (Space, W or Up Arrow)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
        }
        
        // Fast fall (S or Down Arrow)
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            fastFall = true;
        }
    }
}