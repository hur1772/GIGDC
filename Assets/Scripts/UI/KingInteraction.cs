using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingInteraction : MonoBehaviour
{
    float Pos = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Interaction.Inst.ResetPos(Pos + 0.1f);
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
                if (Interaction.Inst.KingDistance < Pos)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        Interaction.Inst.GKey.gameObject.SetActive(true);
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.KingDistance);
                        Interaction.Inst.m_interactionState = InteractionState.king;
                        Interaction.Inst.IsInteraction = true;
                    }
                }
            }
        }
        if (Interaction.Inst.IsInteraction == true)
        {
            if (Interaction.Inst.NPCDistance > Pos && Interaction.Inst.ShopDistance > Pos && Interaction.Inst.PortalDistance > Pos)
            {
                if (Interaction.Inst.KingDistance > Pos)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        Interaction.Inst.GKey.gameObject.SetActive(false);
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.KingDistance);

                        Interaction.Inst.IsInteraction = false;
                    }

                }
            }
        }
    }
}
