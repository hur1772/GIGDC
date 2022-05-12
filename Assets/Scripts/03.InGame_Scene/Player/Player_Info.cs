using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Info : MonoBehaviour
{
    
    public Image m_HpBar = null;
    public Text m_HpText = null;
    [HideInInspector] public float m_MaxHp;
    [HideInInspector] public float m_CurHp;
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_MaxHp = 100;
        m_CurHp = m_MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage(0.01f);
    }

    public void TakeDamage(float a_Val)
    {
        if (m_CurHp <= 0.0f)
            return;
        
        m_CurHp = m_CurHp - a_Val;
        if (m_HpText != null)
            m_HpText.text = m_CurHp.ToString("N0") + " / " + m_MaxHp.ToString("N0");

        if (m_CurHp <= 0.0f)
            m_CurHp = 0.0f;

        if (m_HpBar != null)
            m_HpBar.fillAmount = m_CurHp / m_MaxHp;

        if(m_CurHp==0.0f)
        {
            //Time.timeScale = 0.0f;
            //GameOver();
        }
    }


}
