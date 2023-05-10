using UnityEngine;
using System.Collections;

public class Sca : Character
{
    public Transform player;
    public float chaseRange = 5f;


    private bool isPlayerInRange;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        isPlayerInRange = false;
        isAttacking = false;
        animator.SetBool("Idle", true);
        animator.SetBool("Move", false);
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name!="Attack.ska")
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
            
            if (distanceToPlayer <= 1.2f)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Move", false);
                animator.SetTrigger("Attack");
            }
            else if (distanceToPlayer > 1.2f)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Move", true);
                moveSpeed = 2f;
            }
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
            moveSpeed = 2f;
        }
    }
    protected override IEnumerator ExecuteAttack()
    {

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                Character character = collider.GetComponent<Character>();
                if (character != null)
                {
                    character.TakeDamage();
                }

            }
        }
        yield return null;
    }

}
