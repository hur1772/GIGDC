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

    [HideInInspector] public bool IsInteraction = false;

    [HideInInspector] public float KingDistance = 10;
    [HideInInspector] public float NPCDistance = 10;
    [HideInInspector] public float ShopDistance = 10;
    [HideInInspector] public float PortalDistance =10;

    public Image GKey;

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
                    Debug.Log("Shop");
                }                
                break;

            case InteractionState.Portal:
                if (Input.GetKey(KeyCode.G))
                {
                    SceneManager.LoadScene("04.Stage_1(Palace)");
                }
                break;
        }
    }


}
