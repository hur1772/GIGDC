using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMarble : MonoBehaviour
{
    public float moveSpeed = 0.0f;
    public float rotateSpeed = 0.0f;

    public float shotDelay = 0.0f;
    bool shootRight = false;

    private void Start()
    {

    }

    private void Update()
    {
        if (shotDelay >= 0.0f)
        {
            shotDelay -= Time.deltaTime;
        }
        else
        {
            if(shootRight)
                this.transform.position += Vector3.right * Time.deltaTime * moveSpeed;
            else
                this.transform.position += Vector3.left * Time.deltaTime * moveSpeed;
            transform.Rotate(new Vector3(0.0f, 0.0f, rotateSpeed));
        }
    }

    public void SetRightBool(bool b)
    {
        shootRight = b;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Destroy(this.gameObject);
    }

}