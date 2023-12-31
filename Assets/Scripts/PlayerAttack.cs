using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRadius = 2f;
    public BaseStatsContainer playerStats;
    public float attackCooldown = 1f; // Adjust as needed
    private bool canAttack = true;
    [SerializeField]
    private GameObject clickAttack;
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            StartCoroutine(AttackWithCooldown());
        }
    }

    IEnumerator AttackWithCooldown()
    {
        canAttack = false;

        Attack();

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    void Attack()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Perform AOE damage around the mouse coordinates
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(mousePosition, attackRadius);
        Instantiate(clickAttack, mousePosition, Quaternion.identity);

        foreach (Collider2D enemy in hitEnemies)
        {
            HealthComponent healthComponent = enemy.GetComponent<HealthComponent>();

            if (healthComponent != null)
            {
                // Calculate damage based on player's attack value
                float damage = playerStats.Attack;

                // Apply damage to the enemy's health
                healthComponent.ReceiveDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the attack radius in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint(Input.mousePosition), attackRadius);
    }

    public IEnumerator FreezeEnemy(Enemy enemy)
    {
        enemy.DisableEnemy();
        yield return new WaitForSeconds(2.0f);
        enemy.EnableEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.StartCoroutine(FreezeEnemy(enemy));
        }
    }
}