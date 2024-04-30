using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float velocidade;

    private string[] linhas;
    private int index;
    private bool dialogoAtivo = false;
    private Coroutine typingCoroutine;

    void Update()
    {
        if (dialogoAtivo && Input.GetKeyDown(KeyCode.Return))
        {
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
    }

    IEnumerator TypeLine()
    {
        foreach (char letra in linhas[index].ToCharArray())
        {
            textComponent.text += letra;
            yield return new WaitForSeconds(velocidade);
        }
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
            textComponent.text = string.Empty;
            dialogoAtivo = false;
            gameObject.SetActive(false);
        }
    }
}
