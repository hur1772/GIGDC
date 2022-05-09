using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    Transform player_trans;
    public float horizontal { get; private set; }

    public float vertical { get; private set; }


    // �̷������� ���� �߰��ؼ� Input class �����
    public bool fire { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        player_trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        fire = Input.GetButton("Fire1");

    }
}
