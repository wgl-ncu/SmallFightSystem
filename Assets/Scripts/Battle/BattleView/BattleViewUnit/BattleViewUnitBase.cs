using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleViewUnitBase
{
    public int logicId;
    public GameObject obj;
    public BattleViewUnitAnimCtrl animCtrl;
    public BattleViewUnitFsmManager battleViewUnitFsmManager;
    public BattleViewUnitManager battleViewUnitManager;
    private bool started;
    public BattleViewUnitBase(BattleLogicUnitBase data, BattleViewUnitManager battleViewUnitManager)
    {
        logicId = data.id;
        started = false;
        this.battleViewUnitManager = battleViewUnitManager;
        LoadGameObject();
        battleViewUnitFsmManager = new BattleViewUnitFsmManager(this);
    }

    public void LoadGameObject()
    {
        var path = BattleDataHelper.GetUnitPrefabPath(logicId);
        var spawnPos = BattleDataHelper.GetUnitPos(logicId);
        var gameObj = Resources.Load<GameObject>(path);
        obj = GameObject.Instantiate(gameObj, SceneNodeManager.Instance.ViewUnitRoot);
        animCtrl = obj.GetComponent<BattleViewUnitAnimCtrl>();
        obj.SetActive(true);
        obj.transform.position = spawnPos;
        obj.name = "Enemy" + logicId;
        SceneNodeManager.Instance.RefreshOrderLayer();
    }

    public void Start()
    {
        if (!started)
        {
            battleViewUnitFsmManager.SwitchState(BattleViewUnitState.Spawn);
            started = true;
        }
    }

    public void Update()
    {
        if (!started)
        {
            Start();
        }
        {
            var unit = BattleDataRunTimeHelper.GetLogicUnit(logicId);
            battleViewUnitFsmManager.Update();
            obj.transform.position = unit.propertyCtrl.curProperties.POS.value;
            obj.transform.localScale = new Vector3(unit.towards, 1, 1);
        }
    }

    internal void Uninit()
    {
        Object.Destroy(obj);
    }

    public void RunSkill(BattleLogicUnitSkillBase skill)
    {
        animCtrl.PlayAnim(skill.data.data.animName);
    }

    internal void OnPropChange(LogicUnitProp type, object changeValue)
    {
        switch (type)
        {
            case LogicUnitProp.HP:
                CreateDamageNum(obj.transform, (int)changeValue);
                //Logger.Warn(logicId + " hp变化" + (int)changeValue);
                return;
            case LogicUnitProp.ATK:
                Logger.Warn(logicId + " ATK变化" + (int)changeValue);
                return;
            case LogicUnitProp.POS:
                //Logger.Warn(logicId + " POS变化" + (Vector3)changeValue);
                return;
            case LogicUnitProp.SPEED:
                Logger.Warn(logicId + " Speed变化" + (float)changeValue);
                return;
            default:
                Logger.Error("没有相应的属性变化表现！type: " + type);
                return;
        }

    }

    private void CreateDamageNum(Transform trans, int damage)
    {
        battleViewUnitManager.battleViewManager.uIManager.CreateDamageNum(trans, damage);
    }
}
