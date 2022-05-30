using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region �̰ŷ� �ٲ� ����
public enum PlayerState
{
    player_menu = 0,    //���� class ����
    player_move = 1,    //���� class ����
    player_attack = 2,  //���� class ����
    player_die = 3,
    player_idle = 4,
    player_talk = 5,
    player_map = 6,
    player_takeDamage = 7
}

public enum PlayerMoveState
{
    player_walk = 0,
    player_dash = 1,
    player_jump = 2,
    player_fall = 3,
    player_crawl = 4,
    player_climb = 5,
    player_noMove =6
}

public enum PlayerAttackState
{
    player_no_att = 0,
    player_sword = 1,
    player_bow = 2
}

public enum PlayerAttack1   //sword
{
    player_att1_normal = 0,
    player_att1_skill1 = 1,
    player_att1_skill2 = 2,
    player_att1_skill3 = 3
}
public enum PlayerAttack2   //bow
{
    player_att2_normal = 0,
    player_att2_aim = 1,
    player_att1_skill1 = 2,
    player_att1_skill2 = 3,
    player_att1_skill3 = 4
}

#endregion

public class Player_State_Ctrl : MonoBehaviour
{
    public PlayerState p_state;
    public PlayerMoveState p_Move_state;
    public PlayerAttackState p_Attack_state;

    // Start is called before the first frame update
    void Start()
    {
        p_state = PlayerState.player_idle;
        p_Move_state = PlayerMoveState.player_noMove;
        p_Attack_state = PlayerAttackState.player_no_att;
    }

    // Update is called once per frame
    //void Update()
    //{
    //}
}
