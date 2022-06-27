using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    //--이동 관련 변수들
    Vector3 m_DirTgVec = Vector3.right; //날아 가야할 방향 계산용 변수
    Vector3 a_StartPos = Vector3.zero;  //시작 위치 계산용 변수
    private float m_MoveSpeed = 15.0f;  //한플레임당 이동 시키고 싶은 거리 (이동속도)
    //--이동 관련 변수들
    [HideInInspector] public float bullet_Att = 20.0f;

    ////--유도탄 변수 
    //[HideInInspector] public bool homing_OnOff = false;   //유도탄 OnOff 
    //[HideInInspector] public bool isTaget = false;
    ////한번이라도 타겟이 잡힌 적인 있는지 확인 하는 변수
    //[HideInInspector] public GameObject taget_Obj = null; //타겟 참조 변수
    //Vector3 m_DesiredDir;  //타겟을 향하는 방향 변수
    //--유도탄 변수 

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10.0f); //안전장치
    }

    // Update is called once per frame
    void Update()
    {        
        //if (homing_OnOff == true) //유도탄인 경우...
        //{
        //    if (taget_Obj == null && isTaget == false) //타겟이 없으면...
        //    {
        //        FindEnemy();
        //    }

        //    if (taget_Obj != null) //타겟이 존재하면...
        //        BulletHoming();
        //    else //타겟이 사망했다면...
        //        transform.position += m_DirTgVec * Time.deltaTime * m_MoveSpeed;
        //}
        //else  //일반 총알인 경우
        //{
            transform.position += m_DirTgVec * Time.deltaTime * m_MoveSpeed;
        //}

        //if (InGameMgr.m_SceenWMax.x + 0.5f < this.transform.position.x)
        // 총알 화면 밖으로 나가면 제거해 주기
        //if ( (InGameMgr.m_SceenWMax.x + 0.5f < this.transform.position.x) ||
        //     (this.transform.position.x < InGameMgr.m_SceenWMin.x - 1.0f) ||
        //     (InGameMgr.m_SceenWMax.y + 1.0f < this.transform.position.y) ||
        //     (this.transform.position.y < InGameMgr.m_SceenWMin.y - 1.0f) )
        //{
        //    Destroy(gameObject);
        //}

        //if (this.transform.position.x < InGameMgr.m_SceenWMin.x - 1.0f)
        //{
        //    Destroy(gameObject);
        //}
    }

    public void BulletSpawn(Vector3 a_OwnPos, Vector3 a_DirTgVec,
                        float a_MvSpeed = 15.0f, float att = 20.0f)
    {
        m_DirTgVec = a_DirTgVec;
        a_StartPos = a_OwnPos + (m_DirTgVec * 0.5f);
        a_StartPos.y += 2.3f;
        transform.position = new Vector3(a_StartPos.x,
                                         a_StartPos.y, 0.0f);

        if(a_DirTgVec.x <= 0)
        {
            Vector3 angle = transform.eulerAngles;
            angle.y = 180;
            transform.eulerAngles = angle;
        }

        m_MoveSpeed = a_MvSpeed;
        bullet_Att = att;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("hit");
        if (coll.gameObject.tag == "Monster")
        {
            Debug.Log("Hit");
            coll.gameObject.GetComponent<Monster>().TakeDamage(bullet_Att, 100.0f);
            Destroy(gameObject);
        }
    }


    //void FindEnemy()
    //{
    //    GameObject[] a_EnemyList = GameObject.FindGameObjectsWithTag("Monster");

    //    if (a_EnemyList.Length <= 0) //그냥 먼 어딘 가를 추적하게 한다.
    //        return;

    //    GameObject a_Find_Mon = null;
    //    float a_CacDist = 0.0f;
    //    Vector3 a_CacVec = Vector3.zero;
    //    for (int i = 0; i < a_EnemyList.Length; ++i)
    //    {
    //        a_CacVec = a_EnemyList[i].transform.position - transform.position;
    //        a_CacVec.z = 0.0f;
    //        a_CacDist = a_CacVec.magnitude;

    //        if (4.0f < a_CacDist) //4.0f ~ 5.0f
    //            continue;

    //        a_Find_Mon = a_EnemyList[i].gameObject;
    //        break;
    //    }//for (int i = 0; i < a_EnemyList.Length; ++i)

    //    taget_Obj = a_Find_Mon;
    //    if (taget_Obj != null)
    //        isTaget = true;
    //}//void FindEnemy()

    //void BulletHoming()
    //{
    //    m_DesiredDir = taget_Obj.transform.position - transform.position;
    //    m_DesiredDir.z = 0.0f;
    //    m_DesiredDir.Normalize();

    //    //적을 향해 회전 이동하는 방법
    //    float angle = Mathf.Atan2(m_DesiredDir.y, m_DesiredDir.x) * Mathf.Rad2Deg;
    //    Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
    //    transform.rotation = angleAxis;
    //    m_DirTgVec = transform.right;
    //    transform.Translate(Vector3.right * m_MoveSpeed * Time.deltaTime);
    //}

}
