using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStoreMgr : MonoBehaviour
{
    public Button m_ReturnBtn = null;
    public Text m_UserInfoTxt;

    public GameObject m_Item_ScrollContent; //ScrollContent 차일드로 생성될 Parent 객체
    public GameObject m_Item_NodeObj = null; // Node Prefab

    ItemNoodCtrl[] m_CrNodeList;      //<---스크롤에 붙어 있는 Item 목록들...

    //-- 지금 뭘 구입하려고 시도한 건지?
    ItemType m_BuyCrType;
    int m_SvMyGold = 0;  //서버에 전달하려고 하는 차감된 내 골드가 얼마인지?
    //-- 지금 뭘 구입하려고 시도한 건지?

    // Start is called before the first frame update
    void Start()
    {
        GlobalUserData.InitData();

        //----------------- 아이템 목록 추가
        GameObject a_ItemObj = null;
        ItemNoodCtrl a_ItemNode = null;
        for (int ii = 5; ii < 8; ii++)
        {
            a_ItemObj = (GameObject)Instantiate(m_Item_NodeObj);
            a_ItemNode = a_ItemObj.GetComponent<ItemNoodCtrl>();

            a_ItemNode.InitData(GlobalUserData.m_ItemDataList[ii].m_SkType);

            a_ItemObj.transform.SetParent(m_Item_ScrollContent.transform, false);
            // false  Prefab의 로컬 포지션을 유지하면서 추가해 주겠다는 뜻.

        }//for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
         //----------------- 아이템 목록 추가

        //RefreshCrItemList();
    } //void Start()

    // Update is called once per frame
    void Update()
    {
        
    }
}
