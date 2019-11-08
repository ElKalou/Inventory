using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace kUI
{
    public class SetTextUI : MonoBehaviour
    {
        [Header("Text component in children")]
        [SerializeField] TextMeshProUGUI text;

        private void Start()
        {
            if (text == null)
                text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetText(string newText)
        {
            text.text = newText;
        }

    }
}