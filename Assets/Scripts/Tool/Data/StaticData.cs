using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    public static GameData_SO gameData;
    public static CharacterData_SO playerData;

    public static void Init()
    {
        gameData = ScriptableObjectHelper.GetGameData();
        playerData = ScriptableObjectHelper.GetPlayerData();
    }

    public static void Uninit()
    {
        gameData = null;
        playerData = null;
    }
}
