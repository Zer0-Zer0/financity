using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class introcutscene : MonoBehaviour
{

    public RawImage cutsceneview;

    public CharacterDialogue dublagem;

    public CameraController mouse;

    void Start()
    {
        StartCoroutine(TransicaoCamera());
    }

    IEnumerator TransicaoCamera()
    {

        yield return new WaitForSeconds(7.5f);

        float elapsedTime = 0f;
        Color corInicial = cutsceneview.color;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime / 2.5f;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime);
            cutsceneview.color = new Color(corInicial.r, corInicial.g, corInicial.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(cutsceneview.gameObject);
        mouse.ativo = true;
        dublagem.Speak(13);
    }
}
