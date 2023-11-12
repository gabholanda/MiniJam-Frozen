using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField]
    private float projectileSpeed = 1.0f;
    [SerializeField]
    private float rangedCooldown = 0.5f;
    [SerializeField]
    private float rangedDamage = 1.0f;
    protected bool canAttack = true;

    [SerializeField]
    private GameObject projectilePrefab;

    protected override void Update()
    {
        base.Update();
        if (isInRange)
        {
            canMove = false;
            if (canAttack)
            {
                TriggerRanged();
            }
        }
    }
    public void TriggerRanged()
    {
        canAttack = false;
        animator.Play("Ranged");
    }

    // Add this function as an Event by the middle of Ranged animation
    public void Fire()
    {
        if (!projectilePrefab)
        {
            Debug.LogError("NullPointer error at RangedEnemy for projectilePrefab.");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        projectile.GetComponent<Rigidbody2D>().velocity = cachedDirection * projectileSpeed;

    }
}
