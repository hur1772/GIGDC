using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemState
{
    Lock,
    Active
}

public class ItemNoodCtrl : MonoBehaviour
{
    [HideInInspector] public ItemType m_ItemType = ItemType.ItemCount;  //초기화
    [HideInInspector] public ItemState m_ItemState = ItemState.Lock;

    public Image m_ItemIconImg;
    public Button m_BuyBtn = null;

    // Start is called before the first frame update
    void Start()
    {
        if (m_BuyBtn != null)
        {
            m_BuyBtn.onClick.AddListener(() =>
            {
                ItemStoreMgr a_StoreMgr = null;
                GameObject a_StoreObj = GameObject.Find("Store_Mgr");
                if (a_StoreObj != null)
                    a_StoreMgr = a_StoreObj.GetComponent<ItemStoreMgr>();
                if (a_StoreMgr != null)
                    a_StoreMgr.BuySkItem(m_ItemType);
            });
        }//if (m_BtnCom != null)       

        ////m_LBtn = GetComponentInChildren<Button>();
        //if (m_BuyBtn != null)
        //    m_BuyBtn.onClick.AddListener(m_BuyBtnFunc);
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

    public void SetState(ItemState a_CrState)
    {
        m_ItemState = a_CrState;
        if (a_CrState == ItemState.Lock) //잠긴 상태
        {
            m_ItemIconImg.gameObject.SetActive(false);
            Debug.Log(a_CrState + "Lock");
        }
        else if (a_CrState == ItemState.Active) //활성화 상태
        {
            Debug.Log(a_CrState + "Active");
            m_ItemIconImg.gameObject.SetActive(true);
        }
    }

    void m_BuyBtnFunc()
    {
        Debug.Log("구매하다");
    }
}
