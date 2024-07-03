using UnityEngine;

public class SunManager : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private Light directionalLight;
    [SerializeField] private Gradient lightColor;
    [SerializeField] private AnimationCurve lightIntensity;

    private const float ANGLE_OFFSET = -90f;
    private const float AMBIENT_MULTIPLIER = 0.5f;

    private void Update()
    {
        float time = (float)timeManager.time / (float)TimeManager.MINUTES_IN_A_DAY;
        
        float sunAngle = time * 360f + ANGLE_OFFSET;
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);
        directionalLight.color = lightColor.Evaluate(time);
        directionalLight.intensity = lightIntensity.Evaluate(time);

        RenderSettings.ambientLight = lightColor.Evaluate(time) * AMBIENT_MULTIPLIER;
        RenderSettings.ambientIntensity = lightIntensity.Evaluate(time) * AMBIENT_MULTIPLIER;
    }
}
