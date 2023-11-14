using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    private static int enemiesRemaining;

    void Awake()
    {
        // Count the enemies in the scene
        enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EnemyDied()
    {
        // Decrement the count of remaining enemies
        enemiesRemaining--;
        Debug.Log(enemiesRemaining);
        // Check if all enemies are defeated
        if (enemiesRemaining <= 0)
        {
            // Trigger the transition to the next scene
            TransitionToNextScene();
        }
    }

    void TransitionToNextScene()
    {
        // Get the index of the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene in the build settings
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
