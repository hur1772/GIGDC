using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemStoreMgr : MonoBehaviour
{
    public Button m_ReturnBtn = null;
    public Text m_UserInfoTxt;

    public GameObject m_Item_ScrollContent; //ScrollContent ���ϵ�� ������ Parent ��ü
    public GameObject m_Item_NodeObj = null; // Node Prefab

    ItemNoodCtrl[] m_CrNodeList;      //<---��ũ�ѿ� �پ� �ִ� Item ��ϵ�...

    //-- ���� �� �����Ϸ��� �õ��� ����?
    ItemType m_BuyCrType;
    int m_SvMyGold = 0;  //������ �����Ϸ��� �ϴ� ������ �� ��尡 ������?
    //-- ���� �� �����Ϸ��� �õ��� ����?

    public Text GoldTxt;

    public GameObject ThisPanel;
    public Button CloseBtn;

    public HlepBoxCtrl m_HelpBox;

    float m_HelpBoxTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        GlobalUserData.InitData();

        if (CloseBtn != null)
            CloseBtn.onClick.AddListener(CloseBtnFunc);

        //----------------- ������ ��� �߰�
        GameObject a_ItemObj = null;
        ItemNoodCtrl a_ItemNode = null;
        for (int ii = 5; ii < 8; ii++)
        {
            a_ItemObj = (GameObject)Instantiate(m_Item_NodeObj);
            a_ItemNode = a_ItemObj.GetComponent<ItemNoodCtrl>();

            a_ItemNode.InitData(GlobalUserData.m_ItemDataList[ii].m_SkType);

            a_ItemObj.transform.SetParent(m_Item_ScrollContent.transform, false);
            // false  Prefab�� ���� �������� �����ϸ鼭 �߰��� �ְڴٴ� ��.

        }//for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count; ii++)
         //----------------- ������ ��� �߰�

        RefreshCrItemList();
    } //void Start()

    // Update is called once per frame
    void Update()
    {
        if (GoldTxt != null)
            GoldTxt.text = GlobalUserData.s_GoldCount.ToString();

        m_HelpBoxTime -= Time.deltaTime;

        if (m_HelpBoxTime < 0.0f)
        {
            HideToolTip();
        }
    }

    public void ShowToolTip(int a_itemType, Vector3 pos)
    {
        m_HelpBoxTime = 0.1f;
        if (m_HelpBox != null)
        {
            m_HelpBox.ShowToolTip(a_itemType, pos);
        }
    }

    public void HideToolTip()
    {
        if (m_HelpBox != null)
        {
            m_HelpBox.HideToolTip();
        }
    }

    void RefreshCrItemList()
    {  //Count == 0 �� ���°� ó�� ������ �����۸� ���Ű������� ǥ���� �ش�.
        if (m_Item_ScrollContent != null)
        {
            if (m_CrNodeList == null || m_CrNodeList.Length <= 0)
                m_CrNodeList =
                    m_Item_ScrollContent.GetComponentsInChildren<ItemNoodCtrl>();
        }
        //int a_FindAv = -1;
        for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count-6; ii++)
        //for (int ii = 5; ii < 8; ii++)
        {
            //Debug.Log(GlobalUserData.m_ItemDataList.Count);
            //if (m_CrNodeList[ii].m_ItemType != GlobalUserData.m_ItemDataList[ii].m_SkType)
            //    continue;

            if (0 == GlobalUserData.m_ItemDataList[ii+5].m_CurItemCount) //���Ի���
            {
                m_CrNodeList[ii].SetState(ItemState.Active);
                //Debug.Log("Active" + m_CrNodeList[ii].m_ItemType);
            }
            //else //if (GlobalUserData.m_ItemDataList[ii].m_Level <= 0)

            //if (a_FindAv < 0)
            //{
            //    //m_Level�� 0���� ���� ���°� ó�� ���� ����
            //    //���԰��� ǥ��
            //    m_CrNodeList[ii].SetState(ItemState.BeforeBuy);

            //    a_FindAv = ii;
            //}
            else if (1 == GlobalUserData.m_ItemDataList[ii+5].m_CurItemCount)
            {
                //Debug.Log(ii + "Lock");
                //���� Lock ǥ��
                m_CrNodeList[ii].SetState(ItemState.Lock);
            }

        }//for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count; ii++)
    }
    public void BuySkItem(ItemType a_ItemType)
    {//����Ʈ�信 �ִ� ĳ���� ���ݹ�ư�� ���� ���Խõ��� �� ���
        //Debug.Log(m_CrNodeList[5].m_ItemState);
        m_BuyCrType = a_ItemType;
        BuyBeforeJobCo();
    }

    bool isDifferent = false;
    void BuyBeforeJobCo() //���� 1�ܰ� �Լ�
    { //(�����κ��� ���, ������ ���� �޾ƿͼ� Ŭ���̾�Ʈ�� ����ȭ �����ֱ�..)

        //if (GlobalUserData.g_Unique_ID == "")
        //{
        //    return;            //�α��� ���� ���¶�� �׳� ����
        //}

        //isDifferent = false;

        ////< �÷��̾� ������(Ÿ��Ʋ) >�� Ȱ�� �ڵ�
        ////using PlayFab;
        ////using PlayFab.ClientModels;
        //var request = new GetUserDataRequest()
        //{
        //    PlayFabId = GlobalUserData.g_Unique_ID
        //};

        //PlayFabClientAPI.GetUserData(request,
        //        (result) =>
        //        {   //���� ���� �޾ƿ��� ���� ���� ��
        //            PlayerDataParse(result);
        //        },
        //        (error) =>
        //        {   //���� ���� �޾ƿ��� ���� ���� ��
        //            Debug.Log("������ �ҷ����� ����");
        //        }
        //    );


        //int a_GetValue = PlayerPrefs.GetInt("UserGold", 0);
        //if (a_GetValue != GlobalUserData.s_GoldCount)
        //    isDifferent = true;
        //GlobalUserData.s_GoldCount = a_GetValue;

        //string a_MkKey = "";
        ////for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count; ii++)
        //for (int ii = 5; ii < 8; ii++)
        //{
        //    a_MkKey = "ChrItem_" + ii.ToString();
        //    a_GetValue = PlayerPrefs.GetInt(a_MkKey, 0);
        //    if (a_GetValue != GlobalUserData.m_ItemDataList[ii].m_CurItemCount)
        //        isDifferent = true;
        //    GlobalUserData.m_ItemDataList[ii].m_CurItemCount = a_GetValue;
        //}

        string a_Mess = "";
        ItemState a_ItemState = ItemState.Lock;
        bool a_NeedDelegate = false;
        Item_Info a_SkInfo = GlobalUserData.m_ItemDataList[(int)m_BuyCrType];
        Debug.Log(m_CrNodeList.Length);
        if (m_CrNodeList != null && ((int)m_BuyCrType)-5 < m_CrNodeList.Length)
        {
            a_ItemState = m_CrNodeList[(int)m_BuyCrType-5].m_ItemState;
            Debug.Log(a_ItemState);
        }

        if (a_ItemState == ItemState.Lock) //��� ����
        {
            a_Mess = "�� �������� Lock ���·� ������ �� �����ϴ�.";
        }
        else if (a_ItemState == ItemState.Active) //Ȱ��ȭ(���׷��̵尡��) ����
        {
            int a_Cost = a_SkInfo.m_Price;

            if (GlobalUserData.s_GoldCount < a_Cost)
            {
                a_Mess = "�������� �ʿ��� ���� ��尡 ���ڶ��ϴ�.";
            }
            else
            {
                a_Mess = "���� �����Ͻðڽ��ϱ�?";
                //-----> �� ������ �� ���׷��̵�
                a_NeedDelegate = true; //-----> �� ������ �� ����
            }
        }//else if (a_CrState == CrState.Active) 

        if (isDifferent == true)
            a_Mess += "\n(������ �ٸ� ������ �־ �����Ǿ����ϴ�.)";

        GameObject a_DlgRsc = Resources.Load("DlgBox") as GameObject;
        GameObject a_DlgBoxObj = (GameObject)Instantiate(a_DlgRsc);
        GameObject a_Canvas = GameObject.Find("Shop_Canvas");
        a_DlgBoxObj.transform.SetParent(a_Canvas.transform, false);
        // false Prefab�� ���� �������� �����ϸ鼭 �߰��� �ְڴٴ� ��.
        DlgBox_Ctrl a_DlgBox = a_DlgBoxObj.GetComponent<DlgBox_Ctrl>();
        if (a_DlgBox != null)
        {
            if (a_NeedDelegate == true)
                a_DlgBox.SetMessage(a_Mess, TryBuyCrItem);
            else
                a_DlgBox.SetMessage(a_Mess);
        }

    }

    List<int> a_SetLevel = new List<int>();
    public void TryBuyCrItem()  //���� 2�ܰ� �Լ� 
    {  //(������ ������ ������ ������ �� �����...)
        bool a_BuyOK = false;
        Item_Info a_ItemInfo = null;
        a_SetLevel.Clear();   //������ ���� �����ϱ� ���� �迭

        for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count; ii++)
        {
            a_ItemInfo = GlobalUserData.m_ItemDataList[ii];
            a_SetLevel.Add(a_ItemInfo.m_CurItemCount);

            if (ii != (int)m_BuyCrType || 1 <= a_ItemInfo.m_CurItemCount) //���� ���� üũ
                continue;

            int a_Cost = a_ItemInfo.m_Price;

            if (GlobalUserData.s_GoldCount < a_Cost)
                continue;

            //1, ���⼭ ���(����)�ϰ� ������ ������� �����Ѵ�.
            //GlobalUserData.g_UserGold -= a_Cost; ��尪 �����ϱ� 
            //a_SetLevel[ii]++; �������� 

            //2, �����κ��� ������ ���� ������ ���(����)�� �ִ� ����� �ִ�.
            m_SvMyGold = GlobalUserData.s_GoldCount;
            m_SvMyGold -= a_Cost; //��尪 �����ϱ� ����� ����
            a_SetLevel[ii]++;     //�������� ����� ����

            a_BuyOK = true; //������ ������ ���� ��û�� Ȯ���� �ʿ��ϴٴ� �ǹ� 

        } ////for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count; ii++)

        if (a_BuyOK == true)
            BuyRequestCo();

    } //public void TryBuyCrItem()  //���� 2�ܰ� �Լ� 

    void BuyRequestCo() //���� 3�ܰ� �Լ� (������ ������ �� �����ϱ�...)
    {
        if (a_SetLevel.Count <= 0)
            return;     //������ ����� ���������� ��������� �ʾ����� ����

        RefreshMyInfoCo();

        //if (GlobalUserData.g_Unique_ID == "")
        //    return;            //�α��� ���°� �ƴϸ� �׳� ����

        Dictionary<string, string> a_ItemList = new Dictionary<string, string>();
        string a_MkKey = "";
        a_ItemList.Clear();
        a_ItemList.Add("UserGold", m_SvMyGold.ToString());
        //Dictionary ��� �߰� ���
        for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count; ii++)
        {
            a_MkKey = "ChrItem_" + ii.ToString();
            a_ItemList.Add(a_MkKey, a_SetLevel[ii].ToString());
        }

        //var request = new UpdateUserDataRequest()
        //{
        //    Data = a_ItemList
        //};

        //PlayFabClientAPI.UpdateUserData(request,
        //        (result) =>
        //        {
        //            //�Ŵ����¸� ������ �־�� �Ѵ�.
        //            RefreshMyInfoCo();
        //        },
        //        (error) =>
        //        {
        //            //StatText.text = "������ ���� ����"; 
        //        }
        //     );

    } //void BuyRequestCo() //���� 3�ܰ� �Լ� (������ ������ �� �����ϱ�...)

    void RefreshMyInfoCo() //���� 4�ܰ� �Լ� (������ ����, UI ������ ������ �ֱ�...)
    {
        if (m_BuyCrType < ItemType.Item_0 || ItemType.ItemCount <= m_BuyCrType)
            return;

        GlobalUserData.s_GoldCount = m_SvMyGold;
        GlobalUserData.m_ItemDataList[(int)m_BuyCrType].m_CurItemCount
                                    = a_SetLevel[(int)m_BuyCrType];

        ////----���ÿ� ����
        //PlayerPrefs.SetInt("UserGold", GlobalUserData.g_UserGold);
        //string a_MkKey = "";
        //for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count; ii++)
        //{
        //    a_MkKey = "ChrItem_" + ii.ToString();
        //    PlayerPrefs.SetInt(a_MkKey, GlobalUserData.m_ItemDataList[ii].m_Level);
        //}
        ////----���ÿ� ����

        RefreshCrItemList();
    }
    ////void RefreshCrItemList()
    
    void CloseBtnFunc()
    {
        if (ThisPanel != null)
        {
            ThisPanel.SetActive(false);
            Interaction.Inst.IsUpdate = false;
        }
    }
}
