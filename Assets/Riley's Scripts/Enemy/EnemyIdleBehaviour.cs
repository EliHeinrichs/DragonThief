using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleBehaviour : StateMachineBehaviour
{
    public Data_EnemyData Data;

    Transform player;
    EnemyManager enemyManager;
    SFXAudioManager sfx;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyManager = animator.GetComponent<EnemyManager>();
        sfx = animator.GetComponent<SFXAudioManager>();

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(GameManager.Instance.playerIsDead) {
            return;
        }
        
        Vector3 offset = new Vector3(0f, 4f, 0f);

        Vector2 circleCenter = (Vector2)animator.transform.position + (Vector2)offset;
        // Use Physics2D.OverlapCircle to check for colliders within the detection radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(circleCenter, Data.detectRange);

        foreach (Collider2D collider in colliders)
        {
            // Check if the collider is the player
            if (collider.transform == player)
            {
                animator.SetBool("canMove", true);
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
