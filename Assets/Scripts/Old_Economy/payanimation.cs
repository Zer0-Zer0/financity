using System.Collections;
using TMPro;
using UnityEngine;

public class AnimaObjeto : MonoBehaviour
{
    public Transform objetoParaAnimar;
    public Carteira carteira;
    public AnimationCurve curvaDeAnimacao;
    public TextMeshProUGUI texto1;
    public TextMeshProUGUI texto2;
    public float duracaoDaAnimacao = 2f;
    public float alturaFinal = -550f;
    public float esperaAntesDeDescer = 4f;
    private float dinheiro;

    private Vector3 posicaoInicial;

    public void animação(float custo, string textoinicial, string textofinal, bool emprestimo)
    {
        StartCoroutine(AnimarTexto(custo, textoinicial, textofinal, emprestimo));
    }

    IEnumerator AnimarTexto(float custo, string textoInicial, string textoFinal, bool emprestimo)
    {
        dinheiro = carteira.Saldo;
        texto2.fontSize = 5.5f;
        texto2.text = "";
        texto1.text = textoInicial;
        texto1.color = new Color(0.1045994f, 0.6679245f, 0.3165958f);

        posicaoInicial = objetoParaAnimar.position;
        float tempoDecorrido = 0f;
        while (tempoDecorrido < duracaoDaAnimacao)
        {
            float proporcao = tempoDecorrido / duracaoDaAnimacao;
            float curvaValor = curvaDeAnimacao.Evaluate(proporcao);
            float novaAltura = Mathf.Lerp(posicaoInicial.y, alturaFinal, curvaValor);

            Vector3 novaPosicao = new Vector3(posicaoInicial.x, novaAltura, posicaoInicial.z);
            objetoParaAnimar.position = novaPosicao;

            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        objetoParaAnimar.position = new Vector3(posicaoInicial.x, alturaFinal, posicaoInicial.z);

        yield return new WaitForSeconds(esperaAntesDeDescer);

        if (dinheiro >= custo && !emprestimo)
        {
            texto2.color = new Color(0.7433963f, 0.1483106f, 0.1444713f);
            texto2.text = "-R$ " + custo;
            texto1.text = textoFinal;
            carteira.Subtrair(custo);
        } else if (dinheiro < custo && !emprestimo) 
        {
            texto2.color = new Color(0.7433963f, 0.1483106f, 0.1444713f);
            texto2.text = "   X";
            texto2.fontSize = 13.3f;
            texto1.text = "SALDO\nINSUFICIENTE!";
            texto1.color = new Color(0.7433963f, 0.1483106f, 0.1444713f);
        } else if (emprestimo)
        {
            texto2.color = new Color(0.1045994f, 0.6679245f, 0.3165958f);
            texto2.text = "+R$ " + custo;
            texto1.text = "EMPRESTIMO\nCONCEDIDO!";
            carteira.Adicionar(custo);
        }

        yield return new WaitForSeconds(0.5f);

        float tempoDecorridoDescendo = 0f;
        while (tempoDecorridoDescendo < duracaoDaAnimacao)
        {
            float proporcao = tempoDecorridoDescendo / duracaoDaAnimacao;
            float curvaValor = curvaDeAnimacao.Evaluate(proporcao);
            float novaAltura = Mathf.Lerp(alturaFinal, posicaoInicial.y, curvaValor);

            Vector3 novaPosicao = new Vector3(posicaoInicial.x, novaAltura, posicaoInicial.z);
            objetoParaAnimar.position = novaPosicao;

            tempoDecorridoDescendo += Time.deltaTime;
            yield return null;
        }

        objetoParaAnimar.position = posicaoInicial;
    }
}
