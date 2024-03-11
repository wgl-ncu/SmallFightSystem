using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleViewUnitManager
{
    public BattleViewManager battleViewManager;

    public BattleViewUnitBase player;
    public List<BattleViewUnitBase> enemies;
    public BattleViewUnitManager(BattleViewManager battleViewManager)
    {
        this.battleViewManager = battleViewManager;
        enemies = new List<BattleViewUnitBase>();
        
    }

    public void Update()
    {
        player?.Update();
        for(int i = 0; i < enemies.Count; ++i)
        {
            enemies[i].Update();
        }
    }

    internal void LoadLevel(List<int> enemyIds)
    {
        if(player == null)
        {
            AddPlayer();
        }
        for(int i = 0; i < enemyIds.Count; ++i)
        {
            AddEnemy(enemyIds[i]);
        }
    }

    internal void OnLeaveLoadState()
    {

    }

    private void AddPlayer()
    {
        var logicData = BattleDataRunTimeHelper.GetLogicPlayer();
        player = new BattleViewUnitBase(logicData, this);
    }

    private void AddEnemy(int id)
    {
        var logicData = BattleDataRunTimeHelper.GetLogicUnit(id);
        var enemy = new BattleViewUnitBase(logicData, this);
        enemies.Add(enemy);
    }

    internal BattleViewUnitBase GetViewUnit(int id)
    {
        if(id == player.logicId)
        {
            return player;
        }
        else
        {
            return enemies.Find(a => a.logicId == id);
        }
    }

    internal void OnLevelWin()
    {
        for(int i = 0; i < enemies.Count; ++i)
        {
            enemies[i].Uninit();
        }
        enemies.Clear();
    }

    internal void OnEnterBattleLogicExitState()
    {
        for (int i = 0; i < enemies.Count; ++i)
        {
            enemies[i].Uninit();
        }
        enemies.Clear();
        player.Uninit();
        player = null;
    }
}
