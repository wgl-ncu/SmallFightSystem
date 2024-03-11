using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogLayer
{
    Common = 1 << 0,
    Battle = 1 << 1,
    UI     = 1 << 2
}
public static class Logger
{
    public static bool enable = true;
    public static int logLayer = (int)LogLayer.Common | (int)LogLayer.Battle | (int)LogLayer.UI;

    public static void Log(object message, Object context = null, LogLayer layer = LogLayer.Common)
    {
        if (enable && ((logLayer & (int)layer) != 0))
        {
            Debug.Log(message, context);
        }
    }

    public static void Warn(object message, Object context = null, LogLayer layer = LogLayer.Common)
    {
        if (enable && ((logLayer & (int)layer) != 0))
        {
            Debug.LogWarning(message, context);
        }
    }

    public static void Error(object message, Object context = null, LogLayer layer = LogLayer.Common)
    {
        if (enable && ((logLayer & (int)layer) != 0))
        {
            Debug.LogError(message, context);
        }
    }
}
