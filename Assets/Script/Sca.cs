using UnityEngine;
using System.Collections;

public class Sca : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 5f;
    public float moveSpeed = 2f;

    private Animator animator;
    private bool isPlayerInRange;
    private bool isAttacking;
    private bool isDead = false;
    private int health = 10;
    private bool isHit = false;
    private bool isTakingDamage = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isPlayerInRange = false;
        isAttacking = false;
        animator.SetBool("Idle", true);
        animator.SetBool("Move", false);
        animator.SetBool("Attack", false);
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
                if (!isHit)
                {
                    isHit = true;
                    isTakingDamage = true;
                    StartCoroutine(ResetTakingDamage());
                    animator.SetTrigger("Hit");
                }
            }
        }
    }

    private IEnumerator ResetTakingDamage()
    {
        yield return new WaitForSeconds(0.5f);
        isTakingDamage = false;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            Vector3 direction = player.position - transform.position;
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-2.2f, 2.2f, 2.2f);
            }
            else
            {
                transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
            }
            
            if (distanceToPlayer <= 1.2f && !isAttacking)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Move", false);
                isAttacking = true;
                StartCoroutine(PerformAttack());
            }
            else if (distanceToPlayer > 1.2f && !isAttacking)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Move", true);
                animator.SetBool("Attack", false);
                moveSpeed = 2f;
            }
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
            animator.SetBool("Attack", false);
            moveSpeed = 2f;
            if (isAttacking)
            {
                StopCoroutine(PerformAttack());
                isAttacking = false;
            }
        }
    }

    private IEnumerator PerformAttack()
    {
        moveSpeed = 0f;
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("Attack", false);
        isAttacking = false;
        moveSpeed = 2f;
    }

    private IEnumerator DieAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("Death");
        Destroy(gameObject);
    }
}
