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
    public GameObject CreditCanvas = null;
    public Text InfoText;
    [HideInInspector] public float InfoTimer = 5.0f;

    public static StageMgr Inst = null;

    void Awake()
    {
        Inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //GlobalUserData.Load();
        GlobalUserData.InitData();
        GlobalUserData.InitWeaponData();
        menuOn = false;
        if (BacktoGame_Btn != null)
            BacktoGame_Btn.onClick.AddListener(BackGameFunc);

        if (Volume_Btn != null)
            Volume_Btn.onClick.AddListener(VolumeFunc);

        if (CreditScene_Btn != null)
            CreditScene_Btn.onClick.AddListener(CreditSceneFunc);

        if (ExitGame_Btn != null)
            ExitGame_Btn.onClick.AddListener(ExitGameFunc);

        if(InfoText != null)
        InfoText.gameObject.SetActive(false);

        CreditCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (0.0f < InfoTimer)
        {
            if (InfoText != null)
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
                    InfoText.text = "";
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOn == false)
            {
                ESC_Panel.gameObject.SetActive(true);
                menuOn = true;
                Time.timeScale = 0.0f;
            }
            else
            {
                ESC_Panel.gameObject.SetActive(false);
                menuOn = false;
                Time.timeScale = 1.0f;
            }

        }
    }
    
    public void VolumeFunc()
    {
        Debug.Log("Volume");
    }

    public void BackGameFunc()
    {
        ESC_Panel.gameObject.SetActive(false);
        menuOn = false;
        Time.timeScale = 1.0f;
    }

    public void CreditSceneFunc()
    {
        CreditCanvas.gameObject.SetActive(true);
    }

    public void ExitGameFunc()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }

}
