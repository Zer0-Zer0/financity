using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CubeVisionBlur : MonoBehaviour
{
    public Transform centerCube;
    public Image screenImage;

    public PlayerMovement move;

    public GameObject mapa;
    public Camera mapacam;

    public FadeController transição;

    public RectTransform player;

    public AudioClip cidadesound;

    public AudioSource tocasom;

    private bool isInsideCube;

    void Update()
    {
        if (centerCube != null)
        {
            isInsideCube = centerCube.GetComponent<Collider>().bounds.Contains(transform.position);

            if (isInsideCube)
            {
                float distanceToCenter = Vector3.Distance(transform.position, centerCube.position);
                float normalizedDistance = 1 - Mathf.SmoothStep(0, 1, distanceToCenter - 0.5f);

                Color imageColor = screenImage.color;
                imageColor.a = normalizedDistance;
                screenImage.color = imageColor;

                screenImage.gameObject.SetActive(true);

                if (imageColor.a > 0.95f)
                {
                    move.move = false;
                    mapa.SetActive(true);
                    mapacam.gameObject.SetActive(true);
                    transição.StartFadeIn(0f);
                    player.position = new Vector3(6331.96f, 1.13f, -1892.36f);
                    transição.StartCoroutine(DelayedFadeOut(4f, 3f));
                    tocasom.clip = cidadesound;
                    tocasom.Play();
                }
            }
            else
            {

                screenImage.gameObject.SetActive(false);
            }
        }
    }

        private IEnumerator DelayedFadeOut(float delay, float duration)
    {
        yield return new WaitForSeconds(delay);
        transição.StartFadeOut(duration);
    }
}
