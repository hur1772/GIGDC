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

    PadeOutMgr m_PadeIn;

    public GameObject ScarecrowPrefab = null;
    public GameObject TargetPrefab = null;

    GameObject PrefabPos = null;


    [HideInInspector] public int StageLv = 0;


    //public Text m_CurTxt = null;
    float m_CurTimer = 0.0f;




    // Start is called before the first frame update
    void Start()
    {
        StageLv = 0;
      
        
    }

    // Update is called once per frame
    void Update()
    {
        PadeOutMgr.Inst.PadeIn();

        if (Interaction.Inst.m_interactionState == InteractionState.NPC_talkEnd)
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



}
