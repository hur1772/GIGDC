using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDropCtrl : MonoBehaviour
{
    private float m_Speed = 50.0f;
    [HideInInspector] public bool isGet = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.0f,m_Speed * Time.deltaTime,0.0f));
    }
}
