using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Ctrl : MonoBehaviour
{
    public GameObject target;
    
    public float moveSpeed;
    public float z_move_speed;

    private Vector3 targetPosition;
    private Vector3 z_keyPosition;

    private Player_Input p_Input;

    float key = 0;

    // Start is called before the first frame update
    void Start()
    {
        p_Input = GameObject.Find("Player").GetComponent<Player_Input>();
      
    }

    //Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (0 < p_Input.horizontal)
        {
            key = 1;

            if (target.gameObject != null)
            {
                targetPosition.Set(key *(target.transform.position.x + 4), target.transform.position.y + 4, this.transform.position.z);
                this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime * 5.0f);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                z_keyPosition.Set(key * (this.transform.position.x), this.transform.position.y - 1.5f, this.transform.position.z);
                this.transform.position = Vector3.Lerp(this.transform.position, z_keyPosition, z_move_speed * Time.deltaTime);
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
        else if (p_Input.horizontal < 0)
        {
            key = -1;

            if (target.gameObject != null)
            {
                targetPosition.Set((target.transform.position.x - 3), target.transform.position.y + 4, this.transform.position.z);
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
        Debug.Log(this.transform.position.x);

    }
}
