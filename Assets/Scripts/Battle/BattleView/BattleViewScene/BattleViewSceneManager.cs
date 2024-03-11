using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleViewSceneManager
{
    private BattleViewManager viewManager;
    private GameObject scene;
    public BattleViewSceneManager(BattleViewManager viewManager)
    {
        this.viewManager = viewManager;
    }

    internal void LoadScene()
    {
        if(scene != null)
        {
            GameObject.Destroy(scene);
            scene = null;
        }
        var scendPath = BattleDataRunTimeHelper.GetCurSceneData().path;
        var root = SceneNodeManager.Instance.SceneRoot;
        var sceneRes = Resources.Load<GameObject>(scendPath);
        scene = GameObject.Instantiate(sceneRes, root);
        scene.SetActive(true);
    }

    internal void OnEnterBattleLogicExitState()
    {
        GameObject.Destroy(scene);
    }
}
