using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float stoppingDistance = 1.5f;
    public GameObject player;
    public BaseStatsContainer stats;
    protected float speed;
    protected Rigidbody2D rb;
    protected HealthComponent healthComponent;
    protected Animator animator;
    protected Vector2 cachedDirection;
    protected bool canMove = true;
    protected bool isInRange = false;
    private PlayerAttack playerAttack;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthComponent = GetComponent<HealthComponent>();
        animator = GetComponent<Animator>();
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (healthComponent == null)
        {
            Debug.LogError("HealthComponent not found on enemy!");
        }

        healthComponent.OnDeath += HandleDeath;

        speed = stats.Speed;
    }

    protected virtual void Update()
    {
        if (canMove)
        {
            MoveTowardsPlayer();
        }
    }

    protected void MoveTowardsPlayer()
    {
        if (!player)
        {
            Debug.LogError("Player not found!");
            return;
        }

        Vector2 direction = (player.transform.position - transform.position).normalized;
        cachedDirection = direction;
        rb.velocity = direction * speed;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= stoppingDistance)
        {
            rb.velocity = Vector2.zero;
            isInRange = true;
            return;
        }
        isInRange = false;
    }


    protected void HandleDeath(GameObject dead)
    {
        // Where are  we even getting playerAttack from?
        playerAttack.StartCoroutine(playerAttack.FreezeEnemy(dead, () =>
        {
            Destroy(dead);
            InstantiateBullets();
        }));
    }

    protected void InstantiateBullets()
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

    public float GetSpeed()
    {
        return speed;
    }

    public virtual void DisableEnemy()
    {
        canMove = false;
    }

    public virtual void EnableEnemy()
    {
        canMove = true;
    }
}


