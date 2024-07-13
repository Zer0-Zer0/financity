using UnityEngine;
using UnityEngine.UI;

public class FadeInOutAndDestroy : MonoBehaviour
{
    public float fadeSpeed = 0.5f;
    public float delayBeforeFadeOut = 2f;
    public RawImage rawImage1;
    public RawImage rawImage2;
    private float currentAlpha = 0f;
    private bool fadeInComplete = false;
    private float fadeOutSpeed;

    void Start()
    {

        rawImage2.color = new Color(rawImage2.color.r, rawImage2.color.g, rawImage2.color.b, 0f);
    }

    void FixedUpdate()
    {
        if (!fadeInComplete)
        {

            currentAlpha += fadeSpeed * Time.deltaTime;
            rawImage2.color = new Color(rawImage2.color.r, rawImage2.color.g, rawImage2.color.b, currentAlpha);

            if (currentAlpha >= 1f)
            {
                fadeInComplete = true;
                Invoke("StartFadeOut", delayBeforeFadeOut);
            }
        }
    }

    void StartFadeOut()
    {

        fadeOutSpeed = fadeSpeed / currentAlpha;
        InvokeRepeating("FadeOut", 0f, 0.05f);
    }

    void FadeOut()
    {

        currentAlpha -= fadeOutSpeed * Time.deltaTime * 20;
        rawImage1.color = new Color(rawImage1.color.r, rawImage1.color.g, rawImage1.color.b, currentAlpha);
        rawImage2.color = new Color(rawImage2.color.r, rawImage2.color.g, rawImage2.color.b, currentAlpha);

        if (currentAlpha <= 0f)
        {
            Destroy(rawImage1.gameObject);
            Destroy(rawImage2.gameObject);
        }
    }
}
