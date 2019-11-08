using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/InputEvent")]
public class InputEvent : EventBase
{
    private List<InputEventListener> listListeners = new List<InputEventListener>();
    public ScriptableObject dataToSend { get; set; }

    public void Register(InputEventListener listenerToRegister)
    {
        listListeners.Add(listenerToRegister);
    }

    public void DeRegister(InputEventListener listenerToRegister)
    {
        listListeners.Remove(listenerToRegister);
    }

    public override void Raise()
    {
        for (int i = listListeners.Count - 1; i >= 0; i--)
        {
            listListeners[i].OnReceiveEvent(this);
        }
    }
}
