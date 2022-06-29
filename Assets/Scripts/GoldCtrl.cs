using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCtrl : MonoBehaviour
{    
    float m_Speed = 100.0f;
    Rigidbody2D rigid;
    [HideInInspector] public bool isGet = true;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         transform.Rotate(new Vector3(0.0f, 0.0f, m_Speed * Time.deltaTime));
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
    }


}
