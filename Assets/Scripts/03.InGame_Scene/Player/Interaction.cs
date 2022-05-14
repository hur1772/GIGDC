using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [HideInInspector] public float KingDistance;
    [HideInInspector] public float NPCDistance;
    [HideInInspector] public float ShopDistance;
    [HideInInspector] public float PortalDistance;

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

                break;

            case InteractionState.NPC:

                break;

            case InteractionState.Shop:

                break;

            case InteractionState.Portal:
                Debug.Log("Portal");
                break;
        }
    }



    //void NPCInteraction()
    //{
    //    if (NPCDistance < 5.0f)
    //    {
    //        if (GKey != null)
    //        {
    //            GKey.gameObject.SetActive(true);
    //            animator.SetFloat("Interaction", NPCDistance);
    //        }

    //    }
    //    else
    //    {
    //        if (GKey != null)
    //        {
    //            GKey.gameObject.SetActive(false);
    //            animator.SetFloat("ChaseRange", NPCDistance);

    //        }

    //    }
    //}

    //void ShopInteraction()
    //{
    //    if (ShopDistance < 5.0f)
    //    {
    //        if (GKey != null)
    //        {
    //            GKey.gameObject.SetActive(true);
    //            animator.SetFloat("ChaseRange", ShopDistance);
    //        }

    //    }
    //    else
    //    {
    //        if (GKey != null)
    //        {
    //            GKey.gameObject.SetActive(false);
    //            animator.SetFloat("ChaseRange", ShopDistance);

    //        }

    //    }
    //}
}
