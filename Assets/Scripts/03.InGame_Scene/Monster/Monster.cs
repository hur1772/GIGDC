using UnityEngine;

public enum MonsterState
{
    IDLE,
    PATROL,
    CHASE,
    ATTACK,
    SKILL,
    SKILL2,
    SKILL3,
    STAY,
    DIE,
    Hitted,
    CORPSE
}

public enum FlyMonsterState
{
    IDLE,
    PATROL,
    FLY,
    ATTACK,
    CHASE,
    DIE,
    CORPSE
}

public class Monster : MonoBehaviour
{
    //�÷��̾�, �Ÿ� ���ϱ�� ����
    protected GameObject m_Player;
    protected Vector2 m_CalcVec = Vector3.zero;
    protected Rigidbody2D m_Rb;

    //���� �⺻ ����, ����
    public float m_MaxHP = 0;
    public float m_CurHP = 0;
    public float m_Atk   = 0;
    public float m_MoveSpeed = 0;
    [HideInInspector] public bool MonRight;

    public float m_ChaseDistance = 0;
    public float m_AttackDistance = 0;

    public MonsterState m_Monstate = MonsterState.IDLE;
    protected Animator m_Animator;

    //���� ����ĳ��Ʈ
    protected RaycastHit2D attackhit;
    protected Player_TakeDamage playerTakeDmg;
    public LayerMask playerMask;
    public Transform attackPos;
    public Vector3 originPos;

    // ����
    protected float HittedTIme;


    

    //private void Update()
    //{
        
    //}

    //private void Start()
    //{
        
    //}

    void AiUpdate()
    {
        if (m_Player == null)
        {
            Debug.Log("Player Null");
            return;
        }

        if (m_Monstate == MonsterState.IDLE)
        {

        }
        else if (m_Monstate == MonsterState.PATROL)
        {

        }
        else if (m_Monstate == MonsterState.CHASE)
        {

        }
        else if (m_Monstate == MonsterState.SKILL)
        {

        }
        else if (m_Monstate == MonsterState.ATTACK)
        {

        }
    }

    protected virtual void ChangeRotate1()
    {
        if (m_CalcVec.x < 0.0f)
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        else
            this.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    }

    protected virtual void ChangeRotate2()
    {
        if (m_CalcVec.x < 0.0f)
        {
            this.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            MonRight = false;
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            MonRight = true;
        }
    }


    protected virtual void InitMonster()
    {
        if (m_Animator == null)
            m_Animator = this.GetComponent<Animator>();

        if (m_Player == null)
            m_Player = GameObject.Find("Player");

        if (m_Rb == null)
            m_Rb = GetComponent<Rigidbody2D>();

        m_CurHP = m_MaxHP;
       
    }

    protected virtual void CheckDistanceFromPlayer(bool OnlyX = false)
    {
        m_CalcVec = m_Player.transform.position - this.transform.position;
        if (OnlyX)
            m_CalcVec.y = 0.0f;
        m_Animator.SetFloat("DistanceFromPlayer", m_CalcVec.magnitude);
    }

    public virtual void TakeDamage(int WeaponState)
    {
        if (m_CurHP <= 0.0f)
            return;

        float a_DamVal = GlobalUserData.m_weaponDataList[WeaponState].m_WeaponDamage;
        float a_CritVal = GlobalUserData.m_weaponDataList[WeaponState].m_Critical;
        float a_CritDmg = a_DamVal + (a_DamVal * (GlobalUserData.m_weaponDataList[WeaponState].m_CriticalDmg ));

        int crit = Random.Range(0, 100);

        if(crit < a_CritVal)
        {
            //m_CurHP -=  * GlobalUserData.s_PlayerCriticalDmg;

            m_CurHP -= a_CritDmg;
            if(m_CurHP > 0.0f)
            {
                m_Animator.SetTrigger("Hitted");
                m_Monstate = MonsterState.Hitted;
                HittedTIme = 0.5f;
            }
            Debug.Log("crit!");
            //���� �߰��� ���⼭
        }
        else
        {
            m_CurHP -= a_DamVal;
            Debug.Log("�����ǰ�");
        }

        if (m_CurHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        m_CurHP = 0;
        m_Monstate = MonsterState.DIE;
    }

    public virtual void CoinDrop()
    {
        Vector3 m_Cacy = Vector3.zero;
        m_Cacy.y = transform.position.y + 2.0f;
        GameObject m_Gold = null;        
        m_Gold = (GameObject)Instantiate(Resources.Load("Gold"));
        m_Gold.transform.position = new Vector3(transform.position.x, m_Cacy.y, transform.position.z);
    }
}
