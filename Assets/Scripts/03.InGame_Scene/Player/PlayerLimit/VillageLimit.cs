using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageLimit : MonoBehaviour
{
    Scene nowscene;

    private Transform tr;
    private Vector2 m_Pos = Vector2.zero;
    // Start is called before the first frame update

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        nowscene = SceneManager.GetActiveScene();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if (nowscene.name == "Village")
        {
            m_Pos = tr.position;
            if (tr.position.x < -7.0f)
            {
                m_Pos.x = -7.0f;
            }
            if (86.0f < tr.position.x)
            {
                m_Pos.x = 86.0f;
            }

            tr.position = m_Pos;
        }
        else if(nowscene.name == "1_1")
        {
            m_Pos = tr.position;
            if (tr.position.x < -7.8f)
            {
                m_Pos.x = -7.8f;
            }
            if (461.0f < tr.position.x)
            {
                m_Pos.x = 461.0f;
            }

            tr.position = m_Pos;
        }
        else if (nowscene.name == "1_2")
        {
            m_Pos = tr.position;
            if (tr.position.x < -0.15f)
            {
                m_Pos.x = -0.15f;
            }
            if (485.9f < tr.position.x)
            {
                m_Pos.x = 485.9f;
            }

            tr.position = m_Pos;
        }
        else if (nowscene.name == "1_3")
        {
            m_Pos = tr.position;
            if (tr.position.x < -3.3f)
            {
                m_Pos.x = -3.3f;
            }
            if (365.0f < tr.position.x)
            {
                m_Pos.x = 365.0f;
            }

            tr.position = m_Pos;
        }
    }
}
