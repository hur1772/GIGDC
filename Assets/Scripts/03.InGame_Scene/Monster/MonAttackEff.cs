using UnityEngine;

public class MonAttackEff : MonoBehaviour
{
    Player_TakeDamage takedam;


    private void Start() => StartFunc();

    private void StartFunc()
    {
         
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ���ݴ���");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out takedam))
            {

            }
            else
                Debug.Log("�÷��̾� ���ݴ��� / takedam ����");


        }
    }
}