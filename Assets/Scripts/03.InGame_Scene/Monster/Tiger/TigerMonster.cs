using UnityEngine;

public class TigerMonster : Monster
{

    enum RushEnum
    {
        BACKSTEP,
        RUSH_BEFORE,
        RUSH,
        RUSH_AFTER
    }

    //대기 관련 변수
    float idleDelay;

    //이동 관련 변수
    float MoveTime = 3.0f;
    bool MoveRight = false;

    //공격 관련 변수
    public float m_AttackDelay = 1.5f;

    //스킬 관련 변수
    float SkillCoolTime = 8.0f;
    bool IsSkillOn = false;
    float RushDelay = 2.0f;
    Vector3 RushVec = Vector3.zero;
    Vector3 RushCurVec = Vector3.zero;
    float StayTime = 5.0f;
    RushEnum rushEnum = RushEnum.BACKSTEP;

    SpriteRenderer spRend;
    public Sprite[] sprites = null;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        InitMonster();
        m_Monstate = MonsterState.IDLE;
        idleDelay = Random.Range(1.0f, 2.0f);

        spRend = GetComponent<SpriteRenderer>();
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        CheckDistanceFromPlayer();
        MonUpdate();
        SkillCoolUpdate();
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
        else if(m_Monstate == MonsterState.SKILL)  //주인공 한테 돌격
        {
            RushSkill();
        }
    }

    public void RushSkill()
    {
        if(rushEnum == RushEnum.BACKSTEP)
        {
            BackStep();
            rushEnum = RushEnum.RUSH_BEFORE;
        }
        else if(rushEnum == RushEnum.RUSH_BEFORE)
        {
            if (RushDelay >= 0.0f)
            {
                RushDelay -= Time.deltaTime;
                if (RushDelay <= 0.0f)
                {
                    rushEnum = RushEnum.RUSH;
                    m_MoveSpeed = 3.0f;
                    RushVec = m_Player.transform.position;
                    RushVec.y = this.transform.position.y;
                    RushVec.z = 0.0f;
                    m_Animator.enabled = true;
                }
            }
        }
        else if(rushEnum == RushEnum.RUSH)
        {
            ChangeRotate2();
            RushCurVec = RushVec - this.transform.position;

            m_Rb.transform.position += RushCurVec * Time.deltaTime * m_MoveSpeed;

            if (RushCurVec.magnitude <= 1.0f)
            {
                RushDelay = 2.0f;
                m_MoveSpeed = 2.0f;
                m_Animator.SetTrigger("ThreeAttack");
                rushEnum = RushEnum.RUSH_AFTER;
                m_Animator.SetBool("IsMove", false);
                m_Animator.SetBool("CanAttack", false);
                m_Animator.SetBool("FindPlayer", false);
            }
        }
        else if(rushEnum == RushEnum.RUSH_AFTER)
        {
            MonStay();
        } 
    }

    public void SkillCoolUpdate()
    {
        if(SkillCoolTime >= 0.0f && IsSkillOn == false)
        {
            SkillCoolTime -= Time.deltaTime;
            if(SkillCoolTime <= 0.0f)
            {
                IsSkillOn = true;
                SkillCoolTime = 8.0f;
            }
        }
    }

    public void MonStay()
    {
        ChangeRotate2();

        if(StayTime >= 0.0f)
        {
            Debug.Log(StayTime);
            StayTime -= Time.deltaTime;
            if(StayTime <= 0.0f)
            {
                m_Monstate = MonsterState.CHASE;
                StayTime = 5.0f;
                rushEnum = RushEnum.BACKSTEP;
                m_Animator.SetBool("IsMove", true);
                m_Animator.SetBool("FindPlayer", true);
                IsSkillOn = false;
            }
        }

    }

    public void IdleUpdate()
    {
        if(idleDelay >= 0.0f)
        {
            idleDelay -= Time.deltaTime;
            if(idleDelay <= 0.0f)
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

        if(m_CalcVec.magnitude < m_ChaseDistance)
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
        if (IsSkillOn)
            m_Monstate = MonsterState.SKILL;

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
            m_Animator.SetBool("FindPlayer", false);
            m_MoveSpeed = 1.0f;
        }
    }

    public void AttackUpdate()
    {
        if (IsSkillOn)
        {
            m_Monstate = MonsterState.SKILL;
            m_Animator.SetBool("CanAttack", false);
        }

        ChangeRotate2();

        if (m_CalcVec.magnitude >= m_AttackDistance)
        {
            m_Animator.SetBool("CanAttack", false);
        }
    }

    public void BackStep()
    {
        ChangeRotate2();
        if (m_CalcVec.x < 0)
            m_Rb.AddForce(new Vector3(1, 1, 0) * 300.0f);
        else
            m_Rb.AddForce(new Vector3(-1, 1, 0) * 300.0f);

        m_Animator.enabled = false;
        spRend.sprite = sprites[0]; //대기모션

        m_Rb.gravityScale = 2.0f;
    }
}