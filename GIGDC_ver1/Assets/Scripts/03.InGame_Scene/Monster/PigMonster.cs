using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMonster : Monster
{

    float m_DelayTime = 0.0f;
    bool m_IsRight = false;

    private void Start()
    {
        m_MaxHP = 100;
        m_CurHP = m_MaxHP;

        m_Atk = 5;
        m_MoveSpeed = 5;

        m_ChaseDistance = 7.0f;
        m_AttackDistance = 3.0f;

        m_DelayTime = Random.Range(2.0f, 3.0f);

        if (m_Animator == null)
            m_Animator = this.GetComponent<Animator>();

        if (m_Player == null)
            m_Player = GameObject.Find("Player");

        if (m_Rb == null)
            m_Rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckDistanceFromPlayer();
        MonAiUpdate();
        
        if(Input.GetKeyDown(KeyCode.G))
        {
            m_Animator.SetTrigger("DieTrigger");
        }
    }

    //�÷��̾���� �Ÿ� ���ϱ� �Լ�
    void CheckDistanceFromPlayer()
    {
        m_CalcVec = m_Player.transform.position - this.transform.position;
    }

    void MonAiUpdate()
    {
        if(m_Player == null)
        {
            Debug.Log("Player Null");
            return;
        }

        if(m_Monstate == MonsterState.IDLE)
        {
            if (m_DelayTime >= 0.0f)
            {
                m_DelayTime -= Time.deltaTime;
                if(m_DelayTime <= 0.0f)
                {
                    m_Monstate = MonsterState.PATROL;
                    m_DelayTime = Random.Range(1.0f, 2.0f);
                    m_Animator.SetBool("IsMove", true);
                }
            }

            if (m_CalcVec.magnitude <= m_ChaseDistance) // ü�̽� �Ÿ� �ȿ� ���� ��
            {
                m_Monstate = MonsterState.CHASE;
                m_Animator.SetBool("IsMove", true);

            }

        }
        else if(m_Monstate == MonsterState.PATROL)
        {


            if (m_CalcVec.magnitude <= m_ChaseDistance) // ü�̽� �Ÿ� �ȿ� ���� ��
            {
                m_Monstate = MonsterState.CHASE;
            }

            if(m_DelayTime >= 0.0f)
            {
                m_DelayTime -= Time.deltaTime;

                if(m_IsRight)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    m_Rb.transform.position += Vector3.right * Time.deltaTime * (m_MoveSpeed / 3);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
                    m_Rb.transform.position += Vector3.left * Time.deltaTime * (m_MoveSpeed / 3);
                }

                if (m_DelayTime <= 0.0f)
                {
                    m_Monstate = MonsterState.IDLE;
                    m_DelayTime = Random.Range(1.5f, 3.0f);
                    m_Animator.SetBool("IsMove", false);
                    m_IsRight = !m_IsRight;
                }
            }
        }
        else if(m_Monstate == MonsterState.CHASE)
        {

            if (m_CalcVec.x >= 0.1f)
            {
                m_Rb.transform.position += Vector3.right * m_MoveSpeed * Time.deltaTime;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (m_CalcVec.x <= -0.1f)
            {
                m_Rb.transform.position += Vector3.left * m_MoveSpeed * Time.deltaTime;
                this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            }

            if(m_CalcVec.magnitude >= m_ChaseDistance)
            {
                m_Monstate = MonsterState.IDLE;
                m_Animator.SetBool("IsMove", false);
            }
            else if(m_CalcVec.magnitude <= m_AttackDistance)
            {
                m_Monstate = MonsterState.ATTACK;
                m_Animator.SetBool("IsAttack", true);
            }

        }
        else if(m_Monstate == MonsterState.ATTACK)
        {
            if(m_CalcVec.magnitude >= m_AttackDistance)
            {
                m_Monstate = MonsterState.CHASE;
                m_Animator.SetBool("IsAttack", false);
            }
        }
        
    }

}
