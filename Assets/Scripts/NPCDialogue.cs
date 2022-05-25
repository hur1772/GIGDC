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
    public Sprite cg2;
}


public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite_StandingCG;
    [SerializeField] private SpriteRenderer sprite_StandingCG2;
    [SerializeField] private SpriteRenderer sprite_DialogBox;
    [SerializeField] private Text TalkTxt;
    [SerializeField] private Text NPCLabelTxt;
    [SerializeField] private Text InfoTxt;
    public Button m_Btn = null;
    //[SerializeField] private Button TalkBtn;
    private bool IsDialog = false;
    private int count = 0;

    public GameObject TypeObject = null;

    public static NPCDialogue Inst;
    [SerializeField] private Dialogue[] dialogue;
    GameObject Player;
    GameObject NPC;

    bool IsTalk = false;

    public void ShowDialogue()
    {
        OnOff(true);
        //NextDialouge();
        //if (TalkBtn != null)
        //{
        //    TalkBtn.gameObject.SetActive(false);
        //}
    }

    public void NextDialouge()
    {
        TalkTxt.text = dialogue[count].dialogue;
        sprite_StandingCG.sprite = dialogue[count].cg;
        sprite_StandingCG2.sprite = dialogue[count].cg2;
        //Debug.Log(dialogue[count].dialogue);
        //Debug.Log(count);
        count++;
        //if(count > 3)
        //{
        //    count = 3;
        //    m_Btn.gameObject.SetActive(true);
        //    count++;
        //    if(count > 4)
        //    {
        //        m_Btn.gameObject.SetActive(false);
        //    }
        //}
        
    }

    public void OnOff(bool a_flag)
    {
        sprite_StandingCG.gameObject.SetActive(a_flag);
        sprite_StandingCG2.gameObject.SetActive(a_flag);
        sprite_DialogBox.gameObject.SetActive(a_flag);
        TalkTxt.gameObject.SetActive(a_flag);
        //NPCLabelTxt.gameObject.SetActive(a_flag);
        InfoTxt.gameObject.SetActive(a_flag);
        //m_Btn.gameObject.SetActive(false);
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
            if (Input.GetKeyDown(KeyCode.G) )
            {
                if (count < dialogue.Length) //대화 시작
                {
                    NextDialouge();
                }
                else //대화 끝
                {
                    OnOff(false);
                    Player.SetActive(true);                    
                    if (NPC != null)
                    {
                        NPC.SetActive(true);
                    }
                    //NPCInteraction.Inst.

                    //if (TalkBtn != null)
                    //{
                    //    TalkBtn.gameObject.SetActive(true);
                    //}
                    //Interaction.Inst.ResetPos(10.0f);
                    count = 0;
                    TalkTxt.text = dialogue[count].dialogue;
                    count = 1;
                    if (TypeObject != null)
                    {
                        if (TypeObject.gameObject.name == "King")
                        {
                            Interaction.Inst.m_interactionState = InteractionState.king_talkEnd;
                        }
                        else if(TypeObject.gameObject.name == "NPC")
                        {
                            Interaction.Inst.m_interactionState = InteractionState.NPC_talkEnd;
                            //Interaction.Inst.ResetPos(3.0f);
                        }
                    }
                }
            }
        }
    }
}