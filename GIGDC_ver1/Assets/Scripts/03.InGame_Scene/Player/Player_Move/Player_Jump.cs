using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrl Player_state;

    private float jump_power;

    // Start is called before the first frame update
    void Start()
    {
        Player_state = GetComponent<Player_State_Ctrl>();
        rigid = GetComponent<Rigidbody2D>();
        p_input = GetComponent<Player_Input>();
        Player_state.p_state = PlayerState.player_idle;
        Player_state.p_Move_state = PlayerMoveState.player_noMove;
        Player_state.p_Attack_state = PlayerAttackState.player_no_att;
        jump_power = 600.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player_state.p_state = PlayerState.player_move;
            Player_state.p_Move_state = PlayerMoveState.player_jump;
            P_Move_Jump();
        }
    }

    private void P_Move_Jump()
    {
        rigid.AddForce(transform.up* jump_power);
    }
}
