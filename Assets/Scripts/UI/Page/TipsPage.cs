using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPage : Page
{
    public Text text;

    public override void Open(object param = null)
    {
        base.Open(param);
        if(param is string msg)
        {
            text.text = msg;
        }
    }
}
