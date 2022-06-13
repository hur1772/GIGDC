using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealItemNoodCtrl : MonoBehaviour
{
    [HideInInspector] public ItemType m_ItemType = ItemType.ItemCount;  //√ ±‚»≠

    public Image m_HealIconImg;
    public Text m_HealItemNumTxt;
    public Button m_LBtn = null;
    public Button m_RBtn = null;
    int m_MaxNum = 5;
    int m_CurNum = 0;

    // Start is called before the first frame update
    void Start()
    {
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
        m_HealItemNumTxt.text = m_CurNum.ToString()+" / 5";
    }

    public void InitData(ItemType a_ItemType)
    {
        if (a_ItemType < ItemType.Item_0 || ItemType.ItemCount <= a_ItemType)
            return;

        m_ItemType = a_ItemType;
        m_HealIconImg.sprite = GlobalUserData.m_ItemDataList[(int)a_ItemType].m_ShopIconImg;

    }

    public void SetState(int a_Price)
    {
        m_HealItemNumTxt.text = "0/5";
    }

    void LBtnFunc()
    {
        if (m_CurNum > 0)
        {
            m_CurNum--;
        }
        Debug.Log(m_CurNum);
    }

    void RBtnFunc()
    {
        if (m_CurNum < m_MaxNum)
        {
            m_CurNum++;
        }
        Debug.Log(m_CurNum);
    }
}
