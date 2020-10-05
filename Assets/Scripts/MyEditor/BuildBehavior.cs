using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBehavior : StateMachineBehaviour
{

    Editor editor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        editor = animator.GetComponent<Editor>();

        editor.parts.SetActive(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (!editor.SelectedPart) return;



        editor.LaunchRaycast();

        bool isBuilding = editor.isHit;

        if (isBuilding)
        {
            isBuilding &= editor.hit.collider.CompareTag("In");
        }

        if (isBuilding)
        {
            editor.InstanceSelected.SetActive(true);
            if (Input.GetMouseButtonDown(0)) Attach(editor.hit);
            else HitPreview(editor.hit);
        }
        else
        {
            editor.InstanceSelected.SetActive(false);
        }
    }

    //create a preview where we would attach our new object
    public void HitPreview(RaycastHit hit)
    {
        if (!editor.InstanceSelected) return;
        editor.InstanceSelected.transform.position = hit.collider.transform.position + hit.collider.transform.forward * 0.5f;
        editor.InstanceSelected.transform.rotation = Quaternion.LookRotation(hit.collider.transform.forward);
    }

    //create our object
    public void Attach(RaycastHit hit)
    {
        if (!editor.InstanceSelected) return;
        GameObject part = Instantiate(editor.SelectedPart);
        part.name = editor.SelectedPart.name;
        part.transform.position = hit.collider.transform.position + hit.collider.transform.forward * 0.5f;
        part.transform.rotation = Quaternion.LookRotation(hit.collider.transform.forward);
        part.transform.parent = hit.transform.GetComponentInParent<Root>().transform;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        editor.parts.SetActive(false);
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
