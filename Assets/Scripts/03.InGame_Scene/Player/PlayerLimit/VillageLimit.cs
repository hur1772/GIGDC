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
    

    public GameObject Tiger;
    public GameObject monsters;
    public GameObject Wall1;
    public GameObject Wall2;

    private void Start()
    {
        Debug.Log(GlobalUserData.SwordTier);

        tr = GetComponent<Transform>();
        
    }

    void OnEnable()
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
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
        else if (nowscene.name == "04.TutorialMap")
        {
            m_Pos = tr.position;
            if (tr.position.x < -8.4f)
            {
                m_Pos.x = -8.4f;
            }
            if (25.9f < tr.position.x)
            {
                m_Pos.x = 25.9f;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("Tiger_Trigger_Pos") == true)
        {
            SoundMgr.Instance.PlayGUISound("Tiger_Gen", 1.5f);
            if (Tiger != null)
            {
                Tiger.SetActive(true);
            }
            if (monsters != null)
            {
                monsters.SetActive(false);
            }
            if (Wall1 != null)
            {
                Wall1.SetActive(true);
            }
            if (Wall2 != null)
            {
                Wall2.SetActive(true);
            }


            
        }
    }
}
