 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMonster : Monster
{
    enum ChargeSkill
    {
        CHARGE_BEFORE,
        CHARGE,
        CHARGE_AFTER
    }

    float m_DelayTime = 0.0f;
    bool m_IsRight = false;

    //--- 멧돼지 스킬(돌진)
    float m_SkillCoolTime = 0.0f;
    float m_SkillDelayTime = 0.5f;
    bool m_IsSkillOn = false;
    Vector3 SkillPos = Vector3.zero;
    bool goSkill = false;
    ChargeSkill echargeSkill = ChargeSkill.CHARGE_BEFORE;

    public GameObject attackEff;

    bool m_SkillTriggerBool = true;

    private void Start()
    {
        m_MaxHP = 100;
        m_CurHP = m_MaxHP;

        m_SkillCoolTime = 5.0f;
        m_SkillDelayTime = 1.5f;

        m_DelayTime = Random.Range(2.0f, 3.0f);

        InitMonster();
    }

    private void Update()
    {
        if(attackPos != null)
            originPos = new Vector3(this.transform.position.x, attackPos.position.y, 0.0f);

        CheckDistanceFromPlayer();
        MonAiUpdate();
        SkillCoolUpdate();
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
    //void CheckDistanceFromPlayer()
    //{
    //    m_CalcVec = m_Player.transform.position - this.transform.position;
    //}

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

            if(m_CalcVec.magnitude <= m_AttackDistance)
            {
                m_Monstate = MonsterState.ATTACK;
                m_Animator.SetBool("IsAttack", true);
            }

        }
        else if(m_Monstate == MonsterState.SKILL)
        {
            if(echargeSkill == ChargeSkill.CHARGE_BEFORE)
            {
                NewSkill();
            }
            else if(echargeSkill == ChargeSkill.CHARGE)
            {
                Charge();
            }
            else
            {
                if(m_SkillDelayTime >= 0.0f)
                {
                    m_SkillDelayTime -= Time.deltaTime;
                    if(m_SkillDelayTime <= 0.0f)
                    {
                        m_Monstate = MonsterState.CHASE;
                        m_Animator.SetBool("IsMove", true);
                        m_IsSkillOn = false;
                        m_Animator.SetBool("IsSkillOn", m_IsSkillOn);
                        m_SkillCoolTime = 5.0f;
                        m_SkillTriggerBool = true;
                        echargeSkill = ChargeSkill.CHARGE_BEFORE;
                    }
                }
            }
        }
        else if(m_Monstate == MonsterState.ATTACK)
        {
            if (m_IsSkillOn)
                m_Monstate = MonsterState.SKILL;

            if(m_CalcVec.magnitude >= m_AttackDistance)
            {
                m_Animator.SetBool("IsAttack", false);
            }
        }
        
    }

    void NewSkill()
    {
        if (m_CalcVec.x >= 0.1f)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            SkillPos = this.transform.position + new Vector3(8.0f, 0, 0);
        }
        else if (m_CalcVec.x <= -0.1f)
        {
            this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            SkillPos = this.transform.position + new Vector3(-8.0f, 0, 0);
        }

        if(m_SkillTriggerBool)
        {
            m_Animator.SetTrigger("SkillTrigger");
            m_SkillTriggerBool = false;
        }
    }

    public void GoCharge()
    {
        echargeSkill = ChargeSkill.CHARGE;
        Debug.Log(SkillPos);
        goSkill = true;
    }

    public void Charge()
    {
        Vector3 gopos = (SkillPos - this.transform.position);
        gopos.y = 0.0f;

        m_Rb.transform.position += gopos.normalized * Time.deltaTime * m_MoveSpeed * 4;

        if(gopos.magnitude <= 0.5f)
        {
            m_SkillDelayTime = 1.5f;
            m_Animator.SetBool("IsMove", false);
            m_Animator.SetBool("IsAttack", false);
            goSkill = false;
            echargeSkill = ChargeSkill.CHARGE_AFTER;
        }
    }

    void Attack()
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

    void SpawnEff()
    {
        GameObject eff = Instantiate(attackEff);

        Vector3 spawnPos = attackPos.position;
        spawnPos.y = this.transform.position.y;
        eff.transform.position = spawnPos;
        eff.transform.eulerAngles = attackPos.eulerAngles;

        Destroy(eff, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!goSkill)
            return;

        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent(out playerTakeDmg))
                playerTakeDmg.P_TakeDamage();
        }
    }
}
