using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
  
    public float speed = 10f; // Adjust the speed as needed
    public float lifetime = 3f; // Adjust the lifetime as needed
    public int damage = 1; // Adjust the damage as needed

    void Start()
    {
        // Set the initial velocity of the bullet
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Destroy the bullet after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collides with an object tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Deal damage to the player (or implement your specific logic)
            // You might want to have a HealthComponent on the player to handle damage
            other.GetComponent<HealthComponent>().SetCurrentHealth(other.GetComponent<HealthComponent>().CurrentHealth - damage);

            // Destroy the bullet when it hits the player
            Destroy(gameObject);
        }
        else
        {
            // Destroy the bullet when it hits any other object
            Destroy(gameObject);
        }
    }
}
