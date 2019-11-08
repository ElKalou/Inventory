using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerInventory : InputEventController
{

    public override void ReleaseControl()
    {
        inputControllerManager.ControllerReleaseMaster(this);
    }

    public override void TakeControl()
    {
        StartCoroutine(TakingControl());
    }

    private IEnumerator TakingControl()
    {
        yield return StartCoroutine(GameEventListener.WaitForPossibleEvent(1));
        StopAllCoroutines();
        inputControllerManager.NewControllerIsMaster(this);
    }
}
