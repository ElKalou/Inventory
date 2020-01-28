using System;
using System.Collections;
using System.Collections.Generic;

public class InputControllerManager
{
    private List<InputEventController> defaultControllers;
    private bool defaultControllerIsMaster;

    public InputControllerManager()
    {
        defaultControllers = new List<InputEventController>();
        defaultControllerIsMaster = true;
    }

    public void RegisterAsDefaultController(InputEventController newDefaultController)
    {
        if(!defaultControllers.Contains(newDefaultController))
        { 
            defaultControllers.Add(newDefaultController);
            newDefaultController.isMaster = true;
        }
    }

    public void NewControllerIsMaster(InputEventController newMaster)
    {
        newMaster.isMaster = true;
        if(defaultControllerIsMaster)
            SetDefaultControllersMaster(false);
    }

    public void ControllerReleaseMaster(InputEventController oldMaster)
    {
        oldMaster.isMaster = false;
        if (!defaultControllerIsMaster)
            SetDefaultControllersMaster(true);
    }

    private void SetDefaultControllersMaster(bool b)
    {
        for (int i = 0; i < defaultControllers.Count; i++)
        {
            defaultControllers[i].isMaster = b;
        }

        defaultControllerIsMaster = b;
    }
}
