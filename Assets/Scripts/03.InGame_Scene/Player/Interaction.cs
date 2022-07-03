using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum InteractionState
{
    Nomal,
    king,
    king_talk,
    king_talkEnd,
    NPC,
    NPC_Talk,
    Fight,
    NPC_talkEnd,
    Shop,
    HealItemShop,
    UpGdNPC,
    Beacon,
    Portal
}

public class Interaction : MonoBehaviour
{
    public InteractionState m_interactionState = InteractionState.Nomal;

    public static Interaction Inst = null;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform PlayPos;
    private GameObject Portal;

    public GameObject NPC;

    [HideInInspector] public bool IsInteraction = false;

    [HideInInspector] public float KingDistance = 5.1f;
    [HideInInspector] public float NPCDistance = 5.1f;
    [HideInInspector] public float ShopDistance = 5.1f;
    [HideInInspector] public float PortalDistance =5.1f;
    [HideInInspector] public float HealItemShopDistance = 5.1f;
    [HideInInspector] public float UpgdNPCDistance = 5.1f;
    [HideInInspector] public float BeaconDistance = 5.1f;

    public Image GKey;

    public GameObject UpGdPanel = null;
    public GameObject HealItemShopPanel = null;
    public GameObject ItemShopPanel = null;

    public bool IsUpdate = false;


    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayPos = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayPos = this.gameObject.transform;
        switch (m_interactionState)
        {
            case InteractionState.king:
                if (Input.GetKey(KeyCode.G))
                {
                    m_interactionState = InteractionState.king_talk;
                    NPCDialogue.Inst.ShowDialogue();
                }
                break;

            case InteractionState.NPC:
                if (Input.GetKey(KeyCode.G))
                {
                    m_interactionState = InteractionState.NPC_Talk;
                    NPCDialogue.Inst.ShowDialogue();
                }
                break;

            case InteractionState.Shop:
                if (Input.GetKey(KeyCode.G))
                {
                    if (ItemShopPanel != null)
                    {
                        IsUpdate = true;
                        ItemShopPanel.SetActive(true);
                    }
                }
                break;

            case InteractionState.HealItemShop:
                if (Input.GetKey(KeyCode.G))
                {
                    if (HealItemShopPanel != null)
                    {
                        IsUpdate = true;
                        HealItemShopPanel.SetActive(true);
                    }
                }
                break;

            case InteractionState.UpGdNPC:
                if (Input.GetKey(KeyCode.G))
                {
                    if (UpGdPanel != null)
                    {
                        IsUpdate = true;
                        UpGdPanel.SetActive(true);
                    }
                }
                break;

            case InteractionState.Beacon:
                if(Input.GetKey(KeyCode.G))
                {
                    SoundMgr.Instance.PlayEffSound("Beacon", 0.5f);
                    GlobalUserData.Save();
                    if (InfoUI.Inst.GuideTxt != null)
                    {
                        InfoUI.Inst.GuideTxt.text = "저장되었습니다";
                        InfoUI.Inst.GuideTimer = 4.0f;
                    }
                }
                break;

            case InteractionState.Portal:
                Portal = GameObject.Find("Protal");
                if (Input.GetKey(KeyCode.G))
                {
                    SoundMgr.Instance.PlayEffSound("Portal", 1.0f);
                    if (Portal.tag == "04.Stage_1(Palace)")
                    {
                        SceneManager.LoadScene("02.Stage_1(Palace)");
                        m_interactionState = InteractionState.Nomal;
                    }
                    if(Portal.tag == "04.Stage_1(Palace_In)")
                    {
                        SceneManager.LoadScene("03.Stage_1(Palace_In)");
                        m_interactionState = InteractionState.Nomal;
                    }
                    if (Portal.tag == "01.TutorialMap")
                    {
                        SceneManager.LoadScene("04.TutorialMap");
                        m_interactionState = InteractionState.Nomal;
                    }
                    if (Portal.tag == "Village")
                    {
                        SceneManager.LoadScene("Village");
                        m_interactionState = InteractionState.Nomal;
                    }
                    if (Portal.tag == "Stage")
                    {
                        if(GlobalUserData.CurStageNum == 0)
                        {
                            GlobalUserData.CurStageNum++;
                            SceneManager.LoadScene("1_1");
                        }
                        else if(GlobalUserData.CurStageNum == 1)
                        {
                            GlobalUserData.CurStageNum++;
                            SceneManager.LoadScene("1_2");
                        }
                        else if (GlobalUserData.CurStageNum == 2)
                        {
                            GlobalUserData.CurStageNum++;
                            SceneManager.LoadScene("1_3");
                        }

                        m_interactionState = InteractionState.Nomal;
                    }
                }
                break;
        }
    }

    public void ResetPos(float Pos = 5.1f)
    {
        KingDistance = Pos;
        NPCDistance = Pos;
        ShopDistance = Pos;
        PortalDistance = Pos;
        HealItemShopDistance = Pos;
        UpgdNPCDistance = Pos;
        BeaconDistance = Pos;
        IsInteraction = false;
    }
}
