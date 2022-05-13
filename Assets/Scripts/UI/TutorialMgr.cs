using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TutorialState
{
    NextStage,
    TargetStage,
    Fight,
    ScarecrowStage,
    Clear
}

public class TutorialMgr : MonoBehaviour
{
    public static TutorialState m_TutorialState = TutorialState.NextStage;

    public GameObject ScarecrowPrefab = null;
    public GameObject TargetPrefab = null;

    GameObject PrefabPos = null;


    [HideInInspector] public int StageLv = 0;

    [Header("------ GuideText ------")]
    public Text m_InfoText = null;
    public Text m_CurTxt = null;
    float m_CurTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        StageLv = 0;
        m_InfoText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GuideTimer();
        if (m_CurTimer > 20.0f)
        {
            if (StageLv == 1)
            {
                m_TutorialState = TutorialState.TargetStage;
            }

            else if (StageLv == 3)
            {
                m_TutorialState = TutorialState.ScarecrowStage;
            }
            else if (StageLv == 5)
            {
                m_TutorialState = TutorialState.Clear;
            }

            switch (m_TutorialState)
            {
                case TutorialState.NextStage:
                    StageLv++;
                    break;

                case TutorialState.TargetStage:
                    PrefabPos = Instantiate(TargetPrefab) as GameObject;
                    StageLv++;
                    m_TutorialState = TutorialState.Fight;
                    break;

                case TutorialState.ScarecrowStage:
                    PrefabPos = Instantiate(ScarecrowPrefab) as GameObject;
                    StageLv++;
                    m_TutorialState = TutorialState.Fight;
                    break;

                case TutorialState.Clear:

                    break;

            }
        }

    }

    void GuideTimer()
    {
        if (0.0f <= m_CurTimer)
        {
            m_CurTimer += Time.deltaTime;
            
            if (m_CurTxt != null)
                m_CurTxt.text = " ������\n " + ((int)m_CurTimer / 60 % 60).ToString("00") + " : " +
             ((int)m_CurTimer % 60).ToString("00");//+ Mathf.Round(m_Timer) + "��";        
            if (m_CurTimer > 0.0f)
            {
                m_InfoText.gameObject.SetActive(true);
                m_InfoText.text = "�����忡 ���Ű��� ȯ���մϴ�.\n �� ������ ���� ����� ���ʽÿ�.";
                
            }
            if(m_CurTimer > 7.0f)
            {
                m_InfoText.gameObject.SetActive(false);
            }
            if (m_CurTimer > 10.0f)
            {
                m_InfoText.gameObject.SetActive(true);
                m_InfoText.text = "10�� �� ������ ���� �˴ϴ�.";
            }
            if (m_CurTimer > 20.0f)
            {
                m_InfoText.text = "������ �����Ǿ����ϴ�.\n ��Ŭ������ ���� �����ϼ���! ";
            }
            if(m_CurTimer > 30.0f)
            {
                m_InfoText.gameObject.SetActive(false);
            }

        }
    }
}
