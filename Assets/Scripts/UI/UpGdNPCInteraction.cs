using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGdNPCInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Interaction.Inst.UpgdNPCDistance = Vector2.Distance(Interaction.Inst.PlayPos.transform.position, this.transform.position);
        UpgdNPCInter();
    }

    void UpgdNPCInter()
    {
        if (Interaction.Inst.IsInteraction == false)
        {
            if (Interaction.Inst.UpgdNPCDistance < Interaction.Inst.PortalDistance || Interaction.Inst.UpgdNPCDistance < Interaction.Inst.ShopDistance || Interaction.Inst.UpgdNPCDistance < Interaction.Inst.HealItemShopDistance)
            {
                if (Interaction.Inst.UpgdNPCDistance < 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        Interaction.Inst.GKey.gameObject.SetActive(true);
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.UpgdNPCDistance);
                        Interaction.Inst.m_interactionState = InteractionState.UpGdNPC;
                        Interaction.Inst.IsInteraction = true;
                    }
                }
            }
        }
        if (Interaction.Inst.IsInteraction == true)
        {
            if (Interaction.Inst.HealItemShopDistance > 5.0f && Interaction.Inst.NPCDistance > 5.0f && Interaction.Inst.PortalDistance > 5.0f)
            {
                if (Interaction.Inst.UpgdNPCDistance > 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        Interaction.Inst.GKey.gameObject.SetActive(false);
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.UpgdNPCDistance);
                        Interaction.Inst.IsInteraction = false;
                    }
                }
            }
        }
    }
}
