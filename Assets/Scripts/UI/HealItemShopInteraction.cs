using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItemShopInteraction : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Interaction.Inst.HealItemShopDistance = Vector2.Distance(Interaction.Inst.PlayPos.transform.position, this.transform.position);
        HealItemShopInter();
    }

    void HealItemShopInter()
    {
        if (Interaction.Inst.IsInteraction == false)
        {
            if (Interaction.Inst.HealItemShopDistance < Interaction.Inst.PortalDistance || Interaction.Inst.HealItemShopDistance < Interaction.Inst.ShopDistance || Interaction.Inst.HealItemShopDistance <Interaction.Inst.UpgdNPCDistance || Interaction.Inst.HealItemShopDistance < Interaction.Inst.BeaconDistance)
            {
                if (Interaction.Inst.HealItemShopDistance < 5.0f)
                {
                    if (Interaction.Inst.GKey != null)  
                    {
                        InfoUI.Inst.HStore.gameObject.SetActive(true);
                        Interaction.Inst.GKey.gameObject.SetActive(true);
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.HealItemShopDistance);
                        Interaction.Inst.m_interactionState = InteractionState.HealItemShop;
                        Interaction.Inst.IsInteraction = true;
                    }
                }
            }
        }
        if (Interaction.Inst.IsInteraction == true)
        {
            if (Interaction.Inst.UpgdNPCDistance > 5.0f && Interaction.Inst.ShopDistance > 5.0f && Interaction.Inst.PortalDistance > 5.0f && Interaction.Inst.BeaconDistance > 5.0f)
            {
                if (Interaction.Inst.HealItemShopDistance > 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        InfoUI.Inst.HStore.gameObject.SetActive(false);
                        Interaction.Inst.GKey.gameObject.SetActive(false);
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.HealItemShopDistance);
                        Interaction.Inst.IsInteraction = false;
                    }
                }
            }
        }
    }
}
