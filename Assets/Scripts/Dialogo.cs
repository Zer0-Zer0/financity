using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dialogo : MonoBehaviour
{
    /*
        Para funcionar:
        Adicione isso a caixa de texto na qual o dialogo sera exibido
        Olhe a void Awake, la tem todos os objetos que voce tera que adicionar como filho
        SE HOUVER MAIS DE UM SCRIPT DESSE EM UMA CENA, UM ERRO SERA OUTPUTEADO
    */
    [SerializeField]
    private KeyCode _inputProximaFrase = KeyCode.Return;
    public static Dialogo Instance { get; private set; }

    private int index;

    [Serializable]
    public struct Frases
    {
        [TextArea]
        public string Texto;
        public UnityEvent FraseAcabou;
    }

    private Frases[] frases;
    private Coroutine typingCoroutine;

    private TypewriterEffect _textoDialogo;
    private TMP_Text _textoNome;
    private GameObject _painelDialogo;

    public UnityEvent DialogoAcabou;

    /// <summary>
    /// Encontra e retorna o Transform do objeto filho com o nome especificado.
    /// </summary>
    /// <param name="nome">O nome do objeto filho a ser encontrado.</param>
    /// <returns>O Transform do objeto filho encontrado.</returns>
    /// <exception cref="System.Exception">É lançada se o objeto filho com o nome especificado não for encontrado.</exception>
    private Transform AchaCrianca(string nome, Transform pai = null)
    {
        if (pai == null)
        {
            pai = transform;
        }

        Transform child = pai.Find(nome);

        if (child == null)
        {
            throw new Exception($"Child object with the name {nome} not found.");
        }
        return child;
    }

    /// <summary>
    /// Encontra e retorna um array de botões que são descendentes do Transform pai com o nome especificado.
    /// </summary>
    /// <param name="nomePai">O nome do objeto pai a ser encontrado.</param>
    /// <returns>Um array de botões que são descendentes do objeto pai encontrado.</returns>
    public Button[] AchaBotoes(string nomePai)
    {
        Transform pai = transform.Find(nomePai);

        Button[] children = pai.GetComponentsInChildren<Button>();

        return children;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            //throw new Exception("ERROR: Atempted to add a second instance to the Dialogo singleton");
            Debug.LogWarning("Mais de um objeto dialogo na cena");
            Destroy(gameObject); //Temporariamente adicionado
        }
        else
        {
            Instance = this;
        }

        _painelDialogo = AchaCrianca("PainelDialogo").gameObject;
        _textoNome = AchaCrianca("TextoNome", _painelDialogo.transform).GetComponent<TMP_Text>();
        _textoDialogo = AchaCrianca("TextoDialogo", _painelDialogo.transform)
            .GetComponent<TypewriterEffect>();
        _painelDialogo.SetActive(false);
    }

    public void InicializarDialogo(Frases[] frasesDialogo, string nomeFalante = "")
    {
        frases = frasesDialogo;
        index = 0;
        _textoNome.text = nomeFalante;
        _painelDialogo.SetActive(true);
        typingCoroutine = StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine()
    {
        yield return _textoDialogo.ShowText(frases[index].Texto);
        yield return Waiters.InputWaiter(_inputProximaFrase);
        frases[index].FraseAcabou?.Invoke();
        ProximaFrase();
    }

    void ProximaFrase()
    {
        if (index < frases.Length - 1)
        {
            index++;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            _textoDialogo.ClearText();
            _painelDialogo.SetActive(false);
            InteracaoNPC.InteracaoOcorrendo = false;
            DialogoAcabou?.Invoke();
        }
    }
}
