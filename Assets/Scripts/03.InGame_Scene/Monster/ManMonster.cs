using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManMonster : Monster
{
    float m_DelayTime = 0.0f;
    bool m_IsRight = false;
    float m_DistanceFromPlayer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        InitMonster();
        ManMonsterInit();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceFromPlayer();
        MonAiUpdate();
    }

    //플레이어와의 거리 구하기 함수
    void CheckDistanceFromPlayer()
    {
        m_CalcVec = m_Player.transform.position - this.transform.position;
        m_DistanceFromPlayer = m_CalcVec.magnitude;
        m_Animator.SetFloat("DistanceFromPlayer", m_DistanceFromPlayer);
    }

    void ManMonsterInit()
    {
        m_MaxHP = 100;
        m_CurHP = m_MaxHP;

        m_Atk = 5;
        m_MoveSpeed = 5;

        m_ChaseDistance = 5.0f;
        m_AttackDistance = 3.0f;

        m_DelayTime = Random.Range(2.0f, 3.0f);
    }

    void MonAiUpdate()
    {
        if (m_Player == null)
        {
            Debug.Log("Player Null");
            return;
        }

        if (m_Monstate == MonsterState.IDLE)
        {
            if (m_DelayTime >= 0.0f)
            {
                m_DelayTime -= Time.deltaTime;
                if (m_DelayTime <= 0.0f)
                {
                    m_Monstate = MonsterState.PATROL;
                    m_DelayTime = Random.Range(1.0f, 2.0f);
                }
            }

            if (m_CalcVec.magnitude <= m_ChaseDistance) // 체이스 거리 안에 들어올 시
            {
                m_Monstate = MonsterState.CHASE;
            }
        }
        else if (m_Monstate == MonsterState.PATROL)
        {

            if (m_DelayTime >= 0.0f)
            {
                m_DelayTime -= Time.deltaTime;

                if (m_IsRight)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    m_Rb.transform.position += Vector3.right * Time.deltaTime * (m_MoveSpeed / 3);
                    m_Animator.SetBool("IsMove", true);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
                    m_Rb.transform.position += Vector3.left * Time.deltaTime * (m_MoveSpeed / 3);
                    m_Animator.SetBool("IsMove", true);
                }

                if (m_DelayTime <= 0.0f)
                {
                    m_Monstate = MonsterState.IDLE;
                    m_DelayTime = Random.Range(1.5f, 3.0f);
                    m_IsRight = !m_IsRight;
                    m_Animator.SetBool("IsMove", false);
                }
            }
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
}
