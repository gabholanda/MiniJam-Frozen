using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private HealthComponent playerHealth; // Reference to the player's health component

    void Start()
    {
        playerHealth = GetComponent<HealthComponent>();
        if (playerHealth == null)
        {
            Debug.LogError("Player HealthComponent not assigned to the script!");
            return;
        }

        // Subscribe to the player's death event
        playerHealth.OnDeath += RestartScene;
    }

    void RestartScene(GameObject deadObject)
    {
        // Check if the dead object is the player
        if (deadObject.CompareTag("Player"))
        {
            // Restart the specified scene
            SceneManager.LoadScene("Map1");
        }
    }
}
