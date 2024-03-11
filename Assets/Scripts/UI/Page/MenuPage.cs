using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : Page
{
    public void OnClickExit()
    {
        EventManager.TriggerEvent(CommonEvent.ON_EXIT_BATTLE);
    }
}
