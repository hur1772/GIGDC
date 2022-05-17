using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum InteractionState
{
    Nomal,
    king,
    NPC,
    Shop,
    Portal
}

public class Interaction : MonoBehaviour
{
    public InteractionState m_interactionState = InteractionState.Nomal;

    public static Interaction Inst = null;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform PlayPos;
    private GameObject Portal;

    [HideInInspector] public bool IsInteraction = false;

    [HideInInspector] public float KingDistance = 5.1f;
    [HideInInspector] public float NPCDistance = 5.1f;
    [HideInInspector] public float ShopDistance = 5.1f;
    [HideInInspector] public float PortalDistance =5.1f;

    public Image GKey;

    public GameObject ShopPanel = null;

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
                    Debug.Log("king");                   
                }
                break;

            case InteractionState.NPC:
                if (Input.GetKey(KeyCode.G))
                {
                    Debug.Log("NPC");                    
                }
                break;

            case InteractionState.Shop:
                if (Input.GetKey(KeyCode.G))
                {
                    if (ShopPanel != null)
                    {
                        ShopPanel.SetActive(true);
                    }
                }
                break;

            case InteractionState.Portal:

                Portal = GameObject.Find("Protal");
                if (Input.GetKey(KeyCode.G))
                {
                    if (Portal.tag == "04.Stage_1(Palace)")
                    {
                        SceneManager.LoadScene("04.Stage_1(Palace)");
                        m_interactionState = InteractionState.Nomal;
                        ResetPos();
                    }
                    if(Portal.tag == "04.Stage_1(Palace_In)")
                    {
                        SceneManager.LoadScene("04.Stage_1(Palace_In)");
                        m_interactionState = InteractionState.Nomal;
                        ResetPos();
                    }
                    if (Portal.tag == "01.TutorialMap")
                    {
                        SceneManager.LoadScene("01.TutorialMap");
                        m_interactionState = InteractionState.Nomal;
                    }
                }
                break;
        }
    }

    public void ResetPos()
    {
        KingDistance = 5.1f;
        NPCDistance = 5.1f;
        ShopDistance = 5.1f;
        PortalDistance = 5.1f;
        IsInteraction = false;
    }
}
