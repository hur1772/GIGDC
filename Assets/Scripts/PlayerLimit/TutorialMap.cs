using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMap : MonoBehaviour
{
    private Transform tr;
    private Vector2 m_Pos = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Pos = tr.position;
        if (tr.position.x < -8.4f)
        {
            m_Pos.x = -8.4f;
        }
        if (19.9f < tr.position.x)
        {
            m_Pos.x = 19.9f;
        }

        tr.position = m_Pos;
    }
}
