using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonEvent
{
    public static int ON_GAME_LOAD = 1;
    public static int ON_GAME_LOAD_FINISH = 2;
    public static int ON_BATTLE_START = 3;
    public static int ON_EXIT_BATTLE = 4;
    public static int ON_ENTER_BATTLE_LOGIC_LOAD_STATE = 5;//logicFsm进入Load
    public static int ON_ENTER_BATTLE_LOGIC_ENTER_STATE = 6;//logicFsm进入Enter
    public static int ON_BATTLE_LOGIC_UNIT_CHANGE_STATE = 7;//battle unit切换状态
    public static int ON_LEAVE_BATTLE_LOGIC_LOAD_STATE = 8;//logicFsm离开Load
    public static int ON_UNIT_RELEASE_SKILL = 9;//释放技能
    public static int ON_UNIT_PROP_CHANGE = 10;//单位属性变化
    public static int ON_SKILL_USING_OR_CD = 12;//技能释放中或者cd中

    public static int ON_INPUT_KEY = 13;//需要按住的按钮按住
    public static int ON_PRESS_SKILL_BTN = 14;//点击技能
    public static int ON_INPUT_NONE_KEY = 15;//没有键盘输入
    public static int ON_PLAYER_SKILL_CHANGE_STATE = 16;//玩家技能切换状态
    public static int ON_ENTER_BATTLE_LOGIC_EXIT_STATE = 17;//ogicFsm进入Exit
    public static int ON_LEVEL_WIN = 18;//波次胜利
    public static int ON_PRESS_KEY = 19;//需要按下的按钮按下

    public static int ON_BUFF_START = 20;//buff生效
    public static int ON_BATTLE_WIN = 21;//战斗胜利
}
