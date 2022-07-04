using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGirl : Monster
{
    //대기 관련 변수
    float m_IdleTime = 0.0f;


    //이동 관련 변수
    float MoveTime = 3.0f;
    bool MoveRight = false;

    //공격 관련 변수
    public float m_AttackDelay = 1.5f;
    float m_CurattackDelay;
    GameObject weaponPrefab;
    public Transform weaponPos;
    public float attackDealy_2 = 0.0f;
    public float middleattackDistance = 0.0f;

    //보스소환
    [Header("--- Boss ---")]
    public Transform bossSpawnPos;
    public GameObject bossPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InitMonster();

        m_Monstate = MonsterState.IDLE;

        m_CurattackDelay = m_AttackDelay;

        m_IdleTime = Random.Range(2.0f, 3.0f);

        weaponPrefab = Resources.Load("BossGirl Weapon") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceFromPlayer();
        MonUpdate();

        originPos = new Vector3(this.transform.position.x, attackPos.position.y, 0.0f);

    }

    public void MonUpdate()
    {
        if (m_Monstate == MonsterState.IDLE)
        {
            IdleUpdate();
        }
        else if (m_Monstate == MonsterState.PATROL)
        {
            //SoundMgr.Instance.PlayEffSound("WZombie_Idle", 0.3f);
            PatrolUpdate();
        }
        else if (m_Monstate == MonsterState.CHASE)
        {
            //SoundMgr.Instance.PlayEffSound("WZombie_Idle", 0.3f);
            ChaseUpdate();
        }
        else if (m_Monstate == MonsterState.ATTACK)
        {
            //SoundMgr.Instance.PlayEffSound("WZombie_Idle", 0.3f);
            AttackUpdate();
        }
    }

    public void IdleUpdate()
    {
        SoundMgr.Instance.PlayEffSound("WZombie_Idle", 0.3f);
        if (m_IdleTime >= 0.0f)
        {
            m_IdleTime -= Time.deltaTime;
            if (m_IdleTime <= 0.0f)
            {
                m_IdleTime = Random.Range(2.0f, 3.0f);
                m_Monstate = MonsterState.PATROL;
                m_Animator.SetBool("IsMove", true);

                if (MoveRight)
                    this.transform.rotation = Quaternion.Euler(0, 0.0f, 0);
                else
                    this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            }
        }

        if (m_CalcVec.magnitude <= m_ChaseDistance)
        {
            m_Monstate = MonsterState.CHASE;
            m_Animator.SetBool("IsMove", true);

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

                m_Animator.SetBool("IsMove", false);
                m_Monstate = MonsterState.IDLE;
            }
        }

        if (m_CalcVec.magnitude <= m_ChaseDistance)
        {
            m_Monstate = MonsterState.CHASE;
            m_Animator.SetBool("IsMove", true);
        }
    }

    public void ChaseUpdate()
    {
        if (m_CalcVec.x >= 0.1f)
        {
            m_Rb.transform.position += Vector3.right * m_MoveSpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            MoveRight = true;
        }
        else if (m_CalcVec.x <= -0.1f)
        {
            m_Rb.transform.position += Vector3.left * m_MoveSpeed  * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            MoveRight = false;
        }

        if (m_CalcVec.magnitude <= m_AttackDistance)
        {
            m_Monstate = MonsterState.ATTACK;
            m_Animator.SetBool("IsMove", false);
        }
    }

    public void AttackUpdate()
    {
        if (attackDealy_2 >= 0.0f)
        {
            attackDealy_2 -= Time.deltaTime;
        }
        else
        {
            ChangeRotate2();

            if (m_CalcVec.magnitude >= m_AttackDistance)
            {
                m_Animator.SetBool("IsMove", true);
                m_Monstate = MonsterState.CHASE;
            }

            //공격기능
            if (m_CurattackDelay >= 0.0f)
            {
                m_CurattackDelay -= Time.deltaTime;
                if (m_CurattackDelay <= 0.0f)
                {
                    m_CurattackDelay = m_AttackDelay;
                    if(middleattackDistance > m_CalcVec.magnitude)
                        m_Animator.SetTrigger("Attack");
                    else
                        m_Animator.SetTrigger("ShotAttack");
                    attackDealy_2 = 1.0f;

                }
            }
        }
    }

    public void MiddleAttack()
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

    public void ShotWeapon()
    {
        GameObject weapon = Instantiate(weaponPrefab);
        weapon.transform.position = weaponPos.position;
        weapon.transform.rotation = this.transform.rotation;
    }


    protected override void Die()
    {
        Time.timeScale = 0.3f;
        m_Monstate = MonsterState.DIE;
        m_Animator.SetBool("CanAttack", false);
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        m_Animator.SetTrigger("Die");
    }

    public void SpawnBoss()
    {
        GameObject boss = Instantiate(bossPrefab);
        boss.transform.position = bossSpawnPos.transform.position;
        boss.transform.eulerAngles = bossSpawnPos.eulerAngles;
        this.m_Animator.enabled = false;
        Destroy(this.gameObject);
    }
}
