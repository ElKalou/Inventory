using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kInventory
{
    public class InventoryEventDataCatcher : EventDataCatcherBase<InventoryEvent, InventoryEventListener, InventoryEventData>
    {
        protected override void InstantiateDisableDataUserListeners()
        {
            for (int i = 0; i < disableDataUserOn.Count; i++)
            {
                InventoryEventListener.AddComponentAtRunTime(gameObject, disableDataUserOn[i], DisableDataUser);
            }
        }

        protected override void InstantiateSuspendDataCatcherListeners()
        {
            for (int i = 0; i < suspendOn.Count; i++)
            {
                InventoryEventListener.AddComponentAtRunTime(gameObject, suspendOn[i], SuspendDataTransmission);
            }
        }

        protected override void InstantiateTransmitDataListeners()
        {
            for (int i = 0; i < transmitDataOn.Count; i++)
            {
                InventoryEventListener.AddComponentAtRunTime(gameObject, transmitDataOn[i], TransmitDataToUser);
            }
        }

        protected override void InstantiateUnsuspendDataCatcherListeners()
        {
            for (int i = 0; i < unsuspendOn.Count; i++)
            {
                InventoryEventListener.AddComponentAtRunTime(gameObject, unsuspendOn[i], UnsuspendDataTransmission);
            }
        }

    }
}
