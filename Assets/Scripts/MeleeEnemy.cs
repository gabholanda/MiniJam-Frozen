using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField]
    private float meleePointOffset = 1.0f;
    [SerializeField]
    private float meleeCirlceRadius = 1.0f;
    [SerializeField]
    private float meleeCooldown = 0.5f;
    [SerializeField]
    private float meleeDamage = 1.0f;
    protected bool canMelee = true;
    protected override void Update()
    {
        base.Update();
        if (isInRange)
        {
            canMove = false;
            if (canMelee)
            {
                TriggerMelee();
            }
        }
    }

    public void TriggerMelee()
    {
        canMelee = false;
        if (!animator)
        {
            return;
        }
        animator.Play("Melee");
    }

    // Add this function as an Event by the end of Melee animation
    public void Melee()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(cachedDirection * meleePointOffset, meleeCirlceRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                HealthComponent healthComponent = colliders[i].GetComponent<HealthComponent>();
                healthComponent.ReceiveDamage(meleeDamage);
            }
        }
        canMove = true;
        StartCoroutine(MeleeCooldown());
    }

    public IEnumerator MeleeCooldown()
    {
        yield return new WaitForSeconds(meleeCooldown);
        canMelee = true;
    }

    public override void DisableEnemy()
    {
        base.DisableEnemy();
        canMelee = false;
        StopCoroutine(MeleeCooldown());
    }

    public override void EnableEnemy()
    {
        base.EnableEnemy();
        canMelee = true;
    }
}
