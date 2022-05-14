using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    //rigid body가 필요할지는 모르겠는데 일단 받아둠
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrl Player_State;
    Animator animator;
    private void Start() => StartFunc();

    private void StartFunc()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player_State = GetComponent<Player_State_Ctrl>();
        p_input = GetComponent<Player_Input>();
        Player_State.p_state = PlayerState.player_attack;
        Player_State.p_Attack_state = PlayerAttackState.player_no_att;
        // 공격'상태'State = no_att으로 초기화 해둠
        // 추후 무기 입력에 따라 state 추가할 예정
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }
}