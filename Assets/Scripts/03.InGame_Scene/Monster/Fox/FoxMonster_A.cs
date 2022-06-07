using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMonster_A : Monster
{
    //대기 관련 변수
    float m_IdleTime = 0.0f;


    //이동 관련 변수
    float MoveTime = 3.0f;
    bool MoveRight = false;

    //공격 관련 변수
    public float m_AttackDelay = 1.5f;
    GameObject marblePrefab;
    Transform weaponPos;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        InitMonster();
        m_Monstate = MonsterState.IDLE;

        m_IdleTime = Random.Range(2.0f, 3.0f);
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        CheckDistanceFromPlayer();
        MonUpdate();
    }

    public void MonUpdate()
    {
        if (m_Monstate == MonsterState.IDLE)
        {
            IdleUpdate();
        }
        else if (m_Monstate == MonsterState.PATROL)
        {
            PatrolUpdate();
        }
        else if (m_Monstate == MonsterState.CHASE)
        {
            ChaseUpdate();
        }
        else if (m_Monstate == MonsterState.ATTACK)
        {
            AttackUpdate();
        }
    }

    public void IdleUpdate()
    {
        if (m_IdleTime >= 0.0f)
        {
            m_IdleTime -= Time.deltaTime;
            if (m_IdleTime <= 0.0f)
            {
                m_IdleTime = Random.Range(2.0f, 3.0f);
                m_Monstate = MonsterState.PATROL;
                m_Animator.SetBool("IsMove", true);

                if (MoveRight)
                    this.transform.rotation = Quaternion.Euler(0, 0.0f, 0);
                else
                    this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            }
        }

    }

    public void PatrolUpdate()
    {
        if (MoveTime >= 0.0f)
        {
            MoveTime -= Time.deltaTime;
            if (MoveRight)
            {
                m_Rb.transform.position += Vector3.right * m_MoveSpeed * Time.deltaTime;

            }
            else
            {
                m_Rb.transform.position += Vector3.left * m_MoveSpeed * Time.deltaTime;
            }

            if (MoveTime <= 0.0f)
            {
                MoveTime = Random.Range(3.0f, 5.0f);
                MoveRight = !MoveRight;

                m_Animator.SetBool("IsMove", false);
                m_Monstate = MonsterState.IDLE;
            }
        }

        if (m_CalcVec.magnitude <= m_ChaseDistance)
        {
            m_Monstate = MonsterState.CHASE;

        }
    }

    public void ChaseUpdate()
    {

        if (m_CalcVec.x >= 0.1f)
        {
            m_Rb.transform.position += Vector3.right * m_MoveSpeed * 2.0f * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            MoveRight = true;
        }
        else if (m_CalcVec.x <= -0.1f)
        {
            m_Rb.transform.position += Vector3.left * m_MoveSpeed * 2.0f * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            MoveRight = false;
        }

        if (m_CalcVec.magnitude <= m_AttackDistance)
        {
            m_Monstate = MonsterState.ATTACK;
            m_Animator.SetBool("CanAttack", true);
        }

        if (m_ChaseDistance * 1.5f < m_CalcVec.magnitude)
        {
            m_Monstate = MonsterState.PATROL;
        }
    }

    public void AttackUpdate()
    {
        if (m_CalcVec.magnitude >= m_AttackDistance)
        {
            m_Animator.SetBool("CanAttack", false);
        }
    }

}
