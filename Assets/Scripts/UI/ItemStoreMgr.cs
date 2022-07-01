using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        }//for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count; ii++)
         //----------------- 아이템 목록 추가

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
    {  //Count == 0 인 상태가 처음 나오는 아이템만 구매가능으로 표시해 준다.
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

            if (0 == GlobalUserData.m_ItemDataList[ii+5].m_CurItemCount) //구입상태
            {
                m_CrNodeList[ii].SetState(ItemState.Active);
                //Debug.Log("Active" + m_CrNodeList[ii].m_ItemType);
            }
            //else //if (GlobalUserData.m_ItemDataList[ii].m_Level <= 0)

            //if (a_FindAv < 0)
            //{
            //    //m_Level이 0보다 작은 상태가 처음 나온 상태
            //    //구입가능 표시
            //    m_CrNodeList[ii].SetState(ItemState.BeforeBuy);

            //    a_FindAv = ii;
            //}
            else if (1 == GlobalUserData.m_ItemDataList[ii+5].m_CurItemCount)
            {
                //Debug.Log(ii + "Lock");
                //전부 Lock 표시
                m_CrNodeList[ii].SetState(ItemState.Lock);
            }

        }//for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count; ii++)
    }
    public void BuySkItem(ItemType a_ItemType)
    {//리스트뷰에 있는 캐릭터 가격버튼을 눌러 구입시도를 한 경우
        //Debug.Log(m_CrNodeList[5].m_ItemState);
        m_BuyCrType = a_ItemType;
        BuyBeforeJobCo();
    }

    bool isDifferent = false;
    void BuyBeforeJobCo() //구매 1단계 함수
    { //(서버로부터 골드, 아이템 상태 받아와서 클라이언트와 동기화 시켜주기..)

        //if (GlobalUserData.g_Unique_ID == "")
        //{
        //    return;            //로그인 실패 상태라면 그냥 리턴
        //}

        //isDifferent = false;

        ////< 플레이어 데이터(타이틀) >값 활용 코드
        ////using PlayFab;
        ////using PlayFab.ClientModels;
        //var request = new GetUserDataRequest()
        //{
        //    PlayFabId = GlobalUserData.g_Unique_ID
        //};

        //PlayFabClientAPI.GetUserData(request,
        //        (result) =>
        //        {   //유저 정보 받아오기 성공 했을 때
        //            PlayerDataParse(result);
        //        },
        //        (error) =>
        //        {   //유저 정보 받아오기 실패 했을 때
        //            Debug.Log("데이터 불러오기 실패");
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

        if (a_ItemState == ItemState.Lock) //잠긴 상태
        {
            a_Mess = "이 아이템은 Lock 상태로 구입할 수 없습니다.";
        }
        else if (a_ItemState == ItemState.Active) //활성화(업그레이드가능) 상태
        {
            int a_Cost = a_SkInfo.m_Price;

            if (GlobalUserData.s_GoldCount < a_Cost)
            {
                a_Mess = "레벨업에 필요한 보유 골드가 모자랍니다.";
            }
            else
            {
                a_Mess = "정말 구매하시겠습니까?";
                //-----> 이 조건일 때 업그레이드
                a_NeedDelegate = true; //-----> 이 조건일 때 구매
            }
        }//else if (a_CrState == CrState.Active) 

        if (isDifferent == true)
            a_Mess += "\n(서버와 다른 정보가 있어서 수정되었습니다.)";

        GameObject a_DlgRsc = Resources.Load("DlgBox") as GameObject;
        GameObject a_DlgBoxObj = (GameObject)Instantiate(a_DlgRsc);
        GameObject a_Canvas = GameObject.Find("Shop_Canvas");
        a_DlgBoxObj.transform.SetParent(a_Canvas.transform, false);
        // false Prefab의 로컬 포지션을 유지하면서 추가해 주겠다는 뜻.
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
    public void TryBuyCrItem()  //구매 2단계 함수 
    {  //(서버에 전달할 변동된 데이터 값 만들기...)
        bool a_BuyOK = false;
        Item_Info a_ItemInfo = null;
        a_SetLevel.Clear();   //서버에 값을 전달하기 위한 배열

        for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count; ii++)
        {
            a_ItemInfo = GlobalUserData.m_ItemDataList[ii];
            a_SetLevel.Add(a_ItemInfo.m_CurItemCount);

            if (ii != (int)m_BuyCrType || 1 <= a_ItemInfo.m_CurItemCount) //구매 조건 체크
                continue;

            int a_Cost = a_ItemInfo.m_Price;

            if (GlobalUserData.s_GoldCount < a_Cost)
                continue;

            //1, 여기서 계산(차감)하고 서버에 결과값을 전달한다.
            //GlobalUserData.g_UserGold -= a_Cost; 골드값 차감하기 
            //a_SetLevel[ii]++; 레벨증가 

            //2, 서버로부터 응답을 받은 다음에 계산(차감)해 주는 방법도 있다.
            m_SvMyGold = GlobalUserData.s_GoldCount;
            m_SvMyGold -= a_Cost; //골드값 차감하기 백업해 놓기
            a_SetLevel[ii]++;     //레벨증가 백업해 놓기

            a_BuyOK = true; //서버에 아이템 구매 요청이 확실히 필요하다는 의미 

        } ////for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count; ii++)

        if (a_BuyOK == true)
            BuyRequestCo();

    } //public void TryBuyCrItem()  //구매 2단계 함수 

    void BuyRequestCo() //구매 3단계 함수 (서버에 데이터 값 전달하기...)
    {
        if (a_SetLevel.Count <= 0)
            return;     //아이템 목록이 정상적으로 만들어지지 않았으면 리턴

        RefreshMyInfoCo();

        //if (GlobalUserData.g_Unique_ID == "")
        //    return;            //로그인 상태가 아니면 그냥 리턴

        Dictionary<string, string> a_ItemList = new Dictionary<string, string>();
        string a_MkKey = "";
        a_ItemList.Clear();
        a_ItemList.Add("UserGold", m_SvMyGold.ToString());
        //Dictionary 노드 추가 방법
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
        //            //매뉴상태를 갱신해 주어야 한다.
        //            RefreshMyInfoCo();
        //        },
        //        (error) =>
        //        {
        //            //StatText.text = "데이터 저장 실패"; 
        //        }
        //     );

    } //void BuyRequestCo() //구매 3단계 함수 (서버에 데이터 값 전달하기...)

    void RefreshMyInfoCo() //구매 4단계 함수 (로컬의 변수, UI 값들을 갱신해 주기...)
    {
        if (m_BuyCrType < ItemType.Item_0 || ItemType.ItemCount <= m_BuyCrType)
            return;

        GlobalUserData.s_GoldCount = m_SvMyGold;
        GlobalUserData.m_ItemDataList[(int)m_BuyCrType].m_CurItemCount
                                    = a_SetLevel[(int)m_BuyCrType];

        ////----로컬에 저장
        //PlayerPrefs.SetInt("UserGold", GlobalUserData.g_UserGold);
        //string a_MkKey = "";
        //for (int ii = 0; ii < GlobalUserData.m_ItemDataList.Count; ii++)
        //{
        //    a_MkKey = "ChrItem_" + ii.ToString();
        //    PlayerPrefs.SetInt(a_MkKey, GlobalUserData.m_ItemDataList[ii].m_Level);
        //}
        ////----로컬에 저장

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
