using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
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
        if (tr.position.x < -10.0f)
        {
            m_Pos.x = -10.0f;
        }
        if (69.0f < tr.position.x)
        {
            m_Pos.x = 69.0f;
        }

        tr.position = m_Pos;
    }
}
