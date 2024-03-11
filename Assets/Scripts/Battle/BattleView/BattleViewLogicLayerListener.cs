using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleViewLogicLayerListener
{
    public BattleViewManager viewManager;
    public BattleViewLogicLayerListener(BattleViewManager viewManager)
    {
        this.viewManager = viewManager;
        Register();
    }

    public void Uninit()
    {
        Unregister();
    }

    private void Register()
    {
        EventManager.AddEventListener<List<int>>(CommonEvent.ON_ENTER_BATTLE_LOGIC_LOAD_STATE, OnEnterLoadState);
        EventManager.AddEventListener(CommonEvent.ON_ENTER_BATTLE_LOGIC_ENTER_STATE, OnEnterEnterState);
        EventManager.AddEventListener<int, BattleLogicUnitState>(CommonEvent.ON_BATTLE_LOGIC_UNIT_CHANGE_STATE, OnUnitChangeState);
        EventManager.AddEventListener<int, BattleLogicUnitSkillBase>(CommonEvent.ON_UNIT_RELEASE_SKILL, OnUnitReleaseSkill);
        EventManager.AddEventListener<int, LogicUnitProp, object>(CommonEvent.ON_UNIT_PROP_CHANGE, OnLogicUnitPropChange);
        EventManager.AddEventListener<int, BattleLogicUnitSkillState>(CommonEvent.ON_PLAYER_SKILL_CHANGE_STATE, OnPlayerSkillChangeState);
        EventManager.AddEventListener<List<int>>(CommonEvent.ON_LEAVE_BATTLE_LOGIC_LOAD_STATE, OnLeaveBattleLogicLoadState);
        EventManager.AddEventListener<List<int>>(CommonEvent.ON_ENTER_BATTLE_LOGIC_LOAD_STATE, OnEnterBattleLogicLoadState);
        EventManager.AddEventListener(CommonEvent.ON_ENTER_BATTLE_LOGIC_EXIT_STATE, OnEnterBattleLogicExitState);
        EventManager.AddEventListener(CommonEvent.ON_LEVEL_WIN, OnLevelWin);
        EventManager.AddEventListener<BattleLogicUnitBuffBase>(CommonEvent.ON_BUFF_START, OnBuffStart);
        EventManager.AddEventListener(CommonEvent.ON_BATTLE_WIN, OnBattleWin);
    }

    private void Unregister()
    {
        EventManager.RemoveEventListener<List<int>>(CommonEvent.ON_ENTER_BATTLE_LOGIC_LOAD_STATE, OnEnterLoadState);
        EventManager.RemoveEventListener(CommonEvent.ON_ENTER_BATTLE_LOGIC_ENTER_STATE, OnEnterEnterState);
        EventManager.RemoveEventListener<int, BattleLogicUnitState>(CommonEvent.ON_BATTLE_LOGIC_UNIT_CHANGE_STATE, OnUnitChangeState);
        EventManager.RemoveEventListener<int, BattleLogicUnitSkillBase>(CommonEvent.ON_UNIT_RELEASE_SKILL, OnUnitReleaseSkill);
        EventManager.RemoveEventListener<int, LogicUnitProp, object>(CommonEvent.ON_UNIT_PROP_CHANGE, OnLogicUnitPropChange);
        EventManager.RemoveEventListener<int, BattleLogicUnitSkillState>(CommonEvent.ON_PLAYER_SKILL_CHANGE_STATE, OnPlayerSkillChangeState);
        EventManager.RemoveEventListener<List<int>>(CommonEvent.ON_LEAVE_BATTLE_LOGIC_LOAD_STATE, OnLeaveBattleLogicLoadState);
        EventManager.RemoveEventListener<List<int>>(CommonEvent.ON_ENTER_BATTLE_LOGIC_LOAD_STATE, OnEnterBattleLogicLoadState);
        EventManager.RemoveEventListener(CommonEvent.ON_ENTER_BATTLE_LOGIC_EXIT_STATE, OnEnterBattleLogicExitState);
        EventManager.RemoveEventListener(CommonEvent.ON_LEVEL_WIN, OnLevelWin);
        EventManager.RemoveEventListener<BattleLogicUnitBuffBase>(CommonEvent.ON_BUFF_START, OnBuffStart);
        EventManager.RemoveEventListener(CommonEvent.ON_BATTLE_WIN, OnBattleWin);
    }

    public void OnEnterLoadState(List<int> enemyIds)
    {
        viewManager.OnEnterLoadState(enemyIds);
    }

    private void OnEnterEnterState()
    {
        TipsManager.Instance.CreateTips(string.Format(TextData.EnterLevelText, StaticData.gameData.level), 2);
        TipsManager.Instance.CreateTips(TextData.StartBattle, 2);

    }

    private void OnUnitChangeState(int id, BattleLogicUnitState state)
    {
        var viewUnit = viewManager.GetViewUnit(id);
        viewUnit.battleViewUnitFsmManager.SwitchState((BattleViewUnitState)state);
    }

    private void OnUnitReleaseSkill(int unitId, BattleLogicUnitSkillBase skill)
    {
        var viewUnit = viewManager.GetViewUnit(unitId);
        viewUnit.RunSkill(skill);

        LoadVFX(unitId, skill);
    }

    private void OnLogicUnitPropChange(int unitId, LogicUnitProp type, object changeValue)
    {
        var srcUnit = viewManager.GetViewUnit(unitId);
        if (srcUnit != null)
        {
            srcUnit.OnPropChange(type, changeValue);
        }
    }

    private void LoadVFX(int unitId, BattleLogicUnitSkillBase skill)
    {
        var VFXs = skill.data.data.startVFXs;
        var targets = BattleDataRunTimeHelper.GetSkillReleaseTargets(unitId, skill.data.skillReleasePairs[0], true);
        for (int i = 0; i < VFXs.Count; ++i)
        {
            var VFX = VFXs[i];
            if (VFX.loadWay == SkillVFXLoadWay.FirstTarget)
            {
                if (targets.Count > 0)
                {
                    var tar = viewManager.GetViewUnit(targets[0]);
                    var offset = BattleDataRunTimeHelper.GetLogicUnit(targets[0]).propertyCtrl.data.height;
                    viewManager.LoadVFX(VFX, tar, offset);
                }
            }
            else if (VFX.loadWay == SkillVFXLoadWay.Self)
            {
                var tar = viewManager.GetViewUnit(unitId);
                var offset = BattleDataRunTimeHelper.GetLogicUnit(unitId).propertyCtrl.data.height;
                viewManager.LoadVFX(VFX, tar, offset);
            }
            else if (VFX.loadWay == SkillVFXLoadWay.Center)
            {
                if (targets.Count > 0)
                {
                    Vector3 center = GetCenterPos(targets);
                    viewManager.LoadVFX(VFX, center, 0);
                }
            }
        }
    }

    private Vector3 GetCenterPos(List<int> targets)
    {
        if(targets.Count % 2 == 0)
        {
            int index = targets.Count / 2;
            var tar1 = viewManager.GetViewUnit(targets[index]);
            var tar2 = viewManager.GetViewUnit(targets[index + 1]);
            var pos1 = tar1.obj.transform.position;
            var pos2 = tar2.obj.transform.position;
            return (pos1 + pos2) / 2.0f;
        }
        else
        {
            int index = targets.Count / 2;
            return viewManager.GetViewUnit(targets[index]).obj.transform.position;
        }
    }

    private void OnPlayerSkillChangeState(int skillId, BattleLogicUnitSkillState state)
    {
        viewManager.OnPlayerSkillChangeState(skillId, state);
    }

    private void OnEnterBattleLogicLoadState(List<int> enemies)
    {
        viewManager.OnEnterBattleLogicLoadState(enemies);
    }

    private void OnLeaveBattleLogicLoadState(List<int> enemies)
    {
        viewManager.OnLeaveBattleLogicLoadState(enemies);
    }

    private void OnEnterBattleLogicExitState()
    {
        viewManager.OnEnterBattleLogicExitState();
        TipsManager.Instance.ClearAll();
    }

    private void OnLevelWin()
    {
        viewManager.OnLevelWin();
    }

    private void OnBuffStart(BattleLogicUnitBuffBase buff)
    {
        if(buff.data.target == BuffTarget.Area)
        {
            var pos = buff.pos;
            viewManager.LoadVFX(buff.data.vfxConfig, pos, 0);
        }
        else
        {
            var tar = buff.unitId;
            var unit = viewManager.GetViewUnit(tar);
            var offset = BattleDataRunTimeHelper.GetLogicUnit(tar).propertyCtrl.data.height;
            viewManager.LoadVFX(buff.data.vfxConfig, unit, offset, true);
        }
    }

    private void OnBattleWin()
    {
        TipsManager.Instance.ClearAll();
        TipsManager.Instance.CreateTips(TextData.WinBattle, 2);
    }
}
