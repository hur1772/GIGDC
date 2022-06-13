using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNoodCtrl : MonoBehaviour
{
    [HideInInspector] public ItemType m_ItemType = ItemType.ItemCount;  //초기화

    public Image m_ItemIconImg;
    public Button m_BuyBtn = null;

    // Start is called before the first frame update
    void Start()
    {
        //m_LBtn = GetComponentInChildren<Button>();
        if (m_BuyBtn != null)
            m_BuyBtn.onClick.AddListener(m_BuyBtnFunc);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitData(ItemType a_ItemType)
    {
        if (a_ItemType < ItemType.Item_0 || ItemType.ItemCount <= a_ItemType)
            return;

        m_ItemType = a_ItemType;
        m_ItemIconImg.sprite = GlobalUserData.m_ItemDataList[(int)a_ItemType].m_ShopIconImg;

    }

    public void SetState(int a_Price)
    {
    }

    void m_BuyBtnFunc()
    {
        Debug.Log("구매하다");
    }
}
