using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    Transform player_trans;
    public float horizontal { get; private set; } 
    public float vertical { get; private set; }

    Animator animator;

    float BowAttTimer = 0.5f;
    float BowAttCurTimer = 0.0f;

    public GameObject m_BulletObj = null;
    GameObject a_NewObj = null;
    BulletCtrl a_BulletSC = null;
    //---------- �Ѿ� �߻� ���� ���� ����
    int ArrowNum = 0;


    // �̷������� ���� �߰��ؼ� Input class �����
    public bool fire { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        player_trans = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        BowAttCurTimer -= Time.deltaTime;
        if(BowAttCurTimer <= 0.3f && BowAttCurTimer >= 0.2f)
        {
            if (ArrowNum == 0)
            {
                a_NewObj = (GameObject)Instantiate(m_BulletObj);
                //������Ʈ�� Ŭ��(����ü) ���� �Լ�   
                a_BulletSC = a_NewObj.GetComponent<BulletCtrl>();
                Debug.Log(this.transform.position);
                a_BulletSC.BulletSpawn(this.transform.position, Vector3.right);
                ArrowNum++;
            }           
        }
        if (BowAttCurTimer <= 0.0f)
        {
            animator.SetTrigger("Bow_Attack_End");
            BowAttCurTimer = 0.0f;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (GlobalUserData.Player_Att_State == PlayerAttackState.player_no_att || GlobalUserData.Player_Att_State == PlayerAttackState.player_sword)
            {
                animator.SetTrigger("Sword_Attack_Start");
            }
            if (GlobalUserData.Player_Att_State == PlayerAttackState.player_bow)
            {
                animator.SetTrigger("Bow_Attack");
                BowAttCurTimer = BowAttTimer;
                ArrowNum = 0;
            }
        }
    }
}
