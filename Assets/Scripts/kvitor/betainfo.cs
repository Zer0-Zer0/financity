using UnityEngine;
using TMPro;

public class ShowTextOnCollision : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public string message = "Você entrou em contato com o objeto!";

    void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro não foi atribuído!");
            return;
        }

        textMeshPro.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TargetObject"))
        {
            textMeshPro.text = message;
            textMeshPro.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TargetObject"))
        {
            textMeshPro.gameObject.SetActive(false);
        }
    }
}
