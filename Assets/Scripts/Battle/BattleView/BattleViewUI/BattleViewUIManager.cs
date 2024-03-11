using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleViewUIManager
{
    private DamageNum damageObj;
    private BattleViewManager battleViewManager;
    private BattleViewGuiPage page;

    public BattleViewUIManager(BattleViewManager battleViewManager)
    {
        this.battleViewManager = battleViewManager;
        Init();
    }

    public void Init()
    {
        damageObj = Resources.Load<DamageNum>("Other/Damage");
    }
    public void CreateDamageNum(Transform tar, int num)
    {
        var samageRoot = SceneNodeManager.Instance.DamageRoot;
        var damangeNum = GameObject.Instantiate(damageObj, samageRoot);//放在统一节点下对渲染更友好，为了方便就先这样
        var text = num > 0 ? "+" + num : num.ToString();
        var color = num > 0 ? Color.green : Color.red;
        damangeNum.SetText(text, color, tar);
    }

    internal void OnPlayerSkillChangeState(int skillId, BattleLogicUnitSkillState state)
    {
        
    }

    internal void OnLeaveBattleLogicLoadState(List<int> enemies)
    {
        if (null == page)
        {
            UIManager.Instance.OpenPage(PageDefine.BattlePage);
            page = UIManager.Instance.GetPage(PageDefine.BattlePage) as BattleViewGuiPage;

            var skills = BattleDataRunTimeHelper.GetLogicPlayer().skillManager.skills;
            List<SkillData> skillsData = new List<SkillData>();
            foreach (var skill in skills)
            {
                skillsData.Add(skill.data);
            }
            var normalAtkData = BattleDataRunTimeHelper.GetLogicPlayer().skillManager.normalAtk.data;
            page.Init(skillsData, normalAtkData);
        }
    }

    internal void OnEnterBattleLogicLoadState(List<int> enemies)
    {

    }

    internal void OnEnterBattleLogicExitState()
    {
        if(page != null)
        {
            UIManager.Instance.ClosePage(page);
            page = null;
        }
    }
}
