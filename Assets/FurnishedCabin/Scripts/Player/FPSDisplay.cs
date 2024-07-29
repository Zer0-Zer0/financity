/*using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float deltaTime = 0.0f;
    private Color textColor = Color.white;

    void Update()
    {

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        float fps = 1.0f / deltaTime;

        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";


        if (fps < 60)
        {
            textColor = new Color(1f, 0f, 0f, 0.5f);
        }
        else if (fps >= 60 && fps <= 100)
        {
            textColor = new Color(1f, 1f, 0f, 0.5f);
        }
        else
        {
            textColor = new Color(0f, 1f, 0f, 0.5f);
        }

        fpsText.color = textColor;
    }
}*/