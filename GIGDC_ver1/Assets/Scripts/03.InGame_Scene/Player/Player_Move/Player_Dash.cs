using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dash : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrl Player_state;
    Player_Walk p_walk;

    private float dash_speed;

    // Start is called before the first frame update
    void Start()
    {
        Player_state = GetComponent<Player_State_Ctrl>();
        rigid = GetComponent<Rigidbody2D>();
        p_input = GetComponent<Player_Input>();
        p_walk = GetComponent<Player_Walk>();
        Player_state.p_state = PlayerState.player_idle;
        Player_state.p_Move_state = PlayerMoveState.player_noMove;
        Player_state.p_Attack_state = PlayerAttackState.player_no_att;
        dash_speed = 400.0f;
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
            Player_state.p_state = PlayerState.player_move;
            Player_state.p_Move_state = PlayerMoveState.player_dash;
            P_Move_Dash();
        }

    }

    private void P_Move_Dash()
    {
        //Vector2 p_vector = new Vector2(p_input.horizontal, 0);
        //Vector2 p_dash = p_vector * dash_speed * Time.deltaTime;
        //rigid.position += p_dash;
        rigid.AddForce(p_walk.key * transform.right * dash_speed);
    }
}
