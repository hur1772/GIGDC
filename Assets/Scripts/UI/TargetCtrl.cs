using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetCtrl : MonoBehaviour
{
    public Image HpBar = null;

    float MaxHp = 100;
    float CurHp = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurHp -= Time.deltaTime *10;
        HpBar.fillAmount = CurHp / MaxHp;

        if(CurHp <=0)
        {
            Destroy(this.gameObject);
            TutorialMgr.m_TutorialState = TutorialState.NextStage;
        }
    }
}
