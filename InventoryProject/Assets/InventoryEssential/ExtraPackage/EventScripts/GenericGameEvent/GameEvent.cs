using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Event/GameEvent")]
public class GameEvent : EventBase
{
    private List<GameEventListener> listListeners = new List<GameEventListener>();
    public ScriptableObject dataToSend { get; set; }

    public void Register(GameEventListener listenerToRegister)
    {
        listListeners.Add(listenerToRegister);
    }

    public void DeRegister(GameEventListener listenerToRegister)
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


