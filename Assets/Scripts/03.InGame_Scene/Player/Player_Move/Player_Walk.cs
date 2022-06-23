using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Walk : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrl Player_state;
    private CapsuleCollider2D collider;
    Animator animator;

    AnimationClip clip;

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
        collider = GetComponent<CapsuleCollider2D>();

        Player_state.p_state = PlayerState.player_idle;
        Player_state.p_Move_state = PlayerMoveState.player_noMove;
        Player_state.p_Attack_state = PlayerAttackState.player_no_att;
        Player_state.SwordTier = WeaponSwordTier.Sword_3Tier;
        Player_state.BowTier = WeaponBowTier.Bow_3Tier;

        move_speed = 4.0f;
        crawl_speed = 3.0f;
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
            collider.offset = new Vector2(-0.8f, 3.0f);
            collider.size = new Vector2(4.7f, 6.0f);
        }
        else
        {
            collider.offset = new Vector2(-0.8f, 5.5f);
            collider.size = new Vector2(4.7f, 10.0f);
            move_speed = 4.0f;
        }
    }

    private void P_Move_Walk()
    {
        if (Player_state.p_Move_state == PlayerMoveState.player_dash)
            return;

        //if(Input.GetKey(KeyCode.J))
        //{
        //    animator.Play("player_hit");
        //}

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

        //offset 3으로
        //size  6으로
        animator.SetBool("IsCrawl", true);
        move_speed = crawl_speed;
    }
}
