using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Transform playerSpawnPoint;

    private void Start()
    {
        playerSpawnPoint = transform; // Store the initial checkpoint position
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpawnPoint = transform; // Update the current checkpoint position
        }
    }

    public void RespawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerSpawnPoint.position; // Move the player to the last checkpoint position
    }
}