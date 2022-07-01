using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    public Text GuideTxt;
    public GameObject HStore;
    public GameObject WPStore;
    public GameObject STStore;
    public float GuideTimer = 4.0f;

    public static InfoUI Inst = null;

    void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (0.0f < GuideTimer)
        {
            GuideTimer -= Time.deltaTime;

            if (GuideTimer <= 4.0f)
            {
                GuideTxt.gameObject.SetActive(true);
            }

            if (GuideTimer < 0.0f)
            {
                GuideTimer = 0.0f;
                GuideTxt.gameObject.SetActive(false);
            }
        }

    }
}
