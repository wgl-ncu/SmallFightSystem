using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonEvent
{
    public static int ON_GAME_LOAD = 1;
    public static int ON_GAME_LOAD_FINISH = 2;
    public static int ON_BATTLE_START = 3;
    public static int ON_EXIT_BATTLE = 4;
    public static int ON_ENTER_BATTLE_LOGIC_LOAD_STATE = 5;//logicFsm����Load
    public static int ON_ENTER_BATTLE_LOGIC_ENTER_STATE = 6;//logicFsm����Enter
    public static int ON_BATTLE_LOGIC_UNIT_CHANGE_STATE = 7;//battle unit�л�״̬
    public static int ON_LEAVE_BATTLE_LOGIC_LOAD_STATE = 8;//logicFsm�뿪Load
    public static int ON_UNIT_RELEASE_SKILL = 9;//�ͷż���
    public static int ON_UNIT_PROP_CHANGE = 10;//��λ���Ա仯
    public static int ON_SKILL_USING_OR_CD = 12;//�����ͷ��л���cd��

    public static int ON_INPUT_KEY = 13;//��Ҫ��ס�İ�ť��ס
    public static int ON_PRESS_SKILL_BTN = 14;//�������
    public static int ON_INPUT_NONE_KEY = 15;//û�м�������
    public static int ON_PLAYER_SKILL_CHANGE_STATE = 16;//��Ҽ����л�״̬
    public static int ON_ENTER_BATTLE_LOGIC_EXIT_STATE = 17;//ogicFsm����Exit
    public static int ON_LEVEL_WIN = 18;//����ʤ��
    public static int ON_PRESS_KEY = 19;//��Ҫ���µİ�ť����

    public static int ON_BUFF_START = 20;//buff��Ч
    public static int ON_BATTLE_WIN = 21;//ս��ʤ��
}
