using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public GameFsmManager gameMgr;
    public float deltaTime;

    private void Awake()
    {
        Register();
        StaticData.Init();
        gameMgr = new GameFsmManager(this);
        UIManager.Instance.Init();
        BattleManager.Instance.Init();
    }

    private void OnDestroy()
    {
        Unregister();
        UIManager.Instance.Uninit();
        BattleManager.Instance.Uninit();
    }

    private void Start()
    {
        gameMgr.SwitchState(GameState.Load);
    }

    private void Update()
    {
        deltaTime = Time.deltaTime;
        gameMgr.Update();
    }

    private void Register()
    {
        EventManager.AddEventListener(CommonEvent.ON_GAME_LOAD, OnGameLoad);
        EventManager.AddEventListener(CommonEvent.ON_GAME_LOAD_FINISH, OnGameLoadFinish);
        EventManager.AddEventListener(CommonEvent.ON_BATTLE_START, OnBattleStart);
    }

    private void Unregister()
    {
        EventManager.RemoveEventListener(CommonEvent.ON_GAME_LOAD, OnGameLoad);
        EventManager.RemoveEventListener(CommonEvent.ON_GAME_LOAD_FINISH, OnGameLoadFinish);
        EventManager.RemoveEventListener(CommonEvent.ON_BATTLE_START, OnBattleStart);
    }

    private void OnBattleStart()
    {
        gameMgr.SwitchState(GameState.Fight);
    }

    private void OnGameLoad()
    {
        UIManager.Instance.OpenPage(PageDefine.MainPage);
        EventManager.TriggerEvent(CommonEvent.ON_GAME_LOAD_FINISH);
    }

    private void OnGameLoadFinish()
    {
        gameMgr.loadFinish = true;
    }
}
