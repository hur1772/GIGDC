using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMgr : MonoBehaviour
{
    public Image m_HpBar = null;
    public Image UseItemImg1 = null;
    public Image UseItemImg2 = null;

    public Image HealItemPanel1 = null;
    public Image HealItemPanel2 = null;

    public Text CurNum1Txt = null;
    public Text CurNum2Txt = null;

    public Text GoldTxt;


    float m_CurHp;
    float m_MaxHp;

    Player_TakeDamage pTakeDamage = null;

    // Start is called before the first frame update
    void Start()
    {
        //GlobalUserData.Load();
        Time.timeScale = 1.0f;
        pTakeDamage = GetComponent<Player_TakeDamage>();
        if (GoldTxt != null)
            GoldTxt.text = GlobalUserData.s_GoldCount.ToString();

        GlobalUserData.InitData();

    }

    // Update is called once per frame
    void Update()
    {
        //TakeDamage();
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

        if(CurNum1Txt != null)
        {
            CurNum1Txt.text = GlobalUserData.m_ItemDataList[0].m_CurItemCount.ToString();
        }

        if (CurNum2Txt != null)
        {
            CurNum2Txt.text = GlobalUserData.m_ItemDataList[1].m_CurItemCount.ToString();
        }

        m_CurHp = pTakeDamage.curHp;
        m_MaxHp = pTakeDamage.maxHp;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (UseItemImg1 != null)
            {
                if (GlobalUserData.m_ItemDataList[0].m_CurItemCount == 0)
                {
                    return;
                }

                if (GlobalUserData.m_ItemDataList[0].m_CurItemCount > 0)
                {
                    SoundMgr.Instance.PlayEffSound("Potion", 0.3f);
                    if (m_CurHp < m_MaxHp)
                    {
                        GlobalUserData.m_ItemDataList[0].m_CurItemCount--;
                        pTakeDamage.curHp += 30;
                        m_CurHp += 30;
                    }
                }
            }
        }



        if (Input.GetKeyDown(KeyCode.R))
        {
            if (UseItemImg2 != null)
            {
                if (GlobalUserData.m_ItemDataList[1].m_CurItemCount == 0)
                    return;

                if (GlobalUserData.m_ItemDataList[1].m_CurItemCount > 0)
                {
                    SoundMgr.Instance.PlayEffSound("Potion", 0.5f);
                    if (m_CurHp < m_MaxHp)
                    {
                        GlobalUserData.m_ItemDataList[1].m_CurItemCount--;
                        pTakeDamage.curHp += 50;
                        m_CurHp += 50;
                    }
                }
            }
        }

        if (UseItemImg1 != null)
        {
            if (GlobalUserData.m_ItemDataList[0].m_CurItemCount == 0 && HealItemPanel1 != null)
                HealItemPanel1.gameObject.SetActive(true);
            if (GlobalUserData.m_ItemDataList[0].m_CurItemCount > 0)
                HealItemPanel1.gameObject.SetActive(false);
        }
        if (UseItemImg2 != null)
        {
            if (GlobalUserData.m_ItemDataList[1].m_CurItemCount == 0 && HealItemPanel2 != null)
                HealItemPanel2.gameObject.SetActive(true);
            if (GlobalUserData.m_ItemDataList[1].m_CurItemCount > 0)
                HealItemPanel2.gameObject.SetActive(false);
        }


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
            SoundMgr.Instance.PlayEffSound("Coin", 0.2f);
            if (ctrl.isGet == true)
            {
                AddGold(100);
                ctrl.isGet = false;
                Destroy(coll.gameObject);
            }

        }
        else if(coll.gameObject.name.Contains("BossDrop")== true)
        {
            BossDropCtrl ctrl = coll.gameObject.GetComponent<BossDropCtrl>();
            if (ctrl.isGet == true)
            {
                AddBoss();
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

    public void AddBoss()
    {
        if (StageMgr.Inst.InfoText != null)
        {
            StageMgr.Inst.InfoText.gameObject.SetActive(true);
            StageMgr.Inst.InfoTimer = 5.0f;
            StageMgr.Inst.InfoText.text = "무기도감을 획득했습니다.\n" + "마을로 가서 무기를 강화하십시오";
        }
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
