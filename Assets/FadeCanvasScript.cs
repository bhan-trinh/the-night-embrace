using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvasScript : MonoBehaviour
{

    private float fadeSpeed = 1.0f; // Speed of the fade effect

    private CanvasGroup fadeCanvasGroup;

    private float alpha = 1.0f;

    public bool fadeUIout;
    public bool fadeUIin;

    // Start is called before the first frame update
    void Start()
    {
        fadeCanvasGroup = GetComponent<CanvasGroup>();
        if (fadeCanvasGroup == null)
        {

        }
        if (fadeCanvasGroup != null)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha > 0.0f && fadeUIout)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            fadeCanvasGroup.alpha = alpha;
        }

        else if(alpha <= 0.0f && fadeUIout)
        {
            fadeUIout = false;
        }

        if (alpha < 1f && fadeUIin)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeCanvasGroup.alpha = alpha;
        }

        else if (alpha >= 1f && fadeUIin)
        {
            fadeUIin = false;
        }
    }
}
