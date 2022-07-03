using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public static Player_Attack Inst;
    //rigid body가 필요할지는 모르겠는데 일단 받아둠
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrl Player_State;
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 1.0f;
    public LayerMask enemyLayers;

    float playerAttackDamage;
    float playerCriticalValue;

    public RuntimeAnimatorController[] runtimeAnimatorControllers;

    bool State = true;

    void Awake()
    {
        Inst = this;
        animator = GetComponent<Animator>();
    }

    private void Start() => StartFunc();
    private void StartFunc()
    {
        //GlobalUserData.InitWeaponData();

        rigid = GetComponent<Rigidbody2D>();
        
        Player_State = GetComponent<Player_State_Ctrl>();
        p_input = GetComponent<Player_Input>();
        Player_State.p_state = PlayerState.player_attack;
        GlobalUserData.Player_Att_State = PlayerAttackState.player_sword;
        // 공격'상태'State = no_att으로 초기화 해둠
        // 추후 무기 입력에 따라 state 추가할 예정

        playerAttackDamage = 30.0f;
        playerCriticalValue = 20.0f;

        animator.runtimeAnimatorController = runtimeAnimatorControllers[0 + GlobalUserData.SwordTier * 2];
    }

    private void Update() => UpdateFunc();
    private void UpdateFunc()
    {
        if (Interaction.Inst.IsUpdate == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log((0 + GlobalUserData.SwordTier * 2));
                animator.runtimeAnimatorController = runtimeAnimatorControllers[0 + GlobalUserData.SwordTier * 2];
                Player_State.p_state = PlayerState.player_attack;
                Player_State.p_Attack_state = PlayerAttackState.player_sword;
                GlobalUserData.Player_Att_State = PlayerAttackState.player_sword;
                State = true;
                //animator.SetInteger("WeaponState", (int)PlayerAttackState.player_sword);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log((1 + GlobalUserData.BowTier * 2));
                animator.runtimeAnimatorController = runtimeAnimatorControllers[1 + GlobalUserData.BowTier * 2];
                //animator.SetInteger("WeaponState", (int)PlayerAttackState.player_bow);
                Player_State.p_state = PlayerState.player_attack;
                Player_State.p_Attack_state = PlayerAttackState.player_bow;
                GlobalUserData.Player_Att_State = PlayerAttackState.player_bow;
                State = true;
                //animator.SetInteger("WeaponState", (int)PlayerAttackState.player_bow);
            }
        }
    }

    public void Bow_Attack()
    {
        animator.SetTrigger("Bow_Attack");
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
        //foreach (Collider2D enemy in hitEnemies) 
        //{
        //    Debug.Log("hit");
          
        //}

        foreach (Collider2D collider in hitEnemies)
        {
            Debug.Log("hit");
            if (collider.tag == "target")
            {
                collider.GetComponent<TargetCtrl>().TakeDamage(playerAttackDamage);
            }
            else if(collider.tag == "Monster")
            {
                collider.GetComponent<Monster>().TakeDamage(0 + GlobalUserData.SwordTier * 2);
            }
            else
            {
                //허공 가르는 사운드용 공간
            }
        }
    }

    private void OnDrawGizmosSelected()
    {   
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}