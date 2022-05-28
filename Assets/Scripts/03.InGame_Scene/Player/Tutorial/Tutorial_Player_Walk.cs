using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Player_Walk : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrl Player_state;
    Animator animator;

    public float move_speed;
    public float crawl_speed;
    public int key = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player_state = GetComponent<Player_State_Ctrl>();
        rigid = GetComponent<Rigidbody2D>();
        p_input = GetComponent<Player_Input>();
        animator = GetComponent<Animator>();
        Player_state.p_state = PlayerState.player_idle;
        Player_state.p_Move_state = PlayerMoveState.player_noMove;
        Player_state.p_Attack_state = PlayerAttackState.player_no_att;
        move_speed = 4.0f;
        crawl_speed = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {

        if (p_input.horizontal != 0)
        {
            Player_state.p_state = PlayerState.player_move;
            Player_state.p_Move_state = PlayerMoveState.player_walk;
            P_Move_Walk();
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            Player_state.p_state = PlayerState.player_move;
            Player_state.p_Move_state = PlayerMoveState.player_crawl;
            P_Move_Crawl();
        }
        else
        {
            move_speed = 4.0f;
        }
    }

    private void P_Move_Walk()
    {
        if (Player_state.p_Move_state == PlayerMoveState.player_dash)
            return;

        animator.SetBool("IsWalk", true);
        Vector2 p_vector = new Vector2(p_input.horizontal, .0f);
        Vector2 p_move = p_vector * move_speed * Time.deltaTime;
        rigid.position += p_move;

        if (p_input.horizontal < 0)
            key = -1;

        if (0 < p_input.horizontal)
            key = 1;

        if (Player_state.p_Move_state != PlayerMoveState.player_noMove)
        {
            if (p_input.horizontal != 0)
            {
                transform.localScale = new Vector3(key * 0.3f, 0.3f, 1);
            }
        }
        animator.SetBool("IsCrawl", false);

    }
    private void P_Move_Crawl()
    {
        if (Player_state.p_Move_state == PlayerMoveState.player_dash || Player_state.p_Move_state == PlayerMoveState.player_jump)
            return;

        animator.SetBool("IsCrawl", true);
        move_speed = crawl_speed;
    }
}
