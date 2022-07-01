using UnityEngine;

public class BossMan : Monster
{
    public float m_DelayTime = 0.0f;
    bool m_IsRight = false;
    Vector2 attackSize = new Vector2(3, 3);

    float changeWeaponTime = 3.0f;

    //소환 몬스터
    [Header("--- SpawnMonster ---")]
    [SerializeField] GameObject humanManMon;
    [SerializeField] GameObject humanWomanMon;
    [SerializeField] Transform manSpawnPos;
    [SerializeField] Transform womanSpawnPos;

    bool firstSpawn = false, secondSpawn = false;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        InitMonster();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(originPos, (attackPos.position - originPos));
        Gizmos.DrawCube(attackPos.position, new Vector2(3, 3));
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        //originPos = new Vector3(this.transform.position.x, attackPos.position.y, 0.0f);

        CheckDistanceFromPlayer();
        AiUpdate();
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

            if (m_CalcVec.magnitude <= m_ChaseDistance) // 일정거리 안에 들어올 시 변신
            {
                m_Monstate = MonsterState.PATROL;
                m_Animator.SetBool("IsChange", true);
            }
        }
        else if(m_Monstate == MonsterState.PATROL)  //여기서는 Patrol이 변신 과정
        {
            if (changeWeaponTime >= 0.0f)
            {
                changeWeaponTime -= Time.deltaTime;
                if (changeWeaponTime <= 0.0f)
                {
                    m_Animator.SetBool("IsMove", true);
                    m_Monstate = MonsterState.CHASE;
                }
            }
            else
            {
                m_Monstate = MonsterState.CHASE;
                m_Animator.SetBool("IsMove", true);
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

            if (m_CalcVec.magnitude <= m_AttackDistance)
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
            if (m_CalcVec.magnitude >= .5f)
            {
                m_Animator.SetBool("IsAttack", false);
            }
        }
        else if (m_Monstate == MonsterState.DIE)
        {
            m_Animator.SetTrigger("DieTrigger");
            m_Monstate = MonsterState.CORPSE;

            CoinDrop();
        }
        else if (m_Monstate == MonsterState.CORPSE)
        {
            //아무것도 안할거

        }
        else if (m_Monstate == MonsterState.Hitted)
        {
            m_Animator.SetBool("IsAttack", false);

            if (HittedTIme >= 0.0f)
            {
                HittedTIme -= Time.deltaTime;
                if (HittedTIme <= 0.0f)
                {
                    m_Monstate = MonsterState.CHASE;
                }
            }
        }
    }


    /// <summary>
    /// 몬스터 소환 스킬 애니메이터 이벤트 함수 호출
    /// </summary>
    void SpawnMonster()
    {
        GameObject man = Instantiate(humanManMon);
        man.transform.position = manSpawnPos.position;

        GameObject woman = Instantiate(humanWomanMon);
        woman.transform.position = womanSpawnPos.position;
    }

    public override void TakeDamage(int WeaponState)
    {
        base.TakeDamage(WeaponState);

        if (m_CurHP <= m_MaxHP * 0.7f && !firstSpawn)
        {
            m_Animator.SetTrigger("SpawnMonster");
            firstSpawn = true;
        }
        
        if(m_CurHP <= m_MaxHP * 0.4f && !secondSpawn)
        {
            m_Animator.SetTrigger("SpawnMonster");
            secondSpawn = true;
        }


    }

    public void BossManAttackEff()
    {

        Vector3 attackdir = attackPos.position - originPos;

        //attackhit = Physics2D.Raycast(originPos, attackdir, attackdir.magnitude, playerMask);
        //if (attackhit)
        //{
        //    if (attackhit.collider.gameObject.TryGetComponent(out playerTakeDmg))
        //    {
        //        Debug.Log("몬스터에 의한 데미지");
        //        playerTakeDmg.P_TakeDamage(m_Atk);
        //    }
        //}

        Collider2D coll = Physics2D.OverlapBox(attackPos.position, attackSize * 0.5f, 0, playerMask);
        if (coll != null)
        {
            if (coll.gameObject.TryGetComponent(out playerTakeDmg))
            {
                Debug.Log("몬스터에 의한 데미지");
                playerTakeDmg.P_TakeDamage(m_Atk);
            }
        }
        else
            Debug.Log("검출안됨");
    }

}