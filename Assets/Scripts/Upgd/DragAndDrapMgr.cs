using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDrapMgr : MonoBehaviour
{
    public SlotScript[] m_SlotSc;
    public RawImage[] a_MsObj;

    int m_SaveIndex = -1;
    int m_DrtIndex = -1;  //Direction Index
    bool m_IsPick = false;

    //-------- 아이콘 투명하게 사라지게 하기 연출용 변수
    private float AniDuring = 0.8f;  //페이드아웃 연출을 시간 설정
    private float m_CacTime = 0.0f;
    private float m_AddTimer = 0.0f;
    private Color m_Color;
    //-------- 아이콘 투명하게 사라지게 하기 연출용 변수

    //[Header("-------- Buy Item --------")]
    //public Text m_GoldTxt;
    //public Text m_SkillTxt;

    [Header("-------- Info Txt --------")]
    //public Text m_InfoTxt;
    private float m_InfoDuring = 1.5f;  //페이드아웃 연출을 시간 설정
    private float m_InfoAddTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //GlobalUserData.LoadGameInfo();

        //if (m_GoldTxt != null)
        //{
        //    if (GlobalUserData.s_GoldCount <= 0)
        //        m_GoldTxt.text = "x 00";
        //    else
        //        m_GoldTxt.text = "x " +
        //                GlobalUserData.s_GoldCount.ToString("N0");
        //}

        //if (m_SkillTxt != null)
        //{
        //    if (GlobalUserData.s_SkillCount <= 0)
        //        m_SkillTxt.text = "x 00";
        //    else
        //        m_SkillTxt.text = "x " +
        //                GlobalUserData.s_SkillCount.ToString();
        //}//if(m_SkillTxt != null)   
    }

    // Update is called once per frame
    void Update()
    {             
        if (Input.GetMouseButtonDown(0)) //왼쪽 마우스 버튼을 클릭한 순간
        {
            for(int ii = 0; ii < m_SlotSc[2].ItemResultImg.Length;ii++ )
            {
                m_SlotSc[2].ItemResultImg[ii].gameObject.SetActive(false);
            }

            BuyMouseBtnDown();
        }//if (Input.GetMouseButtonDown(0))

        if (Input.GetMouseButton(0)) //왼쪽 마우스를 누르고 있는 동안
        {
            if (m_IsPick == true)
            {
                a_MsObj[m_SaveIndex].transform.position = Input.mousePosition;
            }
        }//if (Input.GetMouseButton(0)) 

        if (Input.GetMouseButtonUp(0)) //왼쪽 마우스를 누르고 있다가 뗀 순간
        {
            BuyMouseBtnUp();
        }//if (Input.GetMouseButtonUp(0)) 
        if(m_DrtIndex>1)
        {
            BuyDirection();
        }
    }// void Update()

    bool IsCollSlot(GameObject a_CkObj)  //마우스가 UI 슬롯 오브젝트 위에 있느냐? 판단하는 함수
    {
        Vector3[] v = new Vector3[4];
        a_CkObj.GetComponent<RectTransform>().GetWorldCorners(v);
        if (v[0].x <= Input.mousePosition.x && Input.mousePosition.x <= v[2].x &&
           v[0].y <= Input.mousePosition.y && Input.mousePosition.y <= v[2].y)
        {
            return true;
        }

        return false;
    }


    void MouseBtnDown()
    {
        m_SaveIndex = -1;

        for (int ii = 0; ii < m_SlotSc.Length; ii++)
        {
            if (m_SlotSc[ii].ItemImg.gameObject.activeSelf == true &&
               IsCollSlot(m_SlotSc[ii].gameObject) == true)
            {
                m_SaveIndex = ii;
                m_SlotSc[ii].ItemImg.gameObject.SetActive(false);
                m_IsPick = true;
                a_MsObj[m_SaveIndex].gameObject.SetActive(true);
                a_MsObj[m_SaveIndex].transform.position = Input.mousePosition;
                break;
            }
        }//for(int ii = 0; ii < m_SlotSc.Length; ii++)
    }//void MouseBtnDown()

    void MouseBtnUp()
    {
        if (m_IsPick == false)
            return;

        for (int ii = 0; ii < m_SlotSc.Length; ii++)
        {
            if (m_SlotSc[ii].ItemImg.gameObject.activeSelf == false &&
                IsCollSlot(m_SlotSc[ii].gameObject) == true)
            {
                m_SlotSc[ii].ItemImg.gameObject.SetActive(true);
                m_SlotSc[ii].ItemImg.color = Color.white;
                m_AddTimer = AniDuring;
                m_IsPick = false;
                a_MsObj[m_SaveIndex].gameObject.SetActive(false);
                break;
            }
        }//for(int ii = 0; ii < m_SlotSc.Length; ii++)

        if (m_IsPick == true && 0 <= m_SaveIndex)
        {
            m_SlotSc[m_SaveIndex].ItemImg.gameObject.SetActive(true);
            m_IsPick = false;
            a_MsObj[m_SaveIndex].gameObject.SetActive(false);
        }

    }// void MouseBtnUp()


    void BuyMouseBtnDown()
    {
        m_SaveIndex = -1;

        for (int ii = 0; ii < m_SlotSc.Length; ii++)
        {
            //구매 확정 슬롯에서부터 시작은 않하겠다는의미로 
            //구매 확정 슬롯인 경우 스킵
            if (ii == 2)
                continue;

            if (m_SlotSc[ii].ItemImg.gameObject.activeSelf == true &&
               IsCollSlot(m_SlotSc[ii].gameObject) == true)
            {
                m_SaveIndex = ii;
                m_SlotSc[ii].ItemImg.gameObject.SetActive(false);
                m_IsPick = true;
                a_MsObj[m_SaveIndex].gameObject.SetActive(true);
                a_MsObj[m_SaveIndex].transform.position = Input.mousePosition;
                break;
            }
        }//for(int ii = 0; ii < m_SlotSc.Length; ii++)
    }//void BuyMouseBtnDown()

    void BuyMouseBtnUp()
    {
        if (m_IsPick == false)
            return;

        for (int ii = 0; ii < m_SlotSc.Length; ii++)
        {
            if (ii <= 1) //자기 자리에 놓은 경우 구매 불가
                continue;

            if (m_SlotSc[ii].ItemImg.gameObject.activeSelf == false &&
            IsCollSlot(m_SlotSc[ii].gameObject) == true)
            {
                m_SlotSc[ii].ItemResultImg[m_SaveIndex].gameObject.SetActive(true);
                m_SlotSc[ii].ItemResultImg[m_SaveIndex].color = Color.white;
                m_DrtIndex = ii;
                m_AddTimer = AniDuring;
                m_IsPick = false;
                a_MsObj[m_SaveIndex].gameObject.SetActive(false);

                break;
            }
            
        }//for(int ii = 0; ii < m_SlotSc.Length; ii++)

        if (0 <= m_SaveIndex)
        {
            m_SlotSc[m_SaveIndex].ItemImg.gameObject.SetActive(true);
            m_IsPick = false;
            a_MsObj[m_SaveIndex].gameObject.SetActive(false);
        }

    }//void BuyMouseBtnUp()

    void BuyDirection() //구매 연출 함수
    {
        //---------- 장착된 아이콘이 서서히 사라지게 처리하는 연출
        if (0.0f <= m_AddTimer)
        {
            m_AddTimer = m_AddTimer - 0.01f;
            m_CacTime = m_AddTimer / AniDuring;
            m_Color = m_SlotSc[m_DrtIndex].ItemImg.color;
            m_Color.a = m_CacTime;
            m_SlotSc[m_DrtIndex].ItemResultImg[m_SaveIndex].color = m_Color;

            if (m_AddTimer <= 0.0f)
            {
                m_SlotSc[m_DrtIndex].ItemResultImg[m_SaveIndex].gameObject.SetActive(false);
            }

        }//if (0.0f < m_AddTimer)
        //----------장착된 아이콘이 서서히 사라지게 처리하는 연출

        //---------- 구매불가 텍스트 서서히 사라지게 처리하는 연출
        if (0.0f < m_InfoAddTimer)
        {
            m_InfoAddTimer = m_InfoAddTimer - Time.deltaTime;
            m_CacTime = m_InfoAddTimer / (m_InfoDuring - 1.0f);
            if (1.0f < m_CacTime)
                m_CacTime = 1.0f;
            //m_Color = m_InfoTxt.color;
            //m_Color.a = m_CacTime;
            //m_InfoTxt.color = m_Color;

            //if (m_InfoAddTimer <= 0.0f)
            //{
            //    m_InfoTxt.gameObject.SetActive(false);
            //}
        }//if (0.0f < m_InfoAddTimer)
         //---------- 구매불가 텍스트 서서히 사라지게 처리하는 연출

    }//void BuyDirection() //구매 연출 함수
}
