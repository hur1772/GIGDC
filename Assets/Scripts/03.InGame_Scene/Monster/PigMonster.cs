 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMonster : Monster
{

    float m_DelayTime = 0.0f;
    bool m_IsRight = false;

    //--- 멧돼지 스킬(돌진)
    float m_SkillCoolTime = 0.0f;
    float m_SkillDelayTime = 0.5f;
    bool m_IsSkillOn = false;

    private void Start()
    {
        m_MaxHP = 100;
        m_CurHP = m_MaxHP;

        m_Atk = 5;
        m_MoveSpeed = 5;

        m_ChaseDistance = 7.0f;
        m_AttackDistance = 3.0f;

        m_SkillCoolTime = 5.0f;
        m_SkillDelayTime = 0.5f;

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

        SkillCoolUpdate();

        if(Input.GetKeyDown(KeyCode.G))
        {
            m_Animator.SetTrigger("SkillTrigger");
            m_Monstate = MonsterState.SKILL;
        }
    }

    void SkillCoolUpdate()
    {
        if(m_SkillCoolTime >= 0.0f)
        {
            m_SkillCoolTime -= Time.deltaTime;
            if(m_SkillCoolTime <= 0.0f)
            {
                m_IsSkillOn = true;
                m_Animator.SetBool("IsSkillOn", m_IsSkillOn);
            }
        }
    }

    //플레이어와의 거리 구하기 함수
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

            if (m_CalcVec.magnitude <= m_ChaseDistance) // 체이스 거리 안에 들어올 시
            {
                m_Monstate = MonsterState.CHASE;
                m_Animator.SetBool("IsMove", true);

            }

        }
        else if(m_Monstate == MonsterState.PATROL)
        {
            if (m_CalcVec.magnitude <= m_ChaseDistance) // 체이스 거리 안에 들어올 시
            {
                if (m_IsSkillOn)
                    m_Monstate = MonsterState.SKILL;
                else
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
            if (m_IsSkillOn)
                m_Monstate = MonsterState.SKILL;

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
        else if(m_Monstate == MonsterState.SKILL)
        {
            if (m_SkillDelayTime >= 0.0f)
            {
                m_SkillDelayTime -= Time.deltaTime;
                if (m_SkillDelayTime <= 0.0f)
                {
                    if (m_CalcVec.x >= 0.1f)
                    {
                        Debug.Log("right");
                        m_Rb.AddForce(Vector2.right * 500.0f);
                    }
                    else if (m_CalcVec.x <= -0.1f)
                    {
                        Debug.Log("left");
                        m_Rb.AddForce(Vector2.left * 500.0f);
                    }

                    m_Monstate = MonsterState.IDLE;
                    m_SkillDelayTime = 0.5f;
                    m_SkillCoolTime = 5.0f;
                    m_IsSkillOn = false;
                    m_Animator.SetBool("IsSkillOn", m_IsSkillOn);
                }
            }
            else
                m_Monstate = MonsterState.IDLE;


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
