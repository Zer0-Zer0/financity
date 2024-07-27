using System.Collections;
using UnityEngine;
using TMPro;

public class AnimatedText : MonoBehaviour
{
    public float letterSpeed = 0.1f; // Velocidade de exibição das letras
    public float displayDuration = 3f; // Duração de exibição do texto completo
    public TMP_Text textComponent; // Componente de texto

    private Coroutine displayCoroutine;

    void Awake(){
        textComponent.text = "";
    }

    // Método para exibir o texto letra por letra
    private IEnumerator DisplayText(string text)
    {
        textComponent.text = ""; // Limpa o texto inicialmente

        // Exibe cada letra gradualmente
        foreach (char letter in text)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(letterSpeed);
        }

        // Aguarda a duração de exibição do texto completo
        yield return new WaitForSeconds(displayDuration);

        // Desaparece o texto
        textComponent.text = "";
    }

    // Método para iniciar a exibição do texto
    public void ShowText(string text)
    {
        // Se houver uma animação de texto em andamento, interrompa-a
        if (displayCoroutine != null)
            StopCoroutine(displayCoroutine);

        // Inicia a animação de exibição do texto
        displayCoroutine = StartCoroutine(DisplayText(text));
    }
}
