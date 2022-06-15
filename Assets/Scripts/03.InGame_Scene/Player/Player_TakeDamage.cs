using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TakeDamage : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_State_Ctrl Player_State;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Player_State = GetComponent<Player_State_Ctrl>();
        animator = GetComponent<Animator>();
        Player_State.p_state = PlayerState.player_takeDamage;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void P_TakeDamage()
    {
        animator.SetTrigger("Hit");
    }
}
