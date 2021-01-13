using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitelFade : MonoBehaviour
{
    private void Update () {
        if (Input.GetButton("Accel")) {
            StartCoroutine(FadeTo(0.0f, 1.0f));
        }
    }

    // fading out coroutine
    IEnumerator FadeTo (float aValue, float aTime) {
        float alpha = GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }
}
