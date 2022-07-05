using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Interaction.Inst.BeaconDistance = Vector2.Distance(Interaction.Inst.PlayPos.transform.position, this.transform.position);
        BeaconInter();
    }

    void BeaconInter()
    {
        if (Interaction.Inst.IsInteraction == false)
        {
            if (Interaction.Inst.BeaconDistance < Interaction.Inst.PortalDistance || Interaction.Inst.BeaconDistance < Interaction.Inst.ShopDistance || Interaction.Inst.BeaconDistance < Interaction.Inst.UpgdNPCDistance || Interaction.Inst.BeaconDistance < Interaction.Inst.HealItemShopDistance)
            {
                if (Interaction.Inst.BeaconDistance < 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        InfoUI.Inst.HStore.gameObject.SetActive(true);
                        Interaction.Inst.GKey.gameObject.SetActive(true);
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.BeaconDistance);
                        Interaction.Inst.m_interactionState = InteractionState.Beacon;
                        Interaction.Inst.IsInteraction = true;
                    }
                }
            }
        }
        if (Interaction.Inst.IsInteraction == true)
        {
            if (Interaction.Inst.UpgdNPCDistance > 5.0f && Interaction.Inst.ShopDistance > 5.0f && Interaction.Inst.PortalDistance > 5.0f && Interaction.Inst.HealItemShopDistance > 5.0f)
            {
                if (Interaction.Inst.BeaconDistance > 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        InfoUI.Inst.HStore.gameObject.SetActive(false);
                        Interaction.Inst.GKey.gameObject.SetActive(false);
                        Interaction.Inst.m_interactionState = InteractionState.Nomal; 
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.BeaconDistance);
                        Interaction.Inst.IsInteraction = false;
                    }
                }
            }
        }
    }
}
