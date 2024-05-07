using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBehaviour : StateMachineBehaviour
{
    public Data_EnemyData Data;

    public bool canAttack = false;
    EnemyManager enemyManager;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       enemyManager = animator.GetComponent<EnemyManager>();

       Debug.Log("attacking");
       canAttack = false;
       animator.SetBool("canAttack", false);

        enemyManager.ActiveCoroutine();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(canAttack) 
        {
            animator.SetBool("canAttack", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
