using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Mgr : MonoBehaviour
{

    private void Start() => StartFunc();

    private void StartFunc()
    {
        
        SoundMgr.Instance.PlayBGM("Title_BGM", 1.0f);
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("01.Stage_1");
        }
    }
}