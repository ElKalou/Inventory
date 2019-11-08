using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerDefault : InputEventController
{

    private void Awake()
    {
        if (inputControllerManager == null)
            inputControllerManager = new InputControllerManager();
        inputControllerManager.RegisterAsDefaultController(this);
    }

    public override void ReleaseControl()
    {
        
    }

    public override void TakeControl()
    {
        
    }
}
