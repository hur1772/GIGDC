using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealItemStoreMgr : MonoBehaviour
{
    public Button m_ReturnBtn = null;
    public Text m_UserInfoTxt;

    public GameObject m_Item_ScrollContent; //ScrollContent ���ϵ�� ������ Parent ��ü
    public GameObject m_Item_NodeObj = null; // Node Prefab

    HealItemNoodCtrl[] m_CrNodeList;      //<---��ũ�ѿ� �پ� �ִ� Item ��ϵ�...

    //-- ���� �� �����Ϸ��� �õ��� ����?
    ItemType m_BuyCrType;
    int m_SvMyGold = 0;  //������ �����Ϸ��� �ϴ� ������ �� ��尡 ������?
    //-- ���� �� �����Ϸ��� �õ��� ����?

    // Start is called before the first frame update
    void Start()
    {
        GlobalUserData.InitData();

        //----------------- ������ ��� �߰�
        GameObject a_ItemObj = null;
        HealItemNoodCtrl a_HealItemNode = null;
        for (int ii = 0; ii < 2; ii++)
        {
            a_ItemObj = (GameObject)Instantiate(m_Item_NodeObj);
            a_HealItemNode = a_ItemObj.GetComponent<HealItemNoodCtrl>();

            a_HealItemNode.InitData(GlobalUserData.m_ItemDataList[ii].m_SkType);

            a_ItemObj.transform.SetParent(m_Item_ScrollContent.transform, false);
            // false  Prefab�� ���� �������� �����ϸ鼭 �߰��� �ְڴٴ� ��.

        }//for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
         //----------------- ������ ��� �߰�

        //RefreshCrItemList();
    } //void Start()

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //void RefreshCrItemList()
    //{  //Count == 0 �� ���°� ó�� ������ �����۸� ���Ű������� ǥ���� �ش�.
    //    if (m_Item_ScrollContent != null)
    //    {
    //        if (m_CrNodeList == null || m_CrNodeList.Length <= 0)
    //            m_CrNodeList =
    //                m_Item_ScrollContent.GetComponentsInChildren<CharNodeCtrl>();
    //    }

    //    int a_FindAv = -1;
    //    for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
    //    {
    //        if (m_CrNodeList[ii].m_SkType != GlobalValue.m_CrDataList[ii].m_SkType)
    //            continue;

    //        if (0 < GlobalValue.m_CrDataList[ii].m_Level) //���Ի���
    //        {
    //            m_CrNodeList[ii].SetState(CrState.Active,
    //                                    GlobalValue.m_CrDataList[ii].m_UpPrice,
    //                                    GlobalValue.m_CrDataList[ii].m_Level);
    //            continue;
    //        }
    //        //else //if (GlobalValue.m_CrDataList[ii].m_Level <= 0)

    //        if (a_FindAv < 0)
    //        {
    //            //m_Level�� 0���� ���� ���°� ó�� ���� ����
    //            //���԰��� ǥ��
    //            m_CrNodeList[ii].SetState(CrState.BeforeBuy,
    //                             GlobalValue.m_CrDataList[ii].m_Price);

    //            a_FindAv = ii;
    //        }
    //        else
    //        {
    //            //���� Lock ǥ��
    //            m_CrNodeList[ii].SetState(CrState.Lock,
    //                            GlobalValue.m_CrDataList[ii].m_Price);
    //        }

    //    }//for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
    //}//void RefreshCrItemList()

    //public void BuyCharItem(SkillType a_ChType)
    //{//����Ʈ�信 �ִ� ĳ���� ���ݹ�ư�� ���� ���Խõ��� �� ���
    //    m_BuyCrType = a_ChType;
    //    BuyBeforeJobCo();
    //}

    //bool isDifferent = false;
    //void BuyBeforeJobCo() //���� 1�ܰ� �Լ�
    //{ //(�����κ��� ���, ������ ���� �޾ƿͼ� Ŭ���̾�Ʈ�� ����ȭ �����ֱ�..)

    //    isDifferent = false;

    //    int a_GetValue = PlayerPrefs.GetInt("UserGold", 0);
    //    if (a_GetValue != GlobalValue.g_UserGold)
    //        isDifferent = true;
    //    GlobalValue.g_UserGold = a_GetValue;

    //    string a_MkKey = "";
    //    for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
    //    {
    //        a_MkKey = "ChrItem_" + ii.ToString();
    //        a_GetValue = PlayerPrefs.GetInt(a_MkKey, 0);
    //        if (a_GetValue != GlobalValue.m_CrDataList[ii].m_Level)
    //            isDifferent = true;
    //        GlobalValue.m_CrDataList[ii].m_Level = a_GetValue;
    //    }

    //    string a_Mess = "";
    //    CrState a_CrState = CrState.Lock;
    //    bool a_NeedDelegate = false;
    //    Skill_Info a_SkInfo = GlobalValue.m_CrDataList[(int)m_BuyCrType];
    //    if (m_CrNodeList != null && (int)m_BuyCrType < m_CrNodeList.Length)
    //    {
    //        a_CrState = m_CrNodeList[(int)m_BuyCrType].m_CrState;
    //    }

    //    if (a_CrState == CrState.Lock) //��� ����
    //    {
    //        a_Mess = "�� �������� Lock ���·� ������ �� �����ϴ�.";
    //    }
    //    else if (a_CrState == CrState.BeforeBuy) //���� ���� ����
    //    {
    //        if (GlobalValue.g_UserGold < a_SkInfo.m_Price)
    //        {
    //            a_Mess = "����(����) ��尡 ���ڶ��ϴ�.";
    //        }
    //        else
    //        {
    //            a_Mess = "���� �����Ͻðڽ��ϱ�?";
    //            a_NeedDelegate = true; //-----> �� ������ �� ����
    //        }
    //    }
    //    else if (a_CrState == CrState.Active) //Ȱ��ȭ(���׷��̵尡��) ����
    //    {
    //        int a_Cost = a_SkInfo.m_UpPrice +
    //                     (a_SkInfo.m_UpPrice * (a_SkInfo.m_Level - 1));
    //        if (5 <= a_SkInfo.m_Level)
    //        {
    //            a_Mess = "�ְ� �����Դϴ�.";
    //        }
    //        else if (GlobalValue.g_UserGold < a_Cost)
    //        {
    //            a_Mess = "�������� �ʿ��� ����(����) ��尡 ���ڶ��ϴ�.";
    //        }
    //        else
    //        {
    //            a_Mess = "���� ���׷��̵��Ͻðڽ��ϱ�?";
    //            //-----> �� ������ �� ���׷��̵�
    //            a_NeedDelegate = true; //-----> �� ������ �� ����
    //        }
    //    }//else if (a_CrState == CrState.Active) 

    //    if (isDifferent == true)
    //        a_Mess += "\n(������ �ٸ� ������ �־ �����Ǿ����ϴ�.)";

    //    GameObject a_DlgRsc = Resources.Load("DlgBox") as GameObject;
    //    GameObject a_DlgBoxObj = (GameObject)Instantiate(a_DlgRsc);
    //    GameObject a_Canvas = GameObject.Find("Canvas");
    //    a_DlgBoxObj.transform.SetParent(a_Canvas.transform, false);
    //    // false Prefab�� ���� �������� �����ϸ鼭 �߰��� �ְڴٴ� ��.
    //    DlgBox_Ctrl a_DlgBox = a_DlgBoxObj.GetComponent<DlgBox_Ctrl>();
    //    if (a_DlgBox != null)
    //    {
    //        if (a_NeedDelegate == true)
    //            a_DlgBox.SetMessage(a_Mess, TryBuyCrItem);
    //        else
    //            a_DlgBox.SetMessage(a_Mess);
    //    }

    //}//void BuyBeforeJobCo()

    //List<int> a_SetLevel = new List<int>();
    //public void TryBuyCrItem()  //���� 2�ܰ� �Լ� 
    //{  //(������ ������ ������ ������ �� �����...)
    //    bool a_BuyOK = false;
    //    Skill_Info a_SkInfo = null;
    //    a_SetLevel.Clear();   //������ ���� �����ϱ� ���� �迭

    //    for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
    //    {
    //        a_SkInfo = GlobalValue.m_CrDataList[ii];
    //        a_SetLevel.Add(a_SkInfo.m_Level);

    //        if (ii != (int)m_BuyCrType || 5 <= a_SkInfo.m_Level) //���� ���� üũ
    //            continue;

    //        int a_Cost = a_SkInfo.m_Price;
    //        if (0 < a_SkInfo.m_Level)
    //            a_Cost = a_SkInfo.m_UpPrice +
    //                (a_SkInfo.m_UpPrice * (a_SkInfo.m_Level - 1));

    //        if (GlobalValue.g_UserGold < a_Cost)
    //            continue;

    //        //1, ���⼭ ���(����)�ϰ� ������ ������� �����Ѵ�.
    //        //GlobalValue.g_UserGold -= a_Cost; ��尪 �����ϱ� 
    //        //a_SetLevel[ii]++; �������� 

    //        //2, �����κ��� ������ ���� ������ ���(����)�� �ִ� ����� �ִ�.
    //        m_SvMyGold = GlobalValue.g_UserGold;
    //        m_SvMyGold -= a_Cost; //��尪 �����ϱ� ����� ����
    //        a_SetLevel[ii]++;     //�������� ����� ����

    //        a_BuyOK = true; //������ ������ ���� ��û�� Ȯ���� �ʿ��ϴٴ� �ǹ� 

    //    } ////for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)

    //    if (a_BuyOK == true)
    //        BuyRequestCo();

    //} //public void TryBuyCrItem()  //���� 2�ܰ� �Լ� 

    //void BuyRequestCo() //���� 3�ܰ� �Լ� (������ ������ �� �����ϱ�...)
    //{
    //    if (a_SetLevel.Count <= 0)
    //        return;     //������ ����� ���������� ��������� �ʾ����� ����

    //    RefreshMyInfoCo();
    //}

    //void RefreshMyInfoCo() //���� 4�ܰ� �Լ� (������ ����, UI ������ ������ �ֱ�...)
    //{
    //    if (m_BuyCrType < SkillType.Skill_0 || SkillType.SkCount <= m_BuyCrType)
    //        return;

    //    GlobalValue.g_UserGold = m_SvMyGold;
    //    GlobalValue.m_CrDataList[(int)m_BuyCrType].m_Level
    //                                = a_SetLevel[(int)m_BuyCrType];

    //    //----���ÿ� ����
    //    PlayerPrefs.SetInt("UserGold", GlobalValue.g_UserGold);
    //    string a_MkKey = "";
    //    for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
    //    {
    //        a_MkKey = "ChrItem_" + ii.ToString();
    //        PlayerPrefs.SetInt(a_MkKey, GlobalValue.m_CrDataList[ii].m_Level);
    //    }
    //    //----���ÿ� ����

    //    RefreshCrItemList();
    //    m_UserInfoTxt.text = "����(" + GlobalValue.g_NickName
    //                    + ") : �������(" + GlobalValue.g_UserGold + ")";
    //}
}
