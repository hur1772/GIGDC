using UnityEngine;

public class HumanManMonster : Monster
{
    public float m_DelayTime = 0.0f;
    bool m_IsRight = false;


    private void Start() => StartFunc();

    private void StartFunc()
    {
        InitMonster();
        InitState();

    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        CheckDistanceFromPlayer();
        AiUpdate();


        if(Input.GetKeyDown(KeyCode.P))
        {
            m_Monstate = MonsterState.DIE;
        }
    }

    void InitState()
    {
        m_DelayTime = Random.Range(2.0f, 3.0f);
        m_MaxHP = 100;
        m_CurHP = m_MaxHP;

        m_Atk = 5;
        m_MoveSpeed = 4;

        m_ChaseDistance = 7.0f;
        m_AttackDistance = 3.0f;
    }

    void AiUpdate()
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
                    m_DelayTime = Random.Range(2.0f, 3.0f);
                    m_Animator.SetBool("IsMove", true);
                }
            }

            if (m_CalcVec.magnitude <= m_ChaseDistance) // 체이스 거리 안에 들어올 시
            {
                m_Monstate = MonsterState.CHASE;
                m_Animator.SetBool("IsMove", true);
            }
        }
        else if (m_Monstate == MonsterState.PATROL)
        {
            if (m_CalcVec.magnitude <= m_ChaseDistance) // 체이스 거리 안에 들어올 시
            {
                m_Monstate = MonsterState.CHASE;
            }

            if (m_DelayTime >= 0.0f)
            {
                m_DelayTime -= Time.deltaTime;

                if (m_IsRight)
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
        else if (m_Monstate == MonsterState.CHASE)
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

            if (m_CalcVec.magnitude >= m_ChaseDistance)
            {
                m_Monstate = MonsterState.IDLE;
                m_Animator.SetBool("IsMove", false);
            }
            else if (m_CalcVec.magnitude <= m_AttackDistance)
            {
                m_Monstate = MonsterState.ATTACK;
                m_Animator.SetBool("IsAttack", true);
            }
        }
        else if (m_Monstate == MonsterState.SKILL)
        {

        }
        else if (m_Monstate == MonsterState.ATTACK)
        {
            if(m_CalcVec.magnitude >= m_AttackDistance)
            {
                //m_Monstate = MonsterState.CHASE;
                m_Animator.SetBool("IsAttack", false);
            }
        }
        else if(m_Monstate == MonsterState.DIE)
        {
            m_Animator.SetTrigger("DieTrigger");
            m_Monstate = MonsterState.CORPSE;
        }
        else if(m_Monstate == MonsterState.CORPSE)
        {
            //아무것도 안할거
        }
    }
}