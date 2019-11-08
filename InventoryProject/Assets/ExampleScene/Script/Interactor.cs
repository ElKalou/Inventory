using kInventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class Interactor : MonoBehaviour
{
    [Header("InputEventListener to manage")]
    [SerializeField] private InputEventListener listener;

    private Interactable previousInteractable;
    private Interactable newInteractable;

    private Inventory attachedInventory;

    private void Awake()
    {
        if(listener == null)
        {
            Debug.Log("Missing InputEventListener reference in interactor of " + gameObject.name + ". Trying to find component.");
            listener = GetComponent<InputEventListener>();
        }
        listener.enabled = false;

        attachedInventory = GetComponent<ItemContainer>().inventory;
    }


    private void Update()
    {
        if (attachedInventory.isOpen)
        {
            if(previousInteractable != null)
                previousInteractable.StopShine();
            newInteractable = null;
            listener.enabled = false;
            previousInteractable = newInteractable;
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction);
        if(Physics.Raycast(ray, out hit))
        {
            newInteractable = hit.transform.GetComponent<Interactable>();
            if(newInteractable == null && previousInteractable == null)
            {

            }
            else if (newInteractable == previousInteractable)
            {

            }
            else if (newInteractable != null && previousInteractable == null)
            {
                newInteractable.Shine();
                listener.enabled = true;
            }
            else if (newInteractable == null && previousInteractable != null)
            {
                previousInteractable.StopShine();
                listener.enabled = false;
            }
        }
        else
        {
            if (previousInteractable != null)
            {
                previousInteractable.StopShine();
                listener.enabled = false;
            }

            newInteractable = null;
        }

        previousInteractable = newInteractable;
    }

  
}
