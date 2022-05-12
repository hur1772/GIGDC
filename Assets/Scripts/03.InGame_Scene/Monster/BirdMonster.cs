using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMonster : Monster
{

    float m_DelayTime = 0.0f;
    float m_DistanceFromPlayer = 0.0f;
    bool m_IsRight = false;

    bool m_IsFind = false;  //플레이어 탐지
    float m_IdleTimer = 5.0f;   // Idle 상태로 돌아가기 위한 시간
    public float m_FindDist = 5.0f;

    //날기 벡터 계산
    Vector2 m_FirstVec = Vector2.zero;
    float m_CurHeight = 0.0f;
    float m_MaxHeight = 0.0f;
    public float m_FlyHeight = 4.3f;
    public float m_FlySpeed = 0.0f;


    public FlyMonsterState m_FlyMonState;

    // Start is called before the first frame update
    void Start()
    {
        InitMonster();
        m_FlyMonState = FlyMonsterState.IDLE;
        m_FirstVec = this.transform.position;
        m_CurHeight = m_FirstVec.y;
        m_MaxHeight = m_CurHeight + m_FlyHeight;
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

    void MonAiUpdate()
    {
        if (m_Player == null)
        {
            Debug.Log("Player Null");
            return;
        }

        if (m_FlyMonState == FlyMonsterState.IDLE)
        {
            if(m_DistanceFromPlayer <= m_FindDist)    // 플레이어가 사정거리 안으로 들어올시 FLY상태로 변환
            {
                m_FlyMonState = FlyMonsterState.FLY;
                m_IsFind = true;
                m_Animator.SetBool("IsFind", m_IsFind);
            }

            if(this.transform.position.y >= m_FirstVec.y)   //IDle 상태 돌입을 위해 내려가는 코드
            {
                m_Rb.transform.position += Vector3.down * m_FlySpeed * Time.deltaTime;

                if (this.transform.position.y <= m_FirstVec.y)   //땅에 내려온 다음 IDle Animation을 재생하기 위한 코드
                {
                    m_Animator.SetBool("IsFind", m_IsFind);
                }
            }


        }
        else if (m_FlyMonState == FlyMonsterState.FLY)
        {
            if(this.transform.position.y <= m_MaxHeight)    //최대 날기 높이보다 낮다면 날기
            {
                m_Rb.transform.position += Vector3.up * m_FlySpeed * Time.deltaTime;
            }

            if(m_DistanceFromPlayer >= 15.0f)  //사정거리 밖으로 나갔을 시
            {
                if(m_IdleTimer >= 0.0f)
                {
                    m_IdleTimer -= Time.deltaTime;
                    if(m_IdleTimer <= 0.0f)
                    {
                        m_FlyMonState = FlyMonsterState.IDLE;
                        m_IdleTimer = 5.0f;
                        m_IsFind = false;
                    }
                }
            }
            else    //사정거리 다시 안으로 들어오면 타이머 초기화
            {
                m_IdleTimer = 5.0f;
            }
        }
        else if (m_FlyMonState == FlyMonsterState.PATROL)
        {

        }
        else if (m_FlyMonState == FlyMonsterState.CHASE)
        {

        }
        else if (m_FlyMonState == FlyMonsterState.ATTACK)
        {

        }
    }
}
