using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEvent<T> : ScriptableObject
{
    //List of all event listeners
    private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();

    public void Raise(T item)
    {
        //Loop through all event listeners
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(item);
        }
    }

    public void RegisterListener(IGameEventListener<T> listener)
    {
        //If the listener is not in the list
        if (!eventListeners.Contains(listener))
        {
            //Add the listener to the list
            eventListeners.Add(listener);
        }
    }

    public void UnRegisterListener(IGameEventListener<T> listener)
    {
        //If the listener is not in the list
        if (eventListeners.Contains(listener))
        {
            //Remove the listener from the list
            eventListeners.Remove(listener);
        }
    }
}
