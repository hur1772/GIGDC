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
        if (0.0f < m_ShowMsTimer) // ���̵� Ÿ�̸� ��ŸƮ
        {
            m_ShowMsTimer -= Time.deltaTime;
            TextOnOff("Stage1\n ������ ����� ������ ��Ƽ����! ", true); //���̵� on

            if(m_ShowMsTimer <= 0.0f) //n�ʵ� �����
            {
                TextOnOff("", false);
            }
        }

        if (m_EndureTimer == 0.0f)
        {
            return;
        }

        if(0.0f < m_EndureTimer) //��Ƽ�� �ð�
        {
            m_EndureTimer -= Time.deltaTime;
            m_TimerText.text = "���� �ð� " +((int)m_EndureTimer / 60 % 60).ToString() + " : " +
            ((int)m_EndureTimer % 60).ToString();//+ Mathf.Round(m_Timer) + "��";
                                                             
        }
        else
        {
            m_TimerText.gameObject.SetActive(false);
            TextOnOff("Clear!!\n ��Ż�� �̵��ϼ���!", true);
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
