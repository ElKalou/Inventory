using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEventDataUser<T> where T : ScriptableObject
{
    void ReceiveData(T _dataInput);
}



