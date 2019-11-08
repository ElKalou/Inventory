using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kUI
{
    public class UISlide : MonoBehaviour
    {
        [SerializeField]
        Vector3 relativeStartPos = Vector3.zero;
        [SerializeField]
        float timeToFinish = 0;

        private Vector3 startPos;
        private Vector3 finalPos;

        private void OnEnable()
        {
            finalPos = transform.position;
            startPos = transform.position + relativeStartPos;
            transform.position = startPos;
            StartCoroutine(Sliding());
        }

        private IEnumerator Sliding()
        {
            Vector3 direction = (finalPos - startPos).normalized;
            float distance = (finalPos - startPos).magnitude;
            float velocity = distance / timeToFinish;


            float startTime = Time.time;
            while (Time.time < startTime + timeToFinish)
            {
                transform.position += direction * velocity * Time.deltaTime;
                yield return null;
            }
            transform.position = finalPos;
        }


    }
}