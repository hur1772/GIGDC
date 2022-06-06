using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetCtrl : MonoBehaviour
{
    public Image HpBar = null;
    public GameObject ThisGameObject = null;

    float MaxHp = 100;
    float CurHp = 100;
    bool isdie = false;

    public Animator animator;
    float hitAnimTimer = 0.0f;
    float hitAnimCool = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        if (ThisGameObject.name == "ScarecrowObj")
        {
            if (animator != null)
            {
                animator = GetComponent<Animator>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        hitAnimTimer -= Time.deltaTime;
        if (ThisGameObject.name == "ScarecrowObj")
        {
            if (hitAnimTimer <= 0.0f)
            {
                animator.SetBool("Hit", false);
                hitAnimTimer = 0.0f;
            }
        }

        //CurHp -= Time.deltaTime *10;
        //HpBar.fillAmount = CurHp / MaxHp;

        //if (CurHp <= 0)
        //{
        //    Destroy(this.gameObject);
        //    TutorialMgr.m_TutorialState = TutorialState.NextStage;
        //}
    }

    public void TakeDamage(float a_Damage)
    {
        CurHp -= a_Damage;
        HpBar.fillAmount = CurHp / MaxHp;
        hitAnimTimer = hitAnimCool;

        if (ThisGameObject.name == "ScarecrowObj")
        {
            if (hitAnimTimer >= 0.25f)
            {
                animator.SetBool("Hit", true);
            }
        }

        if (CurHp <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("妊旋紫諺!");
            isdie = true;
            if(isdie == true)
            {
                Interaction.Inst.m_interactionState = InteractionState.Nomal;
                //TutorialMgr tutorial = GetComponent<TutorialMgr>();
                //tutorial.TutoGuidetext.gameObject.SetActive(true);
                //tutorial.TutoGuidetext.text = "しけいしいけしいけ";
            }
            
            //if(ThisGameObject.name == "ScarecrowObj")
            //{
            //    TutorialMgr.m_TutorialState = TutorialState.NextStage;
            //}

            //if(ThisGameObject.name == "TtargetObj")
            //{
            //    TutorialMgr.m_TutorialState = TutorialState.NextStage;
            //}
        }
    }
}
