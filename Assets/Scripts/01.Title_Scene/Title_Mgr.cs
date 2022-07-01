using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Mgr : MonoBehaviour
{
    public Button StartBtn;
    public Button CreditBtn;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        if (StartBtn != null)
            StartBtn.onClick.AddListener(StartBtnFunc);

        if (CreditBtn != null)
            CreditBtn.onClick.AddListener(CreditSceneFunc);
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }

    void StartBtnFunc()
    {
        SceneManager.LoadScene("01.Stage_1");
    }

    void CreditSceneFunc()
    {

    }
}