using UnityEngine;
using UnityEngine.UI;

public class BossCtrl1_1 : Monster
{
    public float m_DelayTime = 0.0f;
    bool m_IsRight = false;
    Vector2 attackSize = new Vector2(7, 4);
    public GameObject Protal;

    //변환 및 사망    
    bool isDisappear = false;
    float dieAlpha = 1.0f;
    Color dieColor = Color.white;
    SpriteRenderer _spRenderer;
    [SerializeField] Image HPBar = null;

    public float m_SizeUpDelay = 4.0f;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        InitMonster(); 
        m_Monstate = MonsterState.CHASE;
        m_Animator.SetBool("IsMove", true);

        _spRenderer = GetComponent<SpriteRenderer>();
        
        if(Protal != null)
        {
            Protal.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(originPos, (attackPos.position - originPos));
        Gizmos.DrawCube(attackPos.position, new Vector2(3, 3));
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        originPos = new Vector3(this.transform.position.x, attackPos.position.y, 0.0f); 
        CheckDistanceFromPlayer();
            
        AiUpdate();

        if(m_SizeUpDelay > 0.0f)
        {
            m_SizeUpDelay -= Time.deltaTime;
        }
    }

    void AiUpdate()
    {
        if (m_Player == null)
        {
            Debug.Log("Player Null");
            return;
        }

        if (m_SizeUpDelay <= 0.0f)
        {
            if (m_Monstate == MonsterState.CHASE)
            {
                ChaseState();
            }
            else if (m_Monstate == MonsterState.ATTACK)
            {
                AttackState();
            }
            else if (m_Monstate == MonsterState.DIE)
            {
                DieState();
            }
            else if (m_Monstate == MonsterState.CORPSE)
            {
                CorpseState();
            }
            else if (m_Monstate == MonsterState.Hitted)
            {
                HittedState();
            }
        }
    }


    void ChaseState()
    {
        if (m_CalcVec.x >= 0.1f)
        {
            m_Rb.transform.position += Vector3.right * m_MoveSpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
        }
        else if (m_CalcVec.x <= -0.1f)
        {
            m_Rb.transform.position += Vector3.left * m_MoveSpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (m_CalcVec.magnitude <= m_AttackDistance)
        {
            m_Monstate = MonsterState.ATTACK;
            int randMotion = Random.Range(0, 3);
            if (randMotion == 0)
                m_Animator.SetBool("IsAttack", true);
            else if (randMotion == 1)
                m_Animator.SetBool("IsAttack2", true);
            else
                m_Animator.SetBool("IsAttack3", true);
        }
    }

    void AttackState()
    {
        if (m_DelayTime >= 0.0f)
        {
            m_DelayTime -= Time.deltaTime;
            if (m_DelayTime <= 0.0f)
            {
                ChangeRotate1();
                if (m_CalcVec.magnitude >= m_AttackDistance)
                {
                    m_Monstate = MonsterState.CHASE;
                    m_Animator.SetBool("IsAttack2", false);
                    m_Animator.SetBool("IsAttack3", false);
                    m_Animator.SetBool("IsAttack", false);
                }
            }
        }
        else if (m_DelayTime <= 0.0f && m_Animator.GetBool("IsAttack") == false && m_Animator.GetBool("IsAttack2") == false)
            m_Monstate = MonsterState.CHASE;
    }

    void DieState()
    {
        m_Animator.SetTrigger("DieTrigger");
        m_Monstate = MonsterState.CORPSE;

        CoinDrop();

        if (Protal != null)
        {
            Protal.gameObject.SetActive(true);
        }
    }

    void CorpseState()
    {
        if (isDisappear)
        {
            dieAlpha -= Time.deltaTime;
            dieColor = new Color(1, 1, 1, dieAlpha);
            _spRenderer.color = dieColor;

            if (dieAlpha <= 0.05f)
                Destroy(this.gameObject);

        }
    }

    void HittedState()
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

    public override void TakeDamage(int WeaponState)
    {
        if (m_SizeUpDelay <= 0.0f)
        {
            HPBar.fillAmount = m_CurHP / m_MaxHP;
            base.TakeDamage(WeaponState);

            if (false && m_CurHP <= m_MaxHP * 0.5f)
            {
                m_Animator.SetTrigger("Change");
            }
        }
    }

    void Attackend()
    {
        m_DelayTime = 1.0f;
    }

    public void BossAttack()
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

    public void SecondAlienAttackEff()
    {
        Vector3 attackdir = attackPos.position - originPos;

        Collider2D coll = Physics2D.OverlapBox(attackPos.position, attackSize * 0.5f, 0, playerMask);
        if (coll != null)
        {
            if (coll.gameObject.TryGetComponent(out playerTakeDmg))
            {
                playerTakeDmg.P_TakeDamage(m_Atk);
            }
        }
        else
            Debug.Log("검출안됨");
    }

    /// <summary>
    /// 애니메이션 이벤트로 호출 이게 호출되면 데미지 리턴
    /// </summary>


    /// <summary>
    /// 애니메이션 이벤트로 호출 이게 호출되면 시체가 사라진다..
    /// </summary>

    void CorpseEff()
    {
        isDisappear = true;
    }
}
