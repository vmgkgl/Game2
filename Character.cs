using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected bool isHit = false;
    protected bool isTakingDamage = false;
    [SerializeField]
    protected int health = 10;
    [SerializeField]
    protected float moveSpeed = 2f;
    [SerializeField] 
    protected float attackRange = 1.5f;
    protected Animator animator;
    protected bool isAttacking;
    protected bool isDead = false;
    void Start()
    {
        
    }
    public void TakeDamage()
    {
        if (!isDead && !isTakingDamage)
        {
            health--;
            if (health <= 0)
            {
                StartCoroutine(DieAfterDelay());
                isDead = true;
            }
            else
            {
                isTakingDamage = true;
                StartCoroutine(ResetTakingDamage());
                animator.SetTrigger("Hit");
            }
        }
    }
    private IEnumerator DieAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("Death");
        Destroy(gameObject);
    }
    private IEnumerator ResetTakingDamage()
    {
        yield return new WaitForSeconds(0.5f);

        isTakingDamage = false;
    }
    protected void PerformAttack()
    {
        StartCoroutine(ExecuteAttack());
    }
    protected virtual IEnumerator ExecuteAttack()
    {

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Monster"))
            {
                Character monster = collider.GetComponent<Character>();
                if (monster != null)
                {
                    monster.TakeDamage();
                }

            }
        }
        yield return null;
    }
    void Update()
    {
        
    }
}
