using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Game Data", menuName = "DataCreate/Game Data / Data")]
public class GameData_SO : ScriptableObject
{
    public int level;

    public LevelData_SO GetCurLevelData()
    {
        return BattleDataHelper.GetLevelData(level);
    }

    public void IncreaseLevel()
    {
        level = Mathf.Min(level + 1, BattleDataHelper.MaxLevel);
    }

    public bool IsFinalLevel()
    {
        return level == BattleDataHelper.MaxLevel;
    }
}
