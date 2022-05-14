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
        if (Interaction.Inst.KingDistance < 5.0f)
        {
            if (Interaction.Inst.GKey != null)
            {
                Interaction.Inst.GKey.gameObject.SetActive(true);
                Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.KingDistance);

                if(Input.GetKey(KeyCode.G))
                {
                    Interaction.Inst.m_interactionState = InteractionState.king;
                }
            }

        }
        else
        {
            if (Interaction.Inst.GKey != null)
            {
                Interaction.Inst.GKey.gameObject.SetActive(false);
                Interaction.Inst.animator.SetFloat("Interaction", Interaction.Inst.KingDistance);

            }

        }
    }
}
