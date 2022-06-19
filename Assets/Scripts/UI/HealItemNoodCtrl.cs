using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HealItemState
{
    Lock,
    Active
}

public class HealItemNoodCtrl : MonoBehaviour
{
    [HideInInspector] public ItemType m_ItemType = ItemType.ItemCount;  //초기화

    [HideInInspector] public HealItemState m_HealItemState = HealItemState.Lock;

    public Image m_HealIconImg;
    public Text m_HealItemNumTxt;
    public Button m_LBtn = null;
    public Button m_RBtn = null;
    int m_MaxNum = 5;
    public int m_CurNum = 0;
    int m_InitNum = 0;

    public Button m_BuyBtn = null;

    // Start is called before the first frame update
    void Start()
    {
        if (m_BuyBtn != null)
        {
            m_BuyBtn.onClick.AddListener(() =>
            {
                HealItemStoreMgr a_StoreMgr = null;
                GameObject a_StoreObj = GameObject.Find("Store_Mgr");
                if (a_StoreObj != null)
                    a_StoreMgr = a_StoreObj.GetComponent<HealItemStoreMgr>();
                if (a_StoreMgr != null)
                    a_StoreMgr.BuySkItem(m_ItemType, m_CurNum , m_InitNum);
                //Debug.Log(m_InitNum);
                //Debug.Log(m_CurNum);
                m_CurNum = 0;
            });
        }//if (m_BtnCom != null)    

        //m_LBtn = GetComponentInChildren<Button>();
        if (m_LBtn != null)
            m_LBtn.onClick.AddListener(LBtnFunc);

        //m_RBtn = GetComponentInChildren<Button>();
        if (m_RBtn != null)
            m_RBtn.onClick.AddListener(RBtnFunc);
    }

    // Update is called once per frame
    void Update()
    {
        m_InitNum = GlobalUserData.m_ItemDataList[(int)m_ItemType].m_CurItemCount;
        m_HealItemNumTxt.text = m_CurNum + "/" + (m_MaxNum - m_InitNum);
    }

    public void InitData(ItemType a_ItemType)
    {
        if (a_ItemType < ItemType.Item_0 || ItemType.ItemCount <= a_ItemType)
            return;

        m_InitNum = GlobalUserData.m_ItemDataList[(int)a_ItemType].m_CurItemCount;

        m_HealItemNumTxt.text = "0/" + (m_MaxNum - m_InitNum);

        m_ItemType = a_ItemType;
        m_HealIconImg.sprite = GlobalUserData.m_ItemDataList[(int)a_ItemType].m_ShopIconImg;

    }

    public void SetState(HealItemState a_HealItemState)
    {
        m_HealItemState = a_HealItemState;
        if (a_HealItemState == HealItemState.Lock) //잠긴 상태
        {
            m_HealIconImg.gameObject.SetActive(false);
        }
        else if (a_HealItemState == HealItemState.Active) //활성화 상태
        {
            m_HealIconImg.gameObject.SetActive(true);
        }
    }

    void LBtnFunc()
    {
        if (m_CurNum > 0)
        {
            m_CurNum--;
        }
    }

    void RBtnFunc()
    {
        if (m_CurNum < (m_MaxNum - m_InitNum))
        {
            m_CurNum++;
        }
    }
}
