using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DisableBloomAndFocusCamera : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public Transform focusTransform;

    private DepthOfField depthOfFieldLayer;

    void Start()
    {
        if (postProcessVolume == null)
        {
            Debug.LogWarning("O post não foi definido");
            return;
        }

        postProcessVolume.profile.TryGetSettings(out depthOfFieldLayer);
    }

    public void DisableBloomAndFocus() // chama essa def quando quiser desativar o depth
    {

        if (focusTransform != null && depthOfFieldLayer != null)
        {
            depthOfFieldLayer.focusDistance.value = Vector3.Distance(focusTransform.position, Camera.main.transform.position); // isso aqui dá +/- 19 
        }
    }
}
