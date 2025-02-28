using UnityEngine;
using System.Collections.Generic;

public class AnimationEventDispatcher : MonoBehaviour
{
    private Dictionary<string, List<System.Action>> eventCallbacks =
        new Dictionary<string, List<System.Action>>();

    // Called from animation events
    public void OnAnimationEvent(string eventName)
    {
        if (eventCallbacks.TryGetValue(eventName, out var callbacks))
        {
            foreach (var callback in callbacks)
            {
                callback();
            }
        }
    }

    public void RegisterCallback(string eventName, System.Action callback)
    {
        // Create list if it doesn't exist
        if (!eventCallbacks.TryGetValue(eventName, out var callbacks))
        {
            callbacks = new List<System.Action>();
            eventCallbacks[eventName] = callbacks;
        }

        // Add callback if not already in the list
        if (!callbacks.Contains(callback))
        {
            callbacks.Add(callback);
        }
    }

    public void UnregisterCallback(string eventName, System.Action callback)
    {
        // If the event exists
        if (eventCallbacks.TryGetValue(eventName, out var callbacks))
        {
            // Remove the callback
            callbacks.Remove(callback);

            // If no callbacks remain, remove the event entry
            if (callbacks.Count == 0)
            {
                eventCallbacks.Remove(eventName);
            }
        }
    }
}