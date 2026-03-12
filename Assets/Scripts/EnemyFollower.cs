using UnityEngine;

// EnemyFollower class that makes an enemy follow the player from behind.
// The enemy maintains a distance behind the player and always faces towards the player.
public class EnemyFollower : MonoBehaviour
{
    // ===== Settings =====
    // Reference to the player's transform for tracking position
    [Header("Settings")]
    public Transform player;
    
    // Speed at which the enemy follows the player
    public float followSpeed = 5f;
    
    // Distance the enemy maintains behind the player on the X axis
    public float distanceBehind = 15f;

    // Called every frame to update enemy position and rotation
    private void Update()
    {
        // Exit if player reference is not set
        if (player == null)
            return;

        // Calculate target position: behind the player while maintaining Y position
        Vector3 targetPosition = new Vector3(
            player.position.x - distanceBehind,
            transform.position.y,
            player.position.z
        );

        // Smoothly move towards the target position using lerp
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Calculate direction from enemy to player for rotation
        Vector3 lookDirection = player.position - transform.position;
        // Ignore vertical difference for rotation (keep enemy level)
        lookDirection.y = 0;
        
        // Rotate the enemy to face the player if there's a valid direction
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
