using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GerenciadorDeBotoes : MonoBehaviour
{
    public GameObject painelBotoes; // Referência ao painel que contém os botões
    public Button botao1; // Referência ao primeiro botão
    public Button botao2; // Referência ao segundo botão
    public Button botao3; // Referência ao terceiro botão
    public TextMeshProUGUI textoPainel; // Referência ao texto no painel

    private string textoBotao1; // Texto do primeiro botão
    private string textoBotao2; // Texto do segundo botão
    private string textoBotao3; // Texto do terceiro botão

    private bool elementosAtivos = false; // Variável para verificar se os elementos estão visívei

    void Start()
    {
        // Adicione os listeners de clique para cada botão
        botao1.onClick.AddListener( () => BotaoClicado(textoBotao1) );
        botao2.onClick.AddListener( () => BotaoClicado(textoBotao2) );
        botao3.onClick.AddListener( () => BotaoClicado(textoBotao3) );
    }

    // Método para configurar os botões e o texto do painel
    public void ConfigurarElementos(string texto1, string texto2, string texto3, string textoPainel)
    {
        this.textoPainel.text = textoPainel;

        textoBotao1 = texto1;
        textoBotao2 = texto2;
        textoBotao3 = texto3;

        botao1.GetComponentInChildren<TextMeshProUGUI>().text = texto1;
        botao2.GetComponentInChildren<TextMeshProUGUI>().text = texto2;
        botao3.GetComponentInChildren<TextMeshProUGUI>().text = texto3;
    }

    // Método chamado quando um botão é clicado
    void BotaoClicado(string texto)
    {
        Debug.Log("Botão " + texto + " foi clicado.");
        // Faça o que quiser com o texto do botão clicado

        // Esconda os elementos
        EsconderElementos();
    }

    // Método para mostrar os botões e o texto do painel
    public void MostrarElementos()
    {
        painelBotoes.SetActive(true);
        textoPainel.gameObject.SetActive(true);
        elementosAtivos = true;
    }

    // Método para esconder os botões e o texto do painel
    public void EsconderElementos()
    {
        painelBotoes.SetActive(false);
        textoPainel.gameObject.SetActive(false);
        elementosAtivos = false;
    }

    // Método para verificar se os elementos estão visíveis
    public bool ElementosEstaoAtivos()
    {
        return elementosAtivos;
    }
}
