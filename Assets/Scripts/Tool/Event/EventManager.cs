using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    private static Dictionary<int, System.Object> eventDict = new Dictionary<int, object>();

    public static void AddEventListener(int id, Action handler)
    {
        if (!eventDict.ContainsKey(id))
        {
            eventDict.Add(id, handler);
        }
        else
        {
            if(eventDict[id] is Action action)
            {
                action += handler;
                eventDict[id] = action;
            }
        }
    }

    public static void AddEventListener<T>(int id, Action<T> handler)
    {
        if (!eventDict.ContainsKey(id))
        {
            eventDict.Add(id, handler);
        }
        else
        {
            if (eventDict[id] is Action<T> action)
            {
                action += handler;
                eventDict[id] = action;
            }
        }
    }

    public static void AddEventListener<T, U>(int id, Action<T, U> handler)
    {
        if (!eventDict.ContainsKey(id))
        {
            eventDict.Add(id, handler);
        }
        else
        {
            if (eventDict[id] is Action<T, U> action)
            {
                action += handler;
                eventDict[id] = action;
            }
        }
    }

    public static void AddEventListener<T, U, V>(int id, Action<T, U, V> handler)
    {
        if (!eventDict.ContainsKey(id))
        {
            eventDict.Add(id, handler);
        }
        else
        {
            if (eventDict[id] is Action<T, U, V> action)
            {
                action += handler;
                eventDict[id] = action;
            }
        }
    }

    public static void TriggerEvent(int id)
    {
        if (eventDict.TryGetValue(id, out var e))
        {
            if(e is Action action)
            {
                action.Invoke();
            }
        }
    }

    public static void TriggerEvent<U>(int id, U param)
    {
        if (eventDict.TryGetValue(id, out var e))
        {
            if (e is Action<U> action)
            {
                action.Invoke(param);
            }
        }
    }

    public static void TriggerEvent<U, V>(int id, U param1, V param2)
    {
        if (eventDict.TryGetValue(id, out var e))
        {
            if (e is Action<U, V> action)
            {
                action.Invoke(param1, param2);
            }
        }
    }

    public static void TriggerEvent<T, U, V>(int id, T param1, U param2, V param3)
    {
        if (eventDict.TryGetValue(id, out var e))
        {
            if (e is Action<T, U, V> action)
            {
                action.Invoke(param1, param2, param3);
            }
        }
    }

    public static void RemoveEventListener(int id, Action handler)
    {
        if(eventDict.TryGetValue(id, out var e))
        {
            if(e is Action action)
            {
                action -= handler;
                eventDict[id] = action;
            }
        }
    }

    public static void RemoveEventListener<T>(int id, Action<T> handler)
    {
        if (eventDict.TryGetValue(id, out var e))
        {
            if (e is Action<T> action)
            {
                action -= handler;
                eventDict[id] = action;
            }
        }
    }

    public static void RemoveEventListener<T, U>(int id, Action<T, U> handler)
    {
        if (eventDict.TryGetValue(id, out var e))
        {
            if (e is Action<T, U> action)
            {
                action -= handler;
                eventDict[id] = action;
            }
        }
    }

    public static void RemoveEventListener<T, U, V>(int id, Action<T, U, V> handler)
    {
        if (eventDict.TryGetValue(id, out var e))
        {
            if (e is Action<T, U, V> action)
            {
                action -= handler;
                eventDict[id] = action;
            }
        }
    }
}
