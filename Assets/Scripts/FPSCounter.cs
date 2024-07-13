using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsCounterText;
    public TextMeshProUGUI fixedfpscounter;
    private float updateDeltaTime = 0.0f;
    private float fixedUpdateDeltaTime = 0.0f;

    void Update()
    {
        updateDeltaTime += (Time.deltaTime - updateDeltaTime) * 0.1f;
        float updateFPS = 1.0f / updateDeltaTime;

        UpdateFPSCounter(updateFPS);
    }

    void FixedUpdate()
    {
        fixedUpdateDeltaTime += (Time.fixedDeltaTime - fixedUpdateDeltaTime) * 0.1f;

        if (fixedfpscounter != null)
        {
            fixedfpscounter.text = $"FIX FPS: {fixedUpdateDeltaTime.ToString("F4")}" + "ms";
        }

        float fixedUpdateFPS = 1.0f / fixedUpdateDeltaTime;

        UpdateFPSCounter(fixedUpdateFPS);
    }

    void UpdateFPSCounter(float fps)
    {
        string fpsText = $"FPS: {Mathf.Ceil(fps)}";

        if (fpsCounterText != null)
        {
            fpsCounterText.text = fpsText;
        }
        else
        {
            Debug.LogError("Você esqueceu de referenciar o TextMeshPro para o contador de FPS!");
        }
    }
}
