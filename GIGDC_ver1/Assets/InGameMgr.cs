using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMgr : MonoBehaviour
{
    public Text m_TimerText;
    public Text m_InfoText;
    private float m_LifeTimer;
    private float m_InfoTimer;
  

    // Start is called before the first frame update
    void Start()
    {
        m_LifeTimer = 10.0f;
        m_InfoTimer = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_LifeTimer <= 0.0f)
            return;
       
        if (m_TimerText != null)
        {
            m_LifeTimer -= Time.deltaTime;
            m_InfoTimer -= Time.deltaTime;
        }
        m_TimerText.text = ((int)m_LifeTimer / 60 % 60).ToString() + " : " +
        ((int)m_LifeTimer % 60).ToString();//+ Mathf.Round(m_Timer) + "초";

        if(m_InfoTimer < 30.0f)
        {
            m_InfoText.gameObject.SetActive(true);
            m_InfoText.text = "사람들이 도망갈 때까지 생존하기";
        }
        if(m_InfoTimer <= 0.0f)
        {
            m_InfoText.text = "<color=#0000ff>" + "Clear!!" + "</color>";
            if (m_InfoTimer <= -5.0f)
                m_InfoText.gameObject.SetActive(false);
        }

    }
}
