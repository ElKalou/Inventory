using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kUI
{

    public class HelperUI
    {
        public static void SetAnchorAndPivot(RectTransform rectTransformToSet)
        {
            // set anchor and pivot to the top-left of the rectTransformToSet
            rectTransformToSet.pivot = new Vector2(0, 1);
            rectTransformToSet.anchorMax = new Vector2(0, 1);
            rectTransformToSet.anchorMin = new Vector2(0, 1);
        }
    }
}

