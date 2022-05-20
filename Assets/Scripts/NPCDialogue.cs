using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string dialogue;
}


public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite_StandingCG;
    [SerializeField] private SpriteRenderer sprite_DialogBox;
    [SerializeField] private Text TalkTxt;
    [SerializeField] private Text NPCLabelTxt;
    [SerializeField] private Text InfoTxt;
    [SerializeField] private Button TalkBtn;
    private bool IsDialog = false;
    private int count = 0;

    public static NPCDialogue Inst;
    [SerializeField] private Dialogue[] dialogue;
    GameObject Player;
    GameObject NPC;

    bool IsTalk = false;

    public void ShowDialogue()
    {
        OnOff(true);
        //NextDialouge();
        if (TalkBtn != null)
        {
            TalkBtn.gameObject.SetActive(false);
        }
    }

    public void NextDialouge()
    {
        TalkTxt.text = dialogue[count].dialogue;
        Debug.Log(dialogue[count].dialogue);
        Debug.Log(count);
        count++;
    }

    public void OnOff(bool a_flag)
    {
        sprite_StandingCG.gameObject.SetActive(a_flag);
        sprite_DialogBox.gameObject.SetActive(a_flag);
        TalkTxt.gameObject.SetActive(a_flag);
        NPCLabelTxt.gameObject.SetActive(a_flag);
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
        if (Interaction.Inst.m_interactionState == InteractionState.king_talk)
        {
            NPCInteraction();
        }
    }

    public void TalkFunc()
    {
        ShowDialogue();
    }

    public void NPCInteraction()
    {
        if (IsDialog)
        {
            if (Input.GetKeyDown(KeyCode.G) )
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
                    if (TalkBtn != null)
                    {
                        TalkBtn.gameObject.SetActive(true);
                    }
                    count = 0;
                    TalkTxt.text = dialogue[count].dialogue;
                    count = 1;
                    Interaction.Inst.m_interactionState = InteractionState.Nomal;
                }
            }
        }
    }
}