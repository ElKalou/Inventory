using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClosePanel : MonoBehaviour
{
    [SerializeField] private GameEvent closeInventory = null;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        eventSystem = GetComponent<EventSystem>();
    }

    public void OnInteractKey()
    {
        closeInventory.Raise();
    }

    public void OnClick()
    {
        pointerEventData = new PointerEventData(eventSystem);
        //Set the Pointer Event Position to that of the mouse position
        pointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        graphicRaycaster.Raycast(pointerEventData, results);

        if (results.Count == 0)
        {
            //closeInventory.Raise();
        }
    }
}
