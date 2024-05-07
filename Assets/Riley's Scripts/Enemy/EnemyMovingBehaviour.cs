using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingBehaviour : StateMachineBehaviour
{
    public Data_EnemyData Data;

    Transform player;
    Rigidbody2D rb;
    EnemyManager enemyManager;
    SFXAudioManager sfx;

     //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemyManager = animator.GetComponent<EnemyManager>();
        sfx = animator.GetComponent<SFXAudioManager>();

        sfx.PlaySound("DragonSpotted");

        animator.SetBool("canAttack", true);
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(GameManager.Instance.playerIsDead) {
            animator.SetBool("canAttack", false);
            animator.SetBool("canMove", false);
        }

        enemyManager.LookAtPlayer();
        Vector3 offset = new Vector3(0f, 4f, 0f);

        Vector2 target = new Vector2(player.position.x, player.position.y - offset.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, Data.movementSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        Vector2 circleCenter = (Vector2)animator.transform.position + (Vector2)offset;
        // Use Physics2D.OverlapCircle to check for colliders within the detection radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(circleCenter, Data.attackRange);

        foreach (Collider2D collider in colliders)
        {
            // Check if the collider is the player
            if (collider.transform == player)
            {
                animator.SetTrigger("Attack");
            }
        }
    }

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.SetBool("canAttack", false);
    }
}
