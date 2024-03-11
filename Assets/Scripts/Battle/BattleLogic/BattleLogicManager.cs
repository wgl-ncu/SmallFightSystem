using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicManager
{
    public BattleManager battleManager;
    public BattleLogicFsmManager battleLogicFsmManager;
    public BattleLogicUnitManager battleLogicUnitManager;
    public BattleLogicSceneManager battleLogicSceneManager;

    public LevelData_SO curLevelData;
    public BattleConfig_SO battleConfig;
    public BattleLogicManager(BattleManager battleManager)
    {
        this.battleManager = battleManager;
        battleConfig = ScriptableObjectHelper.GetBattleConfig();
        battleLogicFsmManager = new BattleLogicFsmManager(this);
        battleLogicUnitManager = new BattleLogicUnitManager(this);
        battleLogicSceneManager = new BattleLogicSceneManager(this);
        RefreshLevelSceneData();
        Register();
    }

    public void Uninit()
    {
        Unregister();
    }

    private void Register()
    {
        EventManager.AddEventListener<KeyCode>(CommonEvent.ON_INPUT_KEY, OnGetInputKey);
        EventManager.AddEventListener<KeyCode>(CommonEvent.ON_PRESS_KEY, OnPressKey);
        EventManager.AddEventListener<int>(CommonEvent.ON_PRESS_SKILL_BTN, OnPressSkillBtn);
        EventManager.AddEventListener(CommonEvent.ON_INPUT_NONE_KEY, OnInputNoneKey);
    }

    private void Unregister()
    {
        EventManager.RemoveEventListener<KeyCode>(CommonEvent.ON_INPUT_KEY, OnGetInputKey);
        EventManager.RemoveEventListener<KeyCode>(CommonEvent.ON_PRESS_KEY, OnPressKey);
        EventManager.RemoveEventListener<int>(CommonEvent.ON_PRESS_SKILL_BTN, OnPressSkillBtn);
        EventManager.RemoveEventListener(CommonEvent.ON_INPUT_NONE_KEY, OnInputNoneKey);
    }

    private void OnInputNoneKey()
    {
        battleLogicUnitManager.OnInputNoneKey();
    }


    private void OnPressSkillBtn(int skillid)
    {
        battleLogicUnitManager.OnPressSkillBtn(skillid);
    }

    private void OnGetInputKey(KeyCode key)
    {
        battleLogicUnitManager.OnGetInputKey(key);
    }

    public void Update()
    {
        battleLogicFsmManager.Update();
        battleLogicUnitManager.Update();
    }

    public void OnBattleStart()
    {
        battleLogicFsmManager.SwitchState(BattleLogicState.Load);
    }

    internal void OnBattleEnd()
    {
        battleLogicFsmManager.SwitchState(BattleLogicState.Exit);
        battleLogicUnitManager.ClearAllData();
    }

    public void LoadLevelData()
    {
        battleLogicUnitManager.LoadLevelData();
    }

    private void RefreshLevelSceneData()
    {
        curLevelData = StaticData.gameData.GetCurLevelData();
        var sceneid = curLevelData.sceneId;
        battleLogicSceneManager.LoadSceneData(BattleDataHelper.GetSceneData(sceneid));
    }

    public BattleLogicUnitBase GetLogicUnit(int id)
    {
        return battleLogicUnitManager.GetLogicUnit(id);
    }

    public BattleLogicUnitBase GetPlayer()
    {
        return battleLogicUnitManager.player;
    }

    internal void StartUnits()
    {
        battleLogicUnitManager.StartUnits();
    }

    internal List<int> GetEnemyIds()
    {
        return battleLogicUnitManager.GetEnemyIds();
    }

    public BattleLogicState GetCurState()
    {
        return battleLogicFsmManager.CurState;
    }

    public int GetNearestEnemy(int id)
    {
        if(id == battleLogicUnitManager.player.id)
        {
            var sortedEnemies = battleLogicUnitManager.GetSortedByDistanceEnemies();
            if(sortedEnemies.Count > 0)
            {
                return sortedEnemies[0];
            }
            else
            {
                return -1;
            }
        }
        else
        {
            return battleLogicUnitManager.player.id;
        }
    }

    public List<int> GetSortedByDistanceEnemies()
    {
        return battleLogicUnitManager.GetSortedByDistanceEnemies();
    }

    public List<int> GetEnemiesInRange(float dis, bool needSort = false)
    {
        return battleLogicUnitManager.GetEnemiesInRange(dis, needSort);
    }

    public List<int> GetTargetsInRange(Vector3 targetPoint, float range)
    {
        return battleLogicUnitManager.GetTargetsInRange(targetPoint, range);
    }

    public bool HasAnyEnemyAlive()
    {
        return battleLogicUnitManager.HasAnyEnemyAlive();
    }

    public void OnLevelWin()
    {
        if (StaticData.gameData.IsFinalLevel())
        {
            OnBattleEnd();
            EventManager.TriggerEvent(CommonEvent.ON_BATTLE_WIN);
        }
        else
        {
            StaticData.gameData.IncreaseLevel();
            battleLogicUnitManager.OnLevelWin();
            RefreshLevelSceneData();
            EventManager.TriggerEvent(CommonEvent.ON_LEVEL_WIN);
        }
    }

    private void OnPressKey(KeyCode key)
    {
        battleLogicUnitManager.OnPressKey(key);
    }

    internal SceneData_SO GetCurSceneData()
    {
        return battleLogicSceneManager.data;
    }

    internal bool IsOnGround(Vector3 curPos)
    {
        return battleLogicSceneManager.IsOnGround(curPos);
    }

    public bool TryGetStandGround(Vector3 pos, out SceneGround ground)
    {
        return battleLogicSceneManager.TryGetStandGround(pos, out ground);
    }

    internal float GetNearestBottomGroundHeight(Vector3 pos)
    {
        return battleLogicSceneManager.GetNearestBottomGroundHeight(pos);
    }

    internal List<int> GetUnitsInArea(Vector3 pos, float range, int targetNum, bool dispatchPlayer = false, bool dispatchEnemy = false)
    {
        return battleLogicUnitManager.GetUnitsInArea(pos, range, targetNum, dispatchPlayer, dispatchEnemy);
    }
}
