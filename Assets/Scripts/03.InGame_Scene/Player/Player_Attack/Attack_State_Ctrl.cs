using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_State_Ctrl : StateMachineBehaviour
{
    private Player_Attack p_Attack;
    private Player_State_Ctrl Player_State;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //SoundMgr.Instance.PlayEffSound("SwordAtt_1", 1.0f);
        int ran_att = Random.Range(0, 4);
        p_Attack = animator.GetComponent<Player_Attack>();
        p_Attack.Sword_Attack(ran_att);
        if(ran_att == 0)
        {
            SoundMgr.Instance.PlayEffSound("SwordAtt_1", 1.0f);
        }
        else if(ran_att == 1)
        {
            SoundMgr.Instance.PlayEffSound("SwordAtt_2", 1.0f);
        }
        else if(ran_att == 2)
        {
            SoundMgr.Instance.PlayEffSound("SwordAtt_3", 1.0f);
        }
        else
        {
            SoundMgr.Instance.PlayEffSound("SwordAtt_1", 1.0f);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger("Sword_Attack_end");

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
