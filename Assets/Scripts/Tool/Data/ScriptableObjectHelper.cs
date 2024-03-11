using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ScriptableObjectHelper
{
    public static Dictionary<string, ScriptableObject> SODict = new Dictionary<string, ScriptableObject>();
    public static CharacterData_SO GetCharacterData(string path)
    {
        return GetT<CharacterData_SO>(path);
    }

    public static CharacterData_SO[] GetAllEnemyData(string path)
    {
        return GetAllT<CharacterData_SO>(path);
    }

    public static LevelData_SO[] GetAllLevelData(string path)
    {
        return GetAllT<LevelData_SO>(path);
    }

    public static GameData_SO GetGameData()
    {
        return GetT<GameData_SO>(ScriptableObjetPathDefine.gameData);
    }

    public static CharacterData_SO GetPlayerData()
    {
        return GetCharacterData(ScriptableObjetPathDefine.playerData);
    }

    public static BattleConfig_SO GetBattleConfig()
    {
        return GetT<BattleConfig_SO>(ScriptableObjetPathDefine.battleConfig);
    }

    public static SkillData_SO[] GetAllSkillData(string path)
    {
        return GetAllT<SkillData_SO>(path);
    }

    public static SceneData_SO[] GetAllSceneData(string path)
    {
        return GetAllT<SceneData_SO>(path);
    }

    public static BuffData_SO[] GetAllBuffData(string path)
    {
        return GetAllT<BuffData_SO>(path);
    }

    public static T GetT<T>(string path) where T : ScriptableObject
     {
        if (SODict.TryGetValue(path, out var data))
        {
            return data as T;
        }
        var asset = Resources.Load<T>(path);
        SODict.Add(path, asset);
        return asset;
    }

    public static T[] GetAllT<T>(string path) where T : ScriptableObject
    {
        var assets = Resources.LoadAll(path);
        var res = Array.ConvertAll<UnityEngine.Object, T>(assets, (a) =>
        {
            return a as T;
        });
        return res;
    }
}

public static class ScriptableObjetPathDefine
{
    public static string gameData = @"GameData/GameData";
    public static string playerData = @"GameData/CharacterData/PlayerData";
    public static string levelDataFolder = @"GameData/LevelData";
    public static string enemyDataFolder = @"GameData/CharacterData/Enemy/";
    public static string skillDataFolder = @"GameData/SkillData";
    public static string battleConfig = @"GameData/BattleConfig/BattleConfig";
    public static string sceneDataFolder = @"GameData/SceneData";
    public static string buffDataFloder = @"GameData/BuffData";
}