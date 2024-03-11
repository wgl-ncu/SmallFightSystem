using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitRelativePos
{
    UP = 1 << 0,
    Bottom = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3
}
public static class BattleDataRunTimeHelper
{
    private static BattleLogicManager _battleManager;
    public static void Init(BattleLogicManager battleManager)
    {
        _battleManager = battleManager;
    }
    public static BattleLogicUnitBase GetLogicUnit(int id)
    {
        return _battleManager.GetLogicUnit(id);
    }

    public static BattleLogicUnitBase GetLogicPlayer()
    {
        return _battleManager.GetPlayer();
    }

    public static BattleConfig_SO GetBattleConfig()
    {
        return _battleManager.battleConfig;
    }

    public static BattleLogicManager GetBattleLogicManager()
    {
        return _battleManager;
    }

    public static BattleLogicState GetCurBattleLogicState()
    {
        return _battleManager.GetCurState();
    }

    public static BattleLogicUnitBase GetNearestEnemy(int id)
    {

        int enemyId = _battleManager.GetNearestEnemy(id);
        if(id == -1)
        {
            return null;
        }
        return GetLogicUnit(enemyId);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <param name="relativePos">unit2相对于unit1的位置</param>
    /// <returns></returns>
    public static float GetUnitsDis(int id1, int id2, out int relativePos, out float xDis)
    {
        var unit1 = GetLogicUnit(id1);
        var unit2 = GetLogicUnit(id2);
        var pos1 = unit1.propertyCtrl.curProperties.POS.value;
        var pos2 = unit2.propertyCtrl.curProperties.POS.value;
        xDis = Mathf.Abs(pos1.x - pos2.x);
        relativePos = 0;
        if (pos1.x < pos2.x)
        {
            relativePos |= (int)UnitRelativePos.Right;
        }
        else if(pos1.x > pos2.x)
        {
            relativePos |= (int)UnitRelativePos.Left;
        }
        if(pos1.y < pos2.y)
        {
            relativePos |= (int)UnitRelativePos.UP;
        }
        else if(pos1.y > pos2.y)
        {
            relativePos |= (int)UnitRelativePos.Bottom;
        }
        if(unit1 != null && unit2 != null)
        {
            return Vector3.SqrMagnitude(unit1.propertyCtrl.curProperties.POS.value - unit2.propertyCtrl.curProperties.POS.value);
        }
        else
        {
            return -1;
        }
    }

    public static List<int> GetSkillReleaseTargets(int srcUnitId, SkillReleasePair pair, bool needSort = false)
    {
        List<int> res = new List<int>();
        var unit = GetLogicUnit(srcUnitId);
        if(pair.releaseData.selectWay == SkillReleaseTargetSelect.Self)
        {
            res.Add(srcUnitId);
        }
        else if (unit.unitCamp == UnitCamp.Enemy)
        {
            Vector3 playerPos = GetLogicPlayer().propertyCtrl.curProperties.POS.value;
            var unitPos = unit.propertyCtrl.curProperties.POS.value;
            var dis = Vector3.SqrMagnitude(playerPos - unitPos);
            if (pair.releaseData.range >= dis)
            {
                res.Add(GetLogicPlayer().id);
            }
        }
        else
        {
            var enemies = _battleManager.GetEnemiesInRange(pair.releaseData.range, needSort);
            var selectWay = pair.releaseData.selectWay;
            var selectNum = pair.releaseData.tarNum < 0 ? int.MaxValue : pair.releaseData.tarNum;
            switch (selectWay)
            {
                case SkillReleaseTargetSelect.Nearest:
                    for(int i = 0; i < selectNum && i < enemies.Count; ++i)
                    {
                        res.Add(enemies[i]);
                    }
                    break;
                case SkillReleaseTargetSelect.Farthest:
                    enemies.Reverse();
                    for (int i = 0; i < selectNum && i < enemies.Count; ++i)
                    {
                        res.Add(enemies[i]);
                    }
                    break;
            }
        }
        return res;
    }

    public static List<int> GetSkillReleaseScopeTargets(int srcUnitId, SkillReleasePair pair)
    {
        List<int> res = new List<int>();
        var unit = GetLogicUnit(srcUnitId);
        if (pair.releaseData.selectWay == SkillReleaseTargetSelect.Self)
        {
            res.Add(srcUnitId);
        }
        else if (unit.unitCamp == UnitCamp.Enemy)
        {
            var playerPos = GetLogicPlayer().propertyCtrl.curProperties.POS.value;
            var pos = pair.pos;
            var dis = Vector3.SqrMagnitude(playerPos - pos);
            if (pair.releaseData.scope >= dis)
            {
                res.Add(GetLogicPlayer().id);
            }
        }
        else
        {
            var enemies = _battleManager.GetTargetsInRange(pair.pos, pair.releaseData.scope);
            var selectWay = pair.releaseData.selectWay;
            var selectNum = pair.releaseData.tarNum < 0 ? int.MaxValue : pair.releaseData.tarNum;
            switch (selectWay)
            {
                case SkillReleaseTargetSelect.Nearest:
                    for (int i = 0; i < selectNum && i < enemies.Count; ++i)
                    {
                        res.Add(enemies[i]);
                    }
                    break;
                case SkillReleaseTargetSelect.Farthest:
                    enemies.Reverse();
                    for (int i = 0; i < selectNum && i < enemies.Count; ++i)
                    {
                        res.Add(enemies[i]);
                    }
                    break;
            }
        }
        return res;
    }

    public static Vector3 AdjustJumpV3(Vector3 curPos, Vector3 jumpHeight)
    {
        var targetPos = curPos + jumpHeight;
        var nearHeight = GetNearestBottomGroundHeight(curPos);
        targetPos.y = Mathf.Max(nearHeight, targetPos.y);
        jumpHeight.y = targetPos.y - curPos.y;
        return jumpHeight;
    }

    public static float GetNearestBottomGroundHeight(Vector3 pos)
    {
        return _battleManager.GetNearestBottomGroundHeight(pos);
    }

    public static bool IsOnGround(Vector3 curPos)
    {
        return _battleManager.IsOnGround(curPos);
    }

    public static bool TryGetStandGround(Vector3 pos, out SceneGround ground)
    {
        return _battleManager.TryGetStandGround(pos, out ground);
    }

    public static SceneData_SO GetCurSceneData()
    {
        return _battleManager.GetCurSceneData();
    }

    public static List<int> GetUnitsInArea(Vector3 pos, float range, int targetNum, bool dispatchPlayer = false, bool dispatchEnemy = false)
    {
        return _battleManager.GetUnitsInArea(pos, range, targetNum, dispatchPlayer, dispatchEnemy);
    }
}
