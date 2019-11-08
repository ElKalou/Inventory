using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EventDataCatcherBase<Event, EventListener, EventData> : MonoBehaviour
    where Event : EventBase where EventListener : ListenerBase<Event> where EventData : ScriptableObject
{
    [SerializeField]
    private GameObject prefabWithDataUser = null;
    [Header("Transmit data to DataUser events")]
    [SerializeField]
    protected List<Event> transmitDataOn = null;
    [Header("Disable DataUser events")]
    [SerializeField]
    protected List<Event> disableDataUserOn = null;
    [Header("Suspend/Unsuspend events")]
    [SerializeField]
    protected List<Event> suspendOn = null;
    [SerializeField]
    protected List<Event> unsuspendOn = null;



    public bool suspendDataTransmission { get; set; }

    private GameObject instanceWithDataUser;
    private IEventDataUser<EventData> dataUser;

    public void TransmitDataToUser(EventData dataToTransmit)
    {
        if (suspendDataTransmission)
            return;

        instanceWithDataUser.SetActive(true);
        dataUser.ReceiveData(dataToTransmit);
    }

    protected void DisableDataUser(EventData eventData)
    {
        instanceWithDataUser.SetActive(false);
    }

    private void Awake()
    {
        if(prefabWithDataUser != null)
        {
            instanceWithDataUser = Instantiate(prefabWithDataUser, transform);
            instanceWithDataUser.transform.localPosition = prefabWithDataUser.transform.position;
            dataUser = instanceWithDataUser.GetComponent<IEventDataUser<EventData>>();
            if (dataUser == null)
                Debug.LogError("could not find DataUser in prefab of DataCatcher from gameObject " + gameObject.name);
            instanceWithDataUser.SetActive(false);
        }

        InstantiateTransmitDataListeners();
        InstantiateDisableDataUserListeners();
        InstantiateSuspendDataCatcherListeners();
        InstantiateUnsuspendDataCatcherListeners();
    }

    protected void SuspendDataTransmission(EventData eventData)
    {
        suspendDataTransmission = true;
    }

    protected void UnsuspendDataTransmission(EventData eventData)
    {
        suspendDataTransmission = false;
    }

    private void OnDisable()
    {
        suspendDataTransmission = false;
    }

    private void Update()
    {
        if (dataUser == null)
            dataUser = GetComponentInChildren<IEventDataUser<EventData>>();
        if (dataUser == null)
            Debug.LogError("could not find DataUser in children of DataCatcher from gameObject " + gameObject.name);
    }

    protected abstract void InstantiateTransmitDataListeners();
    protected abstract void InstantiateDisableDataUserListeners();
    protected abstract void InstantiateSuspendDataCatcherListeners();
    protected abstract void InstantiateUnsuspendDataCatcherListeners();
}

