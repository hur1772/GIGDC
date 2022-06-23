using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dash : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrl Player_state;
    Player_Walk p_walk;
    bool isDash;
    private float dash_speed;
    private float dash_Cool;
    Animator animator;

    private float dash_time;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Player_state = GetComponent<Player_State_Ctrl>();
        rigid = GetComponent<Rigidbody2D>();
        p_input = GetComponent<Player_Input>();
        p_walk = GetComponent<Player_Walk>();
        Player_state.p_state = PlayerState.player_idle;
        Player_state.p_Move_state = PlayerMoveState.player_noMove;
        Player_state.p_Attack_state = PlayerAttackState.player_no_att;
        dash_speed = 350.0f;
        dash_time = 0.4f;
        dash_Cool = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player_state.p_Move_state == PlayerMoveState.player_crawl)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Player_state.p_Move_state == PlayerMoveState.player_noMove)
                return;

            if (0.0f <= dash_Cool)
                return;
            isDash = true;
            Player_state.p_state = PlayerState.player_move;
            Player_state.p_Move_state = PlayerMoveState.player_dash;
            dash_time = 0.4f;
            animator.SetBool("IsDash", true);
            dash_Cool = 2.0f;
        }
        else
        {
            dash_speed = p_walk.move_speed;
            dash_time -= Time.deltaTime;
            animator.SetBool("IsDash", false);
            isDash = false;
            dash_Cool -= Time.deltaTime;
        }

        if (0.0f < dash_time)
        {
            P_Move_Dash();

            this.gameObject.layer = 8;
        }
        else
        {
            Player_state.p_Move_state = PlayerMoveState.player_noMove;
            this.gameObject.layer = 6;
        }
    }

    private void P_Move_Dash()
    {
        Vector2 p_vector = new Vector2(p_input.horizontal, 0);
        Vector2 p_dash = p_vector * dash_speed * Time.deltaTime;
        rigid.position += p_dash;
    }
}
