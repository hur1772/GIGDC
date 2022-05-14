using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Chase : MonoBehaviour
{
    //private Animator animator;

    //private Transform target;

    //private float Distance;

    //private EnemyState e_State;

    //Rigidbody2D rigid;

    //float chase_force = 150.0f;

    //public Image Raider;

    ////거리 측정 임시
    //public Text Dis;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    animator = GetComponent<Animator>();
    //    target = GameObject.Find("Player").GetComponent<Transform>();


    //}

    //// Update is called once per frame
    //void Update()
    //{


    //    Distance = Vector2.Distance(transform.position, target.position);

    //    Dis.text = (0.01f - (float)Distance * 0.00001f).ToString();
    //    if (Distance < 11.0f)
    //    {
    //        if(Raider != null)
    //        {
    //            Raider.fillAmount += (0.01f - (float)Distance * 0.0001f);
    //            if(Raider.fillAmount == 1.0f)
    //            {
    //                EnemyChase();
    //                animator.SetFloat("ChaseRange", Distance);
    //            }
    //        }

    //    }
    //    else
    //    {
    //        if (Raider != null)
    //        {
    //            Raider.fillAmount -= 0.1f;
    //            if (Raider.fillAmount == 0.0f)
    //            {
    //                animator.SetFloat("ChaseRange", Distance);
    //            }
    //        }

    //    }
    //}

    //void EnemyChase()
    //{
    //    Debug.Log("Chase!");
    //}
}