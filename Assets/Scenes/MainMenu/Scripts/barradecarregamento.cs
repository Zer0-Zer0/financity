using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class barradecarregamento : MonoBehaviour
{

    public TextMeshProUGUI texto;
    public Image imagem;
    public Slider loading;

    private int frasenum;
    public void jogar()
    {
        StartCoroutine(Carregar());
        imagem.gameObject.SetActive(true);
        frase();
        Cursor.visible = false;
    }

    public void frase()
    {
        frasenum = Random.Range(0, 3);
        if (frasenum == 0)
        {
            texto.text = "'A educação financeira é a chave para a liberdade financeira.'\n– Robert Kiyosaki";
        } else if (frasenum == 1) 
        {
            texto.text = "'O preço é o que você paga. O valor é o que você recebe.'\n– Warren Buffett";
        } else if (frasenum == 2)
        {
            texto.text = "'O dinheiro, assim como as emoções, é algo que você precisa controlar para manter sua vida no caminho certo.'\n– Natasha Munson";
        } else if (frasenum == 3)
        {
            texto.text = "'A maneira mais rápida de dobrar seu dinheiro é dobrá-lo e colocá-lo de volta no bolso.'\n– Will Rogers";
        }
        

    } 

    IEnumerator Carregar()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("SpaceSafezone");
        while (!asyncOperation.isDone)
        {
            loading.value = asyncOperation.progress;
            yield return null;
        }
    }
}
