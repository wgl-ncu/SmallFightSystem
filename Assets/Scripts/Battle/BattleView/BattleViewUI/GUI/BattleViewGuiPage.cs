using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleViewGuiPage : Page
{
    public SkillBtnItem normalAtk;
    public List<SkillBtnItem> btns;
    public override void Open(object param = null)
    {
        base.Open(param);
    }

    public void Init(List<SkillData> skillsData, SkillData normalAtk)
    {
        int i = 0;
        for (; i < skillsData.Count && i < btns.Count; ++i)
        {
            btns[i].Init(skillsData[i]);
        }
        while(i < btns.Count)
        {
            btns[i++].gameObject.SetActive(false);
        }
        this.normalAtk.Init(normalAtk);
        Register();
    }

    public override void OnClose()
    {
        base.OnClose();
        Unregister();
    }

    private void Register()
    {
        EventManager.AddEventListener<int, bool, bool>(CommonEvent.ON_SKILL_USING_OR_CD, OnSkillUsingOrReleasing);
    }

    private void Unregister()
    {
        EventManager.RemoveEventListener<int, bool, bool>(CommonEvent.ON_SKILL_USING_OR_CD, OnSkillUsingOrReleasing);
    }

    private void OnSkillUsingOrReleasing(int skillId, bool isUsing, bool inCd)
    {
        btns.Find(a => a.skillId == skillId)?.ChangeSkillState(isUsing, inCd);
    }
}
