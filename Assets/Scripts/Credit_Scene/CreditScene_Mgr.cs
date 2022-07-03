using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditScene_Mgr : MonoBehaviour
{
    public Button backTitleBtn = null;

    // Start is called before the first frame update
    void Start()
    {
        if (backTitleBtn != null)
            backTitleBtn.onClick.AddListener(BackTitleFunc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackTitleFunc()
    {
        SceneManager.LoadScene("00.TitleScene");
    }
}
