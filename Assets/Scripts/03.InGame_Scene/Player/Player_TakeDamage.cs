using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TakeDamage : MonoBehaviour
{
    private Rigidbody2D rigid;
    public Player_State_Ctrl Player_State;
    Animator animator;
    [HideInInspector] public float maxHp;
    public float curHp;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Player_State = GetComponent<Player_State_Ctrl>();
        animator = GetComponent<Animator>();
        Player_State.p_state = PlayerState.player_takeDamage;
        maxHp = 100;
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void P_TakeDamage(float a_Value = 10.0f)
    {

        if (Player_State.p_state != PlayerState.player_die)
            SoundMgr.Instance.PlayEffSound("Player_Hit", 0.6f);

        if (Player_State.p_Move_state == PlayerMoveState.player_dash)
        {
            Debug.Log("대쉬상태 무적");
            return;
        }

        if (Player_State.p_state == PlayerState.player_die)
            return;

        curHp -= a_Value;
        if(Player_State.p_state != PlayerState.player_die)
        {
            animator.SetTrigger("Hit");
        }
        Debug.Log("플레이어 피격");

        if(curHp <= 0.0f)
        {
            P_Die();
        }
    }

    public void P_Die()
    {
        Player_State.p_state = PlayerState.player_die;
        animator.SetTrigger("DieTrigger");
        SoundMgr.Instance.PlayGUISound("Player_Die", 0.8f);
        //Time.timeScale = 0.5f;
        this.gameObject.layer = 10;
        curHp = 0;
        Debug.Log("Die");
    }
}
