using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonUnity<InputManager>
{
    private KeyCode[] keyCodesGetKey = {KeyCode.D, KeyCode.A};
    private KeyCode[] keyCodes = { KeyCode.W };
    private int keyCodeLength;
    public void Init()
    {
        keyCodeLength = keyCodesGetKey.Length;
    }
    private void Update()
    {
        DetectKeyInput();
    }

    private void DetectKeyInput()
    {
        bool hasAnyKeyPress = false;
        for (int i = 0; i < keyCodeLength; ++i)
        {
            var curKey = keyCodesGetKey[i];
            if (Input.GetKey(curKey))
            {
                EventManager.TriggerEvent(CommonEvent.ON_INPUT_KEY, curKey);
                hasAnyKeyPress = true;
                break;
            }
        }
        for(int i = 0; i < keyCodes.Length; ++i)
        {
            var curKey = keyCodes[i];
            if (Input.GetKeyDown(curKey))
            {
                EventManager.TriggerEvent(CommonEvent.ON_PRESS_KEY, curKey);
                hasAnyKeyPress = true;
                break;
            }
        }
        if (!hasAnyKeyPress)
        {
            EventManager.TriggerEvent(CommonEvent.ON_INPUT_NONE_KEY);
        }
    }

    public void OnPressSkillBtn(int skillId)
    {
        EventManager.TriggerEvent(CommonEvent.ON_PRESS_SKILL_BTN, skillId);
    }
}
