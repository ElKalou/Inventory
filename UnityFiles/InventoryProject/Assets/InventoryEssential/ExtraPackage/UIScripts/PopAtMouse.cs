using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kUI
{
    public class PopAtMouse : MonoBehaviour
    {
        [SerializeField]
        private Vector3 absOffset = new Vector3(50, 50, 0);
        [SerializeField]
        private bool followMouse = false;

        private Vector3 offset;
        private Vector2 imageDim;

        private void Start()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                imageDim.x = 0;
                imageDim.y = 0;
            }
            else
            {
                imageDim.x = rectTransform.rect.width;
                imageDim.y = rectTransform.rect.height;
            }

            absOffset = new Vector3(
                Mathf.Abs(absOffset.x) + imageDim.x / 2,
                Mathf.Abs(absOffset.y) + imageDim.y / 2,
                0);

        }

        private void OnEnable()
        {
            offset = PosMouseQuarterScreen();
            transform.position = Input.mousePosition + offset;
        }

        private void Update()
        {
            if (!followMouse)
                return;

            offset = PosMouseQuarterScreen();
            transform.position = Input.mousePosition + offset;
        }

        private Vector3 PosMouseQuarterScreen()
        {
            Vector3 toReturn = absOffset;
            Vector3 mousePos = Input.mousePosition;

            if (mousePos.x > Screen.width / 2)
                toReturn.x *= -1;
            if (mousePos.y > Screen.height / 2)
                toReturn.y *= -1;

            return toReturn;
        }
    }
}
