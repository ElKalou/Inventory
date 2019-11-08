using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using kUI;

[RequireComponent(typeof(InputEventListener))]
public class Interactable : MonoBehaviour
{
    [Header("InputEventListener to control")]
    [SerializeField] private InputEventListener listener = null;
    [Header("UI prefab")]
    [SerializeField] private Canvas shineCanvasPrefab = null;

    private Canvas shineCanvas;

    private void Start()
    {
        if (listener != null)
            listener.enabled = false;
        shineCanvas = Instantiate(shineCanvasPrefab, transform);
        shineCanvas.GetComponentInChildren<SetTextUI>().SetText(gameObject.name);
        shineCanvas.gameObject.SetActive(false);
    }

    public void Shine()
    {
        shineCanvas.gameObject.SetActive(true);

        if (listener == null)
            return;

        listener.enabled = true;
    }

    public void StopShine()
    {
        shineCanvas.gameObject.SetActive(false);

        if (listener == null)
            return;

        listener.enabled = false;
    }

}
