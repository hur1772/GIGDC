using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditScene_Mgr : MonoBehaviour
{
    public GameObject creditPanel = null;
    public Button backTitleBtn = null;
    public Text CreditTxt = null;


    // Start is called before the first frame update
    void Start()
    {
        if (backTitleBtn != null)
            backTitleBtn.onClick.AddListener(BackTitleFunc);

    }

    // Update is called once per frame
    void Update()
    {
        CreditTxt.transform.position = new Vector2(CreditTxt.transform.position.x, CreditTxt.transform.position.y + 0.3f);

        if (1900f < CreditTxt.transform.position.y)
        {
            CreditTxt.transform.position = new Vector2(CreditTxt.transform.position.x, -1500f);
        }
    }

    public void BackTitleFunc()
    {
        creditPanel.gameObject.SetActive(false);
    }
}
