using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform Player;
    public SwipeControls Controls;

    private bool Lane1 = false;
    private bool Lane2 = true;
    private bool Lane3 = false;

    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float fastFallMultiplier = 2f;
    private float verticalVelocity = 0f;
    private bool isGrounded = true;
    private float groundLevel = 0f;

    private void Start()
    {
        Player = GetComponent<Transform>();
        groundLevel = Player.position.y;
    }

    private void Update()
    {
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

        // Gestion du saut
        if (Controls.jump && isGrounded)
        {
            verticalVelocity = jumpForce;
            isGrounded = false;
        }

        if (!isGrounded)
        {
            float currentGravity = gravity;
            
            if (Controls.fastFall)
            {
                currentGravity *= fastFallMultiplier;
            }
            
            verticalVelocity += currentGravity * Time.deltaTime;
            Player.position += new Vector3(0, verticalVelocity * Time.deltaTime, 0);
            
            if (Player.position.y <= groundLevel)
            {
                Player.position = new Vector3(Player.position.x, groundLevel, Player.position.z);
                verticalVelocity = 0f;
                isGrounded = true;
            }
        }


        #region ChangeBools
        if (Controls.swiperight == true && Lane3 == false && Lane1 == true)
        {
            Lane2 = true;
            Lane1 = false;
            Lane3 = false;
        }
        else if (Controls.swipeleft == true && Lane2 == true && Player.position.z <= 0.2f)
        {
            Lane1 = true;
            Lane2 = false;
            Lane3 = false;
        }
        else if (Controls.swiperight == true && Lane2 == true && Player.position.z >= -0.2f)
        {
            Lane3 = true;
            Lane1 = false;
            Lane2 = false;
        }
        else if (Controls.swipeleft == true && Lane1 == false && Lane3 == true)
        {
            Lane2 = true;
            Lane1 = false;
            Lane3 = false;
        }
        #endregion
    }
}
