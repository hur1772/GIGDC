using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public Image m_HpBar = null;
    public Image UseItemImg1 = null;
    public Image UseItemImg2 = null;
    public Image UseItemImg3 = null;

    public Text GoldTxt;

    float m_CurHp;
    float m_MaxHp;
    float UseItemCoolTime1 = 5.0f;
    float UseItemCoolTime2 = 5.0f;
    float UseItemCoolTime3 = 5.0f;

    Player_TakeDamage pTakeDamage = null;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        pTakeDamage = GetComponent<Player_TakeDamage>();
        if (GoldTxt != null)
            GoldTxt.text = GlobalUserData.s_GoldCount.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        //TakeDamage();

        //if (Input.GetKey(KeyCode.Alpha1) && UseItemCoolTime1 >= 5.0f)
        //{
        //    if (UseItemImg1 != null)
        //    {
        //        UseItemCoolTime1 = 0.0f;
        //    }
        //}

        //if (Input.GetKey(KeyCode.Alpha2) && UseItemCoolTime2 >= 5.0f)
        //{
        //    if (UseItemImg2 != null)
        //    {
        //        UseItemCoolTime2 = 0.0f;
        //    }
        //}

        //if (Input.GetKey(KeyCode.Alpha3) && UseItemCoolTime3 >= 5.0f)
        //{
        //    if (UseItemImg3 != null)
        //    {
        //        UseItemCoolTime3 = 0.0f;
        //    }
        //}

        //if (UseItemCoolTime1 <= 5.0f)
        //{
        //    UseItemCoolTime1 += Time.deltaTime;
        //    UseItemImg1.fillAmount = UseItemCoolTime1 / 5;
        //}

        //if (UseItemCoolTime2 <= 5.0f)
        //{
        //    UseItemCoolTime2 += Time.deltaTime;
        //    UseItemImg2.fillAmount = UseItemCoolTime2 / 5;
        //}

        //if (UseItemCoolTime3 <= 5.0f)
        //{
        //    UseItemCoolTime3 += Time.deltaTime;
        //    UseItemImg3.fillAmount = UseItemCoolTime3 / 5;
        //}

        m_CurHp = pTakeDamage.curHp;
        m_MaxHp = pTakeDamage.maxHp;


        if (GoldTxt != null)
            GoldTxt.text = GlobalUserData.s_GoldCount.ToString();


        if (m_HpBar != null)
            m_HpBar.fillAmount = m_CurHp / m_MaxHp;
    }

    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    if(coll.gameObject.tag == "Monster")
    //    {
    //        pTakeDamage.P_TakeDamage();
    //        TakeDamage(10.0f);
    //    }

    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Monster")
    //    {
    //        pTakeDamage.P_TakeDamage(10.0f);

    //    }
    //}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name.Contains("Gold") == true)
        {
            GoldCtrl ctrl = coll.gameObject.GetComponent<GoldCtrl>();
            
            if(ctrl.isGet == true)
            {
                AddGold(100);
                ctrl.isGet = false;
                Destroy(coll.gameObject);
            }

        }
    }

    public void AddGold(int a_Val = 100)
    {
        GlobalUserData.s_GoldCount += a_Val;
        GoldTxt.text = GlobalUserData.s_GoldCount.ToString();
    }

    //public void TakeDamage(float a_val)
    //{
    //    if (m_CurHp <= 0.0f)
    //        return;
    //    if (pTakeDamage == null)
    //        return;
        
    //    m_CurHp -= a_val;    
    //}
}
