using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBehavior : StateMachineBehaviour
{
    Editor editor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        editor = animator.GetComponent<Editor>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        editor.LaunchRaycast();

        bool isdestroyable = editor.isHit;

        if (isdestroyable)
        {
            isdestroyable &= editor.hit.transform.CompareTag("Part");
        }

        if (isdestroyable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(editor.hit.transform.gameObject);
                //TODO check if other part are detached from core.
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
