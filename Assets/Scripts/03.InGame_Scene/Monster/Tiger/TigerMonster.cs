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
    
    enum JumpEnum
    {
        BACKSTEP,
        JUMP_BEFORE,
        JUMP,
        JUMP_AFTER
    }

    //��� ���� ����
    float idleDelay;

    //�̵� ���� ����
    float MoveTime = 3.0f;
    bool MoveRight = false;

    //���� ���� ����
    public float m_AttackDelay = 1.5f;

    //��ų ���� ����
    float SkillCoolTime = 5.0f;
    bool IsSkillOn = false;
    float RushDelay = 2.0f;
    Vector3 RushVec = Vector3.zero;
    Vector3 RushCurVec = Vector3.zero;
    float StayTime = 2.5f;
    RushEnum rushEnum = RushEnum.BACKSTEP;
    JumpEnum jumpEnum = JumpEnum.BACKSTEP;

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(originPos, (attackPos.position - originPos));
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        originPos = new Vector3(this.transform.position.x, attackPos.position.y, 0.0f);


        CheckDistanceFromPlayer();
        MonUpdate();
        SkillCoolUpdate();

        //�����ϼ�
        if(Input.GetKeyDown(KeyCode.P))
        {
            m_Monstate = MonsterState.SKILL;
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            m_Monstate = MonsterState.SKILL2;
        }
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
        else if(m_Monstate == MonsterState.SKILL)  //���ΰ����� ����
        {
            RushSkill();
        }
        else if(m_Monstate == MonsterState.SKILL2)  //���ΰ����� ��������
        {
            JumpSkill();
        }
    }

    public void JumpSkill()
    {
        if (jumpEnum == JumpEnum.BACKSTEP)
        {
            BackStep();
            jumpEnum = JumpEnum.JUMP_BEFORE;
        }
        else if (jumpEnum == JumpEnum.JUMP_BEFORE)
        {
            if (RushDelay >= 0.0f)
            {
                RushDelay -= Time.deltaTime;
                if (RushDelay <= 0.0f)
                {
                    jumpEnum = JumpEnum.JUMP;
                    m_MoveSpeed = 3.0f;
                    RushVec = m_Player.transform.position;
                    RushVec.y = this.transform.position.y;
                    RushVec.z = 0.0f;
                    m_Animator.enabled = true;
                    m_Rb.AddForce(Vector2.up * 700.0f);
                    m_Animator.SetTrigger("Jump");
                }
            }
        }
        else if (jumpEnum == JumpEnum.JUMP)
        {
            ChangeRotate2();
            RushCurVec = RushVec - this.transform.position;

            m_Rb.transform.position += RushCurVec * Time.deltaTime * m_MoveSpeed;

            if (RushCurVec.magnitude <= 1.0f)
            {
                Debug.Log("arrive");
                RushDelay = 2.0f;
                m_MoveSpeed = 2.0f;
                jumpEnum = JumpEnum.JUMP_AFTER;
                m_Animator.SetBool("IsMove", false);
                m_Animator.SetBool("CanAttack", false);
                m_Animator.SetBool("FindPlayer", false);
            }
        }
        else if (jumpEnum == JumpEnum.JUMP_AFTER)
        {
            MonStay();
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

    public void RandSkillFunc()
    {
        if (m_Monstate == MonsterState.SKILL || m_Monstate == MonsterState.SKILL2)
            return;

        int rand = Random.Range(0, 2);
        Debug.Log(rand);
        if (rand == 0)
            m_Monstate = MonsterState.SKILL;
        else
            m_Monstate = MonsterState.SKILL2;
    }

    public void SkillCoolUpdate()
    {
        if(SkillCoolTime >= 0.0f && IsSkillOn == false)
        {
            SkillCoolTime -= Time.deltaTime;
            if(SkillCoolTime <= 0.0f)
            {
                IsSkillOn = true;
                SkillCoolTime = 5.0f;
            }
        }
    }

    public void MonStay()
    {
        ChangeRotate2();

        if(StayTime >= 0.0f)
        {
            StayTime -= Time.deltaTime;
            if(StayTime <= 0.0f)
            {
                IsSkillOn = false;
                m_Monstate = MonsterState.CHASE;
                StayTime = 2.5f;
                rushEnum = RushEnum.BACKSTEP;
                jumpEnum = JumpEnum.BACKSTEP;
                m_Animator.SetBool("FindPlayer", true);
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
        {
            RandSkillFunc();
            return;
        }

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
    }

    public void AttackUpdate()
    {
        if (m_Animator.GetBool("CanAttack") == false)
            m_Animator.SetBool("CanAttack", true);

        if (IsSkillOn)
        {
            RandSkillFunc();
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
        spRend.sprite = sprites[0]; //�����

        m_Rb.gravityScale = 2.0f;
    }

    public void TigerAttack()
    {
        Vector3 attackdir = attackPos.position - originPos;

        attackhit = Physics2D.Raycast(originPos, attackdir, attackdir.magnitude, playerMask);
        if (attackhit)
        {
            if (attackhit.collider.gameObject.TryGetComponent(out playerTakeDmg))
            {
                playerTakeDmg.P_TakeDamage();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out playerTakeDmg))
            playerTakeDmg.P_TakeDamage();
    }

    protected override void Die()
    {
        base.Die();
        m_Monstate = MonsterState.DIE;
        m_Animator.SetBool("CanAttack", false);
        m_Animator.SetTrigger("Die");
    }
}