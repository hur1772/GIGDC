using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    IDLE,
    PATROL,
    CHASE,
    ATTACK,
    SKILL,
    DIE
}

public enum FlyMonsterState
{
    IDLE,
    PATROL,
    FLY,
    ATTACK,
    CHASE,
    DIE,
}

public class Monster : MonoBehaviour
{
    //플레이어, 거리 구하기용 벡터
    protected GameObject m_Player;
    protected Vector2 m_CalcVec = Vector3.zero;
    protected Rigidbody2D m_Rb;

    //몬스터 기본 스탯, 상태
    protected float m_MaxHP = 0;
    protected float m_CurHP = 0;
    protected float m_Atk   = 0;
    protected float m_MoveSpeed = 0;

    protected float m_ChaseDistance = 0;
    protected float m_AttackDistance = 0;

    protected MonsterState m_Monstate = MonsterState.IDLE;
    protected Animator m_Animator;

    //private void Update()
    //{
        
    //}

    //private void Start()
    //{
        
    //}

    void AiUpdate()
    {
        if (m_Player == null)
        {
            Debug.Log("Player Null");
            return;
        }

        if (m_Monstate == MonsterState.IDLE)
        {

        }
        else if (m_Monstate == MonsterState.PATROL)
        {

        }
        else if (m_Monstate == MonsterState.CHASE)
        {

        }
        else if (m_Monstate == MonsterState.SKILL)
        {

        }
        else if (m_Monstate == MonsterState.ATTACK)
        {

        }
    }

    protected virtual void InitMonster()
    {
        if (m_Animator == null)
            m_Animator = this.GetComponent<Animator>();

        if (m_Player == null)
            m_Player = GameObject.Find("Player");

        if (m_Rb == null)
            m_Rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void TakeDamage(float a_Value)
    {
        m_CurHP -= a_Value;
        if (m_CurHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        m_CurHP = 0;
        Debug.Log("Die");
    }

}
