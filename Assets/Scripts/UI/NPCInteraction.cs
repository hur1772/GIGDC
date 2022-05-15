using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Interaction.Inst.NPCDistance = Vector2.Distance(Interaction.Inst.PlayPos.transform.position, this.transform.position);
        Debug.Log("NPCDistance" + Interaction.Inst.NPCDistance);
        KingInter();
    }

    void KingInter()
    {
        if (Interaction.Inst.NPCDistance < 5.0f || Interaction.Inst.NPCDistance < Interaction.Inst.PortalDistance&& Interaction.Inst.NPCDistance < Interaction.Inst.ShopDistance  && Interaction.Inst.NPCDistance < Interaction.Inst.KingDistance)
        {
            if (Interaction.Inst.GKey != null)
            {
                Interaction.Inst.GKey.gameObject.SetActive(true);
                Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.NPCDistance);

                if (Input.GetKey(KeyCode.G))
                {
                    Interaction.Inst.m_interactionState = InteractionState.NPC;
                }
            }

        }
        else if (Interaction.Inst.NPCDistance > 5.0f || Interaction.Inst.KingDistance > 5.0f && Interaction.Inst.ShopDistance > 5.0f && Interaction.Inst.PortalDistance > 5.0f)
        {
            {
                if (Interaction.Inst.GKey != null)
                {
                    Interaction.Inst.GKey.gameObject.SetActive(false);
                    Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.NPCDistance);

                }

            }
        }
    }
}
