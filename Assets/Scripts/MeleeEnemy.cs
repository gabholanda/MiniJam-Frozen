using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField]
    private float meleePointOffset;
    [SerializeField]
    private float meleeCirlceRadius;
    [SerializeField]
    private float meleeCooldown;
    protected bool canMelee = true;
    public override void Update()
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
                Debug.Log("Do damage on player: " + colliders[i].name);
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
