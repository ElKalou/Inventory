using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace kUI
{
    public class UIDelay : MonoBehaviour
    {
        public float delay;

        private Image[] images;
        private TextMeshProUGUI[] texts;

        private void Awake()
        {
            images = GetComponentsInChildren<Image>();
            texts = GetComponentsInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            SetImages(false);
            SetTexts(false);
            StartCoroutine(DelayUI());
        }

        private void OnDisable()
        {
            StopCoroutine(DelayUI());
            SetImages(true);
            SetTexts(true);
        }

        private IEnumerator DelayUI()
        {
            float t = 0;
            while (t < delay)
            {
                t += Time.deltaTime;
                yield return null;
            }
            SetImages(true);
            SetTexts(true);
        }

        private void SetTexts(bool toSet)
        {
            foreach (var item in texts)
            {
                item.enabled = toSet;
            }
        }

        private void SetImages(bool toSet)
        {
            foreach (var item in images)
            {
                item.enabled = toSet;
            }
        }


    }
}
