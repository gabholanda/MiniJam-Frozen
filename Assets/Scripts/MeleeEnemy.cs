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
        }

        if (canMelee)
        {
            TriggerMelee();
        }
        else
        {
            canMove = true;
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
        StartCoroutine(MeleeCooldown());
    }

    public IEnumerator MeleeCooldown()
    {
        yield return new WaitForSeconds(meleeCooldown);
        canMelee = true;
    }
}
