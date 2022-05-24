using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMgr : MonoBehaviour
{
    public Text m_TimerText;
    public Text m_InfoText;
    private float m_ShowMsTimer;
    private float m_EndureTimer;


    // Start is called before the first frame update
    void Start()
    {
        m_ShowMsTimer = 10.0f;
        m_EndureTimer = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (0.0f < m_ShowMsTimer) // 가이드 타이머 스타트
        {
            m_ShowMsTimer -= Time.deltaTime;
            TextOnOff("Stage1\n 감염된 멧돼지 공격을 버티세요! ", true); //가이드 on

            if(m_ShowMsTimer <= 0.0f) //n초뒤 사라짐
            {
                TextOnOff("", false);
            }
        }

        if (m_EndureTimer == 0.0f)
        {
            return;
        }

        if(0.0f < m_EndureTimer) //버티기 시간
        {
            m_EndureTimer -= Time.deltaTime;
            m_TimerText.text = "남은 시간 " +((int)m_EndureTimer / 60 % 60).ToString() + " : " +
            ((int)m_EndureTimer % 60).ToString();//+ Mathf.Round(m_Timer) + "초";
                                                             
        }
        else
        {
            m_TimerText.gameObject.SetActive(false);
            TextOnOff("Clear!!\n 포탈로 이동하세요!", true);
        }

    }

    void TextOnOff(string a_Mess = "", bool a_isOn = true)
    {
        if (a_isOn == true)
        {
            m_InfoText.text = a_Mess;
            m_InfoText.gameObject.SetActive(true);
            
        }
        else
        {
            m_InfoText.text = "";
            m_InfoText.gameObject.SetActive(false);
        }
    }
}
