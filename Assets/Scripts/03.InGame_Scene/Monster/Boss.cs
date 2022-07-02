using UnityEngine;

public class BossCtrl1_1 : Monster
{
    enum RushEnum
    {
        BACKSTEP,
        RUSH_BEFORE,
        RUSH,
        RUSH_AFTER
    }

    enum JumpEnum
    {
        BACKSTEP,
        JUMP_BEFORE,
        JUMP,
        JUMP_AFTER
    }

    //대기 관련 변수
    float idleDelay;

    //이동 관련 변수
    float MoveTime = 3.0f;
    bool MoveRight = false;

    //공격 관련 변수
    public float m_AttackDelay = 1.5f;

    //스킬 관련 변수
    float SkillCoolTime = 5.0f;
    bool IsSkillOn = false;
    float RushDelay = 2.0f;
    Vector3 RushVec = Vector3.zero;
    Vector3 RushCurVec = Vector3.zero;
    float StayTime = 2.5f;
    RushEnum rushEnum = RushEnum.BACKSTEP;
    JumpEnum jumpEnum = JumpEnum.BACKSTEP;
    bool goSkill = false;
    public GameObject Protal;
    // Start is called before the first frame update
    void Start()
    {
        InitMonster();
        if (Protal != null)
        {
            Protal.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(originPos, (attackPos.position - originPos));
    }

    // Update is called once per frame
    void Update()
    {
        originPos = new Vector3(this.transform.position.x, attackPos.position.y, 0.0f);
        CheckDistanceFromPlayer();
        BossUpDate();
    }

    public void BossUpDate()
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
        //else if (m_Monstate == MonsterState.SKILL)  //주인공한테 돌격
        //{
        //    RushSkill();
        //}
        //else if (m_Monstate == MonsterState.SKILL2)  //주인공한테 점프공격
        //{
        //    JumpSkill();
        //}
        else if (m_Monstate == MonsterState.Hitted)
        {
            m_Monstate = MonsterState.CHASE;
        }
    }

    public void AttackUpdate()
    {
        if (m_Animator.GetBool("GrapAttack") == false)
            m_Animator.SetBool("GrapAttack", true);

        if (IsSkillOn)
        {            
            m_Animator.SetBool("GrapAttack", false);
        }

        ChangeRotate2();

        if (m_CalcVec.magnitude >= m_AttackDistance)
        {
            m_Animator.SetBool("GrapAttack", false);
        }

    }

    public void IdleUpdate()
    {
        if (idleDelay >= 0.0f)
        {
            idleDelay -= Time.deltaTime;
            if (idleDelay <= 0.0f)
            {
                idleDelay = Random.Range(1.0f, 2.0f);
                m_Monstate = MonsterState.PATROL;
                m_Animator.SetBool("IsMove", true);

                if (MoveRight)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0.0f, 0);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
                }
            }
        }

        if (m_CalcVec.magnitude < m_ChaseDistance)
        {
            m_Monstate = MonsterState.CHASE;
            m_Animator.SetBool("FindPlayer", true);
            idleDelay = Random.Range(1.0f, 2.0f);
            m_MoveSpeed = 2.0f;
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
                m_Monstate = MonsterState.IDLE;
                m_Animator.SetBool("IsMove", false);
            }
        }

        if (m_CalcVec.magnitude <= m_ChaseDistance)
        {
            m_Monstate = MonsterState.CHASE;
            m_Animator.SetBool("FindPlayer", true);
            m_MoveSpeed = 2.0f;
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
            m_Animator.SetBool("GrapAttack", true);
        }
    }

    //protected override void Die()
    //{
    //    Protal.gameObject.SetActive(true);
    //    m_Monstate = MonsterState.DIE;
    //    m_Animator.SetBool("CanAttack", false);
    //    this.gameObject.layer = LayerMask.NameToLayer("Default");
    //    m_Animator.SetTrigger("Die");

    //}
}
