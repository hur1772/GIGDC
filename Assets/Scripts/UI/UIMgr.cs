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

    float m_CurHp;
    float m_MaxHp;
    float UseItemCoolTime1 = 5.0f;
    float UseItemCoolTime2 = 5.0f;
    float UseItemCoolTime3 = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_CurHp = 100.0f;
        m_MaxHp = m_CurHp;

        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //TakeDamage(0.01f);

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
    }


    public void TakeDamage(float a_val)
    {
        if (m_CurHp <= 0.0f)
            return;

        m_CurHp -= a_val;

        if (m_HpBar != null)
            m_HpBar.fillAmount = m_CurHp / m_MaxHp;

        if(m_CurHp <=0.0f)
        {
            Debug.Log("»ç¸Á!!");
            // PlayerDie();

            Time.timeScale = 0.0f;
        }
        
    }
}
