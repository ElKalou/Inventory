using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
    public void OnEscapeKey()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Application.Quit();
    }
}
