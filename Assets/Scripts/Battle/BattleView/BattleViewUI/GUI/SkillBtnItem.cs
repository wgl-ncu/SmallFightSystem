using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtnItem : MonoBehaviour
{
    public Image mask;
    public Image usingImg;
    public Text skillName;
    [HideInInspector] public int skillId;

    private float cd;
    private float passTime;
    private float usingTime;
    private bool inCd;
    private bool isUsing;
    internal void Init(SkillData skillData)
    {
        gameObject.SetActive(true);
        skillId = skillData.data.id;
        mask.fillAmount = 0;
        cd = skillData.data.cd;
        usingTime = skillData.data.time;
        passTime = 0;
        inCd = false;
        skillName.text = skillData.data.skillName;
    }

    private void Update()
    {
        if (inCd)
        {
            passTime += Time.deltaTime;
            mask.fillAmount = Mathf.Min(1 - passTime / cd, 1);
        }
        if (isUsing)
        {
            passTime += Time.deltaTime;
            if(passTime >= usingTime)
            {
                usingImg.gameObject.SetActive(false);
            }
        }
    }

    public void ChangeSkillState(bool isUsing, bool isCd)
    {
        if (isUsing)
        {
            usingImg.gameObject.SetActive(true);
            mask.gameObject.SetActive(false);
            this.isUsing = true;
            inCd = false;
        }
        else if (isCd)
        {
            mask.gameObject.SetActive(true);
            usingImg.gameObject.SetActive(false);
            this.isUsing = false;
            inCd = true;
        }
        else if(!isUsing && !isCd)
        {
            mask.gameObject.SetActive(false);
            usingImg.gameObject.SetActive(false);
            this.isUsing = false;
            inCd = false;
        }
        passTime = 0;
    }

    public void OnClick()
    {
        if (!inCd && !isUsing)
        {
            InputManager.Instance.OnPressSkillBtn(skillId);
        }
    }
}
