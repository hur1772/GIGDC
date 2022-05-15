using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        Interaction.Inst.PortalDistance = Vector2.Distance(Interaction.Inst.PlayPos.transform.position, this.transform.position);
        PortalInter();
    }

    void PortalInter()
    {
        if (Interaction.Inst.IsInteraction == false)
        {
            if (Interaction.Inst.PortalDistance < Interaction.Inst.NPCDistance || Interaction.Inst.PortalDistance < Interaction.Inst.ShopDistance || Interaction.Inst.PortalDistance < Interaction.Inst.KingDistance)
            {
                if (Interaction.Inst.PortalDistance < 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        Interaction.Inst.GKey.gameObject.SetActive(true);
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.PortalDistance);
                        Interaction.Inst.m_interactionState = InteractionState.Portal;
                        Interaction.Inst.IsInteraction = true;

                        Debug.Log("PortalDistance" + Interaction.Inst.PortalDistance);

                        
                    }
                }
            }
        }
        if (Interaction.Inst.IsInteraction == true)
        {
            if (Interaction.Inst.NPCDistance > 5.0f || Interaction.Inst.KingDistance > 5.0f || Interaction.Inst.ShopDistance > 5.0f)
            {
                if (Interaction.Inst.PortalDistance > 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        Interaction.Inst.GKey.gameObject.SetActive(false);
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.PortalDistance);

                        Interaction.Inst.IsInteraction = false;

                    }

                }
            }
        }
    }
}
