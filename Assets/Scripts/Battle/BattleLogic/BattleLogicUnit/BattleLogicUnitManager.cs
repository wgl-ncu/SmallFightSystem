using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitManager
{
    public List<BattleLogicUnitEnemy> enemies;
    public BattleLogicUnitPlayer player;
    public BattleLogicManager battleLogicManager;

    public BattleLogicUnitManager(BattleLogicManager battleLogicManager)
    {
        enemies = new List<BattleLogicUnitEnemy>();
        this.battleLogicManager = battleLogicManager;
    }

    public void Update()
    {
        player?.Update();
        for(int i = 0; i < enemies.Count; ++i)
        {
            enemies[i].Update();
        }
    }

    internal void LoadLevelData()
    {
        if (player == null)
        {
            AddPlayer();
        }
        var levelData = battleLogicManager.curLevelData;
        for(int i = 0; i < levelData.enemyIds.Count; ++i)
        {
            AddEnemy(levelData.enemyIds[i]);
        }
    }

    private void AddEnemy(int id)
    {
        var enemyData = BattleDataHelper.GetEnemyData(id);
        var enemy = new BattleLogicUnitEnemy(enemyData, UnitCamp.Enemy);
        enemies.Add(enemy);
    }

    private void AddPlayer()
    {
        var playerData = BattleDataHelper.GetPlayerData();
        player = new BattleLogicUnitPlayer(playerData, UnitCamp.Player);
    }

    internal void StartUnits()
    {

    }

    internal BattleLogicUnitBase GetLogicUnit(int id)
    {
        if(id == player.id)
        {
            return player;
        }
        else
        {
            return enemies.Find(a => a.id == id);
        }
    }

    internal void OnPressSkillBtn(int skillid)
    {
        player?.OnPressSkillBtn(skillid);
    }

    internal void OnInputNoneKey()
    {
        player?.OnInputNoneKey();
    }

    internal void OnGetInputKey(KeyCode key)
    {
        player?.OnGetInputKey(key);
    }

    internal List<int> GetEnemyIds()
    {
        List<int> res = new List<int>();
        for(int i = 0; i <  enemies.Count; ++i)
        {
            res.Add(enemies[i].id);
        }
        return res;
    }

    public List<int> GetSortedByDistanceEnemies()
    {
        List<int> res = new List<int>();
        SortEnemies();
        for (int i = 0; i < enemies.Count; ++i)
        {
            res.Add(enemies[i].id);
        }
        return res;
    }

    public List<int> GetEnemiesInRange(float dis, bool needSort = false)
    {
        List<int> res = new List<int>();
        Vector3 posPlayer = player.propertyCtrl.curProperties.POS.value;
        if (needSort)
        {
            SortEnemies();
        }
        for (int i = 0; i < enemies.Count; ++i)
        {
            if (enemies[i].isAlive)
            {
                Vector3 enemyPos = enemies[i].propertyCtrl.curProperties.POS.value;
                if (dis >= Vector3.SqrMagnitude(enemyPos - posPlayer))
                {
                    res.Add(enemies[i].id);
                }
            }
        }
        return res;
    }

    internal List<int> GetTargetsInRange(Vector3 targetPoint, float range, bool discardPlayer = true, bool discardEnemies = false)
    {
        List<int> res = new List<int>();
        if (!discardPlayer)
        {
            Vector3 posPlayer = player.propertyCtrl.curProperties.POS.value;
            float dis = Vector3.SqrMagnitude(posPlayer - targetPoint);
            if(dis <= range)
            {
                res.Add(player.id);
            }
        }
        if (!discardEnemies)
        {
            for (int i = 0; i < enemies.Count; ++i)
            {
                if (enemies[i].isAlive)
                {
                    Vector3 enemyPos = enemies[i].propertyCtrl.curProperties.POS.value;
                    if (range >= Vector3.SqrMagnitude(enemyPos - targetPoint))
                    {
                        res.Add(enemies[i].id);
                    }
                }
            }
        }
        return res;
    }

    private void SortEnemies()
    {
        enemies.Sort((a, b) => {
            float disA = Vector3.SqrMagnitude(a.propertyCtrl.curProperties.POS.value - player.propertyCtrl.curProperties.POS.value);
            float disB = Vector3.SqrMagnitude(b.propertyCtrl.curProperties.POS.value - player.propertyCtrl.curProperties.POS.value);
            return disA < disB ? -1 : disA == disB ? 0 : 1;
        });
    }

    internal void OnLevelWin()
    {
        ClearEnemyData();
    }

    internal void OnPressKey(KeyCode key)
    {
        player?.OnPressKey(key);
    }

    internal bool HasAnyEnemyAlive()
    {
        return enemies.FindIndex(a => a.isAlive == true) != -1;
    }

    internal List<int> GetUnitsInArea(Vector3 pos, float range, int targetNum, bool dispatchPlayer = false, bool dispatchEnemy = false)
    {
        List<int> res = new List<int>();
        if(targetNum == -1)
        {
            targetNum = int.MaxValue;
        }
        if (!dispatchPlayer)
        {
            var disPlayer = Vector3.SqrMagnitude(pos - player.propertyCtrl.curProperties.POS.value);
            if (disPlayer <= range && res.Count < targetNum)
            {
                res.Add(player.id);
            }
        }
        if (!dispatchEnemy)
        {
            for (int i = 0; i < enemies.Count; ++i)
            {
                var dis = Vector3.SqrMagnitude(pos - enemies[i].propertyCtrl.curProperties.POS.value);
                if (dis <= range && res.Count < targetNum)
                {
                    res.Add(enemies[i].id);
                }
            }
        }
        return res;
    }

    public void ClearAllData()
    {
        ClearEnemyData();
        ClearPlayerData();
    }

    private void ClearPlayerData()
    {
        player.Uninit();
        player = null;
    }

    private void ClearEnemyData()
    {
        for (int i = 0; i < enemies.Count; ++i)
        {
            enemies[i].Uninit();
        }
        enemies.Clear();
    }

    #region static
    private static int globalUnitIndex = 0;
    public static int ApplyUnitId()
    {
        return ++globalUnitIndex;
    }
    #endregion
}
