using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class FadeController : MonoBehaviour
{
    private GameObject fadeImageObject;
    private Image fadeImage;
    private Canvas canvas;

    public PlayerMovement movimento;

    void Start()
    {

        fadeImageObject = new GameObject("FadeImage");
        fadeImageObject.transform.SetParent(transform, false);
        
        canvas = fadeImageObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        fadeImageObject.AddComponent<CanvasScaler>();
        fadeImageObject.AddComponent<GraphicRaycaster>().enabled = false;
        
        fadeImage = fadeImageObject.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.raycastTarget = false;
        RectTransform rectTransform = fadeImage.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }

    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeIn(duration));
    }

    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeOut(duration));
    }

    private IEnumerator FadeIn(float duration)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        canvas.sortingOrder = 1000;

        while (elapsedTime < duration)
        {
            color.a = Mathf.Clamp01(elapsedTime / duration);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    private IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        canvas.sortingOrder = 1000;

        while (elapsedTime < duration)
        {
            color.a = Mathf.Clamp01(1 - (elapsedTime / duration));
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
        movimento.move = true;
        movimento.canMove = true;
        canvas.sortingOrder = -1;
        Destroy(fadeImageObject); 
    }
}
