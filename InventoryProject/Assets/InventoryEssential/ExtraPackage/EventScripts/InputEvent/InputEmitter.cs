using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEmitter : MonoBehaviour
{
    [SerializeField]
    private List<InputKeyPair> inputKeyPairs = null;

    private static float multipleClickDelay = 0.5f;
    private static float previousClickTime;

    void Update()
    {
        for (int i = 0; i < inputKeyPairs.Capacity; i++)
        {
            if (Input.GetKeyDown(inputKeyPairs[i].keyCode))
                inputKeyPairs[i].inputEvent.Raise();
        }
    }

    public static int MultipleMouseClick(int previousClicked)
    {
        int clicked = previousClicked;
        if (clicked == 0)
        {
            clicked++;
            previousClickTime = Time.time;
        }
        else if (clicked >= 1 && Time.time - previousClickTime < multipleClickDelay)
        {
            clicked++;
            previousClickTime = Time.time;
        }
        else if (Time.time - previousClickTime >= multipleClickDelay)
        {
            clicked = 1;
            previousClickTime = Time.time;
        }

        return clicked;
    }


}
