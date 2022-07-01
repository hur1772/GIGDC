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
        ShopInter();
    }

    void ShopInter()
    {
        if (Interaction.Inst.IsInteraction == false)
        {
            if (Interaction.Inst.ShopDistance < Interaction.Inst.PortalDistance || Interaction.Inst.ShopDistance < Interaction.Inst.UpgdNPCDistance || Interaction.Inst.ShopDistance < Interaction.Inst.HealItemShopDistance)
            {
                if (Interaction.Inst.ShopDistance < 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        InfoUI.Inst.STStore.gameObject.SetActive(true);
                        Interaction.Inst.GKey.gameObject.SetActive(true);
                        Interaction.Inst.animator.SetFloat("Interaction",    Interaction.Inst.ShopDistance);
                        Interaction.Inst.m_interactionState = InteractionState.Shop;
                        Interaction.Inst.IsInteraction = true;
                    }
                }
            }
        }
        if (Interaction.Inst.IsInteraction == true)
        {
            if (Interaction.Inst.HealItemShopDistance > 5.0f && Interaction.Inst.PortalDistance > 5.0f && Interaction.Inst.UpgdNPCDistance > 5.0f)
            {
                if (Interaction.Inst.ShopDistance > 5.0f)
                {
                    if (Interaction.Inst.GKey != null)
                    {
                        InfoUI.Inst.STStore.gameObject.SetActive(false);
                        Interaction.Inst.GKey.gameObject.SetActive(false);
                        Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.ShopDistance);
                        Interaction.Inst.IsInteraction = false;
                    }
                }
            }
        }
    }
}
