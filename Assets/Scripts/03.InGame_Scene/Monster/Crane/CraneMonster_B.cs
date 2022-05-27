using UnityEngine;

public class CraneMonster_B : Monster
{

    //이동 관련 변수
    float MoveTime = 3.0f;
    bool MoveRight = false;
    public float MoveSpeed = 1.0f;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        InitMonster();
        m_Monstate = MonsterState.PATROL;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if(m_Monstate == MonsterState.PATROL)
        {
            if(MoveTime >= 0.0f)
            {
                MoveTime -= Time.deltaTime;
                if(MoveRight)
                {
                    m_Rb.transform.position += Vector3.right * MoveSpeed * Time.deltaTime;
                }
                else
                {
                    m_Rb.transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
                }

                if(MoveTime <= 0.0f)
                {
                    MoveTime = Random.Range(3.0f, 5.0f);
                    MoveRight = !MoveRight;

                    if (MoveRight)
                    {
                        this.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
                    }
                    else
                    {
                        this.transform.rotation = Quaternion.Euler(0, 0.0f, 0);
                    }
                }
            }
        }
    }

    
}