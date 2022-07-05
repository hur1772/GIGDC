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

    float m_Delay = 2.0f;

    float m_CurHp;
    float m_MaxHp;

    public static UIMgr Inst;

    BossCtrl1_1 Boss = null;
    SecondAlien Boss2 = null;
    Player_TakeDamage pTakeDamage = null;

    public Text GameEndTxt = null;

    [Header("--- GameEndObject ---")]
    public GameObject UseItem = null;
    public GameObject HpBack = null;
    public GameObject HpBar = null;
    public GameObject PIcon = null;
    public GameObject GoldImg = null;
    float m_PadeOutDelay = 2.0f;

    public Button TitleBtn = null;
    public Button SaveLoadBtn = null;

    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //GlobalUserData.Load();
        Time.timeScale = 1.0f;
        pTakeDamage = GetComponent<Player_TakeDamage>();
        if (GoldTxt != null)
            GoldTxt.text = GlobalUserData.s_GoldCount.ToString();

        if (TitleBtn != null)
            TitleBtn.onClick.AddListener(TitleBtnFunc);

        if (SaveLoadBtn != null)
            SaveLoadBtn.onClick.AddListener(SaveLoadBtnFunc);

        GlobalUserData.InitData();
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss == null && GameObject.Find("Stage1 Boss(Clone)") == true)
        {
            GameObject boss = GameObject.Find("Stage1 Boss(Clone)");
            Boss = boss.GetComponent<BossCtrl1_1>();
        }

        if(Boss!= null)
        {
            if(Boss.m_Monstate == MonsterState.DIE)
            {                
                m_Delay = 2.0f;
                
            }
        }

        if (Boss2 == null && GameObject.Find("Stage1-2 Boss(Clone)") == true)
        {
            GameObject boss = GameObject.Find("Stage1-2 Boss(Clone)");
            Boss = boss.GetComponent<BossCtrl1_1>();
        }

        if (Boss2 != null)
        {
            if (Boss2.m_Monstate == MonsterState.DIE)
            {
                m_Delay = 2.0f;

            }
        }

        m_Delay -= Time.deltaTime;
        if (0.0f >= m_Delay)
            m_Delay = 0.0f;

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

        if (CurNum1Txt != null)
        {
            CurNum1Txt.text = GlobalUserData.m_ItemDataList[0].m_CurItemCount.ToString();
        }

        if (CurNum2Txt != null)
        {
            CurNum2Txt.text = GlobalUserData.m_ItemDataList[1].m_CurItemCount.ToString();
        }

        m_CurHp = pTakeDamage.curHp;
        m_MaxHp = pTakeDamage.maxHp;

        if (pTakeDamage.Player_State.p_state != PlayerState.player_die)
        {
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
        }

        if(pTakeDamage.Player_State.p_state == PlayerState.player_die)
        {
            PadeOutMgr.Inst.PadeOut();
            UseItem.SetActive(false);
            HpBar.SetActive(false);
            HpBack.SetActive(false);
            PIcon.SetActive(false);
            GoldImg.SetActive(false);

            m_PadeOutDelay -= Time.deltaTime;

            if (m_PadeOutDelay <= 0.0f)
            {
                if (GameEndTxt != null)
                {
                    GameEndTxt.gameObject.SetActive(true);
                    TitleBtn.gameObject.SetActive(true);
                    SaveLoadBtn.gameObject.SetActive(true);
                }
            }

            //SceneManager.LoadScene("00.TitleScene");
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
                if (m_Delay <= 0.0f)
                {
                    AddBoss();
                    ctrl.isGet = false;
                    Destroy(coll.gameObject);
                }
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

            GlobalUserData.m_ItemDataList[8].m_CurItemCount += GlobalUserData.CurStageNum * 2;
            Debug.Log(GlobalUserData.m_ItemDataList[8].m_CurItemCount);
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

    void TitleBtnFunc()
    {
        GlobalUserData.s_GoldCount = 0;
        GlobalUserData.SwordTier = 0;
        GlobalUserData.BowTier = 0;
        GlobalUserData.CurStageNum = 0;

        SceneManager.LoadScene("00.TitleScene");
    }

    void SaveLoadBtnFunc()
    {
        GlobalUserData.Load();

        SceneManager.LoadScene("Village");
    }

}
