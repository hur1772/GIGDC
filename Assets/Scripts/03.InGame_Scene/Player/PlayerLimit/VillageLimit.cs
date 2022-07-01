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
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        nowscene = SceneManager.GetActiveScene();
        Debug.Log(nowscene);
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
        
    }
}
