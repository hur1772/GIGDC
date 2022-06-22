using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TakeDamage : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_State_Ctrl Player_State;
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
        curHp -= a_Value;
        animator.SetTrigger("Hit");
        Debug.Log("플레이어 피격");

        if(curHp <= 0.0f)
        {
            P_Die();
        }
    }

    public void P_Die()
    {
        curHp = 0;
        Debug.Log("Die");
        Time.timeScale = 0.0f;
    }
}
