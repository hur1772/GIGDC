using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrop : MonoBehaviour
{
    float m_Speed = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, m_Speed* Time.deltaTime, 0.0f));
    }
}
