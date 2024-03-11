using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleDataHelper
{
    private static Dictionary<int, SkillData_SO> skillData;
    private static Dictionary<int, LevelData_SO> levelData;
    private static Dictionary<int, CharacterData_SO> enemyData;
    private static Dictionary<int, SceneData_SO> sceneData;
    private static Dictionary<int, BuffData_SO> buffData;
    public static int MaxLevel;
    public static void Init()
    {
        LoadData();
    }

    private static void LoadData()
    {
        var skillArray = ScriptableObjectHelper.GetAllSkillData(ScriptableObjetPathDefine.skillDataFolder);
        TransArrayToDict(ref skillData, skillArray, (a) => a.id);
        var levelArray = ScriptableObjectHelper.GetAllLevelData(ScriptableObjetPathDefine.levelDataFolder);
        SetMaxLevel(levelArray);
        TransArrayToDict(ref levelData, levelArray, (a) => a.id);
        var enemyArray = ScriptableObjectHelper.GetAllEnemyData(ScriptableObjetPathDefine.enemyDataFolder);
        TransArrayToDict(ref enemyData, enemyArray, (a) => a.id);
        var sceneArray = ScriptableObjectHelper.GetAllSceneData(ScriptableObjetPathDefine.sceneDataFolder);
        TransArrayToDict(ref sceneData, sceneArray, (a) => a.id);
        var buffArray = ScriptableObjectHelper.GetAllBuffData(ScriptableObjetPathDefine.buffDataFloder);
        TransArrayToDict(ref buffData, buffArray, (a) => a.id);
    }

    private static void TransArrayToDict<T, U>(ref Dictionary<T, U> dict, U[] array, Func<U, T> GetKeyFunc)
    {
        dict = new Dictionary<T, U>();
        for (int i = 0; i < array.Length; ++i)
        {
            U value = array[i];
            T key = GetKeyFunc(value);
            if (dict.ContainsKey(key))
            {
                Logger.Error("¼üÖØ¸´£¡dict: " + dict.ToString() + ", key: " + key);
            }
            else
            {
                dict.Add(key, value);
            }
        }
    }

    public static LevelData_SO GetLevelData(int level)
    {
        return TryGetDictValue(levelData, level);
    }

    public static SkillData_SO GetSkillData(int skillId)
    {
        return TryGetDictValue(skillData, skillId);
    }

    public static CharacterData_SO GetEnemyData(int id)
    {
        return TryGetDictValue(enemyData, id);
    }

    public static CharacterData_SO GetPlayerData()
    {
        return StaticData.playerData;
    }

    public static string GetUnitPrefabPath(int id)
    {
        return BattleDataRunTimeHelper.GetLogicUnit(id).GetPrefabPath();
    }

    public static BattleConfig_SO GetBattleConfig()
    {
        return BattleDataRunTimeHelper.GetBattleLogicManager().battleConfig;
    }

    public static Vector3 GetUnitPos(int id)
    {
        return BattleDataRunTimeHelper.GetLogicUnit(id).propertyCtrl.curProperties.POS.value;
    }

    public static Vector3 GetUnitSpawnPos(int id)
    {
        var camp = BattleDataRunTimeHelper.GetLogicUnit(id).unitCamp;
        return GetUnitSpawnPos(camp);
    }

    public static Vector3 GetUnitSpawnPos(UnitCamp camp)
    {
        var planePos = BattleDataRunTimeHelper.GetBattleConfig().planePos;
        if (camp == UnitCamp.Player)
        {
            return new Vector3(-7, planePos, 0);
        }
        else
        {
            return new Vector3(7, planePos, 0);
        }
    }

    private static U TryGetDictValue<T, U>(Dictionary<T, U> dict, T key)
    {
        if(dict.TryGetValue(key, out U value))
        {
            return value;
        }
        else
        {
            Logger.Error("×ÖµäÖÐÎ´°üº¬key£º" + key +", dict: " + dict.ToString());
            return default;
        }
    }

    private static void SetMaxLevel(LevelData_SO[] levelArray)
    {
        MaxLevel = 0;
        foreach(var level in levelArray)
        {
            MaxLevel = Mathf.Max(level.id, MaxLevel);
        }
    }

    public static SceneData_SO GetSceneData(int id)
    {
        return TryGetDictValue(sceneData, id);
    }

    public static BuffData_SO GetBuffData(int id)
    {
        return TryGetDictValue(buffData, id);
    }
}
