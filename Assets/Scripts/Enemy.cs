using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float stoppingDistance = 1.5f;
    public GameObject player;
    public BaseStatsContainer enemyStat;
    private PlayerAttack playerAttack;

    private Rigidbody2D rb;
    private HealthComponent healthComponent;
    private bool canMove = true;

    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthComponent = GetComponent<HealthComponent>();
        playerAttack = player.GetComponent<PlayerAttack>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (healthComponent == null)
        {
            Debug.LogError("HealthComponent not found on enemy!");
        }

        healthComponent.OnDeath += HandleDeath;
    }

    void Update()
    {
        if (canMove)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * enemyStat.Speed;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= stoppingDistance)
        {
            rb.velocity = Vector2.zero;
        }
    }


    void HandleDeath(GameObject dead)
    {
        playerAttack.StartCoroutine(playerAttack.FreezeEnemy(dead, () =>
        {
            Destroy(dead);
            InstantiateBullets();
        }));
    }

    public void InstantiateBullets()
    {

        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;

        for (int i = 0; i < 8; i++)
        {
            // Calculate rotation based on increments of 45 degrees
            Quaternion rotation = Quaternion.Euler(0, 0, i * 45f);

            // Calculate rotated direction
            Vector2 rotatedDirection = rotation * directionToPlayer;

            // Instantiate a bullet in the rotated direction
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = rotatedDirection * bulletSpeed;
        }
    }
}
    

