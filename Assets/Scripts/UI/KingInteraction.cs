using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Interaction.Inst.KingDistance = Vector2.Distance(Interaction.Inst.PlayPos.transform.position, this.transform.position);
        KingInter();
    }

    void KingInter()
    {
        if(Interaction.Inst.IsInteraction == false)
        {
            if (Interaction.Inst.KingDistance < Interaction.Inst.NPCDistance || Interaction.Inst.KingDistance < Interaction.Inst.ShopDistance || Interaction.Inst.KingDistance < Interaction.Inst.PortalDistance)
            {
                if (Interaction.Inst.KingDistance < 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        Interaction.Inst.GKey.gameObject.SetActive(true);
                        Interaction.Inst.animator.SetFloat("KingInteraction", Interaction.Inst.KingDistance);
                        Interaction.Inst.m_interactionState = InteractionState.king;
                        Interaction.Inst.IsInteraction = true;
                    }
                }
            }
        }
        if (Interaction.Inst.IsInteraction == true)
        {
            if (Interaction.Inst.NPCDistance > 5.0f || Interaction.Inst.ShopDistance > 5.0f || Interaction.Inst.PortalDistance > 5.0f)
            {
                if (Interaction.Inst.KingDistance > 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        Interaction.Inst.GKey.gameObject.SetActive(false);
                        Interaction.Inst.animator.SetFloat("KingInteraction", Interaction.Inst.KingDistance);

                        Interaction.Inst.IsInteraction = false;
                    }

                }
            }
        }
    }
}
