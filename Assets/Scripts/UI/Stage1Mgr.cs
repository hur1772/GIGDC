using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Mgr : MonoBehaviour
{
    public Text InfoText;
    float InfoTimer = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (0.0f < InfoTimer)
        {
            InfoTimer -= Time.deltaTime;

            if (InfoTimer <= 4.0f)
            {
                InfoText.gameObject.SetActive(true);
            }

            if (InfoTimer < 0.0f)
            {
                InfoTimer = 0.0f;
                InfoText.gameObject.SetActive(false);
            }
        }
    }
}
