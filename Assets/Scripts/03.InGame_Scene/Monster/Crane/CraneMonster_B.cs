using UnityEngine;

public class CraneMonster_B : Monster
{

    //이동 관련 변수
    float MoveTime = 3.0f;
    bool MoveRight = false;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        InitMonster();
        m_Monstate = MonsterState.PATROL;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        CheckDistanceFromPlayer(true);
        MonUpdate();

    }

    public void MonUpdate()
    {
        m_CalcVec.y = this.transform.position.y;
        if (m_Monstate == MonsterState.PATROL)
        {
            PatrolUpdate();
        }
        else if(m_Monstate == MonsterState.CHASE)
        {
            ChaseUpdate();
        }
        else if(m_Monstate == MonsterState.ATTACK)
        {
            AttackUpdate();
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

                if (MoveRight)
                {
                    this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(0, 0.0f, 0);
                }
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
            this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            MoveRight = true;
        }
        else if (m_CalcVec.x <= -0.1f)
        {
            m_Rb.transform.position += Vector3.left * m_MoveSpeed * 2.0f * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(0, 0.0f, 0);
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

        if (m_CalcVec.x >= 0.1f)
        {
            this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            MoveRight = true;
        }
        else if (m_CalcVec.x <= -0.1f)
        {
            this.transform.rotation = Quaternion.Euler(0, 0.0f, 0);
            MoveRight = false;
        }
    }
}