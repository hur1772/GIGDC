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

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player_State.p_state = PlayerState.player_attack;
            Player_State.p_Attack_state = PlayerAttackState.player_sword;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Player_State.p_state = PlayerState.player_attack;
            Player_State.p_Attack_state = PlayerAttackState.player_bow;
        }
    }

    public void Sword_Attack(int a)
    {
        //Attack animation play
        if (a == 0)
        {
            animator.SetTrigger("Sword_Attack_1");
        }
        if (a == 1)
        {
            animator.SetTrigger("Sword_Attack_2");
        }
        if (a == 2)
        {
            animator.SetTrigger("Sword_Attack_3");
        }
        if (a == 3)
        {
            animator.SetTrigger("Sword_Attack_4");
        }

        //Detect Enemy,Target...etc
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage Enemy
        foreach (Collider2D enemy in hitEnemies) 
        {
            Debug.Log("hit");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}