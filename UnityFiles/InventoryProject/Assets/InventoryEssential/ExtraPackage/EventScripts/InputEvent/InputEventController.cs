using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputEventController : MonoBehaviour
{
    internal static InputControllerManager inputControllerManager;

    public bool isMaster { get; set; }

    public abstract void TakeControl();
    public abstract void ReleaseControl();
}
