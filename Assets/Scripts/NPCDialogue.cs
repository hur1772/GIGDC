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

    NPCDialogue Inst;
    [SerializeField] private Dialogue[] dialogue;
    GameObject Player;
    GameObject NPC;

    public void ShowDialogue()
    {
        OnOff(true);
        count = 0;
        NextDialouge();
        TalkBtn.gameObject.SetActive(false);
    }

    public void NextDialouge()
    {
        TalkTxt.text = dialogue[count].dialogue;
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
        NPC.SetActive(false);
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
            
        TalkBtn.onClick.AddListener(TalkFunc);
    }

    public void TalkFunc()
    {
        ShowDialogue();
    }

    public void NPCInteraction()
    {
        if (IsDialog)
        {
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
                    NPC.SetActive(true);
                    TalkBtn.gameObject.SetActive(true);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        NPCInteraction();
    }
}