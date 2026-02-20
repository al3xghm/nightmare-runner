using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    [Header("Settings")]
    public Transform player;
    public float followSpeed = 5f;
    public float distanceBehind = 15f;

    private void Update()
    {
        if (player == null)
            return;

        Vector3 targetPosition = new Vector3(
            player.position.x - distanceBehind,
            transform.position.y,
            player.position.z
        );

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0;
        
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
