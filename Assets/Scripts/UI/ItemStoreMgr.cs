using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        GlobalUserData.InitData();

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

        }//for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
         //----------------- ������ ��� �߰�

        //RefreshCrItemList();
    } //void Start()

    // Update is called once per frame
    void Update()
    {
        
    }
}
