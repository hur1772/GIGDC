using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMonster : Monster
{

    float m_DelayTime = 0.0f;
    float m_DistanceFromPlayer = 0.0f;
    bool m_IsRight = false;

    bool m_IsFind = false;  //�÷��̾� Ž��
    float m_IdleTimer = 5.0f;   // Idle ���·� ���ư��� ���� �ð�
    public float m_FindDist = 5.0f;

    //���� ���� ���
    Vector2 m_FirstVec = Vector2.zero;
    float m_CurHeight = 0.0f;
    float m_MaxHeight = 0.0f;
    public float m_FlyHeight = 4.3f;
    public float m_FlySpeed = 0.0f;
    public float m_IdleDistance = 0.0f;

    //Chase���� ���� ����
    public float m_ChaseDelay = 2.0f;
    public Vector3 m_ChaseVec = Vector3.zero;


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

    //�÷��̾���� �Ÿ� ���ϱ� �Լ�
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
            if(m_DistanceFromPlayer <= m_FindDist)    // �÷��̾ �����Ÿ� ������ ���ý� FLY���·� ��ȯ
            {
                m_FlyMonState = FlyMonsterState.FLY;
                m_IsFind = true;
                m_Animator.SetBool("IsFind", m_IsFind);
            }

            if(this.transform.position.y >= m_FirstVec.y)   //IDle ���� ������ ���� �������� �ڵ�
            {
                m_Rb.transform.position += Vector3.down * m_FlySpeed * Time.deltaTime;

                if (this.transform.position.y <= m_FirstVec.y)   //���� ������ ���� IDle Animation�� ����ϱ� ���� �ڵ�
                {
                    m_Animator.SetBool("IsFind", m_IsFind);
                }
            }


        }
        else if (m_FlyMonState == FlyMonsterState.FLY)
        {
            //���� ��ȯ �ڵ�
            ChangeRotate();

            if (this.transform.position.y <= m_MaxHeight)    //�ִ� ���� ���̺��� ���ٸ� ����
            {
                m_Rb.transform.position += Vector3.up * m_FlySpeed * Time.deltaTime;

                if (m_CalcVec.x >= 0.1f)
                {
                    m_Rb.transform.position += Vector3.left * Time.deltaTime * 2.0f;
                }
                else if (m_CalcVec.x <= -0.1f)
                {
                    m_Rb.transform.position += Vector3.right * Time.deltaTime * 2.0f;
                }
            }
            else // ü�̽� �׽�Ʈ
            {
                m_FlyMonState = FlyMonsterState.CHASE;
            }

            if(m_DistanceFromPlayer >= m_IdleDistance)  //�����Ÿ� ������ ������ ��
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
            else    //�����Ÿ� �ٽ� ������ ������ Ÿ�̸� �ʱ�ȭ
            {
                m_IdleTimer = 5.0f;
            }
        }
        else if (m_FlyMonState == FlyMonsterState.PATROL)
        {

        }
        else if (m_FlyMonState == FlyMonsterState.CHASE)
        {
            //������ȯ �ڵ�
            ChangeRotate();

            if (m_ChaseDelay >= 0.0f)    //�i�� ��ġ Ž��
            {
                m_ChaseDelay -= Time.deltaTime;
                if(m_ChaseDelay <= 0.0f)
                {
                    m_ChaseVec = m_Player.transform.position;
                    m_ChaseDelay = 2.0f;
                }
            }

            if(m_ChaseVec.x >= this.transform.position.x)
            {
                m_Rb.transform.position += Vector3.right * Time.deltaTime * 5.0f;
            }
            else
            {
                m_Rb.transform.position += Vector3.left * Time.deltaTime * 5.0f;
            }


        }
        else if (m_FlyMonState == FlyMonsterState.ATTACK)
        {

        }
    }

    void ChangeRotate()
    {
        if (m_CalcVec.x >= 0.1f)
        {
            this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
        }
        else if (m_CalcVec.x <= -0.1f)
        {
            this.transform.rotation = Quaternion.Euler(0, 0.0f, 0);
        }
    }
}
