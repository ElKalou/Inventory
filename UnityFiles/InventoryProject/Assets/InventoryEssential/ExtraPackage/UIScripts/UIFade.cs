using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace kUI
{
    public class UIFade : MonoBehaviour
    {
        public float fadeTime;

        private Image[] images;
        private TextMeshProUGUI[] texts;

        private void Awake()
        {
            images = GetComponentsInChildren<Image>();
            texts = GetComponentsInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            SetAlphas(0);
            StartCoroutine(DelayUI());
        }

        private void OnDisable()
        {
            StopCoroutine(DelayUI());
            SetAlphas(1);
        }

        private IEnumerator DelayUI()
        {
            float t = 0;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                SetAlphas(t / fadeTime);
                yield return null;
            }
        }

        private void SetAlphas(float alpha)
        {
            foreach (var item in texts)
            {
                item.color = SetAlpha(item.color, alpha);
            }
            foreach (var item in images)
            {
                item.color = SetAlpha(item.color, alpha);
            }
        }

        private Color SetAlpha(Color _color, float _alpha)
        {
            _color = new Color(_color.r, _color.g, _color.b, _alpha);
            return _color;
        }
    }
}