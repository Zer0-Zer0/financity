using UnityEngine;

public class SunManager : MonoBehaviour
{
    [SerializeField] TimeManager timeManager;
    public Light directionalLight;
    public Gradient lightColor;
    public AnimationCurve lightIntensity;
    void Update()
    {
        float time = timeManager.time;
        
        float _sunAngle = time * 360f - 90f;
        directionalLight.transform.rotation = Quaternion.Euler(_sunAngle, 170f, 0f);
        directionalLight.color = lightColor.Evaluate(time);
        directionalLight.intensity = lightIntensity.Evaluate(time);

        RenderSettings.ambientLight = lightColor.Evaluate(time) * 0.5f;
        RenderSettings.ambientIntensity = lightIntensity.Evaluate(time) * 0.5f;
    }
}