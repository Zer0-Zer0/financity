using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class CutsceneUm : MonoBehaviour
{
    public MoveRawImage phone;
    private Vector3 vetor;
    public RawImage video;

    public VideoPlayer cut;
    public CharacterDialogue dublagem;

    private bool assistido = false;
    private bool opcaoEscolhida = false;

    public bool objetivo = false;

    private DepthOfField depthOfFieldLayer; 

    public PostProcessVolume postProcessVolume;

    public GameObject panelPergunta;
    public TextMeshProUGUI textoPergunta;
    public Button opcao1;
    public Button opcao2;
    public Button opcao3;

    public ObjectiveSystem objetivosistem;

    public AnimatedText textocentral;

    void Start()
    {
        video.gameObject.SetActive(false);
        postProcessVolume.profile.TryGetSettings(out depthOfFieldLayer);

        panelPergunta.SetActive(false);
    }

    public void iniciarcut()
    {
        if (!assistido && objetivo)
        {
            phone.OpenCellPhone(vetor);
            video.gameObject.SetActive(true);
            cut.Play();

            cut.loopPointReached += VideoEnded;
        } 
        else 
        {
            dublagem.Speak(5);
        }
    }

    void VideoEnded(VideoPlayer vp)
    {
        assistido = true;
        cut.loopPointReached -= VideoEnded;
        SetFocusDistance(0f);
        MostrarPergunta();
    }

    void SetFocusDistance(float distance)
    {
        depthOfFieldLayer.focusDistance.value = distance;
    }

    void MostrarPergunta()
    {
        panelPergunta.SetActive(true);

        textoPergunta.text = "Escolha:";

        opcao1.onClick.AddListener(Opcao1Escolhida);
        opcao2.onClick.AddListener(Opcao2Escolhida);
        opcao3.onClick.AddListener(Opcao3Escolhida);
    }

    void Opcao1Escolhida()
    {
        if (!opcaoEscolhida)
        {
            opcaoEscolhida = true;
            RemoverListeners();
            panelPergunta.SetActive(false);

            StartCoroutine(ExecutarDialogoOpcao1());
        }
        else
        {
            return;
        }
    }

    void Opcao2Escolhida()
    {
        if (!opcaoEscolhida)
        {
            opcaoEscolhida = true;
            RemoverListeners();
            panelPergunta.SetActive(false);

            StartCoroutine(ExecutarDialogoOpcao2());
        }
        else
        {
            return;
        }
    }

    void Opcao3Escolhida()
    {
        if (!opcaoEscolhida)
        {
            opcaoEscolhida = true;
            RemoverListeners();
            panelPergunta.SetActive(false);

            StartCoroutine(ExecutarDialogoOpcao3());
        }
        else
        {
            return;
        }
    }

    void RemoverListeners()
    {
        opcao1.onClick.RemoveAllListeners();
        opcao2.onClick.RemoveAllListeners();
        opcao3.onClick.RemoveAllListeners();
    }

    IEnumerator ExecutarDialogoOpcao1()
    {
        if (opcaoEscolhida)
        {
            SetFocusDistance(2.1f);
            dublagem.Speak(9);
            yield return new WaitForSeconds(2f);

            dublagem.Speak(6);
            yield return new WaitForSeconds(12f);

            dublagem.Speak(12);

            yield return new WaitForSeconds(24.5f);
            Destroy(video.gameObject);
            textocentral.ShowText("Objetivo:\n Vá até a sede da P.E.E.B!");
            objetivosistem.AddObjective(1, "Vá até a sede da P.E.E.B!");
            objetivosistem.CompleteObjective(3);

        }
    }

    IEnumerator ExecutarDialogoOpcao2()
    {
        if (opcaoEscolhida)
        {
            SetFocusDistance(2.1f);
            dublagem.Speak(10);
            yield return new WaitForSeconds(2f);

            dublagem.Speak(7);
            yield return new WaitForSeconds(12f);

            dublagem.Speak(12);

            yield return new WaitForSeconds(24.5f);
            Destroy(video.gameObject);
            textocentral.ShowText("Objetivo:\n Vá até a sede da P.E.E.B!");
            objetivosistem.AddObjective(1, "Vá até a sede da P.E.E.B!");
            objetivosistem.CompleteObjective(3);
        }
    }

    IEnumerator ExecutarDialogoOpcao3()
    {
        if (opcaoEscolhida)
        {
            SetFocusDistance(2.1f);
            dublagem.Speak(11);
            yield return new WaitForSeconds(2f);

            dublagem.Speak(8);
            yield return new WaitForSeconds(12f);

            dublagem.Speak(12);

            yield return new WaitForSeconds(24.5f);
            Destroy(video.gameObject);
            textocentral.ShowText("Objetivo:\n Vá até a sede da P.E.E.B!");
            objetivosistem.AddObjective(1, "Vá até a sede da P.E.E.B!");
            objetivosistem.CompleteObjective(3);
        }
    }
}
