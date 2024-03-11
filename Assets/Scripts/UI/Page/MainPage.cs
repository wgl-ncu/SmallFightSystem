using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPage : Page
{
    public void OnClickStartGame()
    {
        EventManager.TriggerEvent(CommonEvent.ON_BATTLE_START);
    }
}
