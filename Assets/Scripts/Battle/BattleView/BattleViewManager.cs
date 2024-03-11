using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleViewManager
{
    public BattleManager battleManager;
    public BattleViewLogicLayerListener logicLayerListener;
    public BattleViewUnitManager viewUnitManager;
    public BattleViewUIManager uIManager;
    public BattleViewSceneManager sceneManager;

    public BattleViewManager(BattleManager battleManager)
    {
        this.battleManager = battleManager;
        logicLayerListener = new BattleViewLogicLayerListener(this);
        viewUnitManager = new BattleViewUnitManager(this);
        uIManager = new BattleViewUIManager(this);
        sceneManager = new BattleViewSceneManager(this);
    }

    public void Update()
    {
        viewUnitManager.Update();
    }

    internal void OnEnterLoadState(List<int> enemyIds)
    {
        viewUnitManager.LoadLevel(enemyIds);
        sceneManager.LoadScene();
    }

    public void OnLeaveLoadState()
    {
        viewUnitManager.OnLeaveLoadState();
    }

    public BattleViewUnitBase GetViewUnit(int id)
    {
        return viewUnitManager.GetViewUnit(id);
    }

    public void LoadVFX(VFXConfig skillVFX, Vector3 pos, float offset)
    {
        var vfxRoot = SceneNodeManager.Instance.VFXRoot;
        var vfxPrefab = VFXLoadHelper.LoadBattleVFX(skillVFX.path);
        var vfx = GameObject.Instantiate(vfxPrefab);
        vfx.transform.parent = vfxRoot;
        var planePos = BattleDataRunTimeHelper.GetBattleConfig().planePos;
        vfx.transform.position = new Vector3(pos.x, pos.y + skillVFX.offset + offset, 0);
        vfx.Init(false);
    }

    internal void OnEnterBattleLogicLoadState(List<int> enemies)
    {
        uIManager.OnEnterBattleLogicLoadState(enemies);
    }

    internal void OnEnterBattleLogicExitState()
    {
        uIManager.OnEnterBattleLogicExitState();
        viewUnitManager.OnEnterBattleLogicExitState();
        sceneManager.OnEnterBattleLogicExitState();
    }

    internal void OnLeaveBattleLogicLoadState(List<int> enemies)
    {
        uIManager.OnLeaveBattleLogicLoadState(enemies);
    }

    public void LoadVFX(VFXConfig skillVFX, BattleViewUnitBase unit, float offset, bool followTar = false)
    {
        var vfxRoot = SceneNodeManager.Instance.VFXRoot;
        var vfxPrefab = VFXLoadHelper.LoadBattleVFX(skillVFX.path);
        var vfx = GameObject.Instantiate(vfxPrefab);
        vfx.transform.parent = vfxRoot;
        var planePos = BattleDataRunTimeHelper.GetBattleConfig().planePos;
        var obj = unit.obj;
        vfx.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + skillVFX.offset + offset, obj.transform.position.y);
        vfx.Init(followTar, obj);
    }

    internal void OnPlayerSkillChangeState(int skillId, BattleLogicUnitSkillState state)
    {
        uIManager.OnPlayerSkillChangeState(skillId, state);
    }

    internal void OnLevelWin()
    {
        viewUnitManager.OnLevelWin();
    }
}
