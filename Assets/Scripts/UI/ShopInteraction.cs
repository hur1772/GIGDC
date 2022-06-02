using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Interaction.Inst.ShopDistance = Vector2.Distance(Interaction.Inst.PlayPos.transform.position, this.transform.position);
        KingInter();
    }

    void KingInter()
    {
        if (Interaction.Inst.IsInteraction == false)
        {
            if (Interaction.Inst.ShopDistance < Interaction.Inst.PortalDistance || Interaction.Inst.ShopDistance < Interaction.Inst.NPCDistance || Interaction.Inst.ShopDistance < Interaction.Inst.KingDistance)
            {
                if (Interaction.Inst.ShopDistance < 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        Interaction.Inst.GKey.gameObject.SetActive(true);
                        Interaction.Inst.animator.SetFloat("ShopInteraction",    Interaction.Inst.ShopDistance);
                        Interaction.Inst.m_interactionState = InteractionState.Shop;
                        Interaction.Inst.IsInteraction = true;
                    }
                }
            }
        }
        if (Interaction.Inst.IsInteraction == false)
        {
            if (Interaction.Inst.KingDistance > 5.0f && Interaction.Inst.ShopDistance > 5.0f && Interaction.Inst.PortalDistance > 5.0f)
            {
                if (Interaction.Inst.NPCDistance > 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        Interaction.Inst.GKey.gameObject.SetActive(false);
                        Interaction.Inst.animator.SetFloat("ShopInteraction", Interaction.Inst.ShopDistance);
                    }
                }
            }
        }
    }
}
