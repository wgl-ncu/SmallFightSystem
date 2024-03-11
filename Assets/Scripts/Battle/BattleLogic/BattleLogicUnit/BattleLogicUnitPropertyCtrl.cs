using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogicUnitProp
{
    HP,
    ATK,
    POS,
    SPEED,
}

public class PropChangeData
{
    public List<KeyValuePair<LogicUnitProp, int>> intProps = new List<KeyValuePair<LogicUnitProp, int>>();
    public List<KeyValuePair<LogicUnitProp, float>> floatProps = new List<KeyValuePair<LogicUnitProp, float>>();
    public List<KeyValuePair<LogicUnitProp, Vector3>> v3Props = new List<KeyValuePair<LogicUnitProp, Vector3>>();
    public void Add(LogicUnitProp type, int newValue)
    {
        intProps.Add(new KeyValuePair<LogicUnitProp, int>(type, newValue));
    }

    public void Add(LogicUnitProp type, float newValue)
    {
        floatProps.Add(new KeyValuePair<LogicUnitProp, float>(type, newValue));
    }

    public void Add(LogicUnitProp type, Vector3 newValue)
    {
        v3Props.Add(new KeyValuePair<LogicUnitProp, Vector3>(type, newValue));
    }
}

public class BattleLogicUnitPropertyCtrl
{
    public BattleLogicUnitPropertyGroup baseProperties;
    public BattleLogicUnitPropertyGroup curProperties;
    public CharacterData_SO data;

    private BattleLogicUnitBase unit;

    public BattleLogicUnitPropertyCtrl(BattleLogicUnitBase source, CharacterData_SO data)
    {
        this.unit = source;
        this.data = data;
        baseProperties = new BattleLogicUnitPropertyGroup(data, BattleDataHelper.GetUnitSpawnPos(unit.unitCamp));
        curProperties = new BattleLogicUnitPropertyGroup(baseProperties);
    }

    public void DealPropChange(LogicUnitProp type, int newValue)
    {
        int curValue;
        switch (type)
        {
            case LogicUnitProp.HP:
                curValue = curProperties.HP.value;
                curProperties.HP.UpdateValue(curValue + newValue);
                break;
            case LogicUnitProp.ATK:
                curValue = curProperties.ATK.value;
                curProperties.ATK.UpdateValue(curValue + newValue);
                break;
        }
        EventManager.TriggerEvent<int, LogicUnitProp, object>(CommonEvent.ON_UNIT_PROP_CHANGE, unit.id, type, newValue);
    }

    public void DealPropChange(LogicUnitProp type, float newValue)
    {
        float curValue;
        switch (type)
        {
            case LogicUnitProp.SPEED:
                curValue = curProperties.speed.value;
                curProperties.speed.UpdateValue(curValue + newValue);
                break;
        }
        EventManager.TriggerEvent<int, LogicUnitProp, object>(CommonEvent.ON_UNIT_PROP_CHANGE, unit.id, type, newValue);
    }

    public void DealPropChange(LogicUnitProp type, Vector3 newValue)
    {
        Vector3 curValue;
        switch (type)
        {
            case LogicUnitProp.POS:
                curValue = curProperties.POS.value;
                curProperties.POS.UpdateValue(curValue + newValue);
                break;
        }
        EventManager.TriggerEvent<int, LogicUnitProp, object>(CommonEvent.ON_UNIT_PROP_CHANGE, unit.id, type, newValue);
    }
}

[SerializeField]
public class BattleLogicUnitPropertyGroup
{
    public BattleLogicUnitProperty<int> HP = new BattleLogicUnitProperty<int>(LogicUnitProp.HP);
    public BattleLogicUnitProperty<int> ATK = new BattleLogicUnitProperty<int>(LogicUnitProp.ATK);
    public BattleLogicUnitProperty<Vector3> POS = new BattleLogicUnitProperty<Vector3>(LogicUnitProp.POS);
    public BattleLogicUnitProperty<float> speed = new BattleLogicUnitProperty<float>(LogicUnitProp.SPEED);
    //public BattleLogicUnitProperty<float> atkRange = new BattleLogicUnitProperty<float>(LogicUnitProp.ATKRANGE);
    public BattleLogicUnitPropertyGroup(BattleLogicUnitPropertyGroup prop)
    {
        HP.value = prop.HP.value;
        ATK.value = prop.ATK.value;
        POS.value = prop.POS.value;
        speed.value = prop.speed.value;
    }
    public BattleLogicUnitPropertyGroup(CharacterData_SO data, Vector3 pos)
    {
        HP.value = data.hp;
        ATK.value = data.attack;
        POS.value = pos;
        speed.value = data.speed;
    }
}

public class BattleLogicUnitProperty<T>
{
    public T value;
    public LogicUnitProp type;
    public BattleLogicUnitProperty(LogicUnitProp type)
    {
        this.type = type;
    }
    public void UpdateValue(T value)
    {
        this.value = value;
    }
}
