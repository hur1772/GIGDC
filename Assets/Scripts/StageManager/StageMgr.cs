using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageMgr : MonoBehaviour
{

    [Header("----ESC_Menu---")]
    public Image ESC_Panel = null;
    public Image ESC_Menu_panel = null;
    bool menuOn;
    public Button BacktoGame_Btn = null;
    public Button Volume_Btn = null;
    public Button CreditScene_Btn = null;
    public Button ExitGame_Btn = null;

    // Start is called before the first frame update
    void Start()
    {
        menuOn = false;

        //public Button Volume_Btn = null;
        //public Button CreditScene_Btn = null;
        //public Button ExitGame_Btn = null;

        if (BacktoGame_Btn != null)
            BacktoGame_Btn.onClick.AddListener(BackGameFunc);

        if (Volume_Btn != null)
            Volume_Btn.onClick.AddListener(VolumeFunc);

        if (CreditScene_Btn != null)
            CreditScene_Btn.onClick.AddListener(CreditSceneFunc);

        if (ExitGame_Btn != null)
            ExitGame_Btn.onClick.AddListener(ExitGameFunc);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOn == false)
            {
                ESC_Panel.gameObject.SetActive(true);
                menuOn = true;
            }
            else
            {
                ESC_Panel.gameObject.SetActive(false);
                menuOn = false;
            }

        }
    }
    public void VolumeFunc()
    {
        Debug.Log("Volume");
    }

    public void BackGameFunc()
    {
        ESC_Menu_panel.gameObject.SetActive(false);
        menuOn = false;
    }

    public void CreditSceneFunc()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void ExitGameFunc()
    {
        Debug.Log("ExitGame");
    }
}
