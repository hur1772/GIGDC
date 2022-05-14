using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    //rigid body�� �ʿ������� �𸣰ڴµ� �ϴ� �޾Ƶ�
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
        // ����'����'State = no_att���� �ʱ�ȭ �ص�
        // ���� ���� �Է¿� ���� state �߰��� ����
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }
}