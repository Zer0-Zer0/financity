using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    //public GameObject botaoPular;
    //public GameObject botaoAutomatico;
    public float velocidade;
    public float delayParaProximaFala = 1f; // Alterado para delay para próxima fala

    private string[] linhas;
    private int index;
    private bool dialogoAtivo = false;
    private Coroutine typingCoroutine;
    private bool automaticoAtivado = false;

    public sliderempre slider2;

    //public AnimaObjeto payanim;  

    void Update()
    {
        if (dialogoAtivo && Input.GetKeyDown(KeyCode.Return))
        {
            if (!automaticoAtivado)
                proxlinha();
        }
    }

    public void IniciarDialogo(string[] linhasDialogo)
    {
        gameObject.SetActive(true);
        linhas = linhasDialogo;
        index = 0;
        dialogoAtivo = true;
        textComponent.text = string.Empty;
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeLine());

       // botaoPular.SetActive(true);
        //botaoAutomatico.SetActive(true);
    }

    IEnumerator TypeLine()
    {
        foreach (char letra in linhas[index].ToCharArray())
        {
            textComponent.text += letra;
            yield return new WaitForSeconds(velocidade);
        }

        if (automaticoAtivado)
            yield return new WaitForSeconds(delayParaProximaFala);

        if (dialogoAtivo && automaticoAtivado)
            proxlinha();
    }

    void proxlinha()
    {
        if (index < linhas.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            LimparTexto();
            dialogoAtivo = false;
            //gameObject.SetActive(false);

            //botaoPular.SetActive(false);
            //botaoAutomatico.SetActive(false);
            /*if (slider2.feito)
            {
                slider2.feito = false;
                //payanim.animação(slider2.value, "Solicitando\nao banco...", "EMPRESTIMO\nCONCEDIDO!", true);
            }//*/

        }
    }

    public void PularDialogo()
    {
        StopCoroutine(typingCoroutine);
        LimparTexto();
        dialogoAtivo = false;
        //gameObject.SetActive(false);

        //botaoPular.SetActive(false);
        //botaoAutomatico.SetActive(false);
    }

    public void AtivarAutomatico()
    {
        automaticoAtivado = !automaticoAtivado;
    }

    void LimparTexto()
    {
        textComponent.text = string.Empty;
    }
}
