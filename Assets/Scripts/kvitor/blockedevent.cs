using UnityEngine;
using System.Collections;
using TMPro;

public class BlinkTextOnProximity : MonoBehaviour
{
    public Transform targetObject;
    public float proximityDistance = 5f;
    public TextMeshProUGUI textMeshPro;
    public float blinkSpeed = 1f;

    private bool isBlinking = false;
    private Color originalColor;

    void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro não foi atribuído!");
            return;
        }

        originalColor = textMeshPro.color;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetObject.position);

        if (distance <= proximityDistance && !isBlinking)
        {
            StartCoroutine(BlinkText());
        }
        else if (distance > proximityDistance && isBlinking)
        {
            StopCoroutine(BlinkText());
            textMeshPro.color = originalColor;
            isBlinking = false;
        }
    }

    IEnumerator BlinkText()
    {
        isBlinking = true;
        while (true)
        {
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, Mathf.PingPong(Time.time * blinkSpeed, 1));
            yield return null;
        }
    }
}
