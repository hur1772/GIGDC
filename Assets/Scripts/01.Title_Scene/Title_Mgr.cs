using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Mgr : MonoBehaviour
{
    public Image FadeIn = null;

    public Text SpaceTxt = null;
    public Button NewGameBtn = null;
    public Button LoadGameBtn = null;
    public Button CloseBtn = null;

    private void Start() => StartFunc();

    private void Awake()
    {
        FadeIn.gameObject.SetActive(true);
    }
    private void StartFunc()
    {
        if (NewGameBtn != null)
            NewGameBtn.onClick.AddListener(NewGameBtnFunc);

        if (LoadGameBtn != null)
            LoadGameBtn.onClick.AddListener(LoadGameBtnFunc);

        if (CloseBtn != null)
            CloseBtn.onClick.AddListener(CloseBtnFunc);


        SoundMgr.Instance.PlayBGM("Title_BGM", 1.0f);
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceTxt.gameObject.SetActive(false);
            NewGameBtn.gameObject.SetActive(true);
            LoadGameBtn.gameObject.SetActive(true);
            CloseBtn.gameObject.SetActive(true);
            //SceneManager.LoadScene("01.Stage_1");
        }
    }

    void NewGameBtnFunc()
    {
        GlobalUserData.InitData();
        GlobalUserData.InitWeaponData();

        SceneManager.LoadScene("01.Stage_1");
    }

    void LoadGameBtnFunc()
    {
        if(PlayerPrefs.HasKey("s_GoldCount") == true)
        {
            GlobalUserData.Load();

            SceneManager.LoadScene("Village");
        }
        if(PlayerPrefs.HasKey("s_GoldCount") == false)
        {
            return;
        }
    }

    void CloseBtnFunc()
    {
        Application.Quit();
    }
}