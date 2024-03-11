using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : SingletonCSharp<BattleManager>
{
    public float _deltaTime;
    private BattleFsmManager battleFsmManager;
    private BattleLogicManager battleLogicManager;
    private BattleViewManager battleViewManager;

    public void Init()
    {
        Register();
        InputManager.Instance.Init();
        BattleDataHelper.Init();
        battleFsmManager = new BattleFsmManager(this);
        battleLogicManager = new BattleLogicManager(this);
        battleViewManager = new BattleViewManager(this);
        BattleDataRunTimeHelper.Init(battleLogicManager);
        battleFsmManager.SwitchState(BattleState.Idle);
    }

    public void Uninit()
    {
        Unregister();
        battleLogicManager.Uninit();
    }
    public void Update(float dt)
    {
        _deltaTime = dt;
        battleFsmManager.Update();
        battleLogicManager.Update();
        battleViewManager.Update();
    }

    private void Register()
    {
        EventManager.AddEventListener(CommonEvent.ON_BATTLE_START, OnBattleStart);
        EventManager.AddEventListener(CommonEvent.ON_EXIT_BATTLE, OnExitBattle);
    }

    private void Unregister()
    {
        EventManager.RemoveEventListener(CommonEvent.ON_BATTLE_START, OnBattleStart);
        EventManager.RemoveEventListener(CommonEvent.ON_EXIT_BATTLE, OnExitBattle);
    }

    private void OnBattleStart()
    {
        UIManager.Instance.ClosePage(PageDefine.MainPage);
        UIManager.Instance.OpenPage(PageDefine.MenuPage);
        battleFsmManager.SwitchState(BattleState.Load);
        battleLogicManager.OnBattleStart();
    }

    private void OnExitBattle()
    {
        UIManager.Instance.ClosePage(PageDefine.MenuPage);
        UIManager.Instance.OpenPage(PageDefine.MainPage);
        battleFsmManager.SwitchState(BattleState.Idle);
        battleLogicManager.OnBattleEnd();
    }
}
