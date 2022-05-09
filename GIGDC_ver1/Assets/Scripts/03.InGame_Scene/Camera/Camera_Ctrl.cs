using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Ctrl : MonoBehaviour
{
    public GameObject target;
    public GameObject quadGameObject;
    
    float scrollSpeed = 0.5f;
    public float moveSpeed;
    public float z_move_speed;
    private Renderer quadRender;
    private Vector3 targetPosition;
    private Vector3 z_keyPosition;



    // Start is called before the first frame update
    void Start()
    {
        quadRender = quadGameObject.GetComponent<Renderer>();
    }

    //Update is called once per frame
    void Update()
    {
        Vector2 textureOffset = new Vector2(Time.time * scrollSpeed, 0);
        quadRender.material.mainTextureOffset = textureOffset;
    }

    private void LateUpdate()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x + 4, target.transform.position.y + 4, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime * 5.0f);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            z_keyPosition.Set(this.transform.position.x, this.transform.position.y - 1.5f, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, z_keyPosition, z_move_speed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
