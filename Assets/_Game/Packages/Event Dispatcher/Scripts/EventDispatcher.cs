using System;
using System.Collections.Generic;

public class EventDispatcher : SingletonMonoBehaviour<EventDispatcher>
{
    private Dictionary<EventId, Action<object>> _listeners = new();

    public void RegisterListener(EventId eventId, Action<object> callback)
    {
        if (!_listeners.ContainsKey(eventId))
        {
            _listeners.Add(eventId, null);
        }

        _listeners[eventId] += callback;
    }

    public void PostEvent(EventId eventId, object param = null)
    {
        if (!_listeners.ContainsKey(eventId))
        {
            return;
        }

        var callback = _listeners[eventId];

        if (callback != null)
        {
            callback(param);
        }
        else
        {
            _listeners.Remove(eventId);
        }
    }

    public void RemoveListener(EventId eventId, Action<object> callback)
    {
        if (_listeners.ContainsKey(eventId))
        {
            _listeners[eventId] -= callback;
        }
    }

    public void ClearAllListener()
    {
        _listeners.Clear();
    }
}

//public static class EventDispatcherExtension
//{
//    public 
//}