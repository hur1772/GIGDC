using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrl Player_state;
    private Animator animator;

    private float jump_power;

    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Player_state = GetComponent<Player_State_Ctrl>();
        rigid = GetComponent<Rigidbody2D>();
        p_input = GetComponent<Player_Input>();
        Player_state.p_state = PlayerState.player_idle;
        Player_state.p_Move_state = PlayerMoveState.player_noMove;
        Player_state.p_Attack_state = PlayerAttackState.player_no_att;
        jump_power = 8.0f;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Interaction.Inst.IsUpdate == false)
            {

                if (Player_state.p_state == PlayerState.player_die)
                    return;

                if (isJumping == false)
                {
                    isJumping = true;
                    Player_state.p_state = PlayerState.player_move;
                    Player_state.p_Move_state = PlayerMoveState.player_jump;
                    P_Move_Jump();
                }
            }
        }
    }

    void FixedUpdate()
    {
        // Lending Platform
        if (rigid.velocity.y <= 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); //에디터 상에서만 레이를 그려준다
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null) // 바닥 감지를 위해서 레이저를 쏜다! 
            {
                if (rayHit.distance < 0.5f)
                {
                    animator.SetBool("IsJump", false);
                    isJumping = false;
                }
            }
        }
    }

    private void P_Move_Jump()
    {
        SoundMgr.Instance.PlayEffSound("Player_Dash", 0.5f);
        animator.SetBool("IsJump", true);
        rigid.AddForce(transform.up* jump_power, ForceMode2D.Impulse);

    }
}
