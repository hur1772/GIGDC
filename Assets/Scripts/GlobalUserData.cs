using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Item_0 = 0,
    Item_1,
    Item_2,
    Item_3,
    Item_4,
    Item_5,
    Item_6,
    Item_7,
    Item_8,
    ItemCount
}

public class Item_Info  //각 Item 정보
{
    public string m_Name = "";              //아이템 이름
    public ItemType m_SkType = ItemType.Item_0; //아이템 타입
    public Vector2 m_IconSize = Vector2.one;  //아이콘의 가로 사이즈, 세로 사이즈
    public int m_Price = 500;   //아이템 기본 가격 
    public string m_DropMobType = "Monster"; //드롭 몬스터 타입
    public int m_CurItemCount = 0;   //사용할 수 있는 스킬 카운트
    public string m_ItemExp = "";    //스킬 효과 설명
    public Sprite m_IconImg = null;   //캐릭터 아이템에 사용될 이미지
    public Sprite m_ShopIconImg = null;

    public void SetType(ItemType a_CrType)
    {
        m_SkType = a_CrType;
        if (a_CrType == ItemType.Item_0)
        {
            m_Name = "탕약";
            m_IconSize.x = 1.0f; //0.766f;   //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f = 103 픽셀

            m_Price = 500; //기본가격

            m_ItemExp = "Hp 30% 회복";
            m_IconImg = Resources.Load("IconImg/탕약v2", typeof(Sprite)) as Sprite;
            m_ShopIconImg = Resources.Load("탕약", typeof(Sprite)) as Sprite;
        }
        else if (a_CrType == ItemType.Item_1)
        {
            m_Name = "보약";
            m_IconSize.x = 1.0f;    //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 800; //기본가격

            m_ItemExp = "Hp 50% 회복";
            m_IconImg = Resources.Load("IconImg/고약", typeof(Sprite)) as Sprite;
            m_ShopIconImg = Resources.Load("보약", typeof(Sprite)) as Sprite;
        }
        else if (a_CrType == ItemType.Item_2)
        {
            m_Name = "무궁화";
            m_IconSize.x = 1.0f;     //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 0; //기본가격
            m_DropMobType = "Boss";

            m_ItemExp = "HP +50 상승";
            m_IconImg = Resources.Load("IconImg/무궁화v2", typeof(Sprite)) as Sprite;
        }
        else if (a_CrType == ItemType.Item_3)
        {
            m_Name = "나팔꽃";
            m_IconSize.x = 1.0f;     //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 0; //기본가격
            m_DropMobType = "Boss";

            m_ItemExp = "검 공격력 +10 상승";
            m_IconImg = Resources.Load("IconImg/나팔꽃v2", typeof(Sprite)) as Sprite;
        }
        else if (a_CrType == ItemType.Item_4)
        {
            m_Name = "창포";
            m_IconSize.x = 1.0f;     //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 0; //기본가격
            m_DropMobType = "Boss";

            m_ItemExp = "활 공격력 +10 상승";
            m_IconImg = Resources.Load("IconImg/창포꽃", typeof(Sprite)) as Sprite;
        }
        else if (a_CrType == ItemType.Item_5)
        {
            m_Name = "추어탕";
            m_IconSize.x = 1.0f;     //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 2000; //기본가격
            m_CurItemCount = 0;

            m_ItemExp = "공격속도 +5";
            m_IconImg = Resources.Load("IconImg/추어탕v2", typeof(Sprite)) as Sprite;
            m_ShopIconImg = Resources.Load("추어탕", typeof(Sprite)) as Sprite;
        }

        else if (a_CrType == ItemType.Item_6)
        {
            m_Name = "감자전";
            m_IconSize.x = 1.0f;     //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 2000; //기본가격
            m_CurItemCount = 0;

            m_ItemExp = "치명타 데미지 + 5";
            m_IconImg = Resources.Load("IconImg/감자전v2", typeof(Sprite)) as Sprite;
            m_ShopIconImg = Resources.Load("감자전", typeof(Sprite)) as Sprite;
        }

        else if (a_CrType == ItemType.Item_7)
        {
            m_Name = "막걸리";
            m_IconSize.x = 1.0f;     //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 2000; //기본가격
            m_CurItemCount = 0;

            m_ItemExp = "치명타 확률 + 10";
            m_IconImg = Resources.Load("IconImg/막걸리v2", typeof(Sprite)) as Sprite;
            m_ShopIconImg = Resources.Load("막걸리", typeof(Sprite)) as Sprite;
        }

        else if (a_CrType == ItemType.Item_8)
        {
            m_Name = "무기도감";
            m_IconSize.x = 1.0f;     //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 0; //기본가격
            m_DropMobType = "Boss";

            m_ItemExp = "무기 티어업 재료";
            m_IconImg = Resources.Load("IconImg/6_ice_o", typeof(Sprite)) as Sprite;
        }
    }//public void SetType(ItemType a_CrType)
}

public class GlobalUserData 
{
    public static int s_GoldCount = 0;
    public static int s_ItemCount = 0;
    public static string s_NickName = "User";
    public static int BowTier = 0;
    public static int SwordTier = 0;
    public static PlayerAttackState Player_Att_State;

    public static ulong UniqueCount = 0; //임시 Item 고유키 발급기...
                                         //public static List<ItemValue> g_ItemList = new List<ItemValue>();

    public static List<Item_Info> m_ItemDataList = new List<Item_Info>();

    public static void InitData()
    {
        if (m_ItemDataList.Count <= 0)
        {
            Item_Info a_SkItemNd;
            for (int ii = 0; ii < (int)ItemType.ItemCount; ii++)
            {
                a_SkItemNd = new Item_Info();
                a_SkItemNd.SetType((ItemType)ii);
                a_SkItemNd.m_CurItemCount = 0;
                m_ItemDataList.Add(a_SkItemNd);
            }
        }
    }

        void Start()
    {
        Player_Att_State = PlayerAttackState.player_no_att;
    }

    public static void LoadGameInfo()
    {

        //Player_Att_State = PlayerAttackState.player_no_att;
        s_GoldCount = PlayerPrefs.GetInt("GoldCount", 0);
        s_ItemCount = PlayerPrefs.GetInt("ItemCount", 0);
        s_NickName = PlayerPrefs.GetString("UserNick", "User");
        //BowTier= PlayerPrefs.GetInt( "BowTier", 0 );
        //SwordTier= PlayerPrefs.GetInt( "SwordTier", 0);

        ReflashItemLoad();
    }

    //------------ Item Reflash
    public static void ReflashItemLoad()  //<---- g_ItemList  갱신
    {
        //g_ItemList.Clear();

        //ItemValue a_LdNode;
        //string a_KeyBuff = "";
        //int a_ItmCount = PlayerPrefs.GetInt("Item_Count", 0);
        //for (int a_ii = 0; a_ii < a_ItmCount; a_ii++)
        //{
        //    a_LdNode = new ItemValue();
        //    a_KeyBuff = string.Format("IT_{0}_stUniqueID", a_ii);
        //    string stUniqueID = PlayerPrefs.GetString(a_KeyBuff, "");
        //    if (stUniqueID != "")
        //        a_LdNode.UniqueID = ulong.Parse(stUniqueID);
        //    a_KeyBuff = string.Format("IT_{0}_Item_Type", a_ii);
        //    a_LdNode.m_Item_Type = (Item_Type)PlayerPrefs.GetInt(a_KeyBuff, 0);
        //    a_KeyBuff = string.Format("IT_{0}_ItemName", a_ii);
        //    a_LdNode.m_ItemName = PlayerPrefs.GetString(a_KeyBuff, "");
        //    a_KeyBuff = string.Format("IT_{0}_ItemLevel", a_ii);
        //    a_LdNode.m_ItemLevel = PlayerPrefs.GetInt(a_KeyBuff, 0);
        //    a_KeyBuff = string.Format("IT_{0}_ItemStar", a_ii);
        //    a_LdNode.m_ItemStar = PlayerPrefs.GetInt(a_KeyBuff, 0);

        //    g_ItemList.Add(a_LdNode);
        //}
    }

    public static void ReflashItemSave()  //<-- 리스트 다시 저장
    {
        //---------기존에 저장되어 있었던 아이템 목록 제거
        //ItemValue a_SvNode;
        string a_KeyBuff = "";
        int a_ItmCount = PlayerPrefs.GetInt("Item_Count", 0);
        for (int a_ii = 0; a_ii < a_ItmCount + 10; a_ii++)
        {
            a_KeyBuff = string.Format("IT_{0}_stUniqueID", a_ii);
            PlayerPrefs.DeleteKey(a_KeyBuff);
            a_KeyBuff = string.Format("IT_{0}_Item_Type", a_ii);
            PlayerPrefs.DeleteKey(a_KeyBuff);
            a_KeyBuff = string.Format("IT_{0}_ItemName", a_ii);
            PlayerPrefs.DeleteKey(a_KeyBuff);
            a_KeyBuff = string.Format("IT_{0}_ItemLevel", a_ii);
            PlayerPrefs.DeleteKey(a_KeyBuff);
            a_KeyBuff = string.Format("IT_{0}_ItemStar", a_ii);
            PlayerPrefs.DeleteKey(a_KeyBuff);
        }
        PlayerPrefs.DeleteKey("Item_Count");  //아이템 수 제거
        PlayerPrefs.Save(); //폰에서 마지막 저장상태를 확실히 저장하게 하기 위하여...
        //---------기존에 저장되어 있었던 아이템 목록 제거

        //---------- 새로운 리스트 저장
        //PlayerPrefs.SetInt("Item_Count", g_ItemList.Count);
        //for (int a_ii = 0; a_ii < g_ItemList.Count; a_ii++)
        //{
        //    a_SvNode = g_ItemList[a_ii];
        //    a_KeyBuff = string.Format("IT_{0}_stUniqueID", a_ii);
        //    PlayerPrefs.SetString(a_KeyBuff, a_SvNode.UniqueID.ToString());
        //    a_KeyBuff = string.Format("IT_{0}_Item_Type", a_ii);
        //    PlayerPrefs.SetInt(a_KeyBuff, (int)a_SvNode.m_Item_Type);
        //    a_KeyBuff = string.Format("IT_{0}_ItemName", a_ii);
        //    PlayerPrefs.SetString(a_KeyBuff, a_SvNode.m_ItemName);
        //    a_KeyBuff = string.Format("IT_{0}_ItemLevel", a_ii);
        //    PlayerPrefs.SetInt(a_KeyBuff, a_SvNode.m_ItemLevel);
        //    a_KeyBuff = string.Format("IT_{0}_ItemStar", a_ii);
        //    PlayerPrefs.SetInt(a_KeyBuff, a_SvNode.m_ItemStar);
        //}
        //PlayerPrefs.Save(); //폰에서 마지막 저장상태를 확실히 저장하게 하기 위하여...
        ////---------- 새로운 리스트 저장
    }
    //------------ Item Reflash

    public static void ClearGameInfo()
    {
        PlayerPrefs.DeleteAll();
        LoadGameInfo();
    }

    //프로젝트 전체에서 변수나 함수 이름 검색 : < Ctrl > + < < >
   // public static ulong GetUnique() //임시 고유키 발급기...
    //{
        //UniqueCount = (ulong)PlayerPrefs.GetInt("SvUnique", 0);
        //UniqueCount++;
        //ulong a_Index = UniqueCount;

        ////<--자신의 인벤토리에 있는 아이템 번호랑 겹치는 번호보다는 큰 수로 
        ////유니크ID가 발급되게 처리하는 부분
        //if (0 < g_ItemList.Count)
        //    for (int a_bb = 0; a_bb < g_ItemList.Count; ++a_bb)
        //    {
        //        if (g_ItemList[a_bb] == null)
        //            continue;

        //        if (a_Index < g_ItemList[a_bb].UniqueID)
        //        {
        //            a_Index = g_ItemList[a_bb].UniqueID + 1;
        //        }
        //    }//for (int a_bb = 0; a_bb < g_ItemList.Count; ++a_bb)

        //UniqueCount = a_Index;
        //PlayerPrefs.SetInt("SvUnique", (int)UniqueCount);
        //return a_Index;

  //  }//public static ulong GetUnique() //임시 고유키 발급기...
}
