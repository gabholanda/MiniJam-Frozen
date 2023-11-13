using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    [SerializeField]
    private GameObject wallDestructionParticles;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded) return;
        Instantiate(wallDestructionParticles, transform.position, Quaternion.identity);
    }
}
