using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PadeOutMgr : MonoBehaviour
{
    public static PadeOutMgr Inst;
    public Image PadeOutPanel = null;
    public Image PadeInPanel = null;

    private float AniDuring = 2.0f;  //페이드아웃 연출을 시간 설정
    private float m_CacTime = 0.0f;
    private float m_PadeOutTimer = 0.0f;
    private float m_PadeInTimer = 2.0f;
    private Color m_Color;

    public void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Interaction.Inst.m_interactionState == InteractionState.king_talkEnd)
        {
            PadeOut();
        }
    }

    public void PadeOut()
    {
        if (PadeOutPanel != null)
        {
            PadeOutPanel.gameObject.SetActive(true);
            m_PadeOutTimer = m_PadeOutTimer + 0.01f;
            m_CacTime = m_PadeOutTimer / AniDuring;
            m_Color = PadeOutPanel.color;
            m_Color.a = m_CacTime;
            PadeOutPanel.color = m_Color;

            if (m_PadeOutTimer >= 2.0f)
            {
                SceneManager.LoadScene("01.TutorialMap");
                Interaction.Inst.m_interactionState = InteractionState.Nomal;
                //PadeOutPanel.gameObject.SetActive(false);
            }
        }
    }

    public void PadeIn()
    {
        if (PadeInPanel != null)
        {
            m_PadeInTimer = m_PadeInTimer - 0.01f;
            m_CacTime = m_PadeInTimer / AniDuring;
            m_Color = PadeInPanel.color;
            m_Color.a = m_CacTime;
            PadeInPanel.color = m_Color;

            if (m_PadeInTimer <= 0.0f)
            {
                PadeInPanel.gameObject.SetActive(false);
            }
        }
    }
}
