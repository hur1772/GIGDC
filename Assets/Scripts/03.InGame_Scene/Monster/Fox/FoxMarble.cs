using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMarble : MonoBehaviour
{
    public float moveSpeed = 0.0f;
    public float rotateSpeed = 0.0f;
    private void Start()
    {

    }

    private void Update()
    {
        this.transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        transform.Rotate(new Vector3(0.0f, 0.0f, rotateSpeed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
