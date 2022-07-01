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
    [HideInInspector] public ItemType m_ItemType = ItemType.ItemCount;  //�ʱ�ȭ
    [HideInInspector] public ItemState m_ItemState = ItemState.Lock;

    ItemStoreMgr m_StoreMgr = null;

    public Image m_ItemIconImg;
    public Button m_BuyBtn = null;

    // Start is called before the first frame update
    void Start()
    {
        m_StoreMgr = null;
        GameObject a_StoreObj = GameObject.Find("Store_Mgr");
        if (a_StoreObj != null)
            m_StoreMgr = a_StoreObj.GetComponent<ItemStoreMgr>();

        if (m_BuyBtn != null)
        {
            m_BuyBtn.onClick.AddListener(() =>
            {
                if (m_StoreMgr != null)
                    m_StoreMgr.BuySkItem(m_ItemType);
            });
        }//if (m_BtnCom != null)       

        ////m_LBtn = GetComponentInChildren<Button>();
        //if (m_BuyBtn != null)
        //    m_BuyBtn.onClick.AddListener(m_BuyBtnFunc);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_StoreMgr != null)
        {
            Debug.Log(m_ItemType);
            if (IsCollSlot(m_ItemIconImg.gameObject) == true)
            {
                m_StoreMgr.ShowToolTip((int)m_ItemType, transform.position);
            }
        }

    }

    bool IsCollSlot(GameObject a_CkObj)  //���콺�� UI ���� ������Ʈ ���� �ִ���? �Ǵ��ϴ� �Լ�
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
        if (a_CrState == ItemState.Lock) //��� ����
        {
            m_ItemIconImg.gameObject.SetActive(false);
            //Debug.Log(a_CrState + "Lock");
        }
        else if (a_CrState == ItemState.Active) //Ȱ��ȭ ����
        {
            //Debug.Log(a_CrState + "Active");
            m_ItemIconImg.gameObject.SetActive(true);
        }
    }

    void m_BuyBtnFunc()
    {
        Debug.Log("�����ϴ�");
    }
}
