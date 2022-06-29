using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageLimit : MonoBehaviour
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
