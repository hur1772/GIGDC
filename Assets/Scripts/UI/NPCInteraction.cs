using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    float Pos = 8.0f;

    public static NPCInteraction Inst;
     void Awake()
    {
        Inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Interaction.Inst.ResetPos(Pos + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        Interaction.Inst.NPCDistance = Vector2.Distance(Interaction.Inst.PlayPos.transform.position, this.transform.position);
        KingInter();
        
    }

    void KingInter()
    {
        if (Interaction.Inst.IsInteraction == false)
        {
            if (Interaction.Inst.NPCDistance < Interaction.Inst.PortalDistance || Interaction.Inst.NPCDistance < Interaction.Inst.ShopDistance || Interaction.Inst.NPCDistance < Interaction.Inst.KingDistance)
            {
                if (Interaction.Inst.NPCDistance < 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        Interaction.Inst.GKey.gameObject.SetActive(true);
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.NPCDistance);
                        Interaction.Inst.m_interactionState = InteractionState.NPC;
                        Interaction.Inst.IsInteraction = true;
                    }
                }
            }
        }
        if (Interaction.Inst.IsInteraction == true)
        {   
            if (Interaction.Inst.KingDistance > 5.0f && Interaction.Inst.ShopDistance > 5.0f && Interaction.Inst.PortalDistance > 5.0f)
            {
                if (Interaction.Inst.NPCDistance > 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        //Debug.Log("!");
                        Interaction.Inst.m_interactionState = InteractionState.Nomal;
                        Interaction.Inst.GKey.gameObject.SetActive(false);
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.NPCDistance);

                        Interaction.Inst.IsInteraction = false;
                    }
                }
            }
        }
    }
}
