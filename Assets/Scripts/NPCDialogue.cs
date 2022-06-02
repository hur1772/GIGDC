using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string dialogue;
    public Sprite cg;
    public Sprite PlayerCg;
}


public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite_StandingCG;
    [SerializeField] private SpriteRenderer Player_StandingCG;
    [SerializeField] private SpriteRenderer sprite_DialogBox;
    [SerializeField] private Text TalkTxt;
    //[SerializeField] private Text NPCLabelTxt;
    [SerializeField] private Text InfoTxt;
    [SerializeField] private Text GuideTxt;

    //[SerializeField] private Button TalkBtn;
    private bool IsDialog = false;
    private int count = 0;
    private float GuideTimer = 4.0f;
    public GameObject TypeObject = null;

    public static NPCDialogue Inst;
    [SerializeField] private Dialogue[] dialogue;


    GameObject Player;
    GameObject NPC;
    public int[] newcount;

    //bool IsTalk = false;

    public void ShowDialogue()
    {
        OnOff(true);

    }

    public void NextDialouge()
    {
        TalkTxt.text = dialogue[count].dialogue;
        sprite_StandingCG.sprite = dialogue[count].cg;
        Player_StandingCG.sprite = dialogue[count].PlayerCg;
    
        count++;
    }

    public void OnOff(bool a_flag)
    {
        sprite_StandingCG.gameObject.SetActive(a_flag);
        Player_StandingCG.gameObject.SetActive(a_flag);
        sprite_DialogBox.gameObject.SetActive(a_flag);
        TalkTxt.gameObject.SetActive(a_flag);
        //NPCLabelTxt.gameObject.SetActive(a_flag);
        InfoTxt.gameObject.SetActive(a_flag);
        Player.SetActive(false);
        if (NPC != null)
        {
            NPC.SetActive(false);
        }
        IsDialog = a_flag;
    }
    void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        NPC = GameObject.Find("NPC");
    }

    // Update is called once per frame
    void Update()
    {
        if (TypeObject == null)
            return;
        if (TypeObject.gameObject.name == "King")
        {      
            if (0.0f < GuideTimer)
            {
                GuideTimer -= Time.deltaTime;

                if (GuideTimer <= 4.0f)
                {
                    GuideTxt.gameObject.SetActive(true);
                }

                if (GuideTimer < 0.0f)
                {
                    GuideTimer = 0.0f;
                    GuideTxt.gameObject.SetActive(false);
                }
            }
        }
        if(TypeObject.gameObject.name == "NPC")
        {
            if (0.0f < GuideTimer)
            {
                GuideTimer -= Time.deltaTime;

                if (GuideTimer <= 3.0f)
                {
                    GuideTxt.gameObject.SetActive(true);
                }

                if (GuideTimer < 0.0f)
                {
                    GuideTimer = 0.0f;
                    GuideTxt.gameObject.SetActive(false);
                }
            }
        }

        if (Interaction.Inst.m_interactionState == InteractionState.king_talk)
        {
            NPCInteraction();
        }
        if (Interaction.Inst.m_interactionState == InteractionState.NPC)
        {
            NPCInteraction();
        }

    }

    //public void TalkFunc()
    //{
    //    ShowDialogue();
    //}

    public void NPCInteraction()
    {
        if (IsDialog)
        {
            if (sprite_DialogBox.gameObject.activeSelf == true)
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (count < dialogue.Length)
                {
                    NextDialouge();
                }
                else
                {
                    OnOff(false);
                    Player.SetActive(true);
                    if (NPC != null)
                    {
                        NPC.SetActive(true);
                    }
                    count = 0;
                    TalkTxt.text = dialogue[count].dialogue;
                    count = 1;
                   
                    if (TypeObject != null)
                    {
                        if (TypeObject.gameObject.name == "King")
                        {
                            Interaction.Inst.m_interactionState =   InteractionState.king_talkEnd;
                        }
                        else if (TypeObject.gameObject.name == "NPC")
                        {                            
                            Interaction.Inst.m_interactionState =   InteractionState.NPC_talkEnd;
                            Interaction.Inst.ResetPos();
                            NPC.gameObject.SetActive(false);
                            Interaction.Inst.GKey.gameObject.SetActive(false);                                                    
                        }
                        else if (TypeObject.gameObject.name == "Protal")
                        {
                            Interaction.Inst.m_interactionState = InteractionState.Portal;
                            Interaction.Inst.GKey.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}