using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace kInventory
{

    public class MessagePanel : MonoBehaviour, IPanel, IMessageDisplay, IEventDataUser<InventoryEventData>
    {
        [Header("Component in children")]
        [SerializeField]
        private TextMeshProUGUI textMessage = null;
        [Header("Message variable")]
        [SerializeField]
        private float timeToShow = 0;


        public void ReceiveData(InventoryEventData _dataInput)
        {
            StartCoroutine(ShowMessage());
            DisplayMessage(_dataInput.attachedMessage);
        }

        private IEnumerator ShowMessage()
        {
            float t = 0;
            while (t < timeToShow)
            {
                t += Time.deltaTime;
                yield return null;
            }
            ResetPanel();
        }

        public void DisplayMessage(string messageToDisplay)
        {
            textMessage.text = messageToDisplay;
        }

        public void ResetPanel()
        {
            StopCoroutine(ShowMessage());
            gameObject.SetActive(false);
        }
    }
}